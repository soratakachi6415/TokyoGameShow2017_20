using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State_openLeg : MonoBehaviour {

    public PoseManager _posemanager;
    /****ポーズオブジェクトの取得****/
    // ポーズオブジェクトを取得するためにCanvasを取得する
    public Canvas _PoseCanvas;
    // 開脚のオブジェクトを取得
    private Image _OpenLeg;
    public Pose_opneLeg _openLeg;

    // Use this for initialization
    void Start () {
        _posemanager = GameObject.FindGameObjectWithTag("Posemanager").GetComponent<PoseManager>();
        _PoseCanvas = GameObject.Find("Pose_canvas").GetComponent<Canvas>();
        _OpenLeg = GameObject.Find("Pose_OpneLeg").GetComponent<Image>();
        _openLeg = GameObject.Find("Pose_OpneLeg").GetComponent<Pose_opneLeg>();
    }

    // Update is called once per frame
    void Update () {
        if ((_openLeg.R_arm_flag == true &&
            _openLeg.L_arm_flag == true) ||
            (_openLeg.R_leg_flag == true &&
            _openLeg.L_leg_flag == true))
        {
            _posemanager._Pose = PoseManager.PoseState.Open_leg;
        }
        if (_openLeg.L_arm_flag == true &&
            _openLeg.R_arm_flag == true &&
            _openLeg.L_leg_flag == true &&
            _openLeg.R_leg_flag == true)
        {
            _posemanager._ScoreWhole = true;
        }
        /* 両腕の判定が是のとき、上半身ポーズのフラグを是に*/
        if (_openLeg.R_arm_flag == true &&
            _openLeg.L_arm_flag == true)
        {
            _posemanager._ScoreUpper = true;
        }
        /*両足の判定は是のとき、下半身ポーズのフラグを是に*/
        if (_openLeg.R_leg_flag == true &&
            _openLeg.L_leg_flag == true)
        {
            _posemanager._ScoreLower = true;
        }
    }
}
