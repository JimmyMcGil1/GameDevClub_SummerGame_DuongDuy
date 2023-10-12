using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Units.Enemy;
public class SummonUnderlingScript : MonoBehaviour
{
    [SerializeField] GameObject[] underlingsPref;
    public int numberUnderlingInScene = 0;
    [SerializeField] int maxUnderling = 4;
    Transform summonPos;
    [SerializeField] float summonTimmer = 2; 
    float summonCounter;

    private void Awake()
    {
        summonPos = transform.Find("SummonPos").transform;
        summonCounter = Mathf.Infinity;
    }
    private void Update()
    {
        if (summonCounter > summonTimmer)
        {
            if (numberUnderlingInScene < maxUnderling)
            {
                SummonUnderling();
                numberUnderlingInScene++;
                summonCounter = 0;
            }
        }
        summonCounter += Time.deltaTime;
    }
    void SummonUnderling()
    {
        int index = Random.Range(0, underlingsPref.Length);
        GameObject underlink = Instantiate(underlingsPref[index], summonPos.position, Quaternion.identity);
        underlink.GetComponent<WizardUnderlingScript>().SetMaster(gameObject);
        StartCoroutine(underlink.GetComponent<WizardUnderlingScript>().AppearTime());
    }
    
}
