using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTest : MonoBehaviour {
    public bool moveRight;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (moveRight)
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TriggerEnter");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("TriggerExit");
    }
}
