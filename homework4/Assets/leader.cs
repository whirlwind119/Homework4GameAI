using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leader : MonoBehaviour {

    public float speed;
    GameObject target;
    GameObject holder;
    public GameObject[] paths;
    public int mode;
    public bool shouldFlock;

    private GameObject[] boids;
    private bool goingThroughTunnel = false;
    private bool leaderDoSomething = true;
    private bool closestFound = false;
    private GameObject closestBoid;



    // Use this for initialization
    void Start() {
        boids = GetComponentInChildren<GeneralManager>().units;
        shouldFlock = true;
        closestBoid = boids[0];
    }

    // Update is called once per frame
    void Update() {

        if (leaderDoSomething) {
            float mag = calculateMag(boids);
            holder = findTarget();
            if (goingThroughTunnel) {
                if (holder == paths[11]) {
                    boidFollowPath();
                    resetPath(6, 12);
                }
                else {
                    target = holder;
                    transform.Translate(Vector3.right * Time.deltaTime * speed);
                }
            }

            if (holder != null && !goingThroughTunnel) {
                target = holder;
                transform.Translate(Vector3.right * Time.deltaTime * speed);
            }

            //calculate the angle
            if (target != null) {
                turnTowards(target);
            }
        }
        else {
            boidFollowPath();
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

    public float calculateMag(GameObject[] asdf) {
        Vector3 center = Vector3.zero;
        foreach (GameObject boid in asdf) {
            center = center + boid.transform.position;
        }
        return (transform.position - center).magnitude;
    }

    public void goThroughTunnel() {
        foreach (GameObject boid in boids) {
            boid.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        goingThroughTunnel = true;
    }

    public void turnTowards(GameObject target) {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 3f);
    }
    
    public void boidFollowPath() {
        GameObject holder;
        GameObject[] boidsToGoThrough = GetComponentInChildren<GeneralManager>().units;
        leaderDoSomething = false;
        /*
        foreach (GameObject boid in boids) {
            holder = findTarget();
            boid.GetComponent<FlockScript>().turnTowards(holder);
            boid.transform.Translate(Vector3.right * Time.deltaTime * speed);
            if(holder == paths[10]) {
                leaderDoSomething = true;
                goingThroughTunnel = false;
                shouldFlock = true;
                paths[10].GetComponent<PathScript>().found = true;
            }
        }
        */
        holder = findTarget();
        if (!closestFound) {
            for (int i = 0; i < boids.Length; ++i) {
                Debug.Log((boids[i].transform.position - holder.transform.position).magnitude);
                if ((boids[i].transform.position - holder.transform.position).magnitude < (closestBoid.transform.position - holder.transform.position).magnitude) {
                    closestBoid = boids[i];
                }
            }
        }
        closestFound = true;
        closestBoid.GetComponent<FlockScript>().turnTowards(holder);
        closestBoid.transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
    
    public void resetPath(int start, int end) {
        for (int i = start; i <= end; ++i) {
            paths[i].GetComponent<PathScript>().found = false;
        }
    }
 
}
