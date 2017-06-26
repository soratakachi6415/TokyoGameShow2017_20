using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    //ゲームシーン中でのやること
    //時間制限まで堪え切れれば成功
    //制限時間ないにポイントが0になったら失敗
    //ゲーム中のシーン遷移
        //成功ならリザルトシーンへ遷移
        //失敗ならゲームオーバー画面を表示
    //
    public Scene_manager scenemanager_;
    public SleepGageScript sleepGageScript_;
    public Clock clock_;
    //成功判定で使う時間
    public int currentClocktime_;
    //しっぱい判定で使うポイント
    public int currentsheepnum_;
    //アラームが鳴り続ける時間
    private AudioClip alarmtime_;

    //ゲーム結果が失敗してないならtrue,失敗したらfalse
    public bool gameSuccess=true;
    //アラームがなってる時間
    public float alarmtime;
    

    //ゲーム開始前かゲーム中かゲーム終了してるか
    public enum Gamestatus
    {
        Play_bfor,
        Play_now,
        Play_after
    }
    public Gamestatus Gamestatus_;

    void Start()
    {
        sleepGageScript_= GameObject.Find("ScriptController").GetComponent<SleepGageScript>();
        clock_          = GameObject.Find("Clock").GetComponent<Clock>();
        scenemanager_   = GameObject.FindGameObjectWithTag("Scenemanager").GetComponent<Scene_manager>();
        alarmtime_      = GameObject.Find("SoundController").GetComponent<SoundsManager>().alarm;
        Gamestatus_ = Gamestatus.Play_bfor;
    }

    void Update()
    {
        currentClocktime_ = clock_.hour;
        currentsheepnum_ = sleepGageScript_.sleepPoint;

        //時間まで羊が０にならなかった場合
        if (currentClocktime_ <= 0)
        {
            Gamestatus_ = Gamestatus.Play_after;
            OnSuccess();
        }
        
        //０になった場合
        if (currentsheepnum_<=0)
        {
            Gamestatus_ = Gamestatus.Play_after;
            OnFailure();           
        }

        //音が鳴り終わったらシーン遷移
        if (alarmtime <= 0)
        {
            Scenenext();
        }
    }

    //成功した場合のシーン遷移
    public void Scenenext()
    {
        scenemanager_.NextScene();
    }

    //成功した場合
    public void OnSuccess()
    {
        //Resultへ遷移
        //アラームが鳴り終わったらシーン遷移
        alarmtime -= 1.0f * Time.deltaTime;       
    }
    
    //失敗した場合
    public void OnFailure()
    {
        //Titleへ遷移      
        gameSuccess = false;
    }
    //ゲーム開始
    public void GameStart()
    {
        Gamestatus_ = Gamestatus.Play_now;
        clock_.clockStart();
    }
}