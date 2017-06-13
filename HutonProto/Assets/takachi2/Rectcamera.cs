using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectcamera : MonoBehaviour
{
    public Camera camera_;
    public Scene_manager currentsce;
    public float rectX_ = 0.2f;
    public float rectY_ = 0.2f;
    public float rectW_ = 0.5f;
    public float rectH_ = 0.5f;

    // Use this for initialization
    void Start ()
    {
        currentsce = GameObject.FindGameObjectWithTag("Scenemanager").GetComponent<Scene_manager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (currentsce.currentscene == "Result")
        {
            camera_.rect = Camera.main.rect;
            camera_.rect = new Rect(rectX_, rectY_, rectW_, rectH_);
        }
    }
}
