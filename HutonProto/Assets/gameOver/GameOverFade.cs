using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverFade : MonoBehaviour
{
    /*ゲームオーバーで使うもの*/
    //ゲームオーバー背景
    public GameObject GameoverBG;
    public GameObject GameoverImage;
    /*************************/

    private float GameOver_r;
    private float GameOver_g;
    private float GameOver_b;
    public float GameOverBG_a;
    public  float GameOverImage_a;

    //
    public GameSceneManager gameManager;
    public bool gameSuccess;
    public float fadespeed;
    public GameObject GameOverbotton;
    // Use this for initialization
    void Start()
    {
        //ゲームオーバー初期か
        GameoverBG = GameObject.Find("GameOverBG");
        GameoverImage = GameObject.Find("GameOverImage");
        //
        GameOver_r = GameoverBG.GetComponent<Image>().color.r;
        GameOver_g = GameoverBG.GetComponent<Image>().color.g;
        GameOver_b = GameoverBG.GetComponent<Image>().color.b;
        GameOverBG_a = GameoverBG.GetComponent<Image>().color.a;
        GameOverImage_a = GameoverImage.GetComponent<Image>().color.a;
        //
        gameManager = GameObject.Find("GameSceneManager").GetComponent<GameSceneManager>();
        //
        GameOverbotton = GameObject.Find("GameOverButton");
        GameOverbotton.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        gameSuccess = gameManager.gameSuccess;

        GameoverBG.GetComponent<Image>().color = new Color(GameOver_r, GameOver_g, GameOver_b, GameOverBG_a);
        GameoverImage.GetComponent<Image>().color = new Color(1, 1, 1, GameOverImage_a);

        //背景表示
        if (gameSuccess == false)
        {
            GameOverBG_a += fadespeed * Time.deltaTime;
        }

        //ロゴ表示
        if (GameOverBG_a >= 0.5)
        {
            GameOverImage_a += fadespeed * Time.deltaTime;
        }

        if(GameOverImage_a >= 1.0f)
        {
            GameOverbotton.SetActive(true);
        }
    }

    //ゲームオーバー画面処理
    public void retutnTitle()
    {
        SceneManager.LoadSceneAsync("BaseScene");
    }
}