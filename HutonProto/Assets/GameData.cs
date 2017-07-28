using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {
    //
    public string curentscene;

    //レベル選択シーンのレベルの難易度
    public string GameLevel;

	void Start () {

	}

	void Update () {
        curentscene = Scene_manager.Instance.currentscene;
        if (curentscene == "LevelSelect")
        {
            GameLevel = GameObject.Find("Levelselect").GetComponent<LevelselectManager>().gameLevel.ToString();
        }
	}
}
