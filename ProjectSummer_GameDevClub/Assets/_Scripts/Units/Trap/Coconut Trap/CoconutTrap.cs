using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Units.Trap
{

    public class CoconutTrap : TrapObject
    {
        [SerializeField] float activeTimmer = 0.5f;
        float activeCounter = Mathf.Infinity;
        [SerializeField] GameObject coconutPref;
        private void Update()
        {
            activeCounter += Time.deltaTime;
        }
        public override void Active()
        {
            if (activeCounter > activeTimmer)
            {
                Transform initPot = transform.Find("CoconutInitPos");
                GameObject coconut = Instantiate(coconutPref, initPot.position, Quaternion.identity);
                coconut.GetComponent<CoconutObject>().isFalling = true;
                activeCounter = 0;
            }
            else return;

        }
    }
}