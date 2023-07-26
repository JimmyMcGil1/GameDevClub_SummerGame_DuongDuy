using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlotScript : MonoBehaviour
{
    [SerializeField] string p1 = "We are following the journey of Bourbon and his friend, Sword." +
        " Sword is a sword that has been gived to Bourbon since he was a kid by his father, " +
        "a legend pirate is missing now.";
    [SerializeField] string p2 = "One day, Bourbon decides to start an adventure to a " +
        " mysterious island ruled by a powerfull wizard. People said that this island have a " +
        "supreme treasure which can make owner of it became the Lord of Seven ocean.";
    [SerializeField] string p3 = "I have to write all these line in English because the font I use can not add punctuation " +
        "in word. Sorry for any mistake and enjoy the game.";
    private void Start()
    {
        StartCoroutine(StartWrite());
    }
    IEnumerator StartWrite()
    {
        MainCanvas._instance.DisplayTxt(p1, 7);
        yield return new WaitForSeconds(8);
        MainCanvas._instance.DisplayTxt(p2, 6);
        yield return new WaitForSeconds(7);
        MainCanvas._instance.DisplayTxt(p3, 5);
        yield return new WaitForSeconds(6);
        MainCanvas._instance.LoadScene();
    }
}
