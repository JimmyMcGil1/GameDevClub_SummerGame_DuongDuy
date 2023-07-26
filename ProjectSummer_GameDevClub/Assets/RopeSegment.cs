using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    public GameObject conectedAbove, conectedBelow;
    public bool isPlayerAttached = false;
    private void Start()
    {
        conectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeSegment aboveSegment = conectedAbove.GetComponent<RopeSegment>();
        if (aboveSegment != null)
        {
            aboveSegment.conectedBelow = gameObject;
            float spriteBottom = conectedAbove.GetComponent<SpriteRenderer>().bounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom*-1);
        }
        //If the above segment of this node is hook
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = Vector2.zero; 
        }
    }
}
