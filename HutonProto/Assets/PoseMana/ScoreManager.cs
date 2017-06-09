using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 獲得するスコア
    public static int _score;
    // 合計スコア
    public static int _totalscore;

    public void Awake()
    {
        _score = 0;
        _totalscore = 0;
    }

    //Update is called once per frame
    void Update()
    {
        //Debug.Log(_totalscore);
    }
}
