using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BourbonUI : MonoBehaviour
{
    Text healTxt;
    Slider healSld;
    GameObject bourbon;
    int maxHp;
    [SerializeField] ScriptableHero scriptableHero;
    public static BourbonUI instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
        bourbon = GameObject.FindGameObjectWithTag("Bourbon").gameObject;
        healTxt = transform.Find("txt_Heal").gameObject.GetComponent<Text>();
        healSld = transform.Find("slider_Heal").gameObject.GetComponent<Slider>();
       
    }
    private void Start()
    {
        maxHp = scriptableHero.initStats.Health;
        healSld.maxValue = maxHp;
        HealTxtChange(BourbonUnitBase.currStats.Health);
    }
    private void OnEnable()
    {
        BourbonMoveset.changeHeal += HealTxtChange;
        Systems.onEndGame += OnEndGame;
    }
    private void OnDisable()
    {
        BourbonMoveset.changeHeal -= HealTxtChange;
        Systems.onEndGame -= OnEndGame;
    }
    public void HealTxtChange(int currHeal)
    {
        healTxt.text = $"{BourbonMoveset.currStats.Health} / {maxHp}";
        healSld.value = BourbonMoveset.currStats.Health;
    }

    void OnEndGame()
    {
        gameObject.SetActive(false);
    }
    
}
