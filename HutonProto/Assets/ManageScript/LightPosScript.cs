using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightPosScript : MonoBehaviour {

    public GameObject gameObj;
    private GameObject directionLight;

    private float time;
    private float firstTime;

    public float lightUpTime_sec;


    Quaternion from;
    Quaternion to;

    // Use this for initialization
    void Start () {
        directionLight = GameObject.Find("Directional Light");
        directionLight.GetComponent<Light>().shadowStrength = 0.0f;
        directionLight.GetComponent<Light>().intensity = 0.2f;

        //光の初期角度と回転後の角度
        from = gameObj.transform.rotation;
        to = new Quaternion(gameObj.transform.rotation.x * 3, -gameObj.transform.rotation.y, gameObj.transform.rotation.z, gameObj.transform.rotation.w);

        firstTime = time = GameObject.Find("ScriptController").GetComponent<TimeScript>().time_sec;
    }

    // Update is called once per frame
    void Update () {
        if (0 <= time)
        {
            float i = 0;
            float j = 0;

            j = time / firstTime;

            //明るさ変化
            if (time <= lightUpTime_sec)
            {
                i = time / lightUpTime_sec;
                directionLight.GetComponent<Light>().shadowStrength = 1.01f - i;
                directionLight.GetComponent<Light>().intensity = 1.01f - (i * 0.8f);

                //光の角度変化
                gameObj.transform.rotation = Quaternion.Slerp(from, to, 1.01f - i);
            }
            //時間経過
            time -= Time.deltaTime;
        }
    }
}