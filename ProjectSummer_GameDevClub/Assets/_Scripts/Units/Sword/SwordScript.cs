using Pathfinding;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SwordScript : SwordBaseUnit
{
    public static SwordScript _instance { get; private set; }
    GameObject bourbon;

    [Header("Return To Master")]
    [SerializeField] LayerMask tilemapLayer;
    [SerializeField] LayerMask bourbonLayer;
    [SerializeField] float distanceCast;
    [SerializeField] float floatSpeed;
    [SerializeField] float amplitude;
    float y0;
    private Vector3 tempPos;

    bool isFloating;
    BoxCollider2D plankCollider;
    CircleCollider2D bumbleCol;
    CircleCollider2D circleCol;
    Animator anim;
    public enum swordForm
    {
        normal,
        plank,
        bumble,
    }
    public swordForm currentForm { get; private set; }
    private void Awake()
    {

        circleCol = GetComponent<CircleCollider2D>();
        anim = GetComponentInChildren<Animator>();
        bourbon = GameObject.FindGameObjectWithTag("Bourbon");
        currentForm = swordForm.normal;
        anim.SetFloat("form", 0);
        isFloating = false;
        plankCollider = gameObject.transform.Find("swordGFX").Find("PlankCollider").gameObject.GetComponent<BoxCollider2D>();
        plankCollider.enabled = false;
        bumbleCol = gameObject.transform.Find("swordGFX").Find("BumbleCollider").gameObject.GetComponent<CircleCollider2D>();
        bumbleCol.enabled = false;
        gameObject.GetComponent<AIPath>().canMove = false;
    }

    private void Update()
    {
    }
    private void FixedUpdate()
    {

        if (isFloating)
        {
            SeftFloating();
        }
    }

    public void ReturnToBourbon()
    {
        GetComponent<AIPath>().canMove = true;
        if (anim.GetFloat("form") == 1)
            Transform(transform.position, swordForm.normal);

    }

    void SeftFloating()
    {
        tempPos.y = y0 + amplitude * Mathf.Sin(floatSpeed * Time.time);
        transform.position = tempPos;
    }
    public void Transform(Vector2 appearPos, swordForm _form = swordForm.plank)
    {
        gameObject.transform.position = appearPos;
        anim.SetTrigger("transform");
        FormSelect(_form);

    }
    void FormSelect(swordForm _form)
    {
        currentForm = _form;
        switch (_form)
        {
            case swordForm.normal:
                IntoNorForm();
                anim.SetFloat("form", 0);
                break;
            case swordForm.plank:
                anim.SetFloat("form", 1);
                IntoPlankForm();
                break;
            case swordForm.bumble:
                anim.SetFloat("form", 2);
                IntoBumbleForm();
                break;
            default:
                break;
        }
    }

    void IntoBumbleForm()
    {
        GetComponent<AIPath>().enableRotation = false;
        GetComponent<AIPath>().canMove = false;
      //  isFloating = true;
        bumbleCol.enabled = true;
        circleCol.enabled = false;
    }

    void IntoPlankForm()
    {
        GetComponent<AIPath>().enableRotation = false;
        isFloating = true;
        plankCollider.enabled = true;
        y0 = transform.position.y;
        tempPos = transform.position;
        Quaternion quar = Quaternion.Euler(45, 20, 0);
        plankCollider.transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localRotation = quar;
        circleCol.enabled = false;
    }
    void IntoNorForm()
    {
        GetComponent<AIPath>().enableRotation = true;
        plankCollider.enabled = false;
        if (isFloating) isFloating = false;
        Quaternion quar = Quaternion.Euler(0, 0, 0);
        transform.localRotation = quar;
        circleCol.enabled = true;
    }

}
