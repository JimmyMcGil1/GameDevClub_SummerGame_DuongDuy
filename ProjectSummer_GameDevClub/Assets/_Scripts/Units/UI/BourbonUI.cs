using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Units.Bourbon;

    public class BourbonUI : MonoBehaviour
    {
        Text healTxt;
        Slider healSld;
        int maxHp;
        [SerializeField] BourbonController controller;
        [SerializeField] ScriptableHero scriptableHero;
        public static BourbonUI instance { get; private set; }
        private void Awake()
        {   
            if (instance != null && instance != this) Destroy(this.gameObject);
            else instance = this;

            healTxt = transform.Find("txt_Heal").gameObject.GetComponent<Text>();
            healSld = transform.Find("slider_Heal").gameObject.GetComponent<Slider>();

        }
        private void Start()
        {
            maxHp = scriptableHero.initStats.Health;
            healSld.maxValue = maxHp;
            HealTxtChange(controller.currStats.Health);
        }
        private void OnEnable()
        {
            BourbonController.changeHeal += HealTxtChange;
            Systems.onEndGame += OnEndGame;
        }
        private void OnDisable()
        {
            BourbonController.changeHeal -= HealTxtChange;
            Systems.onEndGame -= OnEndGame;
        }
        public void HealTxtChange(int currHeal)
        {
            healTxt.text = $"{controller.currStats.Health} / {maxHp}";
            healSld.value = controller.currStats.Health;
        }

        void OnEndGame()
        {
            gameObject.SetActive(false);
        }

    }

