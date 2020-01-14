using UnityEngine;
using System.Collections.Generic;

/*
 * This controller manages character interaction with the environment and character controls. This includes:
 * - Moving (Walk, Jump, Turn)
 * - Item pickup
 */
public class CharacterController : MonoBehaviour
{
    [SerializeField] DecoupledInputManager inputManager;
    private enum ControlMode
    {
        Tank,
        Direct
    }

    private const float ITEM_ELEVATION = 1.0f;
    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;
    [SerializeField] public GameObject m_item;
    [SerializeField] public GameObject m_itemAnchor;
    [SerializeField] public GameObject m_collectTrigger;

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

    private Item collectableItem;

    private Item collectedItem;

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
        CollectOrDropNearestItem(inputManager.IsInteractionKeyPressed());

        float v = inputManager.GetAxis("Vertical");
        float h = inputManager.GetAxis("Horizontal");

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
        CollectOrDropNearestItem(inputManager.IsInteractionKeyPressed());

        float v = inputManager.GetAxis("Vertical");
        float h = inputManager.GetAxis("Horizontal");

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

        if (jumpCooldownOver && m_isGrounded && inputManager.IsJumpKeyPressed())
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


    public void CollectOrDropNearestItem(bool eventTriggered)
    {
        if (!eventTriggered)
        {
            return;
        }
        if (isCarryingItem == false && collectableItem != null)
        {
            AttachObject();
            isCarryingItem = true;
        }
        else if (isCarryingItem == true)
        {
            DetachObject();
            isCarryingItem = false;
        }
    }

    public void CollectOrDropFromRaycast(bool eventTriggered, RaycastHit hit) 
    {
        if (!eventTriggered)
            return;

        Collider c = hit.collider;
        Item toCollect = c.gameObject.GetComponent<Item>();
        bool isItem = toCollect != null;
        bool isDropTrigger = c.CompareTag("DropTrigger");
        
        // TODO FIXME is item and near enough?
        if (isItem)
        {
            if (isCarryingItem == false)
            {
                // attach
                collectableItem = toCollect;
                AttachObject();
                isCarryingItem = true;
            }
            else if (isCarryingItem)
            {
                // drop
                DetachObject();
                isCarryingItem = false;
            }

        }
        else if (isDropTrigger)
        {
            if (isCarryingItem)
            {
                // drop
                DetachObject();
                isCarryingItem = false;
            }
        }
    }


    public void AttachObject()
    {
        if (collectableItem != null)
        {
            collectedItem = collectableItem;
            Rigidbody rigid = collectedItem.GetComponent<Rigidbody>();
            Collider col = collectedItem.GetComponent<Collider>();
            if (rigid != null && col != null)
            {
                rigid.useGravity = false;
                rigid.isKinematic = true;
                col.enabled = false;
                collectedItem.transform.position = m_itemAnchor.transform.position;
                collectedItem.transform.rotation = m_itemAnchor.transform.rotation;
                collectedItem.transform.SetParent(m_itemAnchor.transform);
            }
            m_collectTrigger?.SetActive(true);
        }
    }

    private void DetachObject()
    {
        if (collectedItem != null)
        {
            Rigidbody rigid = collectedItem.GetComponent<Rigidbody>();
            Collider col = collectedItem.GetComponent<Collider>();
            if (rigid != null && col != null)
            {
                rigid.useGravity = true;
                rigid.isKinematic = false;
                col.enabled = true;
                collectedItem.transform.SetParent(null);
                collectedItem.transform.position = m_itemAnchor.transform.position;
                collectedItem.transform.rotation = m_itemAnchor.transform.rotation;
                collectableItem = null;
                collectedItem = null;
            }
            m_collectTrigger?.SetActive(false);
        }
    }

    public bool IsItemInHands(GameObject gameObject)
    {
        return this.collectedItem != null ? this.collectedItem.Equals(gameObject) : false;
    }

    public bool IsCarryingItem()
    {
        return this.isCarryingItem;
    }

    public Item GetCollectedItem()
    {
        return this.collectedItem;
    }

    public void SetCollectableItem(Item item)
    {
        if (!isCarryingItem)
        {
            this.collectableItem = item;
        }
        else
        {
            //Debug.Log("Collecting item denied since character is already carrying something.");
        }
    }
}
