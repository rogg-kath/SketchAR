using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    GameObject menuPanel;
    GameObject formsPanel;


    // Start is called before the first frame update
    void Start()
    {
        menuPanel = GameObject.Find("Panel_Menu");
        formsPanel = GameObject.Find("Panel_Forms");
        formsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeForm(string newMode)
    {
        Debug.Log("SWICTH FORM: " + Constants.currentMode + " <-from---to-> " + newMode);
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

    public void ChangeMenuState(string newState)
    {

        Debug.Log("SWITCH MENU STATE: " + Constants.currentMenuState + " <-from---to-> " + newState );
        switch (newState)
        {
            case "forms":
                Constants.currentMenuState = Constants.MenuState.Forms;
                formsPanel.SetActive(true);
                menuPanel.SetActive(false);
                break;
            case "main":
                Constants.currentMenuState = Constants.MenuState.Main;
                formsPanel.SetActive(false);
                menuPanel.SetActive(true);
                break;
            default:
                Constants.currentMenuState = Constants.MenuState.None;
                formsPanel.SetActive(false);
                menuPanel.SetActive(true);
                break;
        }
    }
}
