using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialScript : MonoBehaviour
{
    EventSystem eventSystem;
    private void Awake()
    {
        eventSystem = EventSystem.current;
    }
    private void Update()
    {
    }
}
