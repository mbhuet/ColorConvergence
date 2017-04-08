using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorGroup
{
    RED,
    GREEN,
    BLUE,
}

public class ConvergenceManager : MonoBehaviour {

    public static ConvergenceManager Instance;
    List<GameObject> redObjects;
    List<GameObject> greenObjects;
    List<GameObject> blueObjects;

    private void Awake()
    {
       if (Instance == null) Instance = this;
        else GameObject.Destroy(this.gameObject);

        redObjects = new List<GameObject>();
        greenObjects = new List<GameObject>();
        blueObjects = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            TranslateObjects(redObjects, Vector3.right * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            TranslateObjects(redObjects, Vector3.left * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            TranslateObjects(greenObjects, Vector3.up * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            TranslateObjects(greenObjects, Vector3.down * Time.deltaTime);
        }
    }

    void TranslateObjects(List<GameObject> objs, Vector3 translation)
    {
        foreach(GameObject obj in objs)
        {
            obj.transform.Translate(translation);
        }
    }

    public void TranslateColor(ColorGroup color, Vector3 translation)
    {
        switch (color)
        {
            case ColorGroup.RED:
                TranslateObjects(redObjects, translation);
                break;
            case ColorGroup.BLUE:
                TranslateObjects(blueObjects, translation);
                break;
            case ColorGroup.GREEN:
                TranslateObjects(greenObjects, translation);
                break;
        }
    }

    public void RegisterObject(GameObject obj, ColorGroup color)
    {
        switch (color)
        {
            case ColorGroup.RED:
                redObjects.Add(obj);
                break;
            case ColorGroup.GREEN:
                greenObjects.Add(obj);
                break;
            case ColorGroup.BLUE:
                blueObjects.Add(obj);
                break;
        }
    }

}
