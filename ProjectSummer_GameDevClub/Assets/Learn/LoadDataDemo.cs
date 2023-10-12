using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
static public class LoadDataDemo
{
    static BinaryFormatter formatter = new BinaryFormatter();


    static public void SaveData(UIController.GameData data)
    {
        string fileName = "sliderData";
        string folderName = "SaveFolder";
        if (!Directory.Exists(folderName))
        {
            Directory.CreateDirectory(folderName);
        }
        FileStream dataFile = File.Create(folderName + "/" + fileName + ".bin");
        formatter.Serialize(dataFile, data);
        dataFile.Close();
    }
    static public UIController.GameData LoadData()
    {
        UIController.GameData gameData;
        string fileName = "sliderData";
        string folderName = "SaveFolder";
        
        FileStream dataFile = File.Open(folderName + "/" + fileName + ".bin", FileMode.Open);
        gameData = (UIController.GameData)formatter.Deserialize(dataFile);
        dataFile.Close();
        return gameData;
    }

}