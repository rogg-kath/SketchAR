using UnityEngine;

public class CreateCube : MonoBehaviour
{
    Vector3 centerSmall;
    Vector3 centerBig;
    float timer;
    float timerDelay;

    GameObject smallModel;
    GameObject bigModel;

    GameObject newSphere;
    GameObject newSphereBig;


    void Start()
    {
        centerSmall = new Vector3(0, 0, 0);
        timerDelay = Constants.timerDelay;
        timer = timerDelay;
        smallModel = GameObject.Find(Constants.nameSmall);
        bigModel = GameObject.Find(Constants.nameBig);
    }

    void Update()
    {
        if (Constants.currentMode == Constants.Mode.Cube)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Constants.isDrawing = true;
                SetupRenderer();
            }

            if (Input.GetMouseButton(0))
            {
                DrawContinous();
            }

            if (Input.GetMouseButtonUp(0) && Constants.isDrawing) 
            {
                Constants.isDrawing = false;
                newSphere.transform.parent = smallModel.transform;
                newSphereBig.transform.parent = bigModel.transform;
            }
        }
    }

    float GetRadius(Vector3 center, Vector3 endpioint)
    {
        Debug.Log("Radius of: " + center + " and " + endpioint + " = " + Vector3.Distance(center, endpioint));
        return Vector3.Distance(center, endpioint);
    }

    Color randomColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    void SetupRenderer()
    {
        var objectIndex = Constants.newObject();

        centerSmall = Constants.GetPosition(smallModel.transform.position, Constants.scaleSmall);
        centerBig = Constants.GetPosition(bigModel.transform.position, Constants.scaleBig);

        newSphere = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newSphere.name = objectIndex + "_Cube";
        newSphere.tag = "Small";
        newSphere.transform.position = centerSmall;
        newSphere.transform.localScale = new Vector3(0, 0, 0);
        // radius = newSphere.GetComponent<SphereCollider>().radius;

        // var renderer = newSphere.GetComponent<Renderer>();
        // renderer.sharedMaterial = new Material(Shader.Find("Default/Diffuse")) // ("Sprites/Default")
        // {
        //     color = randomColor()
        // };

        newSphereBig = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newSphereBig.name = objectIndex + "_Cube";
        newSphereBig.tag = "Big";
        newSphereBig.transform.position = centerBig;
        newSphereBig.transform.localScale = new Vector3(0, 0, 0);
        // radius = newSphere.GetComponent<SphereCollider>().radius;

        // var rendererBig = newSphere.GetComponent<Renderer>();
        // rendererBig.sharedMaterial = new Material(Shader.Find("Default/Diffuse")) // ("Sprites/Default")
        // {
        //     color = randomColor()
        // };
    }

    void DrawContinous()
    {
        // Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), GetMousePosition(), Color.red);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            var radius = GetRadius(centerSmall, Constants.GetPosition(smallModel.transform.position, Constants.scaleSmall));

            newSphere.GetComponent<BoxCollider>().size = new Vector3(0.1f, 0.1f, 0.1f);//TODO set distance
            newSphere.transform.localScale = new Vector3(radius, radius, radius);

            newSphereBig.GetComponent<BoxCollider>().size = new Vector3(0.1f, 0.1f, 0.1f);//TODO set distance
            newSphereBig.transform.localScale = new Vector3(radius * Constants.scaleBig, radius * Constants.scaleBig, radius * Constants.scaleBig);

            timer = timerDelay;
        }
    }
}
