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
        Colors,
        Main,
    }

    static public float timerDelay = 0.05f;
    static public string nameSmall = "SmallModel";
    static public string nameBig = "BigModel";

    static public bool isDrawing = false;

    static public float scaleSmall;
    static public float scaleBig;

    static public Mode currentMode = Mode.None;
    static public MenuState currentMenuState = MenuState.Main;

    static public string currentColor = "none";

    static float objectIndex = 0;
    static public List<string> namesSmallObjects;
    static public List<string> namesBigObjects;

    GameObject smallModel;
    GameObject bigModel;

    void Start()
    {
        smallModel = GameObject.Find(nameSmall);
        bigModel = GameObject.Find(nameBig);
        scaleSmall = smallModel.transform.localScale.x;
        scaleBig = bigModel.transform.localScale.x;
    }

    void Update()
    {
        // TODO actually only needs to be called after resize
        scaleSmall = smallModel.transform.localScale.x;
        scaleBig = bigModel.transform.localScale.x;
    }

    static public void setScaleBig(float newScale) {
        scaleBig = newScale;
    }

    static public float actualScaleSmall()
    {
        if (scaleBig == 0f)
        {
            return scaleSmall;
        }
        return scaleSmall * (scaleSmall / scaleBig);
    }

    // Actual scale factor (small: 0,25, big: 2 => big = 8 * small)
    static public float actualScaleBig() {
        if (scaleSmall == 0f) {
            return scaleBig;
        }
        return scaleBig * (scaleBig / scaleSmall);
    }

    static public float factorScaleBig() {
        if (scaleSmall == 0f) {
            return scaleBig;
        }
        return scaleBig / scaleSmall;
    }

    static public float factorScaleSmall() {
        if (scaleBig == 0f)
        {
            return scaleSmall;
        }
        return scaleSmall / scaleBig;
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
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = center.z;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        worldPosition.z = center.z;
        Ray fromCenterToPoint = new Ray(center, worldPosition);
        Debug.DrawRay(Camera.main.transform.position, center + worldPosition * scale, Color.red);
        // Debug.Log("World point: " + worldPosition+" Center: "+center + " --->"+(center + worldPosition*scale));
        return center + fromCenterToPoint.direction * scale;
        
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // // Ray fromCenterToPoint = new Ray(center, screenPoint);
        // Vector3 worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, -Input.mousePosition.y, 0));
        // worldPoint.z = center.z;
        // Ray fromCenterToPoint = new Ray(center, worldPoint);
        // Debug.Log("World point: " + worldPoint+" Center: "+center+ " Ray: "+ fromCenterToPoint.direction);
        // return center + fromCenterToPoint.direction * scale;
        
        
        // var centerTest = center;
        // centerTest.z = 1;
        // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Vector3 position = Input.mousePosition;
        // position.z = center.z;
        // position.x = -position.x;
        // position.y = -position.y;
        // Vector3 screenPoint = Camera.main.ScreenToWorldPoint(position);
        // Ray fromCenterToPoint = new Ray(center, screenPoint);

        
        
        // Debug.Log("Center: "+center+"Position-------> "+ (center - fromCenterToPoint.direction * scale) + " Vector: "+screenPoint+ " Ray: "+fromCenterToPoint);
        // return center - fromCenterToPoint.direction * scale;
        // return center + ray.direction * scale;
        // var test = (center + ray.direction * scale);
        // test.z = 1;
        // return test;
    }

}
