using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    List<GameObject> obj = new List<GameObject>();
    private SleepGageScript sleepGauge;
    private bool hit;

    void Start()
    {
        hit = false;

        sleepGauge = GameObject.Find("ScriptController").GetComponent<SleepGageScript>();

        /*旧モデルプレイヤー*/
        //obj.Add(GameObject.Find("Player_LeftHand1"));     //0
        //obj.Add(GameObject.Find("Player_LeftHand2"));     //1
        //obj.Add(GameObject.Find("Player_RightHand1"));    //2
        //obj.Add(GameObject.Find("Player_RightHand2"));    //3
        //obj.Add(GameObject.Find("Player_LeftLeg1"));      //4
        //obj.Add(GameObject.Find("Player_LeftLeg2"));      //5
        //obj.Add(GameObject.Find("Player_RightLeg1"));     //6
        //obj.Add(GameObject.Find("Player_RightLeg2"));     //7
        //obj.Add(GameObject.Find("Player_Body"));          //8
        //obj.Add(GameObject.Find("Player_Head"));          //9

        /*新プレイヤーモデル*/
        obj.Add(GameObject.Find("mixamorig:LeftUpLeg"));    //0
        obj.Add(GameObject.Find("mixamorig:LeftLeg"));      //1
        obj.Add(GameObject.Find("mixamorig:RightUpLeg"));   //2
        obj.Add(GameObject.Find("mixamorig:RightLeg"));     //3
        obj.Add(GameObject.Find("mixamorig:LeftForeArm"));  //4
        obj.Add(GameObject.Find("mixamorig:LeftArm"));      //5
        obj.Add(GameObject.Find("mixamorig:RightForeArm")); //6
        obj.Add(GameObject.Find("mixamorig:RightArm"));     //7
        obj.Add(GameObject.Find("mixamorig:Spine"));        //8
        obj.Add(GameObject.Find("mixamorig:Head"));         //9
        obj.Add(GameObject.Find("mixamorig:Spine1"));       //10
        obj.Add(GameObject.Find("mixamorig:Spine2"));       //11
        
    }

    void Update()
    {

    }

    void OnMouseDown()
    {
        for(int i = 0;i < obj.Count; i++) 
        {
            obj[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        if (gameObject.name.ToString().Contains("Leg")) //触れたオブジェクト座標のY座標だけ固定する
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
        }
        else if (gameObject.name.ToString().Contains("Arm"))
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
        }
        //ボディとヘッドを固定
        obj[8].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        obj[9].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        obj[10].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
        obj[11].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;

        //体か頭を触ると全身の回転とY座標を固定。
        if (gameObject.name.ToString().Contains("Spine") || gameObject.name.ToString().Contains("Head"))
        {
            for (int i = 0; i < obj.Count; i++)
            {
                obj[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    void OnMouseUp()
    {
        //オブジェクトを離すとY固定のみへ
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
                //衝突時快眠ポイントを減らす
                sleepGauge.hitEnemy(true);
            }
            hit = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        hit = false;
    }
}
