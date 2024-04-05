using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeMode(string newMode)
    {
        Debug.Log("SWICTH MODE: " + newMode + " <--> "+ Constants.currentMode);
        switch (newMode)
        {
            case "line":
                Constants.currentMode = Constants.Mode.Line;
                break;
            case "sphere":
                Constants.currentMode = Constants.Mode.Sphere;
                break;
            case "cube":
                Constants.currentMode = Constants.Mode.Cube;
                break;
            case "cylinder":
                Constants.currentMode = Constants.Mode.Cylinder;
                break;
            default:
                Constants.currentMode = Constants.Mode.None;
                break;
        }
    }
}
