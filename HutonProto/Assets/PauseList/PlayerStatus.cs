using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {

    /****体の関節指定****/
    //右肩の角度を所得する    
    private GameObject R_shoulder;
    [SerializeField]
    public float R_shoulder_Y;
    //右肘の角度を所得する   
    private GameObject R_elbow;
    [SerializeField]
    public float R_elbow_Y;
    //右股の角度を所得する    
    private GameObject R_crotch;
    [SerializeField]
    public float R_crotch_Y;
    //右膝の角度を所得する   
    private GameObject R_knee;
    [SerializeField]
    public float R_knee_Y;
    //左肩の角度を所得する   
    private GameObject L_shoulder;
    [SerializeField]
    public float L_shoulder_Y;
    //左肘の角度を所得する   
    private GameObject L_elbow;
    [SerializeField]
    public float L_elbow_Y;
    //左股の角度を所得する    
    private GameObject L_crotch;
    [SerializeField]
    public float L_crotch_Y;
    //左膝の角度を所得する    
    private GameObject L_knee;
    [SerializeField]
    public float L_knee_Y;
    /********************/


    /*プレイヤーの位置と角度を合わせる*/
    //プレイヤーの位置
    public Transform P_pos;
    //プレイヤーの回転角度
    public float P_angle;
   
    /**********************************/

    //角度の誤差の数値
    public float anglePM;

    void Start () {
        //そのうちタグ判別に切り替えたい
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


        //R_shoulder = GameObject.Find("Player_RightHand1");
        //R_elbow = GameObject.Find("Player_RightHand2");
        //R_crotch = GameObject.Find("Player_RightLeg1");
        //R_knee = GameObject.Find("Player_RightLeg2");
        //L_shoulder = GameObject.Find("Player_LeftHand1");
        //L_elbow = GameObject.Find("Player_LeftHand2");
        //L_crotch = GameObject.Find("Player_LeftLeg1");
        //L_knee = GameObject.Find("Player_LeftLeg2");

        //P_pos = GameObject.Find("new playerMODEL").GetComponent<Transform>().transform;
        //P_angle = P_pos.GetComponent<Transform>().transform.eulerAngles.y;
    }

    void Update () {

        //各関節の現在の角度
        R_shoulder_Y    = R_shoulder.transform.localEulerAngles.y;
        R_elbow_Y       = R_elbow.transform.localEulerAngles.y;
        R_crotch_Y      = R_crotch.transform.localEulerAngles.y;
        R_knee_Y        = R_knee.transform.localEulerAngles.y;
        L_shoulder_Y    = L_shoulder.transform.localEulerAngles.y;
        L_elbow_Y       = L_elbow.transform.localEulerAngles.y;
        L_crotch_Y      = L_crotch.transform.localEulerAngles.y;
        L_knee_Y        = L_knee.transform.localEulerAngles.y;
    }
}
