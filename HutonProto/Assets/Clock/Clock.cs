using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    //分針の画像オブジェクト
    private GameObject Minutehand;
    //分針のオブジェクトのZ軸
    private float LongangleZ;
    //時針の画像オブジェクト
    private GameObject Shorthand;
    //時針のオブジェクトのZ軸
    private float ShortanglZ;
    //タイマー
    public float timer = 0;
    //残り時間のカウンター
    public int hour = 6;
    //時計開始のフラグtrueで開始
    [SerializeField]
    public bool ClockStart = false;
    //サウンド
    private SoundsManager soundsManager;
    //時計版の色変更
    private Image clockimage;
    private float clockColor_r, clockColor_g, clockColor_b, clockColor_a;
    private float colorChange;

    void Start()
    {
        //Object検索
        Minutehand = GameObject.Find("Minutehand");
        Shorthand = GameObject.Find("Shorthand");
        soundsManager = GameObject.FindGameObjectWithTag("SoundsManager").GetComponent<SoundsManager>();
        ClockStart = false;
        clockimage = GameObject.Find("clockBG").GetComponent<Image>();
        //時計の版
        clockColor_r = clockimage.GetComponent<Image>().color.r;
        clockColor_g = clockimage.GetComponent<Image>().color.g;
        clockColor_b = clockimage.GetComponent<Image>().color.b;
        clockColor_a= clockimage.GetComponent<Image>().color.a;
    }

    void Update()
    {
        //Z軸所得
        LongangleZ = Minutehand.transform.localEulerAngles.z;
        ShortanglZ = Shorthand.transform.localEulerAngles.z;
        //時計盤の色取得
        clockimage.GetComponent<Image>().color = new Color(clockColor_r, clockColor_g, clockColor_b,clockColor_a);

        //カウントダウン開始
        if (ClockStart == true)
        {
            //時間が進む
            timer += 1 * Time.deltaTime;

            //針を動かす
            if (timer >= 20)
            {
                timer = 0;
                hour--;
            }
            //分針     
            Minutehand.transform.eulerAngles += new Vector3(0f, 0f, -1.0f) * Time.deltaTime * 18;
            //時針
            Shorthand.transform.eulerAngles += new Vector3(0f, 0f, -1.5f) * Time.deltaTime * 1;

            //制限時間終了時
            if (hour == 0 && timer == 0)
            {
                soundsManager.Alarm();
            }

            //点滅時の色変更
            //1秒ごとの判別式
            colorChange = (int)timer % 2;

            //残り時間が1時間を切ったら点滅開始
            if (hour <= 1)
            {
                if (colorChange == 0)
                {
                    clockColor_r = 1.0f;
                    clockColor_g = 0.19f;
                    clockColor_b = 0f;
                    clockColor_a = 1f;
                }
                else
                {
                    clockColor_r = 1f;
                    clockColor_g = 1f;
                    clockColor_b = 1f;
                    clockColor_a = 1f;
                }
            }
        }
        else if (!ClockStart)
        {
            soundsManager.GamePlayBGM();
            ClockStart = true;
        }
    }

    //開始フラグ起動
    public void clockStart()
    {
        ClockStart = true;
    }
}