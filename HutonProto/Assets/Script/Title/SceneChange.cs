using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour {

    public Scene_manager scenemanager_;
    public Fademanager   fadeManager;

    public Color color;

	void Start () {
        scenemanager_ = Scene_manager.Instance;
        fadeManager   = Fademanager.Instance;
	}

	void Update () {

	}

    public void onNextscene()
    {
        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene()
    {
        yield return fadeManager.FadeIn(color);
        scenemanager_.NextScene();
    }
}
