using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pose_eiffelt : MonoBehaviour {
    /*ポーズ_Hの判定を行う*/
    //「H」ポーズの画像を所得
    private Image pause_eiffelt;
    private float r, g, b, alpha;

    /****体の関節指定****/
    //右肩の角度を所得する
    private GameObject R_shoulder;
    public float R_shoulder_Y;
    //右肘の角度を所得する
    private GameObject R_elbow;
    public float R_elbow_Y;
    //右股の角度を所得する
    private GameObject R_crotch;
    public float R_crotch_Y;
    //右膝の角度を所得する
    private GameObject R_knee;
    public float R_knee_Y;
    //左肩の角度を所得する
    private GameObject L_shoulder;
    public float L_shoulder_Y;
    //左肘の角度を所得する
    private GameObject L_elbow;
    public float L_elbow_Y;
    //左股の角度を所得する
    private GameObject L_crotch;
    public float L_crotch_Y;
    //左膝の角度を所得する
    private GameObject L_knee;
    public float L_knee_Y;
    /********************/


    //falseならガイド画像を表示していない、trueなら画像を
    public bool imageDisplay = false;

    //ポーズの各腕、足がそれぞれ指定された範囲内に入っているか
    //falseが入ってない、trueが入ってる
    public bool R_arm_flag = false;
    public bool R_leg_flag = false;
    public bool L_arm_flag = false;
    public bool L_leg_flag = false;

    /*プレイヤーの位置と角度を合わせる*/
    //プレイヤーの回転角度
    public float P_angle;
    //プレイヤーの位置
    public Transform P_pos;
    /**********************************/

    //成功したポーズの判定で使う
    public string Pausename = "Pause_eiffelt";

    void Start()
    {
        //ポーズガイドの画像
        pause_eiffelt = gameObject.GetComponent<Image>();
        r = pause_eiffelt.GetComponent<Image>().color.r;
        g = pause_eiffelt.GetComponent<Image>().color.g;
        b = pause_eiffelt.GetComponent<Image>().color.b;
        alpha = pause_eiffelt.GetComponent<Image>().color.a;

        //名前で検索して所得する
        R_shoulder = GameObject.Find("Player_RightHand1");
        R_elbow = GameObject.Find("Player_RightHand2");
        R_crotch = GameObject.Find("Player_RightLeg1");
        R_knee = GameObject.Find("Player_RightLeg2");
        L_shoulder = GameObject.Find("Player_LeftHand1");
        L_elbow = GameObject.Find("Player_LeftHand2");
        L_crotch = GameObject.Find("Player_LeftLeg1");
        L_knee = GameObject.Find("Player_LeftLeg2");

        P_pos = GameObject.Find("Player_Body").GetComponent<Transform>().transform;
        P_angle = GameObject.Find("Player_Body").GetComponent<Transform>().transform.eulerAngles.y;

        eiffelPoseDisplayfalse();
    }


    void Update()
    {
        pause_eiffelt.GetComponent<Image>().color = new Color(r, g, b, alpha);
        transform.position = new Vector3(P_pos.position.x, 0, P_pos.position.z);


        //各関節の現在の角度
        R_shoulder_Y = R_shoulder.transform.localEulerAngles.y;
        R_elbow_Y = R_elbow.transform.localEulerAngles.y;
        R_crotch_Y = R_crotch.transform.localEulerAngles.y;
        R_knee_Y = R_knee.transform.localEulerAngles.y;
        L_shoulder_Y = L_shoulder.transform.localEulerAngles.y;
        L_elbow_Y = L_elbow.transform.localEulerAngles.y;
        L_crotch_Y = L_crotch.transform.localEulerAngles.y;
        L_knee_Y = L_knee.transform.localEulerAngles.y;

        AnglesCheck();

        //どれかが判定の範囲内に入ったら画像表示
        if (R_arm_flag == true ||
            L_arm_flag == true ||
            R_leg_flag == true ||
            L_leg_flag == true)
        {
            imageDisplay = true;
        }

        //どれも入っていなかったら画像を表示しない
        if (R_arm_flag == false &&
                 L_arm_flag == false &&
                 R_leg_flag == false &&
                 L_leg_flag == false)
        {
            imageDisplay = false;
        }
    }
    void AnglesCheck()
    {
        //右腕の判別

        //右肩の角度
        if (R_shoulder_Y >= 180 && R_shoulder_Y <= 200)
        {
            //右肘
            if (R_elbow_Y >= -10 && R_elbow_Y <= 10)
            {
                R_arm_flag = true;
            }
            else
            {
                R_arm_flag = false;
            }
        }
        else
        {
            R_arm_flag = false;
        }

        //右足
        //右股の角度
        if (R_crotch_Y >= 10 && R_crotch_Y <= 30)
        {
            //右膝
            if (R_knee_Y >= 0 && R_knee_Y <= 10)
            {
                R_leg_flag = true;
            }
            else
            {
                R_leg_flag = false;
            }
        }
        else
        {
            R_leg_flag = false;
        }

        //左側の判別
        //左腕の角度
        if (L_shoulder_Y <= 280 && L_shoulder_Y >= 260)
        {
            //左肘
            if (L_elbow_Y >= -10 && L_elbow_Y <= 10)
            {
                L_arm_flag = true;
            }
            else
            {
                L_arm_flag = false;
            }
        }
        else
        {
            L_arm_flag = false;
        }

        //左股の角度
        if (L_crotch_Y >= 330 && L_crotch_Y <= 350)
        {
            //左膝
            if (L_knee_Y >= 349 && L_knee_Y <= 359)
            {
                L_leg_flag = true;
            }
            else
            {
                L_leg_flag = false;
            }
        }
        else
        {
            L_leg_flag = false;
        }
    }

    //ポーズの画像を表示させる
    public void eiffelPoseDisplaytrue()
    {       
            alpha = 1.0f;        
    }
    //ポーズの画像を表示させない
    public void eiffelPoseDisplayfalse()
    {       
            alpha = 0.0f;       
    }
}
