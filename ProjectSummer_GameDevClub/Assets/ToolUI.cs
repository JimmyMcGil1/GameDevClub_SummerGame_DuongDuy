using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolUI : MonoBehaviour
{
    Image panelImage;
    GameObject panel;
    List<Image> objImg = new List<Image>();
    bool panelIsOpen = false;
    bool processingPanel = false;


    List<Image> manImg = new List<Image>();
    List<Text> manTxt = new List<Text>();
    GameObject manPanel;
    bool processingManPanel = false;
    bool manPanelIsOpen = false;
    private void Awake()
    {
        panel = transform.Find("Panel").gameObject;
        manPanel = transform.Find("ManualPanel").gameObject;
        panelImage = panel.GetComponent<Image>();
        Component[] listImg = panel.transform.GetComponentsInChildren(typeof(Image));
        Component[] manImgArray = manPanel.transform.GetComponentsInChildren(typeof(Image));
        Component[] manTxtArray = manPanel.transform.GetComponentsInChildren(typeof(Text));
        Dumpy(listImg, objImg);
        Dumpy(manImgArray, manImg);
        DumpyTxt(manTxtArray, manTxt);
        DeActiveTxtInList(manTxt);
        manPanel.SetActive(false);
    }
    private void Start()
    {
        Debug.Log(objImg.Count.ToString());
        foreach (var img in objImg)
        {
            img.fillAmount = 0;
        }

        foreach (var img in manImg)
        {
            img.fillAmount = 0;
        }

    }
    void Dumpy(Component[] comps, List<Image> list)
    {
        foreach (var com in comps)
        {
            list.Add((Image)com);
        }
    }
    void DumpyTxt(Component[] comps, List<Text> list)
    {
        foreach (var com in comps)
        {
            list.Add((Text)com);
        }
    }
    public void ObtionButton()
    {
        if (processingPanel) return;
        if (panelIsOpen)
        {
            foreach (var img in objImg)
            {
                StartCoroutine(UnfillImg(img));
            }
        }
        else
        {
            foreach (var img in objImg)
            {
                StartCoroutine(FillImg(img));
            }
        }
        panelIsOpen = !panelIsOpen;
    }
    public void ManButton()
    {
        if (processingManPanel) return;
        if (manPanelIsOpen)
        {
            foreach (var img in manImg)
            {
                StartCoroutine(UnfillManImg(img));
            }
            manPanel.SetActive(false);
        }
        else
        {
            manPanel.SetActive(true);
            foreach (var img in manImg)
            {
                StartCoroutine(FillManImg(img));
            }
        }
        manPanelIsOpen = !manPanelIsOpen;
    }
    IEnumerator FillImg(Image img)
    {
        processingPanel = true;
        while (img.fillAmount < 1)
        {
            img.fillAmount += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }
        Button btn = img.gameObject.GetComponent<Button>();
        if (btn != null)
            btn.interactable = true;
        processingPanel = false;
    }
    IEnumerator UnfillImg(Image img)
    {
        processingPanel = true;
        while (img.fillAmount > 0)
        {
            img.fillAmount -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        img.fillAmount = 0f;
        Button btn = img.gameObject.GetComponent<Button>();
        if (btn != null)
            btn.interactable = false;
        processingPanel = false;
    }

    IEnumerator FillManImg(Image img)
    {
        processingManPanel = true;
        while (img.fillAmount < 1)
        {
            img.fillAmount += 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        processingManPanel = false;
        SetActiveTxtInList(manTxt);
    }
    IEnumerator UnfillManImg(Image img)
    {
        processingManPanel = true;
        while (img.fillAmount > 0)
        {
            img.fillAmount -= 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        img.fillAmount = 0f;
        DeActiveTxtInList(manTxt);
        processingManPanel = false;
    }
    public void TogglePlayOffMusic()
    {
        AudioSystemScript.instance.TogglePlayOffThemeMusic();
        AudioSource themeMusic = AudioSystemScript.instance.music;
        if (themeMusic.isPlaying)
        {
            GameObject myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        }
    }
    void SetActiveTxtInList(List<Text> list)
    {
        foreach (var txt in list)
        {
            txt.gameObject.SetActive(true);
        }
    }
    void DeActiveTxtInList(List<Text> list)
    {
        foreach (var txt in list)
        {
            txt.gameObject.SetActive(false);
        }
    }
    public void ReloadScene()
    {
        Systems.instance.ReloadCurrentScene();
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    void OnEndGame()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Systems.onEndGame += OnEndGame;
    }
    private void OnDisable()
    {
        Systems.onEndGame -= OnEndGame;
    }
}
