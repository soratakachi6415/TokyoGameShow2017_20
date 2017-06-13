using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private float timer = 0;
    //時針を動かすフラグ
    private bool longflag = false;
    //残り時間のカウンター
    public int hour = 6;
    //時計開始のフラグtrueで開始
    [SerializeField]
    private bool ClockStart = false;

    //サウンド
    private SoundsManager soundsManager;


    void Start()
    {
        //Object検索
        Minutehand = GameObject.Find("Minutehand");
        Shorthand = GameObject.Find("Shorthand");
        soundsManager = GameObject.FindGameObjectWithTag("SoundsManager").GetComponent<SoundsManager>();
        ClockStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Z軸所得
        LongangleZ = Minutehand.transform.localEulerAngles.z;
        ShortanglZ = Shorthand.transform.localEulerAngles.z;

        //カウントダウン開始
        if (ClockStart == true)
        {
            //時間が進む
            timer += 1 * Time.deltaTime;

            //針を動かす
            if (timer >= 20)
            {
                timer = 0;
                longflag = true;
                hour--;
            }
            //分針     
            Minutehand.transform.eulerAngles += new Vector3(0f, 0f, -1.0f) * Time.deltaTime * 18;

            //時針
            if (longflag == true)
            {
                Shorthand.transform.eulerAngles += new Vector3(0f, 0f, -30.0f);
                longflag = false;
            }

            //制限時間終了時
            if (hour == 0 && timer == 0)
            {
                soundsManager.Alarm();
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