using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pose_Happy : MonoBehaviour
{
    //保留
    //プレイヤーの角度などの情報
    PlayerStatus playerstatus;

    /*ポーズHappyの判定を行う*/
    //「Happpy」ポーズの画像を所得
    private Image pauseHappy;
    private float r, g, b, alpha;

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
    /************************************/

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
    //ポーズが決まったか
    public bool DecidePose_Happy= false;
    //成功したポーズの判定で使う
    public string Pausename = "pauseHappy";

    void Start()
    {
        //ポーズガイドの画像
        pauseHappy = gameObject.GetComponent<Image>();
        r = pauseHappy.GetComponent<Image>().color.r;
        g = pauseHappy.GetComponent<Image>().color.g;
        b = pauseHappy.GetComponent<Image>().color.b;
        alpha = pauseHappy.GetComponent<Image>().color.a;

        //プレイヤーの関節の角度など
        playerstatus = this.gameObject.GetComponent<PlayerStatus>();
        HappyPoseDisplayfalse();
    }


    void Update()
    {
        pauseHappy.GetComponent<Image>().color = new Color(r, g, b, alpha);

        transform.position = new Vector3(P_pos.position.x, 10, P_pos.position.z);

        //角度の獲得
        R_sholder = playerstatus.R_shoulder_Y;
        R_elbow = playerstatus.R_elbow_Y;
        R_crotch = playerstatus.R_crotch_Y;
        R_knee = playerstatus.R_knee_Y;
        L_shoulder = playerstatus.L_shoulder_Y;
        L_elbow = playerstatus.L_elbow_Y;
        L_crotch = playerstatus.L_crotch_Y;
        L_knee = playerstatus.L_knee_Y;

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

        if (R_arm_flag == true &&
            L_arm_flag == true &&
            R_leg_flag == true &&
            L_leg_flag == true)
        {
            DecidePose_Happy = true;
        }
        else
        {
            DecidePose_Happy = false;
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
        //左腕の角度
        if (L_shoulder_center >= L_shoulderM && L_shoulder_center <= L_shoulderP)
        {
            //左肘
            if (L_elbow_center >= -20 && L_elbow_center <= 20)
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
        if (L_crotch_center >= -20 && L_crotch_center <= 20)
        {
            //左膝
            if (L_knee_center >= -20 && L_knee_center <=20)
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
    public void HappysPoseDisplaytrue()
    {
        alpha = 1.0f;
    }
    //ポーズの画像を表示させない
    public void HappyPoseDisplayfalse()
    {
        alpha = 0.0f;
    }
}