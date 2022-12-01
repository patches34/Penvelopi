using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TrailRenderer))]
public class Player : MonoBehaviour
{
    TrailRenderer renderer;

    Vector3 delta;

    Vector2 screenSize = Vector2.zero;
    Vector2 halfScreenSize;

    [SerializeField]
    float trailWidth = .2f;

    [SerializeField]
    float moveSensitivity = 1f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<TrailRenderer>();
        renderer.widthMultiplier = trailWidth;

        screenSize.y = Camera.main.orthographicSize * 2f;
        screenSize.x = screenSize.y * Camera.main.aspect;

        halfScreenSize = screenSize / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        delta = context.ReadValue<Vector2>();
        delta.x /= screenSize.x;
        delta.y /= screenSize.y;
        delta.z = 0;

        delta *= moveSensitivity;

        //  Check right edge
        if(transform.position.x + delta.x > halfScreenSize.x)
        {
            delta.x = transform.position.x - halfScreenSize.x;
        }
        //  Check left edge
        else if (transform.position.x + delta.x < -halfScreenSize.x)
        {
            delta.x = -halfScreenSize.x - transform.position.x;
        }

        //  Check top edge
        if (transform.position.y + delta.y > halfScreenSize.y)
        {
            delta.y = transform.position.y - halfScreenSize.y;
        }
        //  Check bottom edge
        else if (transform.position.y + delta.y < -halfScreenSize.y)
        {
            delta.y = -halfScreenSize.y - transform.position.y;
        }

        transform.Translate(delta);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(Vector3.zero, screenSize);
    }

    List<Vector3> CheckForLoop()
    {
        List<Vector3> meshVerts = new List<Vector3>();



        return meshVerts;
    }
}
