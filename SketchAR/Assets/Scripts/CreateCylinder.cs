using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class CreateCylinder : MonoBehaviour
{
    Vector3 centerSmall;
    Vector3 centerBig;
    float timer;
    float timerDelay;

    GameObject smallModel;
    GameObject bigModel;

    GameObject newCylinder;
    GameObject newCylinderBig;

    Vector3 point1;// Center TODO OR one bottom rim point
    Vector3 point2;// Bottom rim
    Vector3 point3;// Top rim

    void Start()
    {
        timerDelay = Constants.timerDelay;
        timer = timerDelay;
        smallModel = GameObject.Find(Constants.nameSmall);
        bigModel = GameObject.Find(Constants.nameBig);

        point1 = Vector3.zero;
        point2 = Vector3.zero;
        point3 = Vector3.zero;
    }

    void Update() {
        // Update points on mouse click
        if (Input.GetMouseButtonDown(0)) {
            if (point2 == Vector3.zero) {
                if (point1 == Vector3.zero) {
                    // initial click for new object creation
                    SetupRenderer();
                    point1 = GetPosition(smallModel.transform.position, Constants.scaleSmall);
                } else {
                    point2 = GetPosition(smallModel.transform.position, Constants.scaleSmall);
                }
            } else if (point3 == Vector3.zero) {
                point3 = GetPosition(smallModel.transform.position, Constants.scaleSmall);
                // TODO placeObject() + reset points
            }
        }

        // if we started drwaing -> update
        if ((point1 != null)) {
            DrawContinous();
        }

        // reset points and place object in parent
        if (Input.GetMouseButtonUp(0) && point3 != Vector3.zero) {
            point1 = Vector3.zero;
            point2 = Vector3.zero;
            point3 = Vector3.zero;
            newCylinder.transform.parent = smallModel.transform;
            newCylinderBig.transform.parent = bigModel.transform;
        }
    }

    Vector3 GetPosition(Vector3 center, float scale)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return center + ray.direction * scale;
    }

    float GetDistance(Vector3 center, Vector3 endpioint)
    {
        Debug.Log("Distance of: " + center + " and " + endpioint + " = " + Vector3.Distance(center, endpioint));
        return Vector3.Distance(center, endpioint);
    }

    Color randomColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    void SetupRenderer()
    {
        centerSmall = GetPosition(smallModel.transform.position, Constants.scaleSmall);
        centerBig = GetPosition(bigModel.transform.position, Constants.scaleBig);

        newCylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        newCylinder.tag = "Small";
        newCylinder.transform.position = centerSmall;
        newCylinder.transform.localScale = new Vector3(0, 0, 0);

        newCylinderBig = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        newCylinderBig.tag = "Big";
        newCylinderBig.transform.position = centerBig;
        newCylinderBig.transform.localScale = new Vector3(0, 0, 0);
    }

    void DrawContinous()
    {
        // Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), GetMousePosition(), Color.red);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            var radius = GetDistance(point1, GetPosition(smallModel.transform.position, Constants.scaleSmall));
            var height = point3 != Vector3.zero ? GetDistance(point2, point3) : 0.01f;

            newCylinder.GetComponent<CapsuleCollider>().radius = radius;
            newCylinder.GetComponent<CapsuleCollider>().height = height;
            newCylinder.transform.localScale = new Vector3(radius, radius, height);

            newCylinderBig.GetComponent<CapsuleCollider>().radius = radius * Constants.scaleBig;
            newCylinderBig.GetComponent<CapsuleCollider>().height = height * Constants.scaleBig;
            newCylinderBig.transform.localScale = new Vector3(radius * Constants.scaleBig, radius * Constants.scaleBig, radius * Constants.scaleBig);

            timer = timerDelay;
        }
    }
}
