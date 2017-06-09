using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    List<GameObject> obj = new List<GameObject>();
    private bool hit;

    void Start()
    {
        hit = false;

        obj.Add(GameObject.Find("Player_LeftHand1"));     //0
        obj.Add(GameObject.Find("Player_LeftHand2"));     //1
        obj.Add(GameObject.Find("Player_RightHand1"));    //2
        obj.Add(GameObject.Find("Player_RightHand2"));    //3
        obj.Add(GameObject.Find("Player_LeftLeg1"));      //4
        obj.Add(GameObject.Find("Player_LeftLeg2"));      //5
        obj.Add(GameObject.Find("Player_RightLeg1"));     //6
        obj.Add(GameObject.Find("Player_RightLeg2"));     //7
        obj.Add(GameObject.Find("Player_Body"));          //8
        obj.Add(GameObject.Find("Player_Head"));          //9
    }

    void Update()
    {

    }

    void OnMouseDown()
    {
        for (int i = 0; i < obj.Count; i++) //全てのオブジェクトの座標を固定
        {
            obj[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        if (gameObject.name.ToString().Contains("Hand")) //触れたオブジェクト座標のY座標だけ固定する
        {
            if (gameObject.name.ToString().Contains("Left"))
            {
                obj[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                obj[1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            }
            else if (gameObject.name.ToString().Contains("Right"))
            {
                obj[2].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                obj[3].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            }
            obj[8].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }
        else if (gameObject.name.ToString().Contains("Leg"))
        {
            if (gameObject.name.ToString().Contains("Left"))
            {
                obj[4].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                obj[5].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            }
            else if (gameObject.name.ToString().Contains("Right"))
            {
                obj[6].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                obj[7].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            }
            obj[8].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        }

        ////体か頭を触ると全身の回転とY座標を固定。
        if (gameObject.name.ToString().Contains("Body") || gameObject.name.ToString().Contains("Head"))
        {
            for (int i = 0; i < obj.Count; i++)
            {
                obj[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    void OnMouseUp()
    {
        for (int i = 0; i < obj.Count; i++)
        {
            if (obj[i].GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeRotation 
                || obj[i].GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeRotationY
                || obj[i].GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezeAll)
            {
                obj[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {

    }

    void OnCollisionStay(Collision other)
    {
        //敵との衝突判定
        if (other.collider.tag == "Enemy" || other.collider.tag == "Enemy2")
        {
            if (!hit)
            {
                //敵と衝突時にカメラ揺れ *仕様にはない
                Camera.main.gameObject.GetComponent<ShakeTest>().Shake();

                //衝突時"Z"を減らす
                GameObject.Find("ScriptController").GetComponent<SleepGageScript>().hitEnemy(true);
            }
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            hit = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        hit = false;
    }
}
