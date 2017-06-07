﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pose_shiko : MonoBehaviour
{
    PlayerStatus playerstatus;

    /*ポーズ_Hの判定を行う*/
    //「H」ポーズの画像を所得
    private Image pause_shiko;
    private float r, g, b, alpha;

    //falseならガイド画像を表示していない、trueなら画像を
    public bool imageDisplay = false;

    //ポーズの各腕、足がそれぞれ指定された範囲内に入っているか
    //falseが入ってない、trueが入ってる
    public bool R_arm_flag = false;
    public bool R_leg_flag = false;
    public bool L_arm_flag = false;
    public bool L_leg_flag = false;

    //成功したポーズの判定で使う
    public string Pausename = "pause_shiko";

    /*現在の角度*/
    public float R_sholder;
    public float R_elbow;
    public float R_crotch;
    public float R_knee;
    public float L_shoulder;
    public float L_elbow;
    public float L_crotch;
    public float L_knee;
    /**/

    /*それぞれの手足ごとの判定の数値の中心*/
    //右肩の判定の基本となる数字
    public float R_sholder_center;
    //Pがプラス、Mがマイナス
    private float R_sholdeP, R_sholdeM;
    //右肘
    public float R_elbow_center;
    private float R_elbowP, R_elbowM;
    //右股
    public float R_crotch_center;
    private float R_crotchP, R_crotchM;
    //右膝
    public float R_knee_center;
    private float R_kneeP, R_kneeM;
    //左肩
    public float L_shoulder_center;
    private float L_shoulderP, L_shoulderM;
    //左肘
    public float L_elbow_center;
    private float L_elbowP, L_elbowM;
    //左股
    public float L_crotch_center;
    private float RsholdeP, RsholdeM;
    //左膝
    public float L_knee_center;
    private float L_kneeP, L_kneeM;
    /************************************/

    void Start()
    {
        //ポーズガイドの画像
        pause_shiko = gameObject.GetComponent<Image>();
        r = pause_shiko.GetComponent<Image>().color.r;
        g = pause_shiko.GetComponent<Image>().color.g;
        b = pause_shiko.GetComponent<Image>().color.b;
        alpha = pause_shiko.GetComponent<Image>().color.a;

        //右肩
        R_sholdeP = R_sholder + 20.0f;
        R_sholdeM = R_sholder - 20.0f;
        //右ひじ
        R_elbowP = R_elbow + 20.0f;
        R_elbowM = R_elbow - 20.0f;
        //右股   
        R_crotchP = R_crotch + 20.0f;
           R_crotchM= R_crotch - 20.0f;
        //右膝
        R_kneeP =R_knee + 20.0f;
            R_kneeM= R_knee - 20.0f;
        //左肩
        L_shoulderP = L_shoulder + 20.0f;
                  L_shoulderM = L_shoulder - 20.0f;
        //左肘
        L_elbowP = L_shoulder + 20.0f;
        L_elbowM = L_shoulder - 20.0f;
        //左股
        RsholdeP
                RsholdeM
        //左膝

        L_kneeP
            L_kneeM


        ShikoPoseDisplayfalse();
    }


    void Update()
    {
        R_sholder    = playerstatus.R_shoulder_Y;
        R_elbow      = playerstatus.R_elbow_Y;
        R_crotch     = playerstatus.R_crotch_Y;
        R_knee       = playerstatus.R_knee_Y;
        L_shoulder   = playerstatus.L_shoulder_Y;
        L_elbow      = playerstatus.L_elbow_Y;
        L_crotch     = playerstatus.L_crotch_Y;
        L_knee       = playerstatus.L_knee_Y;



    pause_shiko.GetComponent<Image>().color = new Color(r, g, b, alpha);

        transform.position = new Vector3(playerstatus.P_pos.position.x, 4, playerstatus.P_pos.position.z);

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
        if (R_sholder_center >= 40 && R_sholder_center <= 80)
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
        if (R_crotch_Y >= 70 && R_crotch_Y <= 110)
        {
            //右膝
            if (R_knee_Y >= 70 && R_knee_Y <= 110)
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
        if (L_shoulder_Y >= 280 && L_shoulder_Y <= 320)
        {
            //左肘
            if (L_elbow_Y >= 90 && L_elbow_Y <= 130)
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
        if (L_crotch_Y >= 260 && L_crotch_Y <= 280)
        {
            //左膝
            if (L_knee_Y >= 260 && L_knee_Y <= 280)
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