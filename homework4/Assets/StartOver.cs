using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOver : MonoBehaviour {

    public float velocity;
    public GameObject Leader;
    public float offset1x;
    public float offset1y;
    Vector3 goalPos = Vector3.zero;

    // Use this for initialization
    void Start () {
        Leader = GameObject.Find("leader");
    }
	
	// Update is called once per frame
	void Update () {
        goalPos = Leader.transform.position;
        goalPos.x += offset1x;
        goalPos.y += offset1y;
        Vector3 vectorToTarget = goalPos - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, q, Time.deltaTime * 10f);
        transform.Translate(Vector3.right * Time.deltaTime * velocity);

    }

    void FormationOne()
    {

    }
}
