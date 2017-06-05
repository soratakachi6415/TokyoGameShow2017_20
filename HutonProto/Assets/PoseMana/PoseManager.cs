using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseManager : MonoBehaviour
{
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
    // 開脚のオブジェクトを取得
    private Image _OpenLeg;
    public Pose_opneLeg _openLeg;
    // 四股のオブジェクトを取得
    private Image _Shiko;
    public Pose_shiko _shiko;
    // エッフェル塔のオブジェクトを取得
    private Image _Eiffelt;
    public Pose_eiffelt _eiffelt;
    // アルファベットHのオブジェクトを取得
    private Image _AlphaH;
    public Pose_H _alphaH;
    // アルファベットUのオブジェクトを取得
    private Image _AlphaU;
    public Pose_U _alphaU;
    // アルファベットXのオブジェクトを取得
    private Image _AlphaX;
    public Pose_X _alphaX;
    // 出口のオブジェクトを取得
    private Image _Exit;
    public Pose_exit _exit;
    // 大の字のオブジェクトを取得
    private Image _Big;
    public Pose_big _big;
    // 喜びのオブジェクトを取得
    private Image _Happy;
    public Pose_Happy _happy;

    /*スコアオブジェクトの取得*/
    private Canvas _View;
    public ScoreView _view;
    public static int _Score; //スコア
    public bool _scoreCount;
    [SerializeField, Tooltip("全身でポーズをとれてるか")]
    private bool _ScoreWhole;
    [SerializeField, Tooltip("上半身でポーズをとれてるか")]
    private bool _ScoreUpper;
    [SerializeField, Tooltip("下半身でポーズをとれてるか")]
    private bool _ScoreLower;

    private AudioSource _audioSource;
    
    // Use this for initialization
    void Start()
    {
        // ポーズのオブジェクトを取得する
        _PoseCanvas = GameObject.Find("Pose_canvas").GetComponent<Canvas>();
        _Big = _PoseCanvas.GetComponentInChildren<Image>();
        _OpenLeg = GameObject.Find("Pose_OpneLeg").GetComponent<Image>();
        _Shiko = GameObject.Find("Pose_Shiko").GetComponent<Image>();
        _Eiffelt = GameObject.Find("Pose_Eiffelt").GetComponent<Image>();
        _AlphaH = GameObject.Find("Pose_H").GetComponent<Image>();
        _AlphaU = GameObject.Find("Pose_U").GetComponent<Image>();
        _AlphaX = GameObject.Find("Pose_X").GetComponent<Image>();
        _Exit = GameObject.Find("Pose_Exit").GetComponent<Image>();
        _Happy = GameObject.Find("Pose_Happy").GetComponent<Image>();
        
        // ポーズのスクリプトを取得
        _openLeg = GameObject.Find("Pose_OpneLeg").GetComponent<Pose_opneLeg>();
        _shiko = GameObject.Find("Pose_Shiko").GetComponent<Pose_shiko>();
        _eiffelt = GameObject.Find("Pose_Eiffelt").GetComponent<Pose_eiffelt>();
        _alphaH = GameObject.Find("Pose_H").GetComponent<Pose_H>();
        _alphaU = GameObject.Find("Pose_U").GetComponent<Pose_U>();
        _alphaX = GameObject.Find("Pose_X").GetComponent<Pose_X>();
        _exit = GameObject.Find("Pose_Exit").GetComponent<Pose_exit>();
        _big = GameObject.Find("Pose_Big").GetComponent<Pose_big>();
        _happy = GameObject.Find("Pose_Happy").GetComponent<Pose_Happy>();
        
        // 初期ポーズは指定なし
        _Pose = PoseState.None;

        // スコア
        _View = GameObject.Find("ScoreCanvas").GetComponent<Canvas>();
        _view = _View.GetComponent<ScoreView>();
        _Score = 0; // スコアの初期化
        _scoreCount = false;
        _ScoreWhole = false;
        _ScoreUpper = false;
        _ScoreLower = false;

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPoseState();

        if (_scoreCount == true)
        {
            Pose();
            GetPose();
            GetUpperPose();
            GetLowerPose();
        }
        else
        {
            _ScoreWhole = false;
            _ScoreUpper = false;
            _ScoreLower = false;
            _Pose = PoseState.None;
        }
    }
    
    public void GetPoseState()
    {
        if((_openLeg.R_arm_flag == true &&
            _openLeg.L_arm_flag == true) ||
            (_openLeg.R_leg_flag == true &&
            _openLeg.L_leg_flag == true))
        {
            _Pose = PoseState.Open_leg;
            _scoreCount = true;
        }
        if((_shiko.R_arm_flag == true &&
            _shiko.L_arm_flag == true) ||
            (_shiko.R_leg_flag == true &&
            _shiko.L_leg_flag == true))
        {
            _Pose = PoseState.Chico;
            _scoreCount = true;
        }
        if((_eiffelt.R_arm_flag == true &&
            _eiffelt.L_arm_flag == true) ||
            (_eiffelt.R_leg_flag == true &&
            _eiffelt.L_leg_flag == true))
        {
            _Pose = PoseState.The_Eiffel_Tower;
            _scoreCount = true;
        }
        if((_alphaH.R_arm_flag == true &&
            _alphaH.L_arm_flag == true) ||
            (_alphaH.R_leg_flag == true &&
            _alphaH.L_leg_flag == true))
        {
            _Pose = PoseState.AlphaH;
            _scoreCount = true;
        }
        if((_alphaU.R_arm_flag == true &&
            _alphaU.L_arm_flag == true) ||
            (_alphaU.R_leg_flag == true &&
            _alphaU.L_leg_flag == true))
        {
            _Pose = PoseState.AlphaU;
            _scoreCount = true;
        }
        if((_alphaX.R_arm_flag == true &&
            _alphaX.L_arm_flag == true) ||
            (_alphaX.R_leg_flag == true &&
            _alphaX.L_leg_flag == true))
        {
            _Pose = PoseState.AlphaX;
            _scoreCount = true;
        }
        if((_exit.R_arm_flag == true &&
            _exit.L_arm_flag == true) ||
            (_exit.R_leg_flag == true &&
            _exit.L_leg_flag == true))
        {
            _Pose = PoseState.Exit;
            _scoreCount = true;
        }
        if((_big.R_arm_flag == true &&
            _big.L_arm_flag == true) ||
            (_big.R_leg_flag == true &&
            _big.L_leg_flag == true))
        {
            _Pose = PoseState.Sprawled;
            _scoreCount = true;
        }
        if((_happy.R_arm_flag == true &&
            _happy.L_arm_flag == true) ||
            (_happy.R_leg_flag == true &&
            _happy.L_leg_flag == true))
        {
            _Pose = PoseState.Joy;
            _scoreCount = true;
        }
    }
    // 全身ポーズの判定
    public void GetPose()
    {
        /*上半身、下半身のポーズが是のとき、全身でのポーズのフラグを是に*/
        if(_openLeg.L_arm_flag == true &&
            _openLeg.R_arm_flag == true &&
            _openLeg.L_leg_flag == true &&
            _openLeg.R_leg_flag == true)
        {
            _ScoreWhole = true;
        }
        if(_shiko.L_arm_flag == true &&
            _shiko.R_arm_flag == true &&
            _shiko.L_leg_flag == true &&
            _shiko.R_leg_flag == true)
        {
            _ScoreWhole = true;
        }
        if (_eiffelt.L_arm_flag == true &&
            _eiffelt.R_arm_flag == true &&
            _eiffelt.L_leg_flag == true &&
            _eiffelt.R_leg_flag == true)
        {
            _ScoreWhole = true;
        }
        if (_alphaH.L_arm_flag == true &&
            _alphaH.R_arm_flag == true &&
            _alphaH.L_leg_flag == true &&
            _alphaH.R_leg_flag == true)
        {
            _ScoreWhole = true;
        }
        if (_alphaU.L_arm_flag == true &&
            _alphaU.R_arm_flag == true &&
            _alphaU.L_leg_flag == true &&
            _alphaU.R_leg_flag == true)
        {
            _ScoreWhole = true;
        }
        if (_alphaX.L_arm_flag == true &&
            _alphaX.R_arm_flag == true &&
            _alphaX.L_leg_flag == true &&
            _alphaX.R_leg_flag == true)
        {
            _ScoreWhole = true;
        }
        if (_exit.L_arm_flag == true &&
            _exit.R_arm_flag == true &&
            _exit.L_leg_flag == true &&
            _exit.R_leg_flag == true)
        {
            _ScoreWhole = true;
        }
        if (_big.L_arm_flag == true &&
            _big.R_arm_flag == true &&
            _big.L_leg_flag == true &&
            _big.R_leg_flag == true)
        {
            _ScoreWhole = true;
        }
        if (_happy.L_arm_flag == true &&
            _happy.R_arm_flag == true &&
            _happy.L_leg_flag == true &&
            _happy.R_leg_flag == true)
        {
            _ScoreWhole = true;
        }
    }

    // 両腕ポーズの判定
    public void GetUpperPose()
    {
        /* 両腕の判定が是のとき、上半身ポーズのフラグを是に*/
        if(_openLeg.R_arm_flag == true &&
            _openLeg.L_arm_flag == true)
        {
            _ScoreUpper = true;
        }
        if (_shiko.R_arm_flag == true &&
            _shiko.L_arm_flag == true)
        {
            _ScoreUpper = true;
        }
        if (_eiffelt.R_arm_flag == true &&
            _eiffelt.L_arm_flag == true)
        {
            _ScoreUpper = true;
        }
        if (_alphaH.R_arm_flag == true &&
            _alphaH.L_arm_flag == true)
        {
            _ScoreUpper = true;
        }
        if (_alphaU.R_arm_flag == true &&
            _alphaU.L_arm_flag == true)
        {
            _ScoreUpper = true;
        }
        if (_alphaX.R_arm_flag == true &&
            _alphaX.L_arm_flag == true)
        {
            _ScoreUpper = false;
        }
        if (_exit.R_arm_flag == true &&
            _exit.L_arm_flag == true)
        {
            _ScoreUpper = true;
        }
        if (_big.R_arm_flag == true &&
            _big.L_arm_flag == true)
        {
            _ScoreUpper = true;
        }
        if (_happy.R_arm_flag == true &&
            _happy.L_arm_flag == true)
        {
            _ScoreUpper = true;
        }
    }

    // 両足ポーズの判定
    public void GetLowerPose()
    {
        /*両足の判定は是のとき、下半身ポーズのフラグを是に*/
        if(_openLeg.R_leg_flag == true &&
            _openLeg.L_leg_flag == true)
        {
            _ScoreLower = true;
        }
        if (_shiko.R_leg_flag == true &&
            _shiko.L_leg_flag == true)
        {
            _ScoreLower = true;
        }
        if (_eiffelt.R_leg_flag == true &&
            _eiffelt.L_leg_flag == true)
        {
            _ScoreLower = true;
        }
        if (_alphaH.R_leg_flag == true &&
            _alphaH.L_leg_flag == true)
        {
            _ScoreLower = true;
        }
        if (_alphaU.R_leg_flag == true &&
            _alphaU.L_leg_flag == true)
        {
            _ScoreLower = true;
        }
        if (_alphaX.R_leg_flag == true &&
            _alphaX.L_leg_flag == true)
        {
            _ScoreLower = true;
        }
        if (_exit.R_leg_flag == true &&
            _exit.L_leg_flag == true)
        {
            _ScoreLower = true;
        }
        if (_big.R_leg_flag == true &&
            _big.L_leg_flag == true)
        {
            _ScoreLower = true;
        }
        if (_happy.R_leg_flag == true &&
            _happy.L_leg_flag == true)
        {
            _ScoreLower = true;
        }
    }
    public void Additional_score(int Value)
    {
        _Score += Value;
        _view.View(Value);
        _view.TotalView(_Score);
    }

    // enum ポーズの状態によりスコア加算
    public void Pose()
    {
        switch (_Pose)
        {
            case PoseState.Pose_of_Turbulent_hawk:
                if(_ScoreWhole == true)
                {
                    Additional_score(3000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                } 
                else if(_ScoreUpper == true)
                {
                    Additional_score(40);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if(_ScoreLower == true)
                {
                    Additional_score(25);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Muay_thai:
                if(_ScoreWhole == true)
                {
                    Additional_score(2000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if(_ScoreUpper == true)
                {
                    Additional_score(30);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if(_ScoreLower == true)
                {
                    Additional_score(25);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Open_leg:
                if(_ScoreWhole == true)
                {
                    Additional_score(2500);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if(_ScoreUpper == true)
                {
                    Additional_score(35);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if(_ScoreLower == true)
                {
                    Additional_score(50);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Chico:
                if(_ScoreWhole == true)
                {
                    Additional_score(3000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if(_ScoreUpper == true)
                {
                    Additional_score(60);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if(_ScoreLower == true)
                {
                    Additional_score(40);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.The_Eiffel_Tower:
                if(_ScoreWhole == true)
                {
                    Additional_score(1500);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if(_ScoreUpper == true)
                {
                    Additional_score(35);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if(_ScoreLower == true)
                {
                    Additional_score(40);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Banana:
                if(_ScoreWhole == true)
                {
                    Additional_score(1500);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if(_ScoreUpper == true)
                {
                    Additional_score(20);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if(_ScoreLower == true)
                {
                    Additional_score(25);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.AlphaF:
                if (_ScoreWhole == true)
                {
                    Additional_score(3000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(45);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(45);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.AlphaH:
                if (_ScoreWhole == true)
                {
                    Additional_score(2500);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(50);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(50);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.AlphaJ:
                if (_ScoreWhole == true)
                {
                    Additional_score(2000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(35);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(25);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.AlphaK:
                if (_ScoreWhole == true)
                {
                    Additional_score(2500);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(40);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(40);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.AlphaN:
                if (_ScoreWhole == true)
                {
                    Additional_score(4000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(60);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(60);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.AlphaR:
                if (_ScoreWhole == true)
                {
                    Additional_score(3500);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(40);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(40);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.AlphaU:
                if (_ScoreWhole == true)
                {
                    Additional_score(3500);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(70);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(70);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.AlphaX:
                if (_ScoreWhole == true)
                {
                    Additional_score(2000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(40);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(40);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Yoga_1:
                if (_ScoreWhole == true)
                {
                    Additional_score(5000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(20);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(15);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Yoga_2:
                if (_ScoreWhole == true)
                {
                    Additional_score(1000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(35);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(40);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Yoga_3:
                if (_ScoreWhole == true)
                {
                    Additional_score(8500);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(85);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(85);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Bodybuilding:
                if (_ScoreWhole == true)
                {
                    Additional_score(1500);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(20);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(10);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Frog:
                if (_ScoreWhole == true)
                {
                    Additional_score(2500);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(50);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(50);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Gymnastics:
                if (_ScoreWhole == true)
                {
                    Additional_score(2000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(45);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(45);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Exit:
                if (_ScoreWhole == true)
                {
                    Additional_score(3000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(45);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(45);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Sophomoric_1:
                if (_ScoreWhole == true)
                {
                    Additional_score(2000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(30);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(20);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Sophomoric_2:
                if (_ScoreWhole == true)
                {
                    Additional_score(2500);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(45);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(20);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Conveni:
                if (_ScoreWhole == true)
                {
                    Additional_score(3500);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(30);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(50);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Yell:
                if (_ScoreWhole == true)
                {
                    Additional_score(2000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(20);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(40);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Bend_forward:
                if (_ScoreWhole == true)
                {
                    Additional_score(5000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(70);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(60);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Sprawled:
                if (_ScoreWhole == true)
                {
                    Additional_score(2500);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(50);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(40);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Race_walking:
                if (_ScoreWhole == true)
                {
                    Additional_score(3000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(45);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(20);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Joy:
                if (_ScoreWhole == true)
                {
                    Additional_score(2000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(55);
                     _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(25);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
            case PoseState.Deformed:
                if (_ScoreWhole == true)
                {
                    Additional_score(1000);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreUpper == true)
                {
                    Additional_score(1);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else if (_ScoreLower == true)
                {
                    Additional_score(1);
                    _scoreCount = false;
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                break;
        }
    }
}