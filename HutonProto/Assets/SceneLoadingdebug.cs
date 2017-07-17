using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//参考URL
//http://ft-lab.ne.jp/cgi-bin-unity/wiki.cgi?page=unity_script_change_scene_progress_pro_only
public class SceneLoadingdebug : MonoBehaviour {

    private AsyncOperation m_AsyncOpe = null;
    private bool m_sceneChanged = false;
    public float SceneLodingPercent;
	void Start () {
        // 非同期でシーン「mainScene」を読み込み.
        m_AsyncOpe = SceneManager.LoadSceneAsync("Title",LoadSceneMode.Additive);
        // シーン読み込み完了後、自動的にシーンを切り替えないようにfalseを指定.
        m_AsyncOpe.allowSceneActivation = false;
    }

    // 次のシーンに移行したかどうかのフラグ
    void Update () {
        SceneLodingPercent = m_AsyncOpe.progress;
        if (Input.GetMouseButtonDown(0) && (m_AsyncOpe.progress >= 0.9f))
        {
            // 次のシーンに移行
            m_AsyncOpe.allowSceneActivation = true;
            m_sceneChanged = true;
        }
	}
}
