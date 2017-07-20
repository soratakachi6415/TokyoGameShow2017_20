using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State_Banana : MonoBehaviour {
    public PoseManager _pose_manager;

    /****ポーズオブジェクトの取得****/
    // バナナのオブジェクトを取得
    public Pose_Banana _pose_banana;
    public Image _rogo_banana;
    private float rogo_r, rogo_g, rogo_b, rogo_a;
    public static bool _rogo_flag = false;

    /**** チャンスに使うパラメータの取得****/
    private bool _pose_upper = false;
    private bool _pose_lower = false;
    public Image _Chance;
    private float r, g, b, alpha;

    private GameObject _rShoulder;
    private GameObject _lCrotch;
    private GameObject _lShoulder;
    private GameObject _rCrotch;

    public GameObject _score;
    public GameObject _point;
    // Use this for initialization
    void Start () {
        _pose_manager = GameObject.FindGameObjectWithTag("Posemanager").GetComponent<PoseManager>();
        _pose_banana = GameObject.Find("Pose_Banana").GetComponent<Pose_Banana>();
        _rogo_banana = GameObject.Find("Rogo_Banana").GetComponent<Image>();
        rogo_r = _rogo_banana.GetComponent<Image>().color.r;
        rogo_g = _rogo_banana.GetComponent<Image>().color.g;
        rogo_b = _rogo_banana.GetComponent<Image>().color.b;
        rogo_a = _rogo_banana.GetComponent<Image>().color.a;

        _Chance = GameObject.Find("Chance").GetComponent<Image>();
        r = _Chance.GetComponent<Image>().color.r;
        g = _Chance.GetComponent<Image>().color.g;
        b = _Chance.GetComponent<Image>().color.b;
        alpha = _Chance.GetComponent<Image>().color.a;

        _rShoulder  = GameObject.FindGameObjectWithTag("Player_mixamorig:RightArm");
        _lCrotch      = GameObject.FindGameObjectWithTag("Player_mixamorig:LeftUpLeg");
        _lShoulder  = GameObject.FindGameObjectWithTag("Player_mixamorig:LeftArm");
        _rCrotch     = GameObject.FindGameObjectWithTag("Player_mixamorig:RightArm");

        _score = (GameObject)Resources.Load("Prefab/ScoreImage");
        _point = (GameObject)Resources.Load("Prefab/Point");
    }

    // Update is called once per frame
    void Update()
    {
        _Chance.GetComponent<Image>().color = new Color(r, g, b, alpha);
        _rogo_banana.GetComponent<Image>().color = new Color(rogo_r, rogo_g, rogo_b, rogo_a);
        CheckChance();
        FlagCheck();
        if(_rogo_flag == true)
        {
            RogoDraw();
        }
        else
        {
            RogoHide();
        }
    }

    public void CheckChance()
    {
        /***関節が2つ以上当てはまったとき表示***/
        if (_pose_banana.R_arm_flag == true)
        {
            _pose_upper = true;
            if (_pose_banana.L_arm_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_banana.R_leg_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_banana.L_leg_flag == true)
            {
                ChanceDisplayTrue();
            }
            else
            {
                ChanceDisplayFalse();
            }
        }
        if (_pose_banana.L_arm_flag == true)
        {
            _pose_upper = true;
            if (_pose_banana.R_arm_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_banana.R_leg_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_banana.L_leg_flag == true)
            {
                ChanceDisplayTrue();
            }
            else
            {
                ChanceDisplayFalse();
            }
        }
        if (_pose_banana.R_leg_flag == true)
        {
            _pose_lower = true;
            if (_pose_banana.L_leg_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_banana.R_arm_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_banana.L_arm_flag == true)
            {
                ChanceDisplayTrue();
            }
            else
            {
                ChanceDisplayFalse();
            }
        }
        if (_pose_banana.L_leg_flag == true)
        {
            _pose_lower = true;
            if (_pose_banana.R_leg_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_banana.R_arm_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_banana.L_arm_flag == true)
            {
                ChanceDisplayTrue();
            }
            else
            {
                ChanceDisplayFalse();
            }
        }
    }
    public void FlagCheck()
    {
        if ((_pose_banana.R_arm_flag == true &&
            _pose_banana.L_arm_flag == true) ||
            (_pose_banana.R_leg_flag == true &&
            _pose_banana.L_leg_flag == true))
        {
            _pose_manager.Change_state(PoseManager.PoseState.Banana);
        }
        /*上半身、下半身のポーズが是のとき、全身でのポーズのフラグを是に*/
        if (_pose_manager._ScoreUpper == true &&
            _pose_manager._ScoreLower == true)
        {
            _pose_manager._ScoreWhole = true;
        }
        /* 両腕の判定が是のとき、上半身ポーズのフラグを是に*/
        if (_pose_banana.R_arm_flag == true &&
            _pose_banana.L_arm_flag == true)
        {
            _pose_manager._ScoreUpper = false;
        }
        /*両足の判定は是のとき、下半身ポーズのフラグを是に*/
        if (_pose_banana.R_leg_flag == true &&
            _pose_banana.L_leg_flag == true)
        {
            _pose_manager._ScoreLower = true;
        }
    }

    public static void Additional_score(int Value)
    {
        var _audio = GameObject.FindGameObjectWithTag("Posemanager").GetComponent<AudioSource>();
        var _View = GameObject.Find("ScoreCanvas").GetComponent<Canvas>();
        var _view = _View.GetComponent<ScoreView>();

        ScoreManager._score = Value;
        ScoreManager._totalscore += Value;
        _view.View(ScoreManager._score);
        _audio.PlayOneShot(_audio.clip);
    }

    public void RogoDraw()
    {
        if(_pose_upper == true)
        {
            Vector3 pos = _rCrotch.transform.position;
            Vector3 score_pos = new Vector3(pos.x, pos.y, pos.z);
        }
        rogo_a = 1.0f;
    }

    public void RogoHide()
    {
        rogo_a = 0.0f;
    }

    public void ChanceDisplayTrue()
    {
        /***チャンスの表示位置を決める***/
        // 右上腕の座標を取得する
        if (_pose_upper == true)
        {
            Vector3 pos = _rShoulder.transform.position;
            _Chance.transform.position = new Vector3(pos.x, pos.y + 5.0f, pos.z - 2.0f);
        }
        // 左太ももの座標を取得する
        if (_pose_lower == true)
        {
            Vector3 pos = _lCrotch.transform.position;
            _Chance.transform.position = new Vector3(pos.x, pos.y + 5.0f, pos.z + 2.0f);
        }
        alpha = 1.0f;
    }

    public void ChanceDisplayFalse()
    {
        alpha = 0.0f;
    }
}
