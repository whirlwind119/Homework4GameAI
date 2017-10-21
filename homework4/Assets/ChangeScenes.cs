using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour {

    float native_width = 1920;
    float native_height = 1080;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI() {
        float rx = Screen.width / native_width;
        float ry = Screen.height / native_height;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(rx, ry, 1));

        if (GUI.Button(new Rect((native_width / 2) - 100, (native_height / 2) - 450, 150, 100), "Scalable")) {
            Debug.Log("scalable");
            SceneManager.LoadScene("scene1");
        }
        if (GUI.Button(new Rect((native_width / 2) + 100, (native_height / 2) - 450, 150, 100), "Emergent")) {
            Debug.Log("emergent");
            SceneManager.LoadScene("Emergent");
        }
        if (GUI.Button(new Rect((native_width / 2) + 300, (native_height / 2) - 450, 150, 100), "Two Level")) {
            Debug.Log("Two");
            SceneManager.LoadScene("Another Scene");
        }
    }
}
