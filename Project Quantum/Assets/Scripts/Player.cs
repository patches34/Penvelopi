using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TrailRenderer))]
public class Player : MonoBehaviour
{
    TrailRenderer renderer;

    Vector3 delta;

    [SerializeField]
    float trailWidth = .2f;

    [SerializeField]
    float moveSensitivity = 1f;

    float sqrTrailLength = 0;
    Vector3[] trailPositions;

    [SerializeField]
    GameManagerScriptableObject gameManager;

    public float GetTrailLength()
    {
        return Mathf.Sqrt(sqrTrailLength);
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<TrailRenderer>();
        renderer.widthMultiplier = trailWidth;
    }

    // Update is called once per frame
    void Update()
    {
        trailPositions = new Vector3[renderer.positionCount];
        renderer.GetPositions(trailPositions);

        CalcTrailLength();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        delta = context.ReadValue<Vector2>();
        delta.x /= gameManager.ScreenBounds.size.x;
        delta.y /= gameManager.ScreenBounds.size.y;
        delta.z = 0;

        delta *= moveSensitivity;

        //  Check right edge
        if(transform.position.x + delta.x > gameManager.ScreenBounds.max.x)
        {
            delta.x = transform.position.x - gameManager.ScreenBounds.max.x;
        }
        //  Check left edge
        else if (transform.position.x + delta.x < gameManager.ScreenBounds.min.x)
        {
            delta.x = gameManager.ScreenBounds.min.x - transform.position.x;
        }

        //  Check top edge
        if (transform.position.y + delta.y > gameManager.ScreenBounds.max.y)
        {
            delta.y = transform.position.y - gameManager.ScreenBounds.max.y;
        }
        //  Check bottom edge
        else if (transform.position.y + delta.y < gameManager.ScreenBounds.min.y)
        {
            delta.y = gameManager.ScreenBounds.min.y - transform.position.y;
        }

        transform.Translate(delta);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(gameManager.ScreenBounds.center, gameManager.ScreenBounds.size);
    }

    List<Vector3> CheckForLoop()
    {
        List<Vector3> meshVerts = new List<Vector3>();



        return meshVerts;
    }

    /// <summary>
    /// Calculate the squared length of the TrailRender
    /// </summary>
    void CalcTrailLength()
    {
        sqrTrailLength = 0;
        for (int i = 1; i < trailPositions.Length; i++)
        {
            sqrTrailLength += Vector3.SqrMagnitude(trailPositions[i - 1] - trailPositions[i]);
        }

        gameManager.trailLength = Mathf.Sqrt(sqrTrailLength);
    }
}
