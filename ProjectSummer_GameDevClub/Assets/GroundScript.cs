using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GroundScript : MonoBehaviour
{
    TilemapCollider2D tmCol;
    CompositeCollider2D compoCol;
    private void Awake()
    {
        tmCol = GetComponent<TilemapCollider2D>();
        compoCol = GetComponent<CompositeCollider2D>();
    }
    public bool CheckPosInPlatform(Vector3 pos)
    {
        return compoCol.IsTouchingLayers();
        
    }
}
