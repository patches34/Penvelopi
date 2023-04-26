using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsObject : MonoBehaviour
{
    Vector3 position = Vector3.zero;
    Vector3 velocity = Vector3.zero;

    public Vector3 Velocity
    {
        get
        {
            return velocity;
        }
    }

    Vector3 direction = Vector3.up;

    public Vector3 Direction
    {
        get
        {
            return direction;
        }
    }

    Vector3 acceleration = Vector3.zero;

    [SerializeField]
    float mass = 1f;

    public bool useGravity, useFriction;
    Vector3 gravity = Vector3.down;
    [SerializeField]
    float coeff = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (useGravity)
        {
            ApplyGravity(gravity);
        }

        if (useFriction)
        {
            ApplyFriction(coeff);
        }

        velocity += acceleration * Time.deltaTime;

        //
        //  Update direction when there is a velocity
        //
        if (velocity != Vector3.zero)
        {
            direction = velocity.normalized;
        }

        position += velocity * Time.deltaTime;  // Just like vehicle

        transform.position = position;

        //
        //  Rotate object to look at direction
        //
        transform.rotation = Quaternion.LookRotation(Vector3.back, Direction);

        acceleration = Vector3.zero;
    }

    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    void ApplyFriction(float coeff)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * coeff;

        ApplyForce(friction);
    }

    void ApplyGravity(Vector3 force)
    {
        acceleration += force;
    }

    public void FreezeObject()
    {
        velocity = Vector3.zero;
    }
}
