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
        return scaleSmall / scaleBig;
    }

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return center + ray.direction * scale;
    }

}
