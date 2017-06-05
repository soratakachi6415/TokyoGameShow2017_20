using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fademanager : MonoBehaviour
{
    /*フェードの管理*/
    //テキスト
    public Image fadeimage;
    //カラーR,G,B
    private float r, g, b;
    //α値
    public float a;
    //Fadeの遷移状態の確認用
    public string Fadestatus;
    public enum Fade_status
    {
        FADE_IN,
        FADE_OUT
    }
    public Fade_status fade_image = Fade_status.FADE_OUT;

    void Start()
    {
        //参照先：アタッチしたオブジェクトのテキストを所得する
        fadeimage = gameObject.GetComponent<Image>();
        //色の所得
        r = fadeimage.GetComponent<Image>().color.r;
        g = fadeimage.GetComponent<Image>().color.g;
        b = fadeimage.GetComponent<Image>().color.b;
        a = fadeimage.GetComponent<Image>().color.a;
    }
    void Update()
    {
        //現在の状態確認
        Fadestatus = System.Enum.GetName(typeof(Fade_status), fade_image);
        //色の更新
        fadeimage.GetComponent<Image>().color = new Color(r, g, b, a);
        /*透明度が０～１の間になるように制限*/
        if (a < 0)
        {
            a = 0;
        }
        else if (a > 1.0f)
        {
            a = 1.0f;
        }

        //フェードアウト開始
        if (fade_image == Fade_status.FADE_OUT)
        {
            a -= 0.03f;
        }
        //フェードイン開始
        else if (fade_image == Fade_status.FADE_IN)
        {
            a += 0.03f;
        }
    }
    //フェードイン開始
    public void fadein()
    {
        fade_image = Fade_status.FADE_IN;
    }
    //フェードアウト開始
    public void fadeout()
    {
        fade_image = Fade_status.FADE_OUT;
    }
}