using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject[] ropeNodePref;
    public int numberOfNode = 5;
    public Transform endPoint = null;
    Rigidbody2D currNode;
    private void Awake()
    {
        GenerateToExactPoint();
    }
    private void Start()
    {
    }
    void GenerateRope()
    {
        currNode = hook;
        for (int i = 0; i < numberOfNode; i++)
        {
            int index = Random.Range(0, ropeNodePref.Length);
            GameObject newNode = Instantiate(ropeNodePref[index]);
            newNode.transform.parent = transform;
            newNode.transform.position = transform.position;
            HingeJoint2D hj = newNode.GetComponent<HingeJoint2D>();
            hj.connectedBody = currNode;
            currNode = newNode.GetComponent<Rigidbody2D>();
        }

    }
    void GenerateToExactPoint()
    {
        Vector2 vecDis = endPoint.position - hook.transform.position;
        numberOfNode = Mathf.RoundToInt(vecDis.magnitude / ropeNodePref[0].GetComponent<SpriteRenderer>().bounds.size.y);
        
         
        GenerateRope();
       
    }
}
