using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Slider slider1;
    [SerializeField] Slider slider2;
    [SerializeField] Transform circle;
    GameData gameData;
    private void Awake()
    {
        slider1.maxValue = 100;
        slider2.maxValue = 100;
    }
    void Save() 
    {
        gameData.slider1value = slider1.value;
        gameData.slider2value = slider2.value;
        gameData.pos =  Devectorize(circle.transform.position); 
        LoadDataDemo.SaveData(gameData);
    }

    void Load()
    {
        gameData = LoadDataDemo.LoadData();
        slider1.value = gameData.slider1value;
        slider2.value = gameData.slider2value;
        circle.transform.position = Vectorize(gameData.pos);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) Save();
        else if (Input.GetKeyDown(KeyCode.L)) Load();
    }
    Vector3 Vectorize(float[] arr)
    {
        Vector3 vt;
        vt.x = arr[0];
        vt.y = arr[1];
        vt.z = arr[2];
        return vt;
    }
    float[] Devectorize(Vector3 vt)
    {
        float[] arr = new float[3];
        arr[0] = vt.x;
        arr[1] = vt.y;
        arr[2] = vt.z;
        return arr;
    }
    [Serializable] public struct GameData
    {
        public float slider1value;
        public float slider2value;
        public float[] pos;
    }
}
