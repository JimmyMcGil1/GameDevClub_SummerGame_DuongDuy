using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraFigure : MonoBehaviour
{
    SkeletonAnimation anim;
    int hp = 100;
    private void Awake()
    {
        anim = GetComponent<SkeletonAnimation>();
        anim.AnimationName = "action/idle/normal";
    }
   
    public void TakeDame()
    {
        hp -= 10;
        anim.AnimationName = "defense/hit-by-normal";
        anim.loop = false;
    }
}
