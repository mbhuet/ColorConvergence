using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class SearchZone : MonoBehaviour
{
    public delegate void Trigger2DEvent(Collider2D col);
    public Trigger2DEvent OnTriggerEnter2DEvent;
    public Trigger2DEvent OnTriggerExit2DEvent;
    public Trigger2DEvent OnTriggerStay2DEvent;

    void OnTriggerEnter2D(Collider2D col) {
        if (OnTriggerEnter2DEvent != null) OnTriggerEnter2DEvent.Invoke(col);
    }
    void OnTriggerExit2D(Collider2D col) {
        if (OnTriggerExit2DEvent != null) OnTriggerExit2DEvent.Invoke(col);
    }
    void OnTriggerStay2D(Collider2D col) {    
        if (OnTriggerStay2DEvent != null) OnTriggerStay2DEvent.Invoke(col);
    }

    public ComponentSearchFilter<T> CreateFilter<T>()
    {
        ComponentSearchFilter<T> filter = new ComponentSearchFilter<T>();
        filter.Init();
        OnTriggerEnter2DEvent += filter.OnTriggerEnter2D;
        OnTriggerExit2DEvent += filter.OnTriggerExit2D;
        OnTriggerStay2DEvent += filter.OnTriggerStay2D;

        return filter; 
    }

}

//This separate class exists so I can use Generic T. 
public class ComponentSearchFilter<T> {

    public delegate void SearchEvent(T foundComponent);
    public SearchEvent OnSearchZoneEnter;
    public SearchEvent OnSearchZoneExit;
    List<T> componentsWithin;

    bool initialized = false;

	// Use this for initialization
	void Awake () {
        if (!initialized) Init();
	}

    public void Init()
    {
        componentsWithin = new List<T>();
        initialized = true;
    }


    // Update is called once per frame
    public void OnTriggerEnter2D (Collider2D col) {
        T component = col.GetComponent<T>();
        if(component != null && !componentsWithin.Contains(component))
        {
            componentsWithin.Add(component);
            if(OnSearchZoneEnter != null) OnSearchZoneEnter.Invoke(component);
        }
        
	}

   

    public void OnTriggerExit2D(Collider2D col)
    {
        T component = col.GetComponent<T>();
        if (component != null && componentsWithin.Contains(component))
        {
            componentsWithin.Remove(component);
            if (OnSearchZoneExit != null) OnSearchZoneExit.Invoke(component);

        }
    }

    public void OnTriggerStay2D(Collider2D col)
    {

    }
    

    

    public T[] AllComponentsWithin()
    {
        ClearNullEntries();
        return componentsWithin.ToArray();
    }

    void ClearNullEntries()
    {
        componentsWithin.RemoveAll(item => item == null);
    }
}
