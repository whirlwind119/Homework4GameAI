using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leader : MonoBehaviour {

    public float speed;
    GameObject target;
    GameObject holder;
    public GameObject[] paths;
    public int mode;



    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        holder = findTarget();
        if (holder != null) {
            target = holder;
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }

        //calculate the angle
        if (target != null) {
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 3f);
        }
    }

    public GameObject findTarget() {
        int i;
        for (i = 0; i < paths.Length; i++) {
            if (paths[i].GetComponent<PathScript>().currentlyFalse() == true) {
                return paths[i];
            }
        }
        return null;
    }
}
