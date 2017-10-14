using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocker : MonoBehaviour {

    public GameObject Leader;
    GameObject[] MotherFlockers;
    public float speed;
    public float neighborDistance;
    Vector2 vcentre = Vector2.zero;
    Vector2 vavoid = Vector2.zero;
    int groupSize = 0;
    float dist;


    // Use this for initialization
    void Start () { 
        leader Flockers = Leader.GetComponent<leader>();
        MotherFlockers = Flockers.allFlockers;
    }
	
	// Update is called once per frame
	void Update () {
            vcentre = Vector2.zero;
            vavoid = Vector2.zero;
            foreach (GameObject Flocker in MotherFlockers)
            {
                if (Flocker != this.gameObject)
                {
                    dist = Vector2.Distance(Flocker.transform.position, this.transform.position);
                    if (dist <= neighborDistance)
                    {
                        if (Flocker.tag == this.tag)
                        {
                            Vector2 adder = Flocker.transform.position;
                            vcentre += adder;
                            groupSize++;
                        }
                        else if (dist < 1.5f)
                        {
                            Vector2 enemy = this.transform.position - Flocker.transform.position;
                            vavoid += enemy;
                        }

                        if (dist < 1.5f)
                        {
                            Vector2 subb = this.transform.position - Flocker.transform.position;
                            vavoid += subb;
                        }
                    }
                }
            }
            vcentre /= groupSize;
            vavoid *= 1.5f;
            Vector2 place = Leader.transform.position - this.transform.position;
            Vector2 vectorToTarget = place + vavoid + vcentre;

            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, q, Time.deltaTime * 3);
            transform.Translate(Vector3.right * Time.deltaTime * speed);

        
    }
}
