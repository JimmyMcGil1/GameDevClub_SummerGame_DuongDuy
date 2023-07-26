using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PearlScript : MonoBehaviour
{
    bool isFire = false;
    Vector2 dirMove;
    [SerializeField] int damage = 20;
    [SerializeField] float speed = 5;
    void Start()
    {
        Invoke("Explode", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFire)
            Fire(dirMove);
    }
    public  void Fire(Vector2 dir)
    {
        if (!isFire)
        {
            isFire = true;
            dirMove = dir;
        }
        Vector2 currPos = transform.position;
        currPos.x += dirMove.x * speed * Time.deltaTime;
        currPos.y += dirMove.y * speed * Time.deltaTime;
        transform.position = currPos;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            collision.gameObject.GetComponent<BourbonMoveset>().TakeDamage(-damage);
        }
        GetComponent<Animator>().SetTrigger("hit");
    }
    public void Explode()
    {
        Destroy(gameObject);
    }
}
