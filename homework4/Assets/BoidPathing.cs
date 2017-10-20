using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidPathing : MonoBehaviour {

    public GameObject leader;
    public ScalableFormation sf;
    public float speed;
    GameObject target;
    GameObject holder;
    public GameObject[] paths;
    public bool shouldFindPath = false;
    public bool move = true;




    // Use this for initialization
    void Start() {
        leader = GameObject.Find("GameObject");
        sf = GetComponent<ScalableFormation>();
    }

    // Update is called once per frame
    void Update() {

        holder = findTarget();


        if (holder == paths[11]) {
            shouldFindPath = false;
            turnTowards(holder);
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (move) {
            if (holder != null) {
                target = holder;
                transform.Translate(Vector3.right * Time.deltaTime * 2);
            }

            //calculate the angle
            if (target != null) {
                turnTowards(target);
            }
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

    public void turnTowards(GameObject target) {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 3f);
    }
}
