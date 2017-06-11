﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pose_Fight : MonoBehaviour {
    PlayerStatus playerstatus;

    /*ポーズ_Hの判定を行う*/
    //「H」ポーズの画像を所得
    private Image pause_fight;
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
    public string Pausename = "pause_fight";

    /****現在の角度******/
    private float R_sholder;
    private float R_elbow;
    private float R_crotch;
    private float R_knee;
    private float L_shoulder;
    private float L_elbow;
    private float L_crotch;
    private float L_knee;
    /********************/

    /*それぞれの手足ごとの判定の数値の中心*/
    //右肩の判定の基本となる数字
    public float R_sholder_center;
    //Pがプラス、Mがマイナス
    private float R_sholderP, R_sholderM;
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
    private float L_crotch_P, L_crotch_M;
    //左膝
    public float L_knee_center;
    private float L_kneeP, L_kneeM;
    /************************************/
    //ポーズが決まったか
    public bool DecidePose_Banana;

    void Start()
    {
        //ポーズガイドの画像
        pause_fight = gameObject.GetComponent<Image>();
        r = pause_fight.GetComponent<Image>().color.r;
        g = pause_fight.GetComponent<Image>().color.g;
        b = pause_fight.GetComponent<Image>().color.b;
        alpha = pause_fight.GetComponent<Image>().color.a;
        //プレイヤーの関節の角度など
        playerstatus = this.gameObject.GetComponent<PlayerStatus>();
        fightPoseDisplayfalse();
    }

    void Update()
    {
        //角度の獲得
        R_sholder = playerstatus.R_shoulder_Y;
        R_elbow = playerstatus.R_elbow_Y;
        R_crotch = playerstatus.R_crotch_Y;
        R_knee = playerstatus.R_knee_Y;
        L_shoulder = playerstatus.L_shoulder_Y;
        L_elbow = playerstatus.L_elbow_Y;
        L_crotch = playerstatus.L_crotch_Y;
        L_knee = playerstatus.L_knee_Y;

        /*角度の判定の上下許容範囲*/
        //右肩
        R_sholderP = R_sholder + 20.0f;
        R_sholderM = R_sholder - 20.0f;
        //右ひじ
        R_elbowP = R_elbow + 20.0f;
        R_elbowM = R_elbow - 20.0f;
        //右股   
        R_crotchP = R_crotch + 20.0f;
        R_crotchM = R_crotch - 20.0f;
        //右膝
        R_kneeP = R_knee + 20.0f;
        R_kneeM = R_knee - 20.0f;
        //左肩
        L_shoulderP = L_shoulder + 20.0f;
        L_shoulderM = L_shoulder - 20.0f;
        //左肘
        L_elbowP = L_shoulder + 20.0f;
        L_elbowM = L_shoulder - 20.0f;
        //左股
        L_shoulderP = L_shoulder + 20.0f;
        L_shoulderM = L_shoulder - 20.0f;
        //左膝
        L_kneeP = L_knee + 20.0f;
        L_kneeM = L_knee - 20.0f;
        /***************************************/
        pause_fight.GetComponent<Image>().color = new Color(r, g, b, alpha);

        transform.position = new Vector3(playerstatus.P_pos.position.x, 4, playerstatus.P_pos.position.z);
        //角度check
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

        //全部入ったか
        if (R_arm_flag == true &&
            L_arm_flag == true &&
            R_leg_flag == true &&
            L_leg_flag == true)
        {
            DecidePose_Banana = true;
        }
    }
    void AnglesCheck()
    {
        //右腕の判別
        //右肩の角度
        if (R_sholder_center >= R_sholderM && R_sholder_center <= R_sholderP)
        {
            //右肘
            if (R_elbow_center >= R_elbowM && R_elbow_center <= R_elbowP)
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
        if (R_crotch >= R_crotchM && R_crotch <= R_crotchP)
        {
            //右膝
            if (R_knee >= R_kneeM && R_knee <= R_kneeP)
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
        if (L_shoulder_center >= L_shoulderM && L_shoulder_center <= L_shoulderP)
        {
            //左肘
            if (L_shoulder_center >= L_shoulderM && L_shoulder_center <= L_shoulderP)
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
        if (L_crotch_center >= L_crotch_M && L_crotch_center <= L_crotch_P)
        {
            //左膝
            if (L_crotch_center >= L_crotch_M && L_crotch_center <= L_crotch_P)
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
    public void fightPoseDisplaytrue()
    {
        alpha = 1.0f;
    }

    //ポーズの画像を表示させない
    public void fightPoseDisplayfalse()
    {
        alpha = 0.0f;
    }
}