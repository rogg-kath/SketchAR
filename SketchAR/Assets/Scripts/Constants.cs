//using System;
//using System.Collections;
//using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public enum Mode
    {
        None,
        Line,
        Sphere,
        Cube,
        Cylinder,
    }

    public enum MenuState
    {
        None,
        Forms,
        Main,
    }

    static public float timerDelay = 0.05f;
    static public string nameSmall = "SmallModel";
    static public string nameBig = "BigModel";

    static public bool isDrawing = false;

    static public float scaleSmall = 1f;
    static public float scaleBig = 2f;
    static public Quaternion rotationSmall;
    static public Quaternion rotationBig;

    static public Mode currentMode = Mode.None;
    static public MenuState currentMenuState = MenuState.None;

    static float objectIndex = 0;
    static public List<string> namesSmallObjects;
    static public List<string> namesBigObjects;

    GameObject smallModel;
    GameObject bigModel;

    void Start()
    {
        smallModel = GameObject.Find(nameSmall);
        bigModel = GameObject.Find(nameBig);
    }

    void Update()
    {
        // TODO actually only needs to be called after resize
        scaleSmall = smallModel.transform.localScale.x;
        scaleBig = bigModel.transform.localScale.x;
        // TODO actually only needs to be called after rotation
        rotationSmall = smallModel.transform.rotation;
        rotationBig = bigModel.transform.rotation;
    }

    static public void setScaleBig(float newScale) {
        scaleBig = newScale;
    }

    // Don't need this method as small model is only one that can be modified (nur Verhältnis zum großen Model ist für dieses revevant)
    /*static public float actualScaleSmall()
    {
        if (scaleBig == 0f)
        {
            return scaleSmall;
        }
        return scaleSmall / scaleBig;
    }*/

    static public float actualScaleBig() {
        if (scaleSmall == 0f) {
            return scaleBig;
        }
        return scaleBig / scaleSmall;
    }

    static public float newObject()
    {
        objectIndex = objectIndex + 1;
        return objectIndex;
    }

    static public Vector3 GetPosition(Vector3 center, float scale)
    {
        return GetPositionPC(center, scale);
    }

    static Vector3 GetPositionPC(Vector3 center, float scale)
    {
        // get position projected onto z-plane of conter point
        // does not work good for lines (distortion on border of screen)
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Vector3 result = center + ray.direction * scale;
        result.z = center.z;
        return result;

        // Vector3 screenPosition = Input.mousePosition;
        // screenPosition.z = Camera.main.nearClipPlane;
        // Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        // worldPosition.z = center.z * scale;
        // Vector3 centerProjection = center;
        // centerProjection.z = Camera.main.nearClipPlane;
        // Ray fromCenterToPoint = new Ray(centerProjection, worldPosition);
        // return center + fromCenterToPoint.direction * scale; // results position is still sphere around center
    }


    static public Vector3 rotate_vector_by_quaternion(Quaternion q, Vector3 v)
    {
        // Extract the vector part of the quaternion
        Vector3 u = new Vector3(q.x, q.y, q.z);

        // Extract the scalar part of the quaternion
        float s = q.w;

        // Do the math
        return 2.0f * Vector3.Dot(u, v) * u
              + (s* s - Vector3.Dot(u, u)) * v
              + 2.0f * s* Vector3.Cross(u, v);
    }

}
