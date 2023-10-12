using System.Collections;
using System.Collections.Generic;
using Testing;
using Units.Bourbon;
using Units.Enemy;
using UnityEngine;
using UnityEngine.UI;


namespace Test
{
    public class GameManagerTesting : MonoBehaviour
    {
        Text cdText;
        [SerializeField] float maxTime = 10f;
        public float currentTime;
        GameObject bourbon;
        [SerializeField] ControlStateInScene controller;
        private void Awake()
        {
            cdText = gameObject.GetComponentInChildren<Text>();
            cdText.text = maxTime.ToString();
            bourbon = GameObject.FindGameObjectWithTag("Bourbon");
            currentTime = maxTime;
        }
        float counting = 0;
        int oldCount = 0;
        private void OnEnable()
        {
            BourbonUnitBase.bourbonDead += ResetTime;
        }
        private void Update()
        {
            counting += Time.deltaTime;
            Debug.Log("Update counter");
            if (Mathf.CeilToInt(counting) > oldCount)
            {
                oldCount = Mathf.CeilToInt(counting);
                currentTime -= 1;
                cdText.text = currentTime.ToString();
            }
            if (currentTime == 0)
            {
                controller.ChangeState(transform.parent.Find("DryadState").GetComponent<StateScene>());
            }
        }
        void ResetTime()
        {
            currentTime = maxTime;
        }
    }
}
