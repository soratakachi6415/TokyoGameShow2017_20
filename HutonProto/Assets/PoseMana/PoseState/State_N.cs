using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State_N : MonoBehaviour {

    public PoseManager _posemanager;

    /****ポーズオブジェクトの取得****/
    // 開脚のオブジェクトを取得
    private Image _AlphaN;
    public Pose_N _alphaN;

    public bool _chance = false;
    public Image _Chance;
    private float r, g, b, alpha;

    public float _fadetime;
    private float _fotime;
    private float _fitime;

    // Use this for initialization
    void Start()
    {
        _posemanager = GameObject.FindGameObjectWithTag("Posemanager").GetComponent<PoseManager>();
        _AlphaN = GameObject.Find("Pose_N").GetComponent<Image>();
        _alphaN = _AlphaN.GetComponent<Pose_N>();

        _Chance = GameObject.Find("Chance").GetComponent<Image>();
        r = _Chance.GetComponent<Image>().color.r;
        g = _Chance.GetComponent<Image>().color.g;
        b = _Chance.GetComponent<Image>().color.b;
        alpha = _Chance.GetComponent<Image>().color.a;

        _fadetime = 1.5f;
        _fotime = 0.0f;
        _fitime = _fadetime;
    }

    // Update is called once per frame
    void Update()
    {
        _Chance.GetComponent<Image>().color = new Color(r, g, b, alpha);
        if (_alphaN.L_arm_flag == true ||
            _alphaN.R_arm_flag == true ||
            _alphaN.L_leg_flag == true ||
            _alphaN.R_leg_flag == true)
        {
            _chance = true;
            ChanceDisPlayTrue();
        }
        else
        {
            _chance = false;
            ChanceDisPlayFalse();
        }
        FlagCheck();
    }
    public void FlagCheck()
    {
        if ((_alphaN.R_arm_flag == true &&
            _alphaN.L_arm_flag == true) ||
            (_alphaN.R_leg_flag == true &&
            _alphaN.L_leg_flag == true))
        {
            _posemanager._Pose = PoseManager.PoseState.AlphaN;
        }
        /*上半身、下半身のポーズが是のとき、全身でのポーズのフラグを是に*/
        if (_posemanager._ScoreUpper == true &&
            _posemanager._ScoreLower == true)
        {
            _posemanager._ScoreWhole = true;
        }
        /* 両腕の判定が是のとき、上半身ポーズのフラグを是に*/
        if (_alphaN.R_arm_flag == true &&
            _alphaN.L_arm_flag == true)
        {
            _posemanager._ScoreUpper = true;
        }
        /*両足の判定は是のとき、下半身ポーズのフラグを是に*/
        if (_alphaN.R_leg_flag == true &&
            _alphaN.L_leg_flag == true)
        {
            _posemanager._ScoreLower = true;
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
    public void ChanceDisPlayTrue()
    {
        alpha = 1.0f;
        //_fitime = _fadetime; // 初期化
        //_fotime += Time.deltaTime; // 時間更新(徐々に増やす
        //float alpha = _fotime / _fadetime; // 徐々に1に近づける
        //var color = _Chance.color; // 取得したImageのcolorを取得
        //color.a = alpha; // カラーのアルファ値を徐々に増やす
        //_Chance.color = color; // 取得したImageに適応させる
    }

    public void ChanceDisPlayFalse()
    {
        alpha = 0.0f;
        //_fotime = 0; // 初期化
        //_fitime -= Time.deltaTime; // 時間更新(徐々に減らす
        //float alpha = _fitime / _fadetime; // 徐々に0に近づける
        //var color = _Chance.color; // 取得したImageのcolorを取得
        //color.a = alpha; // カラーのアルファ値を徐々に減らす
        //_Chance.color = color; // 取得したImageに適応させる
    }
}
