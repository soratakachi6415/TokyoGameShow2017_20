using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public void Start()
    {
        SceneManager.sceneLoaded += OnLoadScene;
    }

    private void OnLoadScene(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Title")
        {
            _score = 0;
            _totalscore = 0;
        }
    }

    //Update is called once per frame
    void Update()
    {
    }


}
