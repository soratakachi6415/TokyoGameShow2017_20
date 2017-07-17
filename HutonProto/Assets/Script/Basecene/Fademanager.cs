using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fademanager : MonoBehaviour
{
    /*フェードの管理*/
    //イメージ
    public GameObject fadeimage_obj;
    public Image fadeimage;
    //α値
    public float a;
    //Fadeの遷移状態の確認用
    private string Fadestatus;
    //フェードイメージが存在しているか確認用
    private bool fadeimagecheck = false;
    //タップの入力
    public bool tapflag=false;

    public enum Fade_status
    {
        FADE_IN,
        FADE_OUT,
        FADE_IDLE
    }
    public Fade_status fade_image = Fade_status.FADE_IN;

    public string curentscene_;
    //public bool  

    void Start()
    {       
    }
    void Update()
    {
        //現在のシーン確認
        curentscene_ = GameObject.Find("SceneManager").GetComponent<Scene_manager>().currentscene;
        //現在の状態確認
        Fadestatus = System.Enum.GetName(typeof(Fade_status), fade_image);
        /*フェードイメージがある場合の処理*/
        if (GameObject.FindGameObjectWithTag("fadeImage") != null)
        {
            fadeimagecheck = true;
            fadeimage_obj = GameObject.FindGameObjectWithTag("fadeImage");
            fadeimage = fadeimage_obj.GetComponent<Image>();
            //色の更新
            fadeimage.GetComponent<Image>().color = new Color(0, 0, 0, a);
        }
        else
        {
            fadeimagecheck = false;
        }
      
        /*imageの透明度が０～１の間になるように制限*/
        if (a < 0)
        {
            a = 0;
        }
        else if (a > 1.0f)
        {
            a = 1.0f;
        }

        /*フェードイン、アウト開始*/
        //フェードアウト開始
        if (fade_image == Fade_status.FADE_OUT)
        {
            a -= 0.02f;
        }
        //フェードイン開始
        else if (fade_image == Fade_status.FADE_IN)
        {
            a += 0.02f;
        }
        
        
            /*アルファが０以下ならイメージオブジェクトのセットアクティブをファルスにする*/
            if (a <= 0.0f && fade_image == Fade_status.FADE_OUT)
            {
                FadeimageOffAwake();
                fade_image = Fade_status.FADE_IDLE;
            }
        
        if (curentscene_ == "Title")
        {
            fadeout();
        }
    }

    /*フェードの状態の変更*/
    //フェードの状態をフェードインに変える
    public void fadein()
    {
        fade_image = Fade_status.FADE_IN;
    }
    //フェードの状態をフェードアウトに変える
    public void fadeout()
    {
        fade_image = Fade_status.FADE_OUT;
    }

    /*フェードイメージのアウェイクのon,off*/
    public void FadeimageOnAwake()
    {
        fadeimage_obj.SetActive(true);
    }
    public void FadeimageOffAwake()
    {
        fadeimage_obj.SetActive(false);
    }

    /*フェード開始*/
    //フェードイン開始
    public void fadeinStart()
    {
        //セットアクティブをtrueにする。
        FadeimageOnAwake();
        //フェードの状態をフェードインに変える
        fadein();       
    }
    //フェードアウト開始
    public void fadeoutStart()
    {
        //フェードの状態をフェードアウトに変える
        fadeout();
    }
}