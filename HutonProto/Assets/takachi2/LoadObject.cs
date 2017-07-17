using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadObject : MonoBehaviour
{

    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Title")
        {
            SceneManager.sceneLoaded -= SceneLoaded;
            Destroy(gameObject);
        }
        else if (arg0.name == "Result")
        {
            Debug.Log("Result Scene!!!");

            ResultInit();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    unsafe void ResultInit()
    {

        HingeJoint[] joint = transform.GetComponentsInChildren<HingeJoint>();
        for (int i = 0; i < joint.Length; i++)
        {
            Destroy(joint[i]);
        }

        Rigidbody[] rig = transform.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rig.Length; i++)
        {
            //Debug.Log(r.gameObject.name);
            Destroy(rig[i]);
        }

        PlayerScript[] player = transform.GetComponentsInChildren<PlayerScript>();
        for (int i = 0; i < player.Length; i++)
        {
            Destroy(player[i]);
        }

        Touch_Point[] touch = transform.GetComponentsInChildren<Touch_Point>();
        for (int i = 0; i < touch.Length; i++)
        {
            Destroy(touch[i]);
        }

        Destroy(GetComponent<Animator>());
    }
}
