using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathScript : MonoBehaviour {

    public bool found = false;
    public GameObject leader;

	// Use this for initialization
	void Start () {
        leader = GameObject.Find("GameObject");
	}
	
	// Update is called once per frame
	void Update () { 
	}

    public bool currentlyFalse()
    {
        if (found == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void changeStatus()
    {
        found = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject.tag == "leader") {
                this.changeStatus();
            }
    }

}
