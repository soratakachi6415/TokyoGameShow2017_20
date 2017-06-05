using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    // Use this for initialization
    void Start()
    {
        Tutorial = GameObject.Find("Tutorial");
        LevelSelect = GameObject.Find("LevelSelectUI");
        Levelselect_state = LevelselectState.LevelSelect;
    }

    // Update is called once per frame
    void Update()
    {
        if (Levelselect_state == LevelselectState.LevelSelect)
        {

            onLevelSelect();
        }

        if (Levelselect_state == LevelselectState.Tutorial)
        {
            TutorialStatet();
        }
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
}
