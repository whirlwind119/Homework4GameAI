using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockScript : MonoBehaviour {

    public GameObject manager;
    public Vector2 location = Vector2.zero;
    public Vector2 velocity;
    public GameObject Leader;
    Vector2 goalPos = Vector2.zero;
    Vector2 currentForce;

	// Use this for initialization
	void Start () {
        velocity = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
        location = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
        Leader = GameObject.Find("leader");
    }

    Vector2 seek(Vector2 target) {
        return (target - location);
    }

    void applyForce(Vector2 f) {
        Vector3 force = new Vector3(f.x, f.y, 0);
        if(force.magnitude > manager.GetComponent<GeneralManager>().maxForce) {
            force = force.normalized;
            force *= manager.GetComponent<GeneralManager>().maxForce;
        }
        this.GetComponent<Rigidbody2D>().AddForce(force);

        if (this.GetComponent<Rigidbody2D>().velocity.magnitude > manager.GetComponent<GeneralManager>().maxVelocity) {
            this.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity.normalized;
            this.GetComponent<Rigidbody2D>().velocity *= manager.GetComponent<GeneralManager>().maxVelocity;
        }

        Debug.DrawRay(this.transform.position, force, Color.white);
    }

    Vector2 align() {
        float neightborDist = manager.GetComponent<GeneralManager>().neighborDistance;
        Vector2 sum = Vector2.zero;
        int count = 0;
        foreach (GameObject other in manager.GetComponent<GeneralManager>().units) {
            if (other != this.gameObject) {
                float d = Vector2.Distance(location, other.GetComponent<FlockScript>().location);

                if (d < neightborDist) {
                    sum += other.GetComponent<FlockScript>().velocity;
                    count++;
                }
            }
        }
        if (count > 0) {
            sum /= count;
            Vector2 steer = sum - velocity;
            return steer;
        }
        return Vector2.zero;
    }

    Vector2 cohesion() {
        float neightborDist = manager.GetComponent<GeneralManager>().neighborDistance;
        Vector2 sum = Vector2.zero;
        int count = 0;
        foreach (GameObject other in manager.GetComponent<GeneralManager>().units) {
            if (other != this.gameObject) {
                float d = Vector2.Distance(location, other.GetComponent<FlockScript>().location);

                if (d < neightborDist) {
                    sum += other.GetComponent<FlockScript>().location;
                    count++;
                }
            }
        }
        if (count > 0) {
            sum /= count;
            return seek(sum);
        }
        return Vector2.zero;

    }

    void flock() {
        location = this.transform.position;
        velocity = this.GetComponent<Rigidbody2D>().velocity;

        if (manager.GetComponent<GeneralManager>().obedient) {
            Vector2 ali = align();
            Vector2 coh = cohesion();
            Vector2 gl;
            if (manager.GetComponent<GeneralManager>().seekGoal) {
                gl = seek(goalPos);
                currentForce = gl + ali + coh;
            }
            else {
                currentForce = ali + coh;
            }
            currentForce = currentForce.normalized;
        }
        if (manager.GetComponent<GeneralManager>().willful && Random.Range(0, 50) <=1) {
            if (Random.Range(0, 50) < 1) {
                currentForce = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
            }
        }


        applyForce(currentForce*100f);
    }
	
	// Update is called once per frame
	void Update () {
        if (Leader.GetComponent<leader>().shouldFlock) {
            flock();
            turnTowards(Leader);
        }
        goalPos = manager.transform.position;
    }

    public void turnTowards(GameObject target) {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 3f);
    }
}
