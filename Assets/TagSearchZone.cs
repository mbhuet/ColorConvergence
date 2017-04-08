using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class TagSearchZone : MonoBehaviour {
    Collider2D myCollider;
    public string[] searchTags;
    List<Collider2D> taggedCollidersWithin;


    public delegate void SearchEvent(Collider2D other);
    public SearchEvent TagEnter;
    public SearchEvent TagExit;
    public SearchEvent TagStay;

	// Use this for initialization
	void Awake () {
        taggedCollidersWithin = new List<Collider2D>();
        myCollider = GetComponent<Collider2D>();
        myCollider.isTrigger = true;

        TagStay += AddToTaggedColliders;
	}
	

    void LateUpdate()
    {
        taggedCollidersWithin.Clear();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (SearchingForTag(col.tag)){
           if(TagEnter != null) TagEnter.Invoke(col);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (SearchingForTag(col.tag)){
            if(TagExit != null) TagExit.Invoke(col);
        }
    }

    /*
    void OnTriggerStay2D(Collider2D col)
    {
        if (SearchingForTag(col.tag)){
            if(TagStay != null) TagStay.Invoke(col);
        }
    }
    */

    bool SearchingForTag(string queryTag)
    {
        foreach(string tag in searchTags)
        {
            if (tag == queryTag) return true;
        }
        return false;
    }

    void AddToTaggedColliders(Collider2D col)
    {
        if(!taggedCollidersWithin.Contains(col))
        taggedCollidersWithin.Add(col);
    }

    public Collider2D[] AllTaggedCollidersWithin()
    {
        ClearNullEntries();
        return taggedCollidersWithin.ToArray();
    }

    void ClearNullEntries()
    {
        taggedCollidersWithin.RemoveAll(item => item == null);
    }
}
