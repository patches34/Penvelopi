using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoundsType
{
    Largest,
    Smallest,
    Width,
    Height
}
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteInfo : MonoBehaviour
{
    [SerializeField]
    BoundsType boundsType;

    float radius;

    public float Radius
    {
        get { return radius; }
    }

    SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        switch(boundsType)
        {
            case BoundsType.Largest:
                if(renderer.sprite.bounds.extents.x > renderer.sprite.bounds.extents.y)
                {
                    radius = renderer.sprite.bounds.extents.x * transform.lossyScale.x;
                }
                else
                {
                    radius = renderer.sprite.bounds.extents.y * transform.lossyScale.y;
                }
                break;
            case BoundsType.Smallest:
                if (renderer.sprite.bounds.extents.x < renderer.sprite.bounds.extents.y)
                {
                    radius = renderer.sprite.bounds.extents.x * transform.lossyScale.x;
                }
                else
                {
                    radius = renderer.sprite.bounds.extents.y * transform.lossyScale.y;
                }
                break;
            case BoundsType.Width:
                radius = renderer.sprite.bounds.extents.x * transform.lossyScale.x;
                break;
            case BoundsType.Height:
                radius = renderer.sprite.bounds.extents.y * transform.lossyScale.y;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
