using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour {

    public AudioClip sheepCry;
    public AudioClip walkFloor;
    public AudioClip alarm;
    public AudioClip titleBGM;
    public AudioClip gamePlayBGM;
    public AudioClip popUp;
    public AudioClip bellSweep;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void SheepCry()      //羊が消えたとき(敵エネミーに殴られたとき)
    {
        audioSource.PlayOneShot(sheepCry);
    }

    public void WalkFloor()     //敵エネミ―が再出現するとき
    {
        audioSource.PlayOneShot(walkFloor);
    }

    public void Alarm()         //制限時間終了時に目覚まし音
    {
        audioSource.PlayOneShot(alarm);
    }

    public void TitlePlayBGM()  //タイトルBGM
    {
        audioSource.PlayOneShot(titleBGM);
    }

    public void GamePlayBGM()   //ゲーム中BGM
    {
        audioSource.PlayOneShot(gamePlayBGM);
    }

    public void PopUp()       //ポップアップ音
    {
        audioSource.PlayOneShot(popUp);
    }

    public void BellSweep()   //ベル上昇音
    {
        audioSource.PlayOneShot(bellSweep);
    }
}
