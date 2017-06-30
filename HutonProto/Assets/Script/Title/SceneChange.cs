using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour {

    public Scene_manager scenemanager_;

	void Start () {
        scenemanager_ = GameObject.FindGameObjectWithTag("Scenemanager").GetComponent<Scene_manager>();        	
	}
	
	void Update () {
	}

    public void onNextscene()
    {
        scenemanager_.NextScene();
    }
}
