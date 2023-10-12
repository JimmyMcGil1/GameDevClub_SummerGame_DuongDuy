using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Units.Bourbon
{
    public class BourbonSwordControl : MonoBehaviour
    {
        GameObject sword;
        Camera cam;
        SummonSwordDetect detectDotSummonSword;
        BourbonMoveset moveset;
        Animator anim;
        [SerializeField] AudioClip callSwordClip;
        public bool bourbonIsCalling { get; private set; } = false;
        BourbonController controller;
        private void Awake()
        {
            controller = GetComponent<BourbonController>();
            moveset = GetComponent<BourbonMoveset>();
            anim = GetComponent<Animator>();
            detectDotSummonSword = gameObject.transform.Find("DotDetectLayer").gameObject.GetComponent<SummonSwordDetect>();
            cam = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<Camera>();
            sword = transform.parent.Find("Sword__All").gameObject;
        }
        private void Start()
        {
            detectDotSummonSword.gameObject.SetActive(false);
        }
        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Q) && controller.condition != CharacterConditions.Dead && anim.GetFloat("bringSword") == 0)
            {
                if (!bourbonIsCalling)
                    RecallSword();
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && controller.condition != CharacterConditions.Dead)
            {
                // anim.SetTrigger("cast_transform_sword");
                // Invoke("SummonSword", 0.001f);
                //


                //////////testing bumble form
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                controller.ChangeState();
                sword.SetActive(true);
                sword.GetComponent<SwordScript>().Transform(mousePos, SwordScript.swordForm.bumble);

                ////////
            }
        }
        //This function is called in animation
        public void SummonSword()
        {
            if (anim.GetFloat("bringSword") == 1)
            {
                Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                detectDotSummonSword.gameObject.SetActive(true);
                detectDotSummonSword.transform.position = mousePos;
                if (detectDotSummonSword.isInPlatForm())
                {
                    MainCanvas._instance.DisplayTxt("Can not summon Sword in platform", 1);
                    detectDotSummonSword.gameObject.SetActive(false);
                    return;
                }
                detectDotSummonSword.gameObject.SetActive(false);
                //anim.SetTrigger("cast_transform_sword");
                controller.ChangeState();
                sword.SetActive(true);
                sword.GetComponent<SwordScript>().Transform(mousePos);
            }
        }
        void RecallSword()
        {
            anim.SetTrigger("cast_transform_sword");
            bourbonIsCalling = true;
            AudioSystemScript.instance.PlaySound(callSwordClip, gameObject.transform.position, 1);
            sword.GetComponent<SwordScript>().ReturnToBourbon();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("BourbonSword"))
            {
                //Check Form Of Sword
                if (collision.gameObject.GetComponent<SwordScript>().currentForm == SwordScript.swordForm.normal)
                {
                    controller.ChangeState(false);
                    collision.gameObject.SetActive(false);
                    bourbonIsCalling = false;
                }
                else if (collision.gameObject.GetComponent<SwordScript>().currentForm == SwordScript.swordForm.plank)
                {
                }
            }
        }
    }

}
