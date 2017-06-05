using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour {

    public Scene_manager scenemanager_;


	// Use this for initialization
	void Start () {
        scenemanager_ = GameObject.FindGameObjectWithTag("Scenemanager").GetComponent<Scene_manager>();        	
	}
	
	// Update is called once per frame
	void Update () {      
	}

    public void onNextscene()
    {
        scenemanager_.NextScene();
    }
}
