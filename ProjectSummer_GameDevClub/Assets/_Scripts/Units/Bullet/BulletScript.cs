using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Units.Enemy;
public class BulletScript : MonoBehaviour
{
    [SerializeField] float initSpeed;
    Rigidbody2D rigit;
    public float angle = 60;
    private void Start()
    {
        rigit = GetComponent<Rigidbody2D>();
        Vector3 velocity = Quaternion.Euler(0, 0, angle) * Vector3.right * initSpeed;
        rigit.velocity = velocity;
        rigit.AddForce(Vector3.down * 9.81f, ForceMode2D.Force);
    }

    private void Update()
    {
    }

    public void FireParabol()
    {
        //Vector2 direction = target - (Vector2)transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Vector3 velocity = Quaternion.Euler(0, 0, 60) * Vector3.right * initSpeed;
        //rigit.velocity = velocity;
        //rigit.AddForce(Vector3.down * 9.81f, ForceMode2D.Force);
    }
    public void FireLinear(Vector2 target)
    {
        transform.DOMove(target, 0.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Animator>().SetTrigger("explode");
            collision.GetComponent<EnemyUnitBase>().TakeDamage(-10);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            GetComponent<Animator>().SetTrigger("explode");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            rigit.velocity = Vector2.zero;
            rigit.gravityScale = 0;
            GetComponent<Animator>().SetTrigger("explode");
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
