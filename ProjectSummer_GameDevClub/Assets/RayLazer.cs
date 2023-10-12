using System;
using System.Collections;
using System.Collections.Generic;
using Units.Enemy;
using UnityEngine;

public class RayLazer : MonoBehaviour
{
    bool fire = false;
    [SerializeField] float lazer_speed = 10;
    [SerializeField] int causeDame = 20;
    SpriteRenderer sprite;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        if (fire)
        {
            Vector3 scale = gameObject.transform.localScale;
            scale.x += Time.deltaTime * lazer_speed;
            gameObject.transform.localScale = scale;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        List<int> layers = new List<int> { 6, 9 };
        if (layers.Contains(collision.gameObject.layer))  
             Destroy(gameObject);
        else if (collision.gameObject.layer == 15) //enemy
        {
            collision.gameObject.GetComponent<EnemyUnitBase>().TakeDamage(-causeDame);
            Destroy(gameObject);
        }
    }
    public void Fire()
    {
        fire = true;
    }
}
