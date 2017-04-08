using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHandler : MonoBehaviour {

    LineRenderer line;
    EdgeCollider2D edgeCollider;
    bool initialized = false;

	// Use this for initialization
	void Start () {
        if (!initialized) Initialize();
	}

    void Initialize()
    {
        line = GetComponent<LineRenderer>();
        if (line == null)
        {
            line = gameObject.AddComponent<LineRenderer>();
            Material mat = Resources.Load("Additive", typeof(Material)) as Material;
            line.material = mat;
            line.material.color = Color.white;
            line.startWidth = .1f;
            line.endWidth = .1f;
            line.numCapVertices = 1;
            line.useWorldSpace = false;
        }
        

        edgeCollider = GetComponent<EdgeCollider2D>();
        if (edgeCollider == null)
        {
            edgeCollider = gameObject.AddComponent<EdgeCollider2D>();
        }
        initialized = true;
    }
	

    public void SetPoints(Vector3[] points)
    {
        if (!initialized) Initialize();
        Vector2[] points2D = new Vector2[points.Length];
        for(int i = 0; i<points.Length; i++)
        {
            points2D[i] = (Vector2)points[i];
        }

        if(line.numPositions != points.Length)
        {
            line.numPositions = points.Length;
        }
            line.SetPositions(points);
 
       
            edgeCollider.points = points2D;
        
    }
}
