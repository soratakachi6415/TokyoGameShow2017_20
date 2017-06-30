using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadObject : MonoBehaviour
{
    /*佐伯:追加*/
    Scene_manager scenenmanager_;
    public string curentscene;
    /**********************/

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        scenenmanager_ = GameObject.FindGameObjectWithTag("Scenemanager").GetComponent<Scene_manager>();
    }

    // Update is called once per frame
    void Update()
    {
        curentscene = scenenmanager_.currentscene;
    }
}