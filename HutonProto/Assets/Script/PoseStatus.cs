using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseStatus : MonoBehaviour {

    //全部のポーズで使う変数はここに

    /****体の関節角度指定****/
    //右肩の角度を所得する
    public GameObject R_shoulder;
    public float R_shoulder_Y;
    //右肘の角度を所得する
    public GameObject R_elbow;
    public float R_elbow_Y;
    //右股の角度を所得する
    public GameObject R_crotch;
    public float R_crotch_Y;
    //右膝の角度を所得する
    public GameObject R_knee;
    public float R_knee_Y;
    //左肩の角度を所得する
    public GameObject L_shoulder;
    public float L_shoulder_Y;
    //左肘の角度を所得する
    public GameObject L_elbow;
    public float L_elbow_Y;
    //左股の角度を所得する
    public GameObject L_crotch;
    public float L_crotch_Y;
    //左膝の角度を所得する
    public GameObject L_knee;
    public float L_knee_Y;
    /********************/

    /*プレイヤーの位置と角度を合わせる*/
    //プレイヤーの回転角度
    public float P_angle;
    //プレイヤーの位置
    public Transform P_pos;
    /**********************************/

    //falseならガイド画像を表示していない、trueなら画像を
    public bool imageDisplay;

   
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
    }

	void Update () {
        //各関節の現在の角度
        R_shoulder_Y = R_shoulder.transform.localEulerAngles.y;
        R_elbow_Y = R_elbow.transform.localEulerAngles.y;
        R_crotch_Y = R_crotch.transform.localEulerAngles.y;
        R_knee_Y = R_knee.transform.localEulerAngles.y;
        L_shoulder_Y = L_shoulder.transform.localEulerAngles.y;
        L_elbow_Y = L_elbow.transform.localEulerAngles.y;
        L_crotch_Y = L_crotch.transform.localEulerAngles.y;
        L_knee_Y = L_knee.transform.localEulerAngles.y;
    }
}
