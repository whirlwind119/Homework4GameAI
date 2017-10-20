using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergentFormation : MonoBehaviour {

    public GameObject[] units;
    public GameObject unitPrefab;
    public int numUnits = 3;
    public Vector3 range = new Vector3(5, 5, 5);

    // Use this for initialization
    void Start () {
        units = new GameObject[numUnits];

        for(int i = 0; i < numUnits; ++i) {
            units[i] = Instantiate(unitPrefab);
        }


    }
	
	// Update is called once per frame
	void Update () {

    }
}
