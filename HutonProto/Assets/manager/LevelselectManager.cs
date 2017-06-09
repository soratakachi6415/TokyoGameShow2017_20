using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelselectManager : MonoBehaviour
{
    //レベル選択か操作説明中か

    public enum LevelselectState
    {
        LevelSelect,
        Tutorial
    }
    public LevelselectState Levelselect_state;


    public GameObject Tutorial;
    public GameObject LevelSelect;

    public Scene_manager scenemanager_;

    // Use this for initialization
    void Start()
    {
        Tutorial = GameObject.FindGameObjectWithTag("Tutorial");
        LevelSelect = GameObject.Find("LevelSelectUI");
        Levelselect_state = LevelselectState.LevelSelect;
        //Tutorial.SetActive(false);
        scenemanager_ = GameObject.FindGameObjectWithTag("Scenemanager").GetComponent<Scene_manager>();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onLevelSelect()
    {
        Tutorial.SetActive(false);
        LevelSelect.SetActive(true);
    }
    //ボタンで選択するほう
    public void onTutorialStatet()
    {
        Levelselect_state = LevelselectState.Tutorial;
    }

    public void TutorialStatet()
    {
        LevelSelect.SetActive(false);
        Tutorial.SetActive(true);
    }

    public void BackTitle()
    {
        scenemanager_.Scene_state = Scene_manager.Scenestate.TitleScene;
        SceneManager.UnloadSceneAsync("LevelSelect");
    }
}
