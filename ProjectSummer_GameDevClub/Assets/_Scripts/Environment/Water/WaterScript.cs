using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Units.Bourbon;
public class WaterScript : MonoBehaviour
{

    
    private int ConnersCount = 2;
    [SerializeField] private SpriteShapeController waterShapeController;

   
    [SerializeField] int wavesCount = 6;

    [SerializeField] List<WaterSrping> springs = new List<WaterSrping>();
    [SerializeField] GameObject wavePointPref;
    [SerializeField] GameObject wavePoints;
    public float springStiffness = 0.1f;
    public float dampening = 0.03f;
    public float spread = 0.006f;



    void UpdateSprings()
    {
        int count = springs.Count;
        float[] leftSpread = new float[count];
        float[] rightSpread = new float[count];
        for (int i = 0; i < count; i++)
        {
            if (i > 0)
            {
                leftSpread[i] = spread * (springs[i].height - springs[i - 1].height);
                springs[i - 1].velocity += leftSpread[i];
            }
            if (i < count - 1)
            {
                rightSpread[i] = spread * (springs[i].height - springs[i + 1].height);
                springs[i + 1].velocity += rightSpread[i];
            }
        }
    }
    private void FixedUpdate()
    {

        foreach (WaterSrping waterSpringComponent in springs)
        {
            waterSpringComponent.WaveSpringUpdate(springStiffness, dampening);
            waterSpringComponent.WavePointUpdate();
        }

        UpdateSprings();
    }
    void Splash(int index, float speed)
    {
        if (index >= 0 && index < springs.Count)
        {
            springs[index].velocity += speed;
        }
    }
    void CreateSprings(Spline waterPline)
    {
        springs = new();
        for (int i = 0; i <= wavesCount + 1; i++)
        {
            int index = i + 1;
            GameObject wavePoint = Instantiate(wavePointPref, wavePoints.transform, false);
            wavePoint.transform.localPosition = waterPline.GetPosition(index);
            WaterSrping waterSrping = wavePoint.GetComponent<WaterSrping>();
            waterSrping.Init(waterShapeController);
            springs.Add(waterSrping);
        }
    }
    private void SetWaves()
    {
        Spline waterSpline = waterShapeController.spline;
        int waterPointCount = waterSpline.GetPointCount();
        for (int i = ConnersCount; i < waterPointCount - ConnersCount; i++)
        {
            waterSpline.RemovePointAt(ConnersCount);
        }


        Vector3 waterTopLeftCorner = waterSpline.GetPosition(1);
        Vector3 waterTopRightCorner = waterSpline.GetPosition(2);
        float waterWidth = waterTopRightCorner.x - waterTopLeftCorner.x;
        float spacingPerWave = waterWidth / (wavesCount + 1);
        for (int i = wavesCount; i > 0; i--)
        {
            int index = ConnersCount;
            float xPosition = waterTopLeftCorner.x + (spacingPerWave * i);
            Vector3 wavePoint = new Vector3(xPosition, waterTopLeftCorner.y, waterTopLeftCorner.z);
            waterSpline.InsertPointAt(index, wavePoint);
            waterSpline.SetHeight(index, 0.1f);
            waterSpline.SetCorner(index, false);
            waterSpline.SetTangentMode(index, ShapeTangentMode.Continuous);
        }

        springs = new();
        for (int i = 0; i <= wavesCount + 1; i++)
        {
            int index = i + 1;

            Smoothen(waterSpline, index);

            GameObject wavePoint = Instantiate(wavePointPref, wavePoints.transform, false);
            wavePoint.transform.localPosition = waterSpline.GetPosition(index);

            WaterSrping waterSpring = wavePoint.GetComponent<WaterSrping>();
            waterSpring.Init(waterShapeController);
            springs.Add(waterSpring);

        }


    }
    private void Smoothen(Spline waterSpline, int index)
    {
        Vector3 position = waterSpline.GetPosition(index);
        Vector3 positionPrev = position;
        Vector3 positionNext = position;
        if (index > 1)
        {
            positionPrev = waterSpline.GetPosition(index - 1);
        }
        if (index - 1 <= wavesCount)
        {
            positionNext = waterSpline.GetPosition(index + 1);
        }

        Vector3 forward = gameObject.transform.forward;

        float scale = Mathf.Min((positionNext - position).magnitude, (positionPrev - position).magnitude) * 0.33f;

        Vector3 leftTangent = (positionPrev - position).normalized * scale;
        Vector3 rightTangent = (positionNext - position).normalized * scale;

        SplineUtility.CalculateTangents(position, positionPrev, positionNext, forward, scale, out rightTangent, out leftTangent);

        waterSpline.SetLeftTangent(index, leftTangent);
        waterSpline.SetRightTangent(index, rightTangent);
    }

    void OnValidate()
    {
        // Clean waterpoints 
        StartCoroutine(CreateWaves());
    }
    IEnumerator CreateWaves()
    {
        foreach (Transform child in wavePoints.transform)
        {
            StartCoroutine(Destroy(child.gameObject));
        }
        yield return null;
        SetWaves();
        yield return null;
    }
    IEnumerator Destroy(GameObject go)
    {
        yield return null;
        DestroyImmediate(go);
    }
    //private void Start()
    //{
    //    StartCoroutine(SetUp());
    //}
    //IEnumerator SetUp()
    //{
    //    SetWaves();
    //    yield return new WaitForSeconds(1f);
    //    CreateSprings(waterShapeController.spline);

    //}
    IEnumerator StartBlur()
    {
        Color color = gameObject.GetComponent<SpriteShapeRenderer>().color;
        while (color.a > 0.5f)
        {
            color.a -= 0.05f;
            gameObject.GetComponent<SpriteShapeRenderer>().color = color;
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator StarClear()
    {
        Color color = gameObject.GetComponent<SpriteShapeRenderer>().color;
        while (color.a < 1f)
        {
            color.a += 0.05f;
            gameObject.GetComponent<SpriteShapeRenderer>().color = color;
            yield return new WaitForSeconds(0.05f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            StartCoroutine(StartBlur());
            collision.gameObject.GetComponent<BourbonController>().IntoWater();
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            StartCoroutine(StarClear());
            collision.gameObject.GetComponent<BourbonController>().OutWater();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bourbon"))
        {
            collision.gameObject.GetComponent<Animator>().SetBool("isSwimming", true);
        }
    }
}
