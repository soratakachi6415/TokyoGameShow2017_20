using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pose_Fight : MonoBehaviour {
    //プレイヤーの角度参照
    PlayerStatus playerstatus;

    /*ポーズ_Hの判定を行う*/
    //「H」ポーズの画像を所得
    private Image pose_fight;
    private float r, g, b, alpha;

    //falseならガイド画像を表示していない、trueなら画像を
    public bool imageDisplay = false;

    //ポーズの各腕、足がそれぞれ指定された範囲内に入っているか
    //falseが入ってない、trueが入ってる
    public bool R_arm_flag = false;
    public bool R_leg_flag = false;
    public bool L_arm_flag = false;
    public bool L_leg_flag = false;
    //角度の誤差の数値
    private float anglePM;

    /****現在の角度******/
    //private float R_sholder;
    //private float R_elbow;
    //private float R_crotch;
    //private float R_knee;
    //private float L_shoulder;
    //private float L_elbow;
    //private float L_crotch;
    //private float L_knee;
    /********************/

    /*それぞれの手足ごとの判定の数値の中心*/
    //右肩の判定の基本となる数字
    public float R_sholder;
    public float R_sholder_center;   
    //Pがプラス、Mがマイナス
    private float R_sholderP, R_sholderM;
    //右肘
    public float R_elbow;
    public float R_elbow_center;
    private float R_elbowP, R_elbowM;
    //右股
    public float R_crotch;
    public float R_crotch_center;
    private float R_crotchP, R_crotchM;
    //右膝    
    public float R_knee;
    public float R_knee_center;
    private float R_kneeP, R_kneeM;
    //左肩
    public float L_shoulder;
    public float L_shoulder_center;
    private float L_shoulderP, L_shoulderM;
    //左肘
    public float L_elbow;
    public float L_elbow_center;
    private float L_elbowP, L_elbowM;
    //左股
    public float L_crotch;
    public float L_crotch_center;
    private float L_crotch_P, L_crotch_M;
    //左膝
    public float L_knee;
    public float L_knee_center;
    private float L_kneeP, L_kneeM;
    /************************************/
    //ポーズが決まったか
    public bool DecidePose_fight;

    //成功したポーズの判定で使う
    public string posename = "pose_fight";
    void Start()
    {
        //ポーズガイドの画像
        pose_fight = gameObject.GetComponent<Image>();
        r = pose_fight.GetComponent<Image>().color.r;
        g = pose_fight.GetComponent<Image>().color.g;
        b = pose_fight.GetComponent<Image>().color.b;
        alpha = pose_fight.GetComponent<Image>().color.a;
        //プレイヤーの関節の角度など
        playerstatus = GameObject.FindGameObjectWithTag("PlayerStatus").GetComponent<PlayerStatus>();
        anglePM = playerstatus.anglePM;
        fightPoseDisplayfalse();
    }
    void Update()
    {
        //ポーズの画像の情報
        pose_fight.GetComponent<Image>().color = new Color(r, g, b, alpha);
        //画像をプレイヤーの上、X、Yの調整
        transform.position = new Vector3(playerstatus.P_pos.position.x, 10, playerstatus.P_pos.position.z);

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
        R_sholderP = R_sholder + anglePM;
        R_sholderM = R_sholder - anglePM;
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
        pose_fight.GetComponent<Image>().color = new Color(r, g, b, alpha);

        transform.position = new Vector3(playerstatus.P_pos.position.x, 4, playerstatus.P_pos.position.z);
       
        ArmflagCheck();
        //足を基準にした場合の判定
        FootflagCheck();

        if (imageDisplay == false)
        {
            fightPoseDisplayfalse();
        }

        if (imageDisplay == true)
        {
            fightPoseDisplaytrue();
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
            DecidePose_fight = true;
        }
    }
    void ArmflagCheck()
    {
        //右腕が範囲内にあるとき
        if (R_arm_flag == true)
        {
            //右足が範囲内にあるとき
            if (R_leg_flag == true)
            {
                imageDisplay = true;
            }
            //左足が範囲内にあるとき
            else if (L_leg_flag == true)
            {
                imageDisplay = true;
            }
            else if (L_leg_flag == false || R_leg_flag == false)
            {
                imageDisplay = false;
            }
        }
        //左腕が範囲内にあるとき
        else if (L_arm_flag == true)
        {
            //右足が範囲内にあるとき
            if (R_leg_flag == true)
            {
                imageDisplay = true;
            }
            //左足が範囲内にあるとき
            else if (L_leg_flag == true)
            {
                imageDisplay = true;
            }
            else if (L_leg_flag == false || R_leg_flag == false)
            {
                imageDisplay = false;
            }
        }
    }
    void FootflagCheck()
    {
        //右足が範囲内にあるとき
        if (R_leg_flag == true)
        {
            //右腕が範囲内にあるとき
            if (R_arm_flag == true)
            {
                imageDisplay = true;
            }
            //左腕が範囲内にあるとき
            else if (L_arm_flag == true)
            {
                imageDisplay = true;
            }
            else if (L_arm_flag == false || R_arm_flag == false)
            {
                imageDisplay = false;
            }
        }
        //左腕が範囲内にあるとき
        else if (L_leg_flag == true)
        {
            //右足が範囲内にあるとき
            if (R_arm_flag == true)
            {
                imageDisplay = true;
            }
            //左足が範囲内にあるとき
            else if (L_arm_flag == true)
            {
                imageDisplay = true;
            }
            else if (L_arm_flag == false || R_arm_flag == false)
            {
                imageDisplay = false;
            }
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
