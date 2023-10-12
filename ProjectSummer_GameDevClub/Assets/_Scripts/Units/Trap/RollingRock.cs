using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Bourbon;
public class RollingRock : MonoBehaviour
{
    [SerializeField] bool moveLeft = false;
    CircleCollider2D cirCol;
    [SerializeField] MapManager mapManager;
    [SerializeField] float moveSpeed = 2;
    Vector2 dirMove = Vector2.right;
    
    private void Awake() {
        cirCol = GetComponent<CircleCollider2D>();
        if (moveLeft) dirMove = Vector2.left;
        mapManager = GameObject.FindGameObjectWithTag("Map Manager").GetComponent<MapManager>();
    }
    private void Update() {
        Vector2 checkStandingPos = new Vector2(cirCol.bounds.center.x, cirCol.bounds.min.y - 0.3f);
        mapManager.DestroyTile(checkStandingPos);
        Vector2 pos = transform.position;
        pos.x += Time.deltaTime * moveSpeed * dirMove.x;
        transform.position = pos;
    }
   
    private void OnEnable()
    {
        BourbonUnitBase.bourbonDead += ResetPos;
    }
    private void OnDisable()
    {
        BourbonUnitBase.bourbonDead -= ResetPos;
    }
    public void ResetPos()
    {
        Rigidbody2D rigit = GetComponent<Rigidbody2D>();
       // rigit.AddForce(Vector2.left * -350, ForceMode2D.Impulse);
        rigit.velocity = Vector2.zero;
        rigit.rotation = 0f;
        rigit.angularVelocity = 0f;
        Transform initPos = transform.parent.Find("InitPos");
        transform.localPosition = initPos.localPosition;
        GameObject rockTrap = transform.parent.Find("RockTrap").gameObject;
        rockTrap.GetComponent<BoxCollider2D>().enabled = true;
    }
}
