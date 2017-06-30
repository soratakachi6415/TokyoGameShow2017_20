using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelselectManager : MonoBehaviour
{
    public GameObject Tutorial;
    public GameObject LevelSelect;

    public Scene_manager scenemanager_;

    //ゲームの難易度
    public enum GameLevel
    {
        Easy,
        Normal,
        Hard
    }
    public GameLevel gameLevel;

    // Use this for initialization
    void Start()
    {
        Tutorial = GameObject.FindGameObjectWithTag("Tutorial");
        LevelSelect = GameObject.FindGameObjectWithTag("LevelSelectUI");
        scenemanager_ = GameObject.FindGameObjectWithTag("Scenemanager").GetComponent<Scene_manager>();
        gameLevel = GameLevel.Easy;
        Startstate();
    }

    // Update is called once per frame
    void Update()
    {
        //Tutorial = GameObject.Find("Canvas").transform.FindChild("Scroll View").gameObject;
    }

    public void Startstate()
    {
        onLevelSelect();     
    }

    public void LevelEasy()
    {
        onTutorial();
        gameLevel = GameLevel.Easy;
    }
    public void LevelNormal()
    {
        onTutorial();
        gameLevel = GameLevel.Normal;
    }
    public void LeveleHard()
    {
        onTutorial();
        gameLevel = GameLevel.Hard;
    }
    public void onLevelSelect()
    {
        //Debug.Log("レベルセレクト表示");
        LevelSelect.SetActive(true);
       // Debug.Log("チュートリアル非表示");
        Tutorial.SetActive(false);
    }

    public void onTutorial()
    {
      //  Debug.Log("レベルセレクト非表示");
        LevelSelect.SetActive(false);
      //  Debug.Log("チュートリアル表示");
        Tutorial.SetActive(true);
    }
    public void BackTitle()
    {
        scenemanager_.Scene_state = Scene_manager.Scenestate.TitleScene;
        SceneManager.UnloadSceneAsync("LevelSelect");
    }
}
