using System.Collections;
using System.Collections.Generic;
using System.Net;
// using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    GameObject menuPanel;
    GameObject formsPanel;
    GameObject colorsPanel;


    // Start is called before the first frame update
    void Start()
    {
        menuPanel = GameObject.Find("Panel_Menu");
        formsPanel = GameObject.Find("Panel_Forms");
        colorsPanel = GameObject.Find("Panel_Colors");
        formsPanel.SetActive(false);
        colorsPanel.SetActive(false);
        ColorBlock cb = GameObject.Find("Button_Colors").GetComponent<Button>().colors;
        cb.normalColor = Color.red;
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
            case "main":
                Constants.currentMenuState = Constants.MenuState.Main;
                menuPanel.SetActive(true);
                formsPanel.SetActive(false);
                colorsPanel.SetActive(false);
                break;
            case "forms":
                Constants.currentMenuState = Constants.MenuState.Forms;
                menuPanel.SetActive(false);
                formsPanel.SetActive(true);
                colorsPanel.SetActive(false);
                break;
            case "colors":
                Constants.currentMenuState = Constants.MenuState.Main;
                menuPanel.SetActive(false);
                formsPanel.SetActive(false);
                colorsPanel.SetActive(true);
                break;
            // default:
            //     Constants.currentMenuState = Constants.MenuState.None;
            //     menuPanel.SetActive(false);
            //     formsPanel.SetActive(false);
            //     colorsPanel.SetActive(false);
            //     break;
        }

        // Set the image of the Color Button to right color
        if (newState == "main") {
            Button colorButton = GameObject.Find("Button_Colors").GetComponent<Button>();
            Debug.Log("Button: "+colorButton.name+ " Color: "+Constants.currentColor+" --- "+colorButton.GetComponent<Image>().color);
            ColorBlock cb = colorButton.colors;
            switch (Constants.currentColor)
            {
                case "red":
                    colorButton.GetComponent<Image>().color = new Vector4(230,30,30,1);
                    cb.normalColor = new Vector4(230,30,30,1);
                    Debug.Log("NEW COLOR: "+Constants.currentColor+" --- "+colorButton.GetComponent<Image>().color);
                    break;
                case "green":
                    colorButton.GetComponent<Image>().color = new Vector4(30,230,30,1);
                    cb.normalColor =  new Vector4(30,230,30,1);
                    break;
                case "blue":
                    colorButton.GetComponent<Image>().color = new Vector4(30,30,230,1);
                    cb.normalColor = new Vector4(30,30,230,1);
                    break;
                case "yellow":
                    colorButton.GetComponent<Image>().color = new Vector4(230,230,30,1);
                    cb.normalColor = new Vector4(230,230,30,1);
                    break;
                case "special":
                    break;
                default:
                    colorButton.GetComponent<Image>().color = new Vector4(0,0,0,1);
                    break;
            }
        }
    }

    public void ChangeColor(string newColor)
    {
        switch (newColor)
        {
            case "red":
                Constants.currentColor = "red";
                break;
            case "green":
                Constants.currentColor = "green";
                break;
            case "blue":
                Constants.currentColor = "blue";
                break;
            case "yellow":
                Constants.currentColor = "yellow";
                break;
            case "special":
                Constants.currentColor = "special";
                break;
            default:
                Constants.currentColor = "none";
                break;
        }
        
    }
}
