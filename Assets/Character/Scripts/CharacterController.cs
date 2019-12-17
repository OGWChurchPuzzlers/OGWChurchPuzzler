using UnityEngine;
using System.Collections.Generic;

/*
 * This controller manages character interaction with the environment and character controls. This includes:
 * - Moving (Walk, Jump, Turn)
 * - Item pickup
 */
public class CharacterController : MonoBehaviour
{

    private enum ControlMode
    {
        Tank,
        Direct
    }

    private const float COLLECT_TOLERANCE = 0.01f;
    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;
    [SerializeField] public GameObject m_item;
    [SerializeField] public GameObject m_itemAnchor;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;

    private bool m_isGrounded;
    private List<Collider> m_collisions = new List<Collider>();

    private bool isCarryingItem = false;

    private GameObject collectableItem;

    private GameObject collectedItem;

    private Quaternion collectedItemSavedRotation;

    private Vector3 anchorAdaption;

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }

    void Update()
    {
        m_animator.SetBool("Grounded", m_isGrounded);

        switch (m_controlMode)
        {
            case ControlMode.Direct:
                DirectUpdate();
                break;

            case ControlMode.Tank:
                TankUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }

        m_wasGrounded = m_isGrounded;
    }

    private void TankUpdate()
    {
        CollectOrDrop();

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0)
        {
            if (walk) { v *= m_backwardsWalkScale; }
            else { v *= m_backwardRunScale; }
        }
        else if (walk)
        {
            v *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        m_animator.SetFloat("MoveSpeed", m_currentV);

        JumpingAndLanding();
    }

    private void DirectUpdate()
    {
        CollectOrDrop();

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        JumpingAndLanding();
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }

    private void CollectOrDrop()
    {
        if (isCarryingItem == false && collectableItem != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collect();
                isCarryingItem = true;
            }
        }
        else if (isCarryingItem == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Drop();
                isCarryingItem = false;
            }
        }
    }

    void Collect()
    {
        AdaptAnchorPointToObjectBounds();
        AttachObject();
    }


    void Drop()
    {
        DetachObject();
        ResetAnchorPoint();
    }

    private void AttachObject()
    {
        if (collectableItem != null)
        {
            collectedItem = collectableItem;
            collectableItem = null;
            collectedItem.GetComponent<Rigidbody>().useGravity = false;
            collectedItem.GetComponent<Rigidbody>().isKinematic = true;
            collectedItem.transform.position = m_itemAnchor.transform.position;

            this.collectedItemSavedRotation = this.collectedItem.transform.rotation;

            //collectedItem.transform.rotation = m_itemAnchor.transform.rotation;
            collectedItem.transform.SetParent(m_itemAnchor.transform.transform);

        }
    }

    private void DetachObject()
    {
        if (collectedItem != null)
        {
            collectedItem.GetComponent<Rigidbody>().useGravity = true;
            collectedItem.GetComponent<Rigidbody>().isKinematic = false;
            collectedItem.transform.SetParent(null);
            collectedItem.transform.position = m_itemAnchor.transform.position;
            //collectedItem.transform.rotation = this.collectedItemSavedRotation;
        }
    }

    private void ResetAnchorPoint()
    {
        m_itemAnchor.transform.Translate(-this.anchorAdaption);
        collectedItem = null;
    }

    private void AdaptAnchorPointToObjectBounds()
    {
        Collider itemCollider = collectableItem.GetComponent<Collider>();
        float offsetZ = itemCollider.bounds.size.z / 2.0f + COLLECT_TOLERANCE;
        float offsetY = itemCollider.bounds.size.y / 2.0f + 0.01f;
        this.anchorAdaption = new Vector3(0, offsetY, offsetZ);

        m_itemAnchor.transform.Translate(anchorAdaption);
    }

    public bool IsItemInHands(GameObject gameObject)
    {
        return this.collectedItem != null ? this.collectedItem.Equals(gameObject) : false;
    }

    public bool IsCarryingItem()
    {
        return this.isCarryingItem;
    }

    public GameObject GetCollectedItem()
    {
        return this.collectedItem;
    }

    public void SetCollectableItem(GameObject item)
    {
        if (!isCarryingItem)
        {
            this.collectableItem = item;
        }
        else
        {
            Debug.Log("Collecting item denied since character is already carrying something.");
        }
    }
}
