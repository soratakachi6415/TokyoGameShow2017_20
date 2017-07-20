using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseDisplaymanager : MonoBehaviour
{
    //ポーズのスクリプト
    private Pose_Banana Pose_banana_;
    private Pose_big Pose_big_;
    private Pose_Defo Pose_defo_;
    private Pose_exit Pose_exit_;
    private Pose_H Pose_h_;
    private Pose_Hawk Pose_hawk_;
    private Pose_K Pose_k_;
    private Pose_opneLeg Pose_opneLeg_;
    private Pose_shiko Pose_shiko_;
    private Pose_X Pose_x_;

    //ポーズごとのフラグ
    public bool Pose_banana_flag;
    public bool Pose_big_flag;
    public bool Pose_defo_flag;
    public bool Pose_exit_flag;
    public bool Pose_h_flag;
    public bool Pose_hawk_flag;
    public bool Pose_k_flag;
    public bool Pose_opneLeg_flag;
    public bool Pose_shiko_flag;
    public bool Pose_x_flag;
    //優先度
    //int[] priority = { 1, 2, 3, 4, 5, 6, 8, 10, 22 };
    int Pose_banana_priority = 1;
                      int Pose_big_priority = 2;
    int Pose_defo_priority = 3;
    int Pose_exit_priority = 4;
    int Pose_h_priority = 5;
    int Pose_hawk_priority = 6;
                      int Pose_k_priority = 8;
    int Pose_opneLeg_priority = 10;
    int Pose_shiko_priority = 22;
                      int Pose_x_priority = 25;

    //ポーズをとった回数
    int Pose_banana_Playcount = 0;
    public int Pose_big_Playcount = 0;
    int Pose_defo_Playcount = 0;
    int Pose_exit_Playcount = 0;
    int Pose_h_Playcount = 0;
    int Pose_hawk_Playcount = 0;
    public int Pose_k_Playcount = 0;
    int Pose_opneLeg_Playcount = 0;
    int Pose_shiko_Playcount = 0;
    public int Pose_x_Playcount = 0;

    [SerializeField, TooltipAttribute("onだとポーズ表示/offだと非表示")]
    public bool Displayfswitch = false;


    void Start()
    {
        Pose_banana_ = GameObject.Find("Pose_Banana").GetComponent<Pose_Banana>();
        Pose_big_ = GameObject.Find("Pose_Big").GetComponent<Pose_big>();
        Pose_defo_ = GameObject.Find("Pose_Defolt").GetComponent<Pose_Defo>();
        Pose_exit_ = GameObject.Find("Pose_Exit").GetComponent<Pose_exit>();
        Pose_h_ = GameObject.Find("Pose_H").GetComponent<Pose_H>();
        Pose_hawk_ = GameObject.Find("Pose_Hawk").GetComponent<Pose_Hawk>();
        Pose_k_ = GameObject.Find("Pose_K").GetComponent<Pose_K>();
        Pose_opneLeg_ = GameObject.Find("Pose_OpneLeg").GetComponent<Pose_opneLeg>();
        Pose_shiko_ = GameObject.Find("Pose_Shiko").GetComponent<Pose_shiko>();
        Pose_x_ = GameObject.Find("Pose_X").GetComponent<Pose_X>();
    }

    void Update()
    {

        Pose_big_flag = Pose_big_.imageDisplay;
        Pose_k_flag = Pose_k_.imageDisplay;
        Pose_x_flag = Pose_x_.imageDisplay;

        Pose_banana_flag = Pose_banana_.imageDisplay;
        Pose_defo_flag = Pose_defo_.imageDisplay;
        Pose_exit_flag = Pose_exit_.imageDisplay;
        Pose_h_flag = Pose_h_.imageDisplay;
        Pose_hawk_flag = Pose_hawk_.imageDisplay;
        Pose_opneLeg_flag = Pose_opneLeg_.imageDisplay;
        Pose_shiko_flag = Pose_shiko_.imageDisplay;


        if (Pose_x_flag == false)
        {
            Pose_x_.XPoseDisplayfalse();
        }
        if (Pose_k_flag == false)
        {
            Pose_k_.KPoseDisplayfalse();
        }
        if (Pose_big_flag == false)
        {
            Pose_big_.BigPoseDisplayfalse();
        }


        if (Pose_big_flag == true)
        {
            Pose_big_.BigPoseDisplaytrue();
        }

        if (Pose_k_flag == true&&Pose_big_flag==false)
        {
            Pose_k_.KPoseDisplaytrue();
        }

        if (Pose_x_flag == true && Pose_k_flag == false && Pose_big_flag == false)
        {
            Pose_x_.XPoseDisplaytrue();
        }
    }
}