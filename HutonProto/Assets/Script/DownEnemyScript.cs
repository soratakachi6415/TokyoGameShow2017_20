using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownEnemyScript : MonoBehaviour
{
    public bool getUp_upEnemy;

    // Use this for initialization
    void Start()
    {
        getUp_upEnemy = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player" && !getUp_upEnemy)
        {
            GameObject.Find("ScriptController").GetComponent<UdEnemyManage>().downHit();
        }
    }
}
