using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{

    GameObject doorUI;
    private void Awake()
    {
        doorUI = transform.Find("DoorCanvas").gameObject;
        doorUI.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            GetComponent<Animator>().SetBool("door_open", true);
            StartCoroutine(CanvasAppear());
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                MainCanvas._instance.LoadScene();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            GetComponent<Animator>().SetBool("door_open", false);
            StartCoroutine(CanvasDisappear());
        }
    }

    IEnumerator CanvasAppear()
    {
        doorUI.SetActive(true);
        Text text = doorUI.transform.Find("door_text").GetComponent<Text>();
        Image letterImg = doorUI.transform.Find("door_letter_img").GetComponent<Image>();
        Color color = text.color;
        while (color.a < 1)
        {
            color.a += 0.05f;
            text.color = color;
            letterImg.color = color;
            yield return new WaitForSeconds(0.05f);
        }

    }
    IEnumerator CanvasDisappear()
    {
        Text text = doorUI.transform.Find("door_text").GetComponent<Text>();
        Image letterImg = doorUI.transform.Find("door_letter_img").GetComponent<Image>();
        Color color = text.color;
        while (color.a > 0)
        {
            color.a -= 0.05f;
            text.color = color;
            letterImg.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        color.a = 0f;
        text.color = color;
        letterImg.color = color; doorUI.SetActive(false);
        doorUI.SetActive(false);
    }
}
