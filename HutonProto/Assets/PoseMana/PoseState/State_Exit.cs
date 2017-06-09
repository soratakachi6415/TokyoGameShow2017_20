using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State_Exit : MonoBehaviour {
    public PoseManager _posemanager;

    /****ポーズオブジェクトの取得****/
    // ポーズオブジェクトを取得するためにCanvasを取得する
    public Canvas _PoseCanvas;
    // 出口のオブジェクトを取得
    private Image _Exit;
    public Pose_exit _exit;

    // Use this for initialization
    void Start () {
        _posemanager = GameObject.FindGameObjectWithTag("Posemanager").GetComponent<PoseManager>();
        _PoseCanvas = GameObject.Find("Pose_canvas").GetComponent<Canvas>();
        _Exit = GameObject.Find("Pose_Exit").GetComponent<Image>();
        _exit = GameObject.Find("Pose_Exit").GetComponent<Pose_exit>();
    }

    // Update is called once per frame
    void Update () {
        if ((_exit.R_arm_flag == true &&
            _exit.L_arm_flag == true) ||
            (_exit.R_leg_flag == true &&
            _exit.L_leg_flag == true))
        {
            _posemanager._Pose = PoseManager.PoseState.Exit;
        }
        if (_exit.R_arm_flag == true &&
            _exit.L_arm_flag == true)
        {
            _posemanager._ScoreUpper = true;
        }
        if (_exit.R_leg_flag == true &&
            _exit.L_leg_flag == true)
        {
            _posemanager._ScoreLower = true;
        }

    }
}
