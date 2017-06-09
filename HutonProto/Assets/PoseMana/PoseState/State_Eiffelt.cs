using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State_Eiffelt : MonoBehaviour {
    public PoseManager _posemanager;
    // ポーズオブジェクトを取得するためにCanvasを取得する
    public Canvas _PoseCanvas;

    // エッフェル塔のオブジェクトを取得
    private Image _Eiffelt;
    public Pose_eiffelt _eiffelt;
    // Use this for initialization
    void Start () {
        _posemanager = GameObject.FindGameObjectWithTag("Posemanager").GetComponent<PoseManager>();
        _Eiffelt = GameObject.Find("Pose_Eiffelt").GetComponent<Image>();
        _eiffelt = GameObject.Find("Pose_Eiffelt").GetComponent<Pose_eiffelt>();
    }

    // Update is called once per frame
    void Update () {
        if ((_eiffelt.R_arm_flag == true &&
            _eiffelt.L_arm_flag == true) ||
            (_eiffelt.R_leg_flag == true &&
            _eiffelt.L_leg_flag == true))
        {
            _posemanager._Pose = PoseManager.PoseState.The_Eiffel_Tower;
        }
        if (_eiffelt.L_arm_flag == true &&
            _eiffelt.R_arm_flag == true &&
            _eiffelt.L_leg_flag == true &&
            _eiffelt.R_leg_flag == true)
        {
            _posemanager._ScoreWhole = true;
        }
        if (_eiffelt.R_arm_flag == true &&
            _eiffelt.L_arm_flag == true)
        {
            _posemanager._ScoreUpper = true;
        }
        if (_eiffelt.R_leg_flag == true &&
            _eiffelt.L_leg_flag == true)
        {
            _posemanager._ScoreLower = true;
        }
    }
}
