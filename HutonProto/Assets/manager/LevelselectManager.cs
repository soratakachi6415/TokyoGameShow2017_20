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
       
    }

    public void Startstate()
    {
        LevelSelect.SetActive(true);
        Tutorial.GetComponent<Canvas>().enabled=false;
    }

    public void LevelEasy()
    {
        LevelSelect.SetActive(false);
        Tutorial.GetComponent<Canvas>().enabled = true;
        gameLevel = GameLevel.Easy;
    }
    public void LevelNormal()
    {
        LevelSelect.SetActive(false);
        Tutorial.GetComponent<Canvas>().enabled = true;
        gameLevel = GameLevel.Normal;
    }
    public void LeveleHard()
    {
        LevelSelect.SetActive(false);
        Tutorial.GetComponent<Canvas>().enabled = true;
        gameLevel = GameLevel.Hard;
    }

    public void BackTitle()
    {
        scenemanager_.Scene_state = Scene_manager.Scenestate.TitleScene;
        SceneManager.UnloadSceneAsync("LevelSelect");
    }
}
