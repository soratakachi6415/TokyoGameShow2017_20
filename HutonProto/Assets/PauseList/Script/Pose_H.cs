using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pose_H : MonoBehaviour
{

    // PoseStatus poseStatus;

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

    /*ポーズ_Hの判定を行う*/
    //「H」ポーズの画像を所得
    public Image pause_H;
    public float r, g, b, alpha;


    //各手足の条件
    public bool R_arm_flag = false;
    public bool R_leg_flag = false;
    public bool L_arm_flag = false;
    public bool L_leg_flag = false;

    //falseならガイド画像を表示していない、trueなら画像を
    public bool imageDisplay;

    //ポーズが決まったか
    public bool DecidePose_H;

    /*プレイヤーの位置と角度を合わせる*/
    //プレイヤーの回転角度
    public float P_angle;
    //プレイヤーの位置
    public Transform P_pos;
    /**********************************/

    void Start()
    {

        //そのうちタグ判別に切り替えたい
        R_shoulder = GameObject.Find("Player_RightHand1");
        R_elbow = GameObject.Find("Player_RightHand2");
        R_crotch = GameObject.Find("Player_RightLeg1");
        R_knee = GameObject.Find("Player_RightLeg2");
        L_shoulder = GameObject.Find("Player_LeftHand1");
        L_elbow = GameObject.Find("Player_LeftHand2");
        L_crotch = GameObject.Find("Player_LeftLeg1");
        L_knee = GameObject.Find("Player_LeftLeg2");

        pause_H = gameObject.GetComponent<Image>();

        P_pos = GameObject.Find("Player_Body").GetComponent<Transform>().transform;
        P_angle = P_pos.transform.localEulerAngles.y; 
            //GameObject.Find("Player_Body").GetComponent<Transform>().transform.eulerAngles.y;
      
        //ポーズガイドの画像
        pause_H = gameObject.GetComponent<Image>();
        r = pause_H.GetComponent<Image>().color.r;
        g = pause_H.GetComponent<Image>().color.g;
        b = pause_H.GetComponent<Image>().color.b;
        alpha = pause_H.GetComponent<Image>().color.a;

        DecidePose_H = false;

        HPoseDisplayfalse();
    }


    void Update()
    {
        pause_H.GetComponent<Image>().color = new Color(r, g, b, alpha);
        //プレイヤーの追従
        transform.position = new Vector3(P_pos.position.x, 4, P_pos.position.z);

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

        //imageDisplayがtrueの時はどれか一部分が入っている
        // DecidePoseがtrueの時はポーズが決まった時
        //どれか一つが入ってる場合
        if (R_arm_flag == true ||
            L_arm_flag == true ||
            R_leg_flag == true ||
            L_leg_flag == true)
        {
            imageDisplay = true;
        }
        //どれも入ってない場合
        if (R_arm_flag == false &&
                 L_arm_flag == false &&
                 R_leg_flag == false &&
                 L_leg_flag == false)
        {
            imageDisplay = false;
        }

        //全部入った場合
        if (R_arm_flag == true &&
            L_arm_flag == true &&
            R_leg_flag == true &&
            L_leg_flag == true)
        {
            DecidePose_H = true;
        }
        /************************/

    }

    /*手足が範囲内に入っているか*/
    //2017/06/05:角度の変更
    void AnglesCheck()
    {        
        //右腕の判別
        //右肩の角度
        if (R_shoulder_Y >= 80 && R_shoulder_Y <= 100)
        {
            //右肘
            if (R_elbow_Y >= -10 && R_elbow_Y <= 10)
            {
                R_arm_flag = true;
            }
            else
            {
                //Debug.Log("右肘が駄目");
                R_arm_flag = false;
            }
        }
        else
        {
            //Debug.Log("右肩がダメ");
            R_arm_flag = false;
        }


        //右足
        //右股の角度
        if (R_crotch_Y >= 260 && R_crotch_Y <= 280)
        {
            //右膝
            if (R_knee_Y >= -10 && R_knee_Y <= 10)
            {
                R_leg_flag = true;
            }
            else
            {
                //Debug.Log("右膝が駄目");
                R_leg_flag = false;
            }
        }
        else
        {
            //Debug.Log("右股が駄目");
            R_leg_flag = false;
        }


        //左側の判別
        //左腕の角度
        if (L_shoulder_Y >= 260 && L_shoulder_Y <= 280)
        {
            //左肘
            if (L_elbow_Y >= -10 && L_elbow_Y <= 10)
            {
                L_arm_flag = true;
            }
            else
            {
                //Debug.Log("左肘が駄目");
                L_arm_flag = false;
            }          
        }
        else
        {
            //Debug.Log("左肩が駄目");
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
                //Debug.Log("左膝が駄目");
                L_leg_flag = false;
            }
        }
        else
        {
            //Debug.Log("左股が駄目");
            L_leg_flag = false;
        }
    }

    /*外部から表示非表示の切り替え*/
    //ポーズの画像を表示させる
    public void HPoseDisplaytrue()
    {
        alpha = 1.0f;
    }
    //ポーズの画像を表示させない
    public void HPoseDisplayfalse()
    {
        alpha = 0.0f;
    }
    /*****************************/
}