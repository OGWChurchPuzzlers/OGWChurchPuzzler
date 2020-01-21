using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour, ScriptEffect
{
    [SerializeField] private GameObject platform;
    [SerializeField] private float distance = 4f;
    [SerializeField] private float speed = .1f;
    private Vector3 downPosition;
    private Vector3 topPosition;

    public bool inAnimate = false;
    public float animTime = 0f;
    public bool reachedDestination {
        get
        {
            return !inAnimate;
        }
    }
    public bool moveUp = false;
    public float DBGcurrDistance = 0.0f;

    public void ExecuteScriptedEffect(string callerButtonId)
    {
        if (!string.IsNullOrWhiteSpace(callerButtonId))
        {
            switch (callerButtonId)
            {
                case "down":
                    if(moveUp == false && reachedDestination)
                    {
                        // lift ist unten und wartet
                        callUp();
                    }
                    else
                    {
                        callDown();
                    }
                    break;
                case "top":
                    if (moveUp == true && reachedDestination)
                    {
                        // lift ist unten und wartet
                        callDown();
                    }
                    else
                    {
                        callUp();
                    }
                    break;
                default:
                    break;
            }
        }
        void callDown()
        {
            moveUp = false;
            inAnimate = true;
        }
        void callUp()
        {
            moveUp = true;
            inAnimate = true;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        downPosition = platform.transform.position;
        topPosition = platform.transform.position + new Vector3(0, distance, 0);

    }
    public void ResetLift()
    {
        inAnimate = false;
        animTime = 0f;
        platform.transform.localPosition = downPosition;
    }

    private float distanceToDestination(bool top)
    {
        float dist;

        if (top)
        {
            dist = Vector3.Distance(topPosition, platform.transform.position);
          //  print("Distance to Top: " + dist);
        }
        else
        {
            dist = Vector3.Distance(downPosition, platform.transform.position);
         //   print("Distance to Bottom: " + dist);
        }
        DBGcurrDistance = dist;
        return dist;
    }

    // Update is called once per frame
    void Update()
    {
        if (inAnimate)
        {
            float deltaTime = Time.deltaTime;
            float toMove = deltaTime * speed;

            if (distanceToDestination(moveUp) <= toMove)
            {
                // destination reached
                platform.transform.position = moveUp ? topPosition : downPosition;
                inAnimate = false;
                return;
            }
            else
            {
                if (moveUp)
                 platform.transform.position += new Vector3(0, toMove, 0);
                else
                 platform.transform.position += new Vector3(0, -toMove, 0);
            }
        }
    }
}
