using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PhysicsObject))]
public abstract class Agent : MonoBehaviour
{
    [SerializeField]
    public PhysicsObject physicsObject;

    Vector3 cameraPosition;
    float cameraHalfHeight;
    float cameraHalfWidth;
    Vector2 cameraSize;

    [SerializeField]
    float maxSpeed = 5f, maxForce = 2f;

    protected Vector3 totalForces = Vector3.zero;

    [SerializeField]
    GameManagerScriptableObject gameData;

    // Start is called before the first frame update
    void Start()
    {
        if (physicsObject == null)
        {
            physicsObject = GetComponent<PhysicsObject>();
        }

        cameraPosition = Camera.main.transform.position;
        cameraPosition.z = transform.position.z;

        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;

        cameraSize = new Vector2(cameraHalfWidth * 2f, cameraHalfHeight * 2f);
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = transform.position.z;*/

        CalculateSteeringForces();

        //  Limit total force
        totalForces = Vector3.ClampMagnitude(totalForces, maxForce);

        physicsObject.ApplyForce(totalForces);

        totalForces = Vector3.zero;
    }

    /// <summary>
    /// Has to set TotalForces
    /// </summary>
    public abstract void CalculateSteeringForces();

    public Vector3 Seek(Agent target)
    {
        return Seek(target.transform.position);
    }
    public Vector3 Seek(Vector3 targetPos)
    {
        // Calculate desired velocity
        Vector3 desiredVelocity = targetPos - transform.position;

        // Set desired = max speed
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // Calculate seek steering force
        Vector3 seekingForce = desiredVelocity - physicsObject.Velocity;

        // Return seek steering force
        return seekingForce;
    }

    public Vector3 Flee(Agent target)
    {
        return Flee(target.transform.position);
    }
    public Vector3 Flee(Vector3 targetPos)
    {
        // Calculate desired velocity
        Vector3 desiredVelocity = transform.position - targetPos;

        // Set desired = max speed
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // Calculate flee steering force
        Vector3 fleeingForce = desiredVelocity - physicsObject.Velocity;

        // Return flee steering force
        return fleeingForce;
    }

    public Vector3 Pursue(Agent target)
    {
        //  scale time by velocity
        return Seek(target.CalculateFuturePosition(4f));
    }

    public Vector3 Wander(float angle)
    {
        Vector3 forward = physicsObject.Direction;

        forward = Quaternion.Euler(0, 0, angle) * forward;

        return Seek(transform.position + forward);
    }

    public Vector3 StayInBounds(float time)
    {
        Vector3 futurePos = CalculateFuturePosition(time);

        //  Check if past any edge
        if (futurePos.x > gameData.ScreenBounds.max.x ||
            futurePos.x < gameData.ScreenBounds.min.x ||
            futurePos.y > gameData.ScreenBounds.max.y ||
            futurePos.y < gameData.ScreenBounds.min.y)
        {
            return Seek(cameraPosition);
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 Separate()
    {
        float closetDist = Mathf.Infinity;
        Agent closetAgent = null;

        //  Loop through all agents
        foreach(Agent agent in gameData.AgentManager.Agents)
        {
            float dist = Vector3.Distance(transform.position, agent.transform.position);

            if(dist <= Mathf.Epsilon)
            {
                //  This agent is me!
                continue;
            }

            if(dist < closetDist &&
                dist <= physicsObject.radius)
            {
                closetAgent = agent;

                closetDist = dist;
            }
        }

        if(closetAgent != null)
        {
            return Flee(closetAgent.transform.position);
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 CalculateFuturePosition(float futureTime)
    {
        return physicsObject.Velocity * futureTime + transform.position;
    }
}
