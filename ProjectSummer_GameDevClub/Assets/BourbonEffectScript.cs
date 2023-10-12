using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Bourbon
{
    public class BourbonEffectScript : MonoBehaviour
    {
        public void CastDustOfDash()
        {
            gameObject.transform.Find("Dust").gameObject.GetComponent<Animator>().SetTrigger("dust");
        }
        public void CastBlood()
        {
            gameObject.transform.Find("Blood effect").gameObject.GetComponent<Animator>().SetTrigger("cast_effect");

        }
        public void CastGrounding()
        {
            gameObject.transform.Find("groundingEffect").gameObject.GetComponent<Animator>().SetTrigger("cast_effect");

        }
        public void CastHit(Transform pos = null)
        {
            GameObject hit_effect = gameObject.transform.Find("hit_effect").gameObject;
            if (pos != null)
                hit_effect.transform.position = pos.position;
            hit_effect.GetComponent<Animator>().SetTrigger("cast_effect");

        }
    }
}

