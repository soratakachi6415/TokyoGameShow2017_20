using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour {

    public AudioClip sheepCry;
    public AudioClip walkFloor;
    public AudioClip alarm;
    public AudioClip titleBGM;
    public AudioClip gamePlayBGM;

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
        print("BGM再生");
    }

    public void GamePlayBGM()   //ゲーム中BGM
    {
        audioSource.PlayOneShot(gamePlayBGM);
        print("BGM再生");
    }
}
