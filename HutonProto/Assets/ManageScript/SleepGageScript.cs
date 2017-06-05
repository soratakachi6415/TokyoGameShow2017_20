using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepGageScript : MonoBehaviour {
    
    public GameObject sleepImage;
    private GameObject[] _origin;

    public int sleepPoint; 　//快眠ゲージのポイント数

    private int healCnt;　　//快眠ゲージが回復し始める時間
    private int Cnt;
    private float a_color;
    private bool hit;　　

	// Use this for initialization
	void Start () {

        healCnt = 0;
        Cnt = 0;
        a_color = 1;
        hit = false;


        _origin = new GameObject[10];

        //快眠ゲージの数
        for (int i = 1;i < sleepPoint;i++)
        {
            _origin[i] = Instantiate(sleepImage) as GameObject;
            _origin[i].transform.localPosition = new Vector3(sleepImage.transform.position.x + i * 105,sleepImage.transform.position.y,sleepImage.transform.position.z);
            _origin[i].transform.SetParent(sleepImage.transform.parent);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (hit)　　//点滅処理
        {
            if(Cnt % 2 == 0 && Cnt < 20)
            {
                if(a_color == 0.5f) a_color = 1.0f;

                else if(a_color == 1.0f) a_color =  0.5f;
            }
            else if(Cnt == 20)
            {
                a_color = 1.0f;
                hit = false;
                Cnt = 0;
            }
            Cnt++;
        }

        //画像の透明度設定
        if(sleepImage != null)sleepImage.GetComponent<Image>().color = new Color(255,255,255,a_color);
        for (int i = 1; i < sleepPoint; i++)
        {
            _origin[i].GetComponent<Image>().color = new Color(255, 255, 255, a_color);
        }
        
        healSleepPoint();
    }


    public void hitEnemy(bool soundPlay)　　//プレイヤーと敵が衝突時の処理
    {
        if (sleepPoint > 0 && !hit)
        {
            sleepPoint--;
            drawImage();
            hit = true;
            //羊の鳴き声
            if (soundPlay) GameObject.Find("SoundController").GetComponent<SoundsManager>().SheepCry();
        }
    }


    public void healSleepPoint()  //回復処理
    {
        if (sleepPoint < 10 && sleepImage != null)
        {
            if (healCnt >= 600)
            {
                sleepPoint++;
                drawImage();
                healCnt = 0;
            }
            healCnt++;
        }
    }

    public void drawImage()  // 衝突時と回復時の描画処理
    {
        for(int i = 1;i < 10;i++)  //一度全てのインスタンスを削除
        {
            if (_origin[i] != null)
            {
                Destroy(_origin[i]);
            }
        }

        for (int i = 1; i < sleepPoint; i++) //ポイント数に応じて羊を再描画
        {
            _origin[i] = Instantiate(sleepImage) as GameObject;
            _origin[i].transform.localPosition = new Vector3(sleepImage.transform.position.x + i * 105, sleepImage.transform.position.y, sleepImage.transform.position.z);
            _origin[i].transform.SetParent(sleepImage.transform.parent); //親子関係を作る
        }

        if(sleepPoint <= 0)  //ポイントがゼロになったら画像の親を削除
        {
            if (sleepImage != null)
            {
                Destroy(sleepImage);
                sleepImage = null;
            }
        }
    }
}
