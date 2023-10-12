using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Units.Bourbon;
public class Bullet : MonoBehaviour
{
    [SerializeField] int damage = 10;
    Vector2 dirMove = Vector2.right;
    [SerializeField] float speed = 4;
    bool isShooting = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            collision.gameObject.GetComponent<BourbonController>().TakeDamage(-damage);
        }
        GetComponent<Animator>().SetTrigger("hit");
    }
    private void Update() {
        if (isShooting) {
            MoveInDirection();
        }
    }
    public void Explode()
    {
        Destroy(gameObject);
    }
    public void ShootByVector(Vector2 dir) {
        this.dirMove = dir;
        isShooting = true;
    }
    void MoveInDirection() {
        Vector2 currentPos = transform.position;
        currentPos.x += dirMove.x * speed * Time.deltaTime;
        currentPos.y += dirMove.y * speed * Time.deltaTime;
        transform.position = currentPos;
    }
}
