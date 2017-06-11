﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pose_shiko : MonoBehaviour
{
    //0612 変更完了

    //プレイヤーの角度参照
    PlayerStatus playerstatus;
    /*ポーズ_shikoの判定を行う*/
    //「shiko」ポーズの画像を所得
    protected Image pause_shiko;
    protected float r, g, b, alpha;
    //角度の誤差の数値
    public float anglePM;       

    /****現在の角度******/
    public float R_shoulder;
    public float R_elbow;
    public float R_crotch;
    public float R_knee;
    public float L_shoulder;
    public float L_elbow;
    public float L_crotch;
    public float L_knee;
    /********************/

    /*それぞれの手足ごとの判定の数値の中心*/
    //右肩の判定の基本となる数字
    public float R_shoulder_center;
    //Pがプラス、Mがマイナス
    protected float R_shoulderP, R_shoulderM;
    //右肘
    public float R_elbow_center;
    protected float R_elbowP, R_elbowM;
    //右股
    public float R_crotch_center;
    protected float R_crotchP, R_crotchM;
    //右膝
    public float R_knee_center;
    protected float R_kneeP, R_kneeM;
    //左肩
    public float L_shoulder_center;
    protected float L_shoulderP, L_shoulderM;
    //左肘
    public float L_elbow_center;
    protected float L_elbowP, L_elbowM;
    //左股
    public float L_crotch_center;
    protected float RsholdeP, RsholdeM;
    //左膝
    public float L_knee_center;
    protected float L_kneeP, L_kneeM;
    /************************************/

    //falseならガイド画像を表示していない、trueなら画像を
    public bool imageDisplayflag = false;
    //成功したポーズの判定で使う
    public string Pausename = "pause_shiko";
    //ポーズが決まったか
    public bool DecidePose_Shiko;

    //ポーズの各腕、足がそれぞれ指定された範囲内に入っているか
    //falseが入ってない、trueが入ってる
    public bool R_arm_flag = false;
    public bool R_leg_flag = false;
    public bool L_arm_flag = false;
    public bool L_leg_flag = false;

  

    void Start()
    {
        //ポーズガイドの画像
        pause_shiko = gameObject.GetComponent<Image>();
        r = pause_shiko.GetComponent<Image>().color.r;
        g = pause_shiko.GetComponent<Image>().color.g;
        b = pause_shiko.GetComponent<Image>().color.b;
        alpha = pause_shiko.GetComponent<Image>().color.a;

        playerstatus=this.gameObject.GetComponent<PlayerStatus>();
        ShikoPoseDisplayfalse();
    }


    void Update()
    {
        R_shoulder = playerstatus.R_shoulder_Y;
        R_elbow = playerstatus.R_elbow_Y;
        R_crotch = playerstatus.R_crotch_Y;
        R_knee = playerstatus.R_knee_Y;
        L_shoulder = playerstatus.L_shoulder_Y;
        L_elbow = playerstatus.L_elbow_Y;
        L_crotch = playerstatus.L_crotch_Y;
        L_knee = playerstatus.L_knee_Y;

        /*角度の判定の上下許容範囲*/
        //右肩
        R_shoulderP = R_shoulder + anglePM;
        R_shoulderM = R_shoulder - anglePM;
        //右ひじ
        R_elbowP = R_elbow + anglePM;
        R_elbowM = R_elbow - anglePM;
        //右股   
        R_crotchP = R_crotch + anglePM;
        R_crotchM = R_crotch - anglePM;
        //右膝
        R_kneeP = R_knee + anglePM;
        R_kneeM = R_knee - anglePM;
        //左肩
        L_shoulderP = L_shoulder + anglePM;
        L_shoulderM = L_shoulder - anglePM;
        //左肘
        L_elbowP = L_shoulder + anglePM;
        L_elbowM = L_shoulder - anglePM;
        //左股
        L_shoulderP = L_shoulder + anglePM;
        L_shoulderM = L_shoulder - anglePM;
        //左膝
        L_kneeP = L_knee + anglePM;
        L_kneeM = L_knee - anglePM;
        /***************************************/

        //ポーズの画像の情報
        pause_shiko.GetComponent<Image>().color = new Color(r, g, b, alpha);
        //画像をプレイヤーの上、X、Yの調整
        transform.position = new Vector3(playerstatus.P_pos.position.x, 10, playerstatus.P_pos.position.z);

        AnglesCheck();

        if (R_arm_flag == true ||
             L_arm_flag == true ||
             R_leg_flag == true ||
             L_leg_flag == true)
        {
            imageDisplayflag = true;
            ShikoPoseDisplaytrue();
        }

        //どれも入っていなかったら画像を表示しない
        if (R_arm_flag == false &&
                 L_arm_flag == false &&
                 R_leg_flag == false &&
                 L_leg_flag == false)
        {
            imageDisplayflag = false;
            ShikoPoseDisplayfalse();
        }

        if (R_arm_flag == true &&
           L_arm_flag == true &&
           R_leg_flag == true &&
           L_leg_flag == true)
        {
            //ポーズが決まったか
            DecidePose_Shiko = true;
            ShikoPoseDisplaytrue();
        }
    }
        void AnglesCheck()
    {
        //右腕の判別
        //右肩の角度
        if (R_shoulder_center >= 40 && R_shoulder_center <= 80)
        {
            //右肘
            if (R_elbow_center >= -10 && R_elbow_center <= 30)
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
        if (R_crotch >=R_crotchM && R_crotch <= R_crotchP)
        {
            //右膝
            if (R_knee>= R_kneeM && R_knee <= R_kneeP)
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
        //左肩の角度
        if (L_shoulder_center >= 280 && L_shoulder <= 320)
        {
            //左肘
            if (L_shoulder >= 90 && L_shoulder <= 130)
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
        if (L_crotch >= 260 && L_crotch <= 280)
        {
            //左膝
            if (L_knee >= 260 && L_knee <= 280)
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
    public void ShikoPoseDisplaytrue()
    {
        alpha = 1.0f;
    }

    //ポーズの画像を表示させない
    public void ShikoPoseDisplayfalse()
    {
        alpha = 0.0f;
    }
}