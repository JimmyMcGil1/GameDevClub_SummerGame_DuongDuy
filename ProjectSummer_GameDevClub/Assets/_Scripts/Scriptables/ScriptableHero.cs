using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Create a scriptable hero 
/// </summary>
[CreateAssetMenu(fileName = "New Scriptable Hero")]
public class ScriptableHero : ScriptableExampleUnitBase
{
    public ExampleHeroType HeroType;
    public Stats initStats;
}

[Serializable]
public enum ExampleHeroType
{
    Tarodev = 0,
    Snorlax = 1
}
