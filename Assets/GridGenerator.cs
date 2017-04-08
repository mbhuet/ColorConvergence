using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour {
    public float width;
    public float height;

    public int rows;
    public int cols;
    public float maxVertexDistance;
    public bool centered;

	// Use this for initialization
	void Start () {
        CreateGrid();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateGrid()
    {
        float x_offset = 0;
        float y_offset = 0;

        if (centered)
        {
            x_offset = -width / 2f;
            y_offset = -height / 2f;
        }

        int numSegmentsPerRow = (int)(width / maxVertexDistance) + (width % maxVertexDistance > 0 ? 1 : 0);
        int numPointsPerRow = numSegmentsPerRow + 1;

        for (int r = 0; r<rows; r++)
        {
            GameObject lineObj = new GameObject();
            LineHandler lineHandler = lineObj.AddComponent<LineHandler>();
            lineHandler.name = "Row " + r;

            float y = height / (rows - 1) * r;
            Vector3[] points = new Vector3[numPointsPerRow];
            for (int i = 0; i < numPointsPerRow; i++)
            {
                float x = i * width / numSegmentsPerRow;
                points[i] = new Vector3(x + x_offset , y +y_offset, 0);
            }
            lineHandler.SetPoints(points);
            ColorSplitter splitter = lineHandler.gameObject.AddComponent<ColorSplitter>();
            splitter.Split();

        }

        int numSegmentsPerCol = (int)(height / maxVertexDistance)+(height%maxVertexDistance>0 ? 1: 0);
        int numPointsPerCol = numSegmentsPerCol+1;

        for (int c = 0; c<cols; c++)
        {
            GameObject lineObj = new GameObject();
            LineHandler lineHandler = lineObj.AddComponent<LineHandler>();
            lineHandler.name = "Col " + c;

            float x = width/(cols-1) * c;
            Vector3[] points = new Vector3[numPointsPerCol];
            for (int i = 0; i < numPointsPerCol; i++)
            {
                float y = i * height/numSegmentsPerCol;
                points[i] = new Vector3(x + x_offset, y + y_offset, 0);
            }
            lineHandler.SetPoints(points);
            ColorSplitter splitter = lineHandler.gameObject.AddComponent<ColorSplitter>();
            splitter.Split();
        }
    }
}
