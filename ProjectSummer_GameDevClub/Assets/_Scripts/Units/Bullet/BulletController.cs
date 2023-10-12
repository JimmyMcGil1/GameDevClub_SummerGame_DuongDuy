using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] GameObject bulletPref;
    [SerializeField] public float speedOfBullet = 3f;
    public void FireParabol()
    {
        GameObject bullet =  Instantiate(bulletPref, transform.parent.position + Vector3.right * 0.5f, Quaternion.identity);
        GameObject bullet1 = Instantiate(bulletPref, transform.parent.position + Vector3.right * 0.5f, Quaternion.identity);
        GameObject bullet2 = Instantiate(bulletPref, transform.parent.position + Vector3.right * 0.5f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().angle = 45;
        bullet1.GetComponent<BulletScript>().angle = 60;
        bullet2.GetComponent<BulletScript>().angle = 30;
    }   

}
