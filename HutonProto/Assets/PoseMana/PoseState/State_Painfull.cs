using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State_Painfull : MonoBehaviour {

    public PoseManager _pose_manager;

    /****ポーズオブジェクトの取得****/
    // 開脚のオブジェクトを取得
    public Pose_painfullpose1 _pose_painfullpose1;

    /**** チャンスに使うパラメータの取得****/
    private bool _pose_upper = false;
    private bool _pose_lower = false;
    public Image _Chance;
    private float r, g, b, alpha;

    public GameObject _rShoulder;
    public GameObject _lCrotch;
    

    // Use this for initialization
    void Start()
    {
        _pose_manager = GameObject.FindGameObjectWithTag("Posemanager").GetComponent<PoseManager>();
        _pose_painfullpose1 = GameObject.Find("Pose_PainfulPose1").GetComponent<Pose_painfullpose1>();

        _Chance = GameObject.Find("Chance").GetComponent<Image>();
        r = _Chance.GetComponent<Image>().color.r;
        g = _Chance.GetComponent<Image>().color.g;
        b = _Chance.GetComponent<Image>().color.b;
        alpha = _Chance.GetComponent<Image>().color.a;

        _rShoulder = GameObject.Find("Player_RightHand1");
        _lCrotch = GameObject.Find("Player_LeftLeg1");
        
    }

    // Update is called once per frame
    void Update()
    {
        _Chance.GetComponent<Image>().color = new Color(r, g, b, alpha);
        CheckChance();
        FlagCheck();
    }

    public void CheckChance()
    {
        /***関節が2つ以上当てはまったとき表示***/
        if (_pose_painfullpose1.R_arm_flag == true)
        {
            _pose_upper = true;
            if (_pose_painfullpose1.L_arm_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_painfullpose1.R_leg_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_painfullpose1.L_leg_flag == true)
            {
                ChanceDisplayTrue();
            }
            else
            {
                ChanceDisplayFalse();
            }
        }
        if (_pose_painfullpose1.L_arm_flag == true)
        {
            _pose_upper = true;
            if (_pose_painfullpose1.R_arm_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_painfullpose1.R_leg_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_painfullpose1.L_leg_flag == true)
            {
                ChanceDisplayTrue();
            }
            else
            {
                ChanceDisplayFalse();
            }
        }
        if (_pose_painfullpose1.R_leg_flag == true)
        {
            _pose_lower = true;
            if (_pose_painfullpose1.L_leg_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_painfullpose1.R_arm_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_painfullpose1.L_arm_flag == true)
            {
                ChanceDisplayTrue();
            }
            else
            {
                ChanceDisplayFalse();
            }
        }
        if (_pose_painfullpose1.L_leg_flag == true)
        {
            _pose_lower = true;
            if (_pose_painfullpose1.R_leg_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_painfullpose1.R_arm_flag == true)
            {
                ChanceDisplayTrue();
            }
            else if (_pose_painfullpose1.L_arm_flag == true)
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
        if ((_pose_painfullpose1.R_arm_flag == true &&
            _pose_painfullpose1.L_arm_flag == true) ||
            (_pose_painfullpose1.R_leg_flag == true &&
            _pose_painfullpose1.L_leg_flag == true))
        {
            _pose_manager.Change_state(PoseManager.PoseState.Painfull1);
        }
        /*上半身、下半身のポーズが是のとき、全身でのポーズのフラグを是に*/
        if (_pose_manager._ScoreUpper == true &&
            _pose_manager._ScoreLower == true)
        {
            _pose_manager._ScoreWhole = true;
        }
        /* 両腕の判定が是のとき、上半身ポーズのフラグを是に*/
        if (_pose_painfullpose1.R_arm_flag == true &&
            _pose_painfullpose1.L_arm_flag == true)
        {
            _pose_manager._ScoreUpper = true;
        }
        /*両足の判定は是のとき、下半身ポーズのフラグを是に*/
        if (_pose_painfullpose1.R_leg_flag == true &&
            _pose_painfullpose1.L_leg_flag == true)
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
