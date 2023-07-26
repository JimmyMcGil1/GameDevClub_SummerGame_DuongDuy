using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SummonSwordDetect : MonoBehaviour
{
    [SerializeField] LayerMask platformLayer;
    CircleCollider2D cirCol;
    private void Awake()
    {
        cirCol = GetComponent<CircleCollider2D>();
    }
   
    public bool isInPlatForm()
    {
        RaycastHit2D hit = Physics2D.CircleCast(cirCol.bounds.center, cirCol.radius, Vector2.down, 0.0001f, platformLayer);
        return hit.collider != null;
    }
    void DestroyObj()
    {
        Destroy(gameObject);
    }
    
}
