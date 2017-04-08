using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSplitter : MonoBehaviour {
    public bool splitOnStart = false;

	// Use this for initialization
	void Start () {
        if(splitOnStart) Split();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Split()
    {
        Color originalColor = Color.white;
        Renderer originalRend = this.GetComponent<Renderer>();
        if (originalRend != null)
        {
            originalColor = originalRend.material.color;
        }



        //0-R, 1-B, 2-G
        string colorName = "";
        ColorGroup group = ColorGroup.RED;
        for (int i = 0; i<3; i++)
        {
            Color splitColor = originalColor;
            switch (i)
            {
                case 0:
                    colorName = "Red";
                    if (originalColor.r == 0) continue;
                    splitColor.b = 0;
                    splitColor.g = 0;
                    group = ColorGroup.RED;
                    break;
                case 1:
                    colorName = "Green";
                    if (originalColor.g == 0) continue;
                    splitColor.b = 0;
                    splitColor.r = 0;
                    group = ColorGroup.GREEN;
                    break;
                case 2:
                    colorName = "Blue";
                    if (originalColor.b == 0) continue;
                    splitColor.r = 0;
                    splitColor.g = 0;
                    group = ColorGroup.BLUE;
                    break;
            }
            GameObject fragment = CreateFragment(splitColor, LayerMask.NameToLayer(colorName), colorName);

            ConvergenceManager.Instance.RegisterObject(fragment, group);

        }

        originalRend.enabled = false;
    }

    GameObject CreateFragment(Color color, int layer, string name)
    {
        GameObject fragment = GameObject.Instantiate(this.gameObject);
        Renderer rend = fragment.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material.color = color;
        }
        fragment.name = fragment.name + " " + name;
        fragment.layer = layer;

        ColorSplitter splitter = fragment.GetComponent<ColorSplitter>();
        splitter.enabled = false;
        return fragment;

    }
}
