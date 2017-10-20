using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    public float speed;
    GameObject target;
    GameObject holder;
    public GameObject[] paths;
    public bool move = true;

    public GameObject leader;
    public int numBoids;
    public List<GameObject> units = new List<GameObject>();
    public ScalableFormation sf;
    public GameObject currentBoid;
    public bool moveNext = true;
    public int index = 5;
    public int counter = -1;
    public bool moveThisBoid = false;

    public bool BoidsAreMoving = false;
    public bool allDone = false;




    // Use this for initialization
    void Start()
    {
        leader = GameObject.Find("GameObject");
        sf = leader.GetComponent<ScalableFormation>();
        numBoids = leader.GetComponent<ScalableFormation>().numUnits;
        units = leader.GetComponent<ScalableFormation>().units;
    }

    // Update is called once per frame
    void Update() {
        numBoids = leader.GetComponent<ScalableFormation>().numUnits;
        holder = findTarget();
        if (move) {
            if (holder != null) {
                Debug.Log(holder);
                target = holder;
                transform.Translate(Vector3.right * Time.deltaTime * 2);
            }

            //calculate the angle
            if (target != null) {
                turnTowards(target);
            }
        }
        else if (moveThisBoid && holder != paths[11]) {
            Vector3 vectorToTarget = holder.transform.position - currentBoid.transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            units[index].transform.rotation = Quaternion.Slerp(currentBoid.transform.rotation, q, Time.deltaTime * 5f);
            currentBoid.transform.Translate(Vector3.right * Time.deltaTime * 2);
        }
        else if (moveThisBoid && holder == paths[11] && counter != numBoids) { 
            Vector3 center = leader.transform.position;
            int a = 360 / numBoids * index;
            Vector3 pos = RandomCircle(center, ((float)numBoids) / 8, a);
            currentBoid.transform.position = pos;
            currentBoid.transform.eulerAngles = leader.transform.eulerAngles;
            resetPath(5, 12);
            if(index == numBoids-1) {
                index = 0;
            }
            else {
                index++;
            }
            counter++;
            moveNextBoid();
        }
        else if (counter == numBoids) {
            Vector3 center = leader.transform.position;
            int a = 360 / numBoids * index;
            Vector3 pos = RandomCircle(center, ((float)numBoids) / 8, a);
            currentBoid.transform.position = pos;
            currentBoid.transform.eulerAngles = leader.transform.eulerAngles;
            if (index == numBoids - 1) {
                index = 0;
            }
            else {
                index++;
            }
        }
        if (counter == numBoids + 1) {
            allDone = true;
        }
        if (allDone) {
            sf.moveBoids = true;
            move = true;
        }
        else if (holder == paths[5]) {
            sf.moveBoids = false;
        }
        else if (holder == paths[11] && move) {
            move = false;
            resetPath(5, 12);
            moveNextBoid();
            units[index].transform.Translate(Vector3.right * Time.deltaTime * 2);
        }
    }

    public GameObject findTarget()
    {
        int i;
        for(i = 0; i < paths.Length; i++){
            if(paths[i].GetComponent<PathScript>().currentlyFalse() == true)
            {
                return paths[i];
            }
        }
        return null;
    }

    public void moveNextBoid() {
        GameObject holder = findTarget();
        BoidsAreMoving = true;
        if (counter == numBoids) {
            sf.moveBoids = true;
            move = true;
            BoidsAreMoving = false;
            paths[5].GetComponent<PathScript>().found = true;
            paths[6].GetComponent<PathScript>().found = true;
            paths[7].GetComponent<PathScript>().found = true;
            paths[8].GetComponent<PathScript>().found = true;
            paths[9].GetComponent<PathScript>().found = true;
            paths[10].GetComponent<PathScript>().found = true;
            paths[11].GetComponent<PathScript>().found = true;

        }
        else if (moveNext) {
            Vector3 vectorToTarget = holder.transform.position - units[index].transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            units[index].transform.rotation = Quaternion.Slerp(units[index].transform.rotation, q, Time.deltaTime * 5f);
            units[index].transform.Translate(Vector3.right * Time.deltaTime * 2);
            currentBoid = units[index];
            moveThisBoid = true;
        }
    }

    public void resetPath(int start, int end) {
        for (int i = start; i <= end; ++i) {
            paths[i].GetComponent<PathScript>().found = false;
        }
    }

    public void turnTowards(GameObject target) {
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 3f);
    }

    Vector3 RandomCircle(Vector3 center, float radius, int a) {
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}
