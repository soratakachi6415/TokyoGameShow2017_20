using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

/// <summary> フェードを管理するクラスです </summary>
[RequireComponent(typeof(Image))]
public class Fademanager : MonoBehaviour
{
    ///*フェードの管理*/
    ////イメージ
    //public GameObject fadeimage_obj;
    //public Image fadeimage;
    ////α値
    //public float a;
    ////Fadeの遷移状態の確認用
    //private string Fadestatus;
    ////フェードイメージが存在しているか確認用
    //private bool fadeimagecheck = false;
    ////タップの入力
    //public bool tapflag=false;

    //public enum Fade_status
    //{
    //    FADE_IN,
    //    FADE_OUT,
    //    FADE_IDLE
    //}
    //public Fade_status fade_image = Fade_status.FADE_IN;

    //public string curentscene_;
    ////public bool




    //private void Awake()
    //{

    //}

    //void Start()
    //{
    //}

    //public static void SceneChange()
    //{

    //}

    //void Update()
    //{
    //    //現在のシーン確認
    //    curentscene_ = GameObject.Find("SceneManager").GetComponent<Scene_manager>().currentscene;
    //    //現在の状態確認
    //    Fadestatus = System.Enum.GetName(typeof(Fade_status), fade_image);
    //    /*フェードイメージがある場合の処理*/
    //    if (GameObject.FindGameObjectWithTag("fadeImage") != null)
    //    {
    //        fadeimagecheck = true;
    //        fadeimage_obj = GameObject.FindGameObjectWithTag("fadeImage");
    //        fadeimage = fadeimage_obj.GetComponent<Image>();
    //        //色の更新
    //        fadeimage.GetComponent<Image>().color = new Color(0, 0, 0, a);
    //    }
    //    else
    //    {
    //        fadeimagecheck = false;
    //    }

    //    /*imageの透明度が０～１の間になるように制限*/
    //    if (a < 0)
    //    {
    //        a = 0;
    //    }
    //    else if (a > 1.0f)
    //    {
    //        a = 1.0f;
    //    }

    //    /*フェードイン、アウト開始*/
    //    //フェードアウト開始
    //    if (fade_image == Fade_status.FADE_OUT)
    //    {
    //        a -= 0.02f;
    //    }
    //    //フェードイン開始
    //    else if (fade_image == Fade_status.FADE_IN)
    //    {
    //        a += 0.02f;
    //    }


    //        /*アルファが０以下ならイメージオブジェクトのセットアクティブをファルスにする*/
    //        if (a <= 0.0f && fade_image == Fade_status.FADE_OUT)
    //        {
    //            FadeimageOffAwake();
    //            fade_image = Fade_status.FADE_IDLE;
    //        }

    //    if (curentscene_ == "Title")
    //    {
    //        fadeout();
    //    }
    //}

    ///*フェードの状態の変更*/
    ////フェードの状態をフェードインに変える
    //public void fadein()
    //{
    //    fade_image = Fade_status.FADE_IN;
    //}
    ////フェードの状態をフェードアウトに変える
    //public void fadeout()
    //{
    //    fade_image = Fade_status.FADE_OUT;
    //}

    ///*フェードイメージのアウェイクのon,off*/
    //public void FadeimageOnAwake()
    //{
    //    fadeimage_obj.SetActive(true);
    //}
    //public void FadeimageOffAwake()
    //{
    //    fadeimage_obj.SetActive(false);
    //}

    ///*フェード開始*/
    ////フェードイン開始
    //public void fadeinStart()
    //{
    //    //セットアクティブをtrueにする。
    //    FadeimageOnAwake();
    //    //フェードの状態をフェードインに変える
    //    fadein();
    //}
    ////フェードアウト開始
    //public void fadeoutStart()
    //{
    //    //フェードの状態をフェードアウトに変える
    //    fadeout();
    //}



    //簡易的なシングルトンの実装
    private static Fademanager instance;
    public  static Fademanager Instance
    {
        get { return instance; }
    }

    private Image image;
    private float speed = 1.0f;

    private bool isFade = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //2つ以上存在することになるので
            Destroy(gameObject);
        }

        image = GetComponent<Image>();
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void Update()
    {
        bool isCanTouch = (image.color.a == 0.0f);

        image.raycastTarget = !isCanTouch;
    }

    public void Initialize()
    {
        isFade = false;
        StopAllCoroutines();
        speed = 1.0f;
        SetColor(0.0f, 0.0f, 0.0f, 0.0f);
    }

    private void SceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Title")
        {
            instance = FindObjectOfType<Fademanager>();
        }
        instance.FadeOut(speed);
    }

    private void OnDestroy()
    {
        instance = null;
    }

    /// <summary> フェードインを開始します デフォルトは黒色 </summary>>
    public Coroutine FadeIn(float fadeSpeed = 1.0f)
    {
        if (isFade) { return null; }

        speed = fadeSpeed;
        return StartCoroutine(_FadeIn());
    }

    /// <summary> フェードインを開始します </summary>
    public Coroutine FadeIn(Color color, float fadeSpeed = 1.0f)
    {
        if (isFade) { return null; }

        speed = fadeSpeed;
        image.color = color;
        return StartCoroutine(_FadeIn());
    }

    /// <summary> フェードアウトを開始します デフォルトは黒色 </summary>>
    public Coroutine FadeOut(float fadeSpeed = 1.0f)
    {
        if (isFade) { return null; }

        speed = fadeSpeed;
        return StartCoroutine(_FadeOut());
    }

    /// <summary> フェードアウトを開始します </summary>
    public Coroutine FadeOut(Color color, float fadeSpeed = 1.0f)
    {
        if (isFade) { return null; }

        speed = fadeSpeed;
        image.color = color;
        return StartCoroutine(_FadeOut());
    }

    private IEnumerator _FadeIn()
    {
        isFade = true;
        SetAlpha(0.0f);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime * speed)
        {
            SetAlpha(t);
            yield return null;
        }
        SetAlpha(1.0f);
        isFade = false;
    }

    private IEnumerator _FadeOut()
    {
        isFade = true;
        SetAlpha(1.0f);
        for (float t = 1.0f; t > 0.0f; t -= Time.deltaTime * speed)
        {
            SetAlpha(t);
            yield return null;
        }
        SetColor(0.0f, 0.0f, 0.0f, 0.0f);
        isFade = false;
    }

    private void SetColor(float r, float g, float b, float a = 1.0f)
    {
        Color c = image.color;
         c.r = r;
         c.g = g;
         c.b = b;
         c.a = a;
        image.color = c;
    }

    private void SetAlpha(float a)
    {
        Color color = image.color;
        color.a = a;
        image.color = color;
    }
}
