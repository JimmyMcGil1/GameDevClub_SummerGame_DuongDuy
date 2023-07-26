using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float dampingFloat;
    [SerializeField] float speedFloat;
    [SerializeField] GameObject player;
    float y0;
    bool isFloat;
    private void Awake()
    {
        isFloat = false;
        y0 = transform.position.y;
    }
    private void OnMouseDown()
    {
        isFloat = true;
    }
    private void OnMouseExit()
    {
        isFloat = false;
    }
    private void Update()
    {
        if (isFloat) Floating();
    }
    void Floating()
    {
        Vector2 pos = transform.position;
        pos.y = y0 + Mathf.Sin(speedFloat * Time.deltaTime ) * dampingFloat;
        transform.position = pos;
    }
    void MovingTo(Vector3 target)
    {
        
    }
}
