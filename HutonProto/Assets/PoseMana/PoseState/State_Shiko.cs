using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State_Shiko : MonoBehaviour {
    public PoseManager _posemanager;
    /****ポーズオブジェクトの取得****/
    // ポーズオブジェクトを取得するためにCanvasを取得する
    public Canvas _PoseCanvas;
    // 四股のオブジェクトを取得
    private Image _Shiko;
    public Pose_shiko _shiko;

    // Use this for initialization
    void Start () {
        _posemanager = GameObject.FindGameObjectWithTag("Posemanager").GetComponent<PoseManager>();
        _PoseCanvas = GameObject.Find("Pose_canvas").GetComponent<Canvas>();
        _Shiko = GameObject.Find("Pose_Shiko").GetComponent<Image>();
        _shiko = GameObject.Find("Pose_Shiko").GetComponent<Pose_shiko>();
    }

    // Update is called once per frame
    void Update () {
        if ((_shiko.R_arm_flag == true &&
            _shiko.L_arm_flag == true) ||
            (_shiko.R_leg_flag == true &&
            _shiko.L_leg_flag == true))
        {
            _posemanager._Pose = PoseManager.PoseState.Chico;
        }
        /*上半身、下半身のポーズが是のとき、全身でのポーズのフラグを是に*/
        if (_shiko.L_arm_flag == true &&
            _shiko.R_arm_flag == true &&
            _shiko.L_leg_flag == true &&
            _shiko.R_leg_flag == true)
        {
            _posemanager._ScoreWhole = true;
        }
        /* 両腕の判定が是のとき、上半身ポーズのフラグを是に*/
        if (_shiko.R_arm_flag == true &&
            _shiko.L_arm_flag == true)
        {
            _posemanager._ScoreUpper = true;
        }
        /*両足の判定は是のとき、下半身ポーズのフラグを是に*/
        if (_shiko.R_leg_flag == true &&
            _shiko.L_leg_flag == true)
        {
            _posemanager._ScoreLower = true;
        }
    }
}
