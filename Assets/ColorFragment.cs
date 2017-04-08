using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TagSearchZone))]
public class ColorFragment : MonoBehaviour {
    public ColorGroup color;
    TagSearchZone fragmentSearch;

    List<Collider2D> otherFragments;
    Collider2D collider2D;

    public float convTest = 0;

	// Use this for initialization
	void Start () {
        RegisterSelf();
        otherFragments = new List<Collider2D>();
        collider2D = this.GetComponent<Collider2D>();
        fragmentSearch = GetComponent<TagSearchZone>();
        fragmentSearch.TagEnter += FragmentEnter;
        fragmentSearch.TagExit += FragmentExit;
	}
	
	// Update is called once per frame
	void Update () {
        convTest = PercentConvergence();
	}

    void RegisterSelf()
    {
        ConvergenceManager.Instance.RegisterObject(this.gameObject, color);
    }

    float PercentConvergence()
    {
        Bounds convergenceBounds = new Bounds();
        Bounds myBounds = collider2D.bounds;
        convergenceBounds.Encapsulate(myBounds);

        foreach(Collider2D col in otherFragments)
        {
            convergenceBounds.Encapsulate(col.bounds);
        }

        return (myBounds.size.x * myBounds.size.y) / (convergenceBounds.size.x * convergenceBounds.size.y);

    }

    void FragmentEnter(Collider2D col)
    {
        //SHOULD report convergence to Convergence Manager
        if(!otherFragments.Contains(col)) otherFragments.Add(col);
    }

    void FragmentExit(Collider2D col)
    {
        if (otherFragments.Contains(col)) otherFragments.Remove(col);
    }
}
