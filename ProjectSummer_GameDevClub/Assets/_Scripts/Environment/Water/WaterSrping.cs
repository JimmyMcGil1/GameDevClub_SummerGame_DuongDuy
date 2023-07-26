using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class WaterSrping : MonoBehaviour
{

    
    public float velocity;
    float force;
    //current height
    public float height = 0f;
    //nor height
    float target_height = 0f;
    [SerializeField] float dampening;
    private int waveIndex = 0;
    [SerializeField] private float resistance = 1f;
    private static SpriteShapeController spriteShapeController = null;

    public void WaveSpringUpdate(float springStiffness, float dampening)
    {
        height = transform.localPosition.y;
        var x = height - target_height;
        var loss = -dampening * velocity;
        force = -springStiffness * x + loss;
        velocity += force;
        var y = transform.localPosition.y;
        transform.localPosition = new Vector3(transform.localPosition.x, y + velocity, transform.localPosition.z);
    }
    public void Init(SpriteShapeController ssc)
    {

        var index = transform.GetSiblingIndex();
        waveIndex = index + 1;

        spriteShapeController = ssc;
        velocity = 0;
        height = transform.localPosition.y;
        target_height = transform.localPosition.y;
    }

    private void Update()
    {
        if (Mathf.Abs(velocity) > 0.004) dampening += 0.08f;
        else dampening = 0.03f;
    }
    public void WavePointUpdate()
    {
        if (spriteShapeController != null)
        {
            Spline waterPline = spriteShapeController.spline;
            Vector3 wavePointPosition = waterPline.GetPosition(waveIndex);
            waterPline.SetPosition(waveIndex, new Vector3(wavePointPosition.x, transform.localPosition.y, wavePointPosition.z));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            Rigidbody2D rgBourbon = collision.gameObject.GetComponent<Rigidbody2D>();
            var speed = rgBourbon.velocity;
            velocity += speed.y / resistance;
        }
      

       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            Rigidbody2D rgBourbon = collision.gameObject.GetComponent<Rigidbody2D>();
            var speed = rgBourbon.velocity;
            velocity = Mathf.Abs(velocity + speed.y / resistance) <= 0.001 ? velocity + speed.y / resistance : velocity;
        }   
    }
}
