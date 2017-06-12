using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pose_Default : MonoBehaviour {
    /*ポーズ_Hの判定を行う*/
    //「H」ポーズの画像を所得
    private Image pose_def;
    private float r, g, b, alpha;

    /****体の関節指定****/
    //右肩の角度を所得する
    public GameObject R_shoulder;
    private float R_shoulder_Y;
    //右肘の角度を所得する
    public GameObject R_elbow;
    private float R_elbow_Y;
    //右股の角度を所得する
    public GameObject R_crotch;
    private float R_crotch_Y;
    //右膝の角度を所得する
    public GameObject R_knee;
    private float R_knee_Y;
    //左肩の角度を所得する
    public GameObject L_shoulder;
    private float L_shoulder_Y;
    //左肘の角度を所得する
    public GameObject L_elbow;
    private float L_elbow_Y;
    //左股の角度を所得する
    public GameObject L_crotch;
    private float L_crotch_Y;
    //左膝の角度を所得する
    public GameObject L_knee;
    private float L_knee_Y;
    /********************/


    //falseならガイド画像を表示していない、trueなら画像を
    public bool imageDisplay = false;

    //ポーズの各腕、足がそれぞれ指定された範囲内に入っているか
    //falseが入ってない、trueが入ってる
    public bool R_arm_flag = false;
    public bool R_leg_flag = false;
    public bool L_arm_flag = false;
    public bool L_leg_flag = false;

    //成功したポーズの判定で使う
    public string posename = "pose_def";

    void Start()
    {
        //ポーズガイドの画像
        pose_def = gameObject.GetComponent<Image>();
        r = pose_def.GetComponent<Image>().color.r;
        g = pose_def.GetComponent<Image>().color.g;
        b = pose_def.GetComponent<Image>().color.b;
        alpha = pose_def.GetComponent<Image>().color.a;

        //そのうちタグ判別に切り替えたい
        //R_shoulder = GameObject.FindWithTag("Rightshoulder");
        //R_elbow = GameObject.FindWithTag("Rightelbow");
        //R_crotch = GameObject.FindWithTag("Rightcrotch");
        //R_knee = GameObject.FindWithTag("Rightknee");
        //L_shoulder = GameObject.FindWithTag("Leftshoulder");
        //L_elbow = GameObject.FindWithTag("Leftelbow");
        //L_crotch = GameObject.FindWithTag("Leftcrotch");
        //L_knee = GameObject.FindWithTag("Leftknee");
    }


    void Update()
    {
        pose_def.GetComponent<Image>().color = new Color(r, g, b, alpha);

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

        //一定数以上の条件を見たしたらimageDisplayをtrueにする
        poseimageDisplay();

    }
    void AnglesCheck()
    {
        //右腕の判別

        //右肩の角度
        if (R_shoulder_Y >= -10 && R_shoulder_Y <= 10)
        {
            //右肘
            if (R_elbow_Y >= -10 && R_elbow_Y <= 10)
            {
                R_arm_flag = true;
            }
            else
            {
                Debug.Log("右肘が駄目");

                R_arm_flag = false;
            }
        }
        else
        {
            Debug.Log("右肩がダメ");
            R_arm_flag = false;
        }


        //右足

        //右股の角度
        if (R_crotch_Y <= 280 && R_crotch_Y >= 260)
        {
            //右膝
            if (R_knee_Y >= -10 && R_knee_Y <= 10)
            {
                R_leg_flag = true;
            }
            else
            {
                Debug.Log("右膝が駄目");
                R_leg_flag = false;
            }
        }
        else
        {
            Debug.Log("右股が駄目");
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
                Debug.Log("左肘が駄目");
                L_arm_flag = false;
            }
        }
        else
        {
            Debug.Log("左肩が駄目");
            L_arm_flag = false;
        }


        //左股の角度
        if (L_crotch_Y >= 80 && L_crotch_Y <= 100)
        {
            //左膝
            if (L_knee_Y >= -10 && L_knee_Y <= 10)
            {
                L_leg_flag = true;
            }
            else
            {
                Debug.Log("左膝が駄目");
                L_leg_flag = false;
            }
        }
        else
        {
            Debug.Log("左股が駄目");
            L_leg_flag = false;
        }
    }

    //ポーズの画像を表示させる
    void poseimageDisplay()
    {
        /*一回ポーズをとった後ほかのポーズを取らないと得点にならない*/
        if (imageDisplay == true)
        {
            alpha = 1.0f;
        }

        if (imageDisplay == false)
        {
            alpha = 0.0f;
        }
    }
}
