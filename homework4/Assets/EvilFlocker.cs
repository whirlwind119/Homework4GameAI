﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilFlocker : MonoBehaviour {

    public float speed;

    private GameObject[] units;
    GameObject spawner;
    ScalableFormation script;


    // Use this for initialization
    void Start () {
        spawner = GameObject.Find("GameObject");
        script = spawner.GetComponent<ScalableFormation>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime,transform.position.y);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }


    }

    private void OnCollisionEnter2D(Collision2D c) {
        if (c.transform.tag == "boid") {
            script.numUnits -= 1;
            script.toDestroy.Add(c.gameObject);
        }

    }
}
