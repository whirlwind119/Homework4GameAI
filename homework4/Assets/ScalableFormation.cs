using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalableFormation : MonoBehaviour {

    public int numUnits = 12;
    public GameObject prefab;
    public List<GameObject> units = new List<GameObject>();
    public List<GameObject> toDestroy = new List<GameObject>();
    public bool moveBoids = true;




    void Start() {
        Vector3 center = transform.position;
        for (int i = 0; i < numUnits; i++) {
            int a = 360 / numUnits * i;
            Vector3 pos = RandomCircle(center, numUnits/8, a);
            units.Add((GameObject)Instantiate(prefab, pos, Quaternion.identity));
        }
        units[0].GetComponent<SpriteRenderer>().color = Color.red;
        units[1].GetComponent<SpriteRenderer>().color = Color.green;
    }
    Vector3 RandomCircle(Vector3 center, float radius, int a) {
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    void Update() {
        if (moveBoids) {
            Vector3 center = transform.position;
            for (int i = 0; i < numUnits; i++) {
                int a = 360 / numUnits * i;
                Vector3 pos = RandomCircle(center, ((float)numUnits)/8, a);
                units[i].transform.position = pos;
                units[i].transform.eulerAngles = transform.eulerAngles;
            }
        }
        foreach(GameObject unit in toDestroy) {
            Destroy(unit.gameObject);
            units.Remove(unit);
        }
    }
}
