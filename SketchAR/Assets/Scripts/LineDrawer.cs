using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    List<Vector3> linePoints;
    List<Vector3> linePointsBig;
    float timer;
    float timerDelay;

    GameObject smallModel;
    GameObject bigModel;

    GameObject newLine;
    GameObject newLineBig;
    LineRenderer drawLine;
    LineRenderer drawLineBig;
    float lineWidth;



    // Start is called before the first frame update
    void Start()
    {
        linePoints = new List<Vector3>();
        linePointsBig = new List<Vector3>();
        timerDelay = Constants.timerDelay;
        timer = timerDelay;
        smallModel = GameObject.Find(Constants.nameSmall);
        bigModel = GameObject.Find(Constants.nameBig);
        lineWidth = 0.05f;
        Debug.Log(Constants.scaleBig);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetupLineRenderer();
        }

        if (Input.GetMouseButton(1))
        {
            Constants.scaleBig = bigModel.transform.localScale.x;
            DrawContinous();
        }

        if (Input.GetMouseButtonUp(1))
        {
            // Debug.Log(linePoints.Count);
            newLine.transform.parent = smallModel.transform;
            newLineBig.transform.parent = bigModel.transform;
            linePoints.Clear();
            linePointsBig.Clear();
        }
    }

    Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return ray.origin + ray.direction * 10;
    }

    Vector3 GetPosition(Vector3 center, float scale)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log("Kamera: ---> "+Camera.main.name);
        return center + ray.direction * scale;
    }

    Color randomColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    void SetupLineRenderer()
    {
        var color = randomColor();
        var objectIndex = Constants.newObject();

        newLine = new GameObject(objectIndex + "_Line")
        {
            tag = "Small"
        };
        drawLine = newLine.AddComponent<LineRenderer>();
        drawLine.useWorldSpace = false;
        drawLine.material = new Material(Shader.Find("Sprites/Default"));
        drawLine.startColor = color;
        drawLine.endColor = drawLine.startColor;
        drawLine.startWidth = lineWidth * (1 / Constants.scaleBig);
        drawLine.endWidth = lineWidth * (1 / Constants.scaleBig);

        newLineBig = new GameObject(objectIndex+"_LineBig")
        {
            tag = "Big"
        };
        drawLineBig = newLineBig.AddComponent<LineRenderer>();
        drawLineBig.useWorldSpace = false;
        drawLineBig.material = new Material(Shader.Find("Sprites/Default"));
        drawLineBig.startColor = color;
        drawLineBig.endColor = drawLineBig.startColor;
        drawLineBig.startWidth = lineWidth; // TODO soll kleines Modell auch gescaled werden können?
        drawLineBig.endWidth = lineWidth;
    }

    void DrawContinous()
    {
        // Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), GetMousePosition(), Color.red);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            linePoints.Add(GetPosition(smallModel.transform.position, Constants.scaleSmall));
            drawLine.positionCount = linePoints.Count;
            drawLine.SetPositions(linePoints.ToArray());

            linePointsBig.Add(GetPosition(bigModel.transform.position, Constants.scaleBig));
            drawLineBig.positionCount = linePointsBig.Count;
            drawLineBig.SetPositions(linePointsBig.ToArray());

            // Debug.Log("\n" + smallModel + "\n" + bigModel + "\nPosition Small: " + GetPosition(smallModel.transform.position, 1) + "\nPosition Big: " + GetPosition(bigModel.transform.position, 2));
            timer = timerDelay;
        }
    }
}