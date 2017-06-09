using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseManager : MonoBehaviour
{
    public Scene_manager _scenemanager;
    // ポーズ
    public enum PoseState
    {
        None, //ポーズ無し
        Pose_of_Turbulent_hawk, // 荒ぶる鷹のポーズ
        Muay_thai, // ムエタイ
        Open_leg, // 開脚
        Chico, // 四股
        The_Eiffel_Tower, // エッフェル塔
        Banana, // バナナ
        AlphaF, // アルファベットF
        AlphaH, // アルファベットH
        AlphaJ, // アルファベットJ
        AlphaK, // アルファベットK
        AlphaN, // アルファベットN
        AlphaR, // アルファベットR
        AlphaU, // アルファベットU
        AlphaX, // アルファベットX
        Yoga_1, // ヨガ1
        Yoga_2, // ヨガ2
        Yoga_3, // ヨガ3
        Bodybuilding, // ボディービル
        Frog, // カエル
        Gymnastics, // 体操
        Exit, // 出口マーク
        Sophomoric_1, // 厨二1
        Sophomoric_2, // 厨二2
        Conveni, // しゃがみ
        Yell, // 気合
        Bend_forward, // 前屈
        Sprawled, // 大の字
        Race_walking, // 競歩
        Joy, // 喜び
        Deformed // デフォルメ
    }
    [SerializeField, Tooltip("現在のポーズ")]
    public PoseState _Pose;

    /****ポーズオブジェクトの取得****/
    // ポーズオブジェクトを取得するためにCanvasを取得する
    public Canvas _PoseCanvas;

    /*スコアオブジェクトの取得*/
    private Canvas _View;
    public ScoreView _view;
    public bool _scoreCount;
    [SerializeField, Tooltip("全身でポーズをとれてるか")]
    public bool _ScoreWhole;
    [SerializeField, Tooltip("上半身でポーズをとれてるか")]
    public bool _ScoreUpper;
    [SerializeField, Tooltip("下半身でポーズをとれてるか")]
    public bool _ScoreLower;

    private AudioSource _audioSource;

    // Use this for initialization
    void Start()
    {
        _scenemanager = GameObject.FindGameObjectWithTag("Scenemanager").GetComponent<Scene_manager>();


        // 初期ポーズは指定なし
        _Pose = PoseState.None;

        // スコア
        if (GameObject.Find("ScoreCanvas") != null)
        { 
            _View = GameObject.Find("ScoreCanvas").GetComponent<Canvas>();
        _view = _View.GetComponent<ScoreView>();
    }
        _scoreCount = false;
        _ScoreWhole = false;
        _ScoreUpper = false;
        _ScoreLower = false;

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_scenemanager.Scene_state == Scene_manager.Scenestate.GameScene)
        {
            // ポーズのオブジェクトを取得する
            if(GameObject.Find("Pose_canvas") != null)
            _PoseCanvas = GameObject.Find("Pose_canvas").GetComponent<Canvas>();
        }
        StatePose();
    }

    public void Additional_score(int Value)
    {
        ScoreManager._score = Value;
        ScoreManager._totalscore += Value;
        _view.View(ScoreManager._score);
        _audioSource.PlayOneShot(_audioSource.clip);
    }

    void StatePose()
    {
        switch (_Pose)
        {
            case PoseState.None:
                _ScoreWhole = false;
                _ScoreUpper = false;
                _ScoreLower = false;
                _scoreCount = false;
                break;
            case PoseState.Pose_of_Turbulent_hawk:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(3000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(40);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(25);
                }
                break;
            case PoseState.Muay_thai:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(2000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(30);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(25);
                }
                break;
            case PoseState.Open_leg:
                if (_ScoreWhole == true)
                {
                    Additional_score(2500);
                    _Pose = PoseState.None;
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(35);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(50);
                }
                break;
            case PoseState.Chico:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(3000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(60);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(40);
                }
                break;
            case PoseState.The_Eiffel_Tower:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(1500);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(35);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(40);
                }
                break;
            case PoseState.Banana:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(1500);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(20);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(25);
                }
                break;
            case PoseState.AlphaF:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(3000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(45);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(45);
                }
                break;
            case PoseState.AlphaH:
                if (_ScoreWhole == true)
                {
                    Additional_score(2500);
                    _Pose = PoseState.None;
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(50);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(50);
                }
                break;
            case PoseState.AlphaJ:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(2000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(35);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(25);
                }
                break;
            case PoseState.AlphaK:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(2500);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(40);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(40);
                }
                break;
            case PoseState.AlphaN:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(4000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(60);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(60);
                }
                break;
            case PoseState.AlphaR:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(3500);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(40);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(40);
                }
                break;
            case PoseState.AlphaU:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(3500);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(70);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(70);
                }
                break;
            case PoseState.AlphaX:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(2000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(40);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(40);
                }
                break;
            case PoseState.Yoga_1:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(5000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(20);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(15);
                }
                break;
            case PoseState.Yoga_2:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(1000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(35);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(40);
                }
                break;
            case PoseState.Yoga_3:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(8500);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(85);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(85);
                }
                break;
            case PoseState.Bodybuilding:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(1500);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(20);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(10);
                }
                break;
            case PoseState.Frog:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(2500);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(50);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(50);
                }
                break;
            case PoseState.Gymnastics:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(2000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(45);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(45);
                }
                break;
            case PoseState.Exit:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(3000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(45);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(45);
                }
                break;
            case PoseState.Sophomoric_1:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(2000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(30);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(20);
                }
                break;
            case PoseState.Sophomoric_2:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(2500);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(45);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(20);
                }
                break;
            case PoseState.Conveni:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(3500);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(30);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(50);
                }
                break;
            case PoseState.Yell:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(2000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(20);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(40);
                }
                break;
            case PoseState.Bend_forward:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(5000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(70);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(60);
                }
                break;
            case PoseState.Sprawled:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(2500);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(50);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(40);
                }
                break;
            case PoseState.Race_walking:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(3000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(45);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(20);
                }
                break;
            case PoseState.Joy:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(2000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(55);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(25);
                }
                break;
            case PoseState.Deformed:
                if (_ScoreWhole == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(1000);
                }
                else if (_ScoreUpper == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(1);
                }
                else if (_ScoreLower == true)
                {
                    _Pose = PoseState.None;
                    Additional_score(1);
                }
                break;
        }
    }
}