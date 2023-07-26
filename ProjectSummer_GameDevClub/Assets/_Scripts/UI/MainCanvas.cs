using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{

    Text sysLogTxt;
    public static MainCanvas _instance { get; private set; }
    bool isTxtDisplaying = false;
    string currMess;
    int currSec;
    Slider progressSld;
    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this);
        else _instance = this;
        sysLogTxt = transform.Find("SystemTxt").gameObject.GetComponent<Text>();
        progressSld = transform.Find("Progress Slider").gameObject.GetComponent<Slider>();
        progressSld.gameObject.SetActive(false);
    }
   
    public void LoadScene()
    {
        int nextSceneIndex = Systems.instance.GetNextSceneIndex();
        Debug.Log(nextSceneIndex.ToString());
        if (nextSceneIndex != -1) StartCoroutine(LoadSceneCoroutine(nextSceneIndex));
        else
        {
            Systems.instance.Quit();
        }
    }
    IEnumerator LoadSceneCoroutine(int index)
    {
        progressSld.value = 0;
        progressSld.gameObject.SetActive(true);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        asyncOperation.allowSceneActivation = false;
        float progress = 0;
        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            progressSld.value = progress;
            if (progress >= 0.9f)
            {
                progressSld.value = 1;
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    public IEnumerator DisplayText(string mess,int sec)
    {
        sysLogTxt.text = mess;
        currMess = mess;
        currSec = sec;
        StartCoroutine(Helpers.TextApper(sysLogTxt));
        yield return new WaitForSeconds(sec);
        StartCoroutine(Helpers.TextDisappear(sysLogTxt));
        isTxtDisplaying = false;
    }
    public void DisplayTxt(string mess, int sec)
    {
        if (isTxtDisplaying) 
        {
            return;
        }
        else
        {
            isTxtDisplaying = true;
            StartCoroutine(DisplayText(mess, sec));
        }
    }
   
}
