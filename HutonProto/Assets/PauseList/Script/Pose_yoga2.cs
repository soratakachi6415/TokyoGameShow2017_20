using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pose_yoga2 : MonoBehaviour {
    PlayerStatus playerstatus;

    /*ポーズ_バナナの判定を行う*/
    //「バナナ」ポーズの画像を所得
    protected Image pose_yoga2;
    protected float r, g, b, alpha;
    //角度の誤差の数値
    private float anglePM;

    /****現在の角度******/
    protected float R_sholder;
    protected float R_elbow;
    protected float R_crotch;
    protected float R_knee;
    protected float L_shoulder;
    protected float L_elbow;
    protected float L_crotch;
    protected float L_knee;
    /********************/

    /*それぞれの手足ごとの判定の数値の中心*/
    //右肩の判定の基本となる数字
    //Pがプラス、Mがマイナス
    public float R_sholder_center;
    protected float R_sholderP, R_sholderM;
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
    protected float L_crotch_P, L_crotch_M;
    //左膝
    public float L_knee_center;
    protected float L_kneeP, L_kneeM;
    //許容範囲数値
    public float anglenumber;
    /************************************/
    //ポーズが決まったか
    public bool DecidePose_yoga2;
    //ポーズの各腕、足がそれぞれ指定された範囲内に入っているか
    //falseが入ってない、trueが入ってる
    public bool R_arm_flag = false;
    public bool R_leg_flag = false;
    public bool L_arm_flag = false;
    public bool L_leg_flag = false;
    //falseならガイド画像を表示していない、trueなら画像を
    public bool imageDisplay = false;

    //成功したポーズの判定で使う
    public string posename = "pose_yoga2";

    void Start()
    {
        //ポーズガイドの画像
        pose_yoga2 = gameObject.GetComponent<Image>();
        r = pose_yoga2.GetComponent<Image>().color.r;
        g = pose_yoga2.GetComponent<Image>().color.g;
        b = pose_yoga2.GetComponent<Image>().color.b;
        alpha = pose_yoga2.GetComponent<Image>().color.a;
        //プレイヤーの関節の角度など
        playerstatus = GameObject.FindGameObjectWithTag("PlayerStatus").GetComponent<PlayerStatus>();
        anglePM = playerstatus.anglePM;
        yoga2PoseDisplayfalse();
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
        pose_yoga2.GetComponent<Image>().color = new Color(r, g, b, alpha);

        transform.position = new Vector3(playerstatus.P_pos.position.x, 10, playerstatus.P_pos.position.z + 3.0f);
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
            DecidePose_yoga2 = true;            
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
        if (R_crotch_center >= R_crotchM && R_crotch_center <= R_crotchP)
        {
            //右膝
            if (R_knee_center >= R_kneeM && R_knee_center <= R_kneeP)
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
    public void yoga2PoseDisplaytrue()
    {
        alpha = 1.0f;
    }

    //ポーズの画像を表示させない
    public void yoga2PoseDisplayfalse()
    {
        alpha = 0.0f;
    }
}
