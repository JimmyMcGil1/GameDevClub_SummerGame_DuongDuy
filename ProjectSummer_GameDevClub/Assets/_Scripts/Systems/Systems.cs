
using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Systems : PersistentSingleton<Systems>
{

    [SerializeField] GameObject bourbonAllPref;
    public static Systems instance  {get; private set;}
    public static event Action onEndGame;
    public static bool isEndGame = true;
    protected override void Awake()
    {
        base.Awake();
        if (instance != this && instance != null)
        {
            Destroy(this);
        }
        else instance = this;
    }
    public void ReloadCurrentScene()
    {
        int currSceneIndex =  SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIndex);
    }
    public void LoadNextScene()
    {
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int maxScene = SceneManager.sceneCountInBuildSettings;
        if (currSceneIndex < maxScene) SceneManager.LoadScene(currSceneIndex + 1); 

    }
    public void SpawningBourbon()
    {
        Transform startPoint = GameObject.FindGameObjectWithTag("StartingPoint").transform;

        GameObject newBourbon = Instantiate(bourbonAllPref, startPoint.position, Quaternion.identity);
        GameObject bourbonGrap = newBourbon.transform.Find("character").gameObject;
        GameObject CMcam = GameObject.FindGameObjectWithTag("CM Cam");
        CMcam.GetComponent<CinemachineVirtualCamera>().Follow = newBourbon.transform.Find("character");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Debug.Log(enemies.Length.ToString());
            enemy.GetComponent<EnemyUnitBase>().RespawnBourbon(bourbonGrap);
        }
    }
    public int GetNextSceneIndex()
    {
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int maxScene = SceneManager.sceneCountInBuildSettings;
        if (currSceneIndex < maxScene) return currSceneIndex + 1;
        else return -1;
    }
    public void EndGame()
    {
        StartCoroutine(StartEndGame());
    }
    IEnumerator StartEndGame()
    {
        MainCanvas._instance.DisplayTxt("Congratulation, the twos have found the treasure!", 2);
        GameObject bourbon = GameObject.FindGameObjectWithTag("Bourbon");
        BourbonMoveset ms = bourbon.GetComponent<BourbonMoveset>();
        ms.MoveInDirection(1, 8f);
        yield return new WaitForSeconds(9f);
        MainCanvas._instance.LoadScene();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
