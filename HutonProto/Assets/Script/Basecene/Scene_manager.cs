using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Scene_manager : MonoBehaviour
{
    /*シーン遷移の管理*/
    // 2017/5/09 最終編集佐伯 

    //現在のシーンの所得
    public string currentscene;

    /*********Sceneの名前***********/
    //シーンを追加した場合ここに追加する。
    public enum Scenestate
    {
        TitleScene,
        BaseScene,
        LevelSelect,
        GameScene,
        ResultScene
    }
    public Scenestate Scene_state;
    /******************************/

    //変数
    //string Base = "BaseScene";
    string Title = "Title";
    string LevelSelect = "LevelSelect";
    string Game = "main";
    string Result = "Result";

    //フェードしてくれるスクリプト
    private Fademanager fademane;

    //trueがロード中,falseがロード中じゃない
    [SerializeField]
    private bool loadingnow = false;
    //
    //フェードの値
    private float fade_alpha;

    void Start()
    {
        BaseScene();
        //オブジェクト検索
        fademane = GameObject.Find("fadeImage").GetComponent<Fademanager>();
    }

    void Update()
    {
        currentscene = SceneManager.GetActiveScene().name;

        //フェードのα数値
        fade_alpha = fademane.a;

        //タイトルシーン
        if (Scene_state == Scenestate.TitleScene)
        {
            TitleScene();
        }
        if (Scene_state == Scenestate.LevelSelect)
        {
            LevelSelectScene();
        }
        //ゲームシーン
        else if (Scene_state == Scenestate.GameScene)
        {
            Gamescene();
        }
        //リザルトシーン
        else if (Scene_state == Scenestate.ResultScene)
        {
            Resurt();
        }

#if UNITY_EDITOR
        //Titleへ移行
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Scene_state = Scenestate.TitleScene;
        }
        //レベルセレクトへ移行
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Scene_state = Scenestate.LevelSelect;
        }
        //ゲームメインへ移行
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Scene_state = Scenestate.GameScene;
        }
        //リザルトへ移行
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Scene_state = Scenestate.ResultScene;
        }


#endif

    }
    void BaseScene()
    {
        Scene_state = Scenestate.TitleScene;
    }

    //タイトルシーン
    public void TitleScene()
    {
        //同じ名前のシーンがあるか
        if (!ContainsScene(Title))
        {
            //なければ追加
            SceneManager.LoadSceneAsync(Title, LoadSceneMode.Additive);
        }

        //シーンのアクティブ化
        if (SceneActive(Title))
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Title));
        }
#if UNITY_EDITOR
        //レベルシーンがあれば
        if (ContainsScene(LevelSelect))
        {
            SceneManager.UnloadSceneAsync(LevelSelect);
        }
        //シーンがあれば
        if (ContainsScene(Game))
        {
            //シーンのアンロード
            SceneManager.UnloadSceneAsync(Game);
        }
        //リザルトシーンのアンロード
        if (ContainsScene(Result))
        {
            SceneManager.UnloadSceneAsync(Result);
        }
#endif

        loadingnow = false;
    }
    //レベルセレクトシーン
    void LevelSelectScene()
    {
        //同じ名前のシーンがあるか
        if (!ContainsScene(LevelSelect))
        {
            //なければ追加
            SceneManager.LoadSceneAsync(LevelSelect, LoadSceneMode.Additive);
        }

        //シーンのアクティブ化
        if (SceneActive(LevelSelect))
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(LevelSelect));
        }
#if UNITY_EDITOR
        //タイトルシーンのアンロード
        if (ContainsScene(Title))
        {
            SceneManager.UnloadSceneAsync(Title);
        }
        //シーンがあれば
        if (ContainsScene(Game))
        {
            //シーンのアンロード
            SceneManager.UnloadSceneAsync(Game);
        }
        //リザルトシーンのアンロード
        if (ContainsScene(Result))
        {
            SceneManager.UnloadSceneAsync(Result);
        }
#endif

        fademane.fadeout();
        loadingnow = false;
    }

    //ゲームシーン
    void Gamescene()
    {
        //同じ名前のシーンがあるか
        if (!ContainsScene(Game))
        {
            //なければ追加
            SceneManager.LoadSceneAsync(Game, LoadSceneMode.Additive);
        }

        //シーンのアクティブ化
        if (SceneActive(Game))
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Game));
        }
        fademane.fadeout();

#if UNITY_EDITOR
        //タイトルシーンのアンロード
        if (ContainsScene(Title))
        {
            SceneManager.UnloadSceneAsync(Title);
        }
        //レベルシーンがあれば
        if (ContainsScene(LevelSelect))
        {
            SceneManager.UnloadSceneAsync(LevelSelect);
        }
        //リザルトシーンのアンロード
        if (ContainsScene(Result))
        {
            SceneManager.UnloadSceneAsync(Result);
        }
#endif

        loadingnow = false;
    }

    //リザルトシーン
    public void Resurt()
    {
        //同じ名前のシーンがあるか
        if (!ContainsScene(Result))
        {
            //なければ追加
            SceneManager.LoadSceneAsync(Result, LoadSceneMode.Additive);
        }

        //シーンのアクティブ化
        if (SceneActive(Result))
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Result));
        }

#if UNITY_EDITOR
        //タイトルシーンのアンロード
        if (ContainsScene(Title))
        {
            SceneManager.UnloadSceneAsync(Title);
        }
        //レベルシーンがあれば
        if (ContainsScene(LevelSelect))
        {
            SceneManager.UnloadSceneAsync(LevelSelect);
        }
        //シーンがあれば
        if (ContainsScene(Game))
        {
            //シーンのアンロード
            SceneManager.UnloadSceneAsync(Game);
        }
#endif
        fademane.fadeout();
        loadingnow = false;
    }

    //次のシーンへ移動
    public void NextScene()
    {
        if (loadingnow == false)
        {
            loadingnow = true;
            //タイトルシーンステートからゲームシーンステートへ
            if (Scene_state == Scenestate.TitleScene)
            {
                fademane.fadein();

                //タイトルシーンのアンロード
                if (ContainsScene(Title))
                {                   
                    SceneManager.UnloadSceneAsync(Title);
                }              

                //シーンステートをゲームシーン変更
                Scene_state = Scenestate.LevelSelect;
            }

            //レベルセレクトシーンからゲームシーンステートへ
            else if (Scene_state == Scenestate.LevelSelect)
            {
                fademane.fadein();

                //シーンのアンロード
                if (ContainsScene(LevelSelect))
                {                   
                    SceneManager.UnloadSceneAsync(LevelSelect);
                }               

                //シーンステートをゲームシーン変更
                Scene_state = Scenestate.GameScene;
            }

            //ゲームシーンステートからリザルトシーンステートへ
           else if (Scene_state == Scenestate.GameScene)
            {
                fademane.fadein();
                          
                //シーンがあれば
                if (ContainsScene(Game))
                {
                    //シーンのアンロード
                    SceneManager.UnloadSceneAsync(Game);
                }

                //シーンステートをResultシーンの変更
                Scene_state = Scenestate.ResultScene;
            }

            //リザルトシーンステートからタイトルシーンステートへ
            else if (Scene_state == Scenestate.ResultScene)
            {               
                //追加ではなく読み込みし直しをしてスコア等の初期化
                SceneManager.LoadScene("BaseScene");
            }
        }
    }

    // ゲーム内に同じ名前のシーンがあるか検索する//
    bool ContainsScene(string SceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == SceneName)
            {
                return true;
            }
        }
        return false;
    }

    //シーンのアクティブ化//
    //今アクティブになっているシーンの名前を確認してアクティブじゃなければアクティブにする
    bool SceneActive(string SceneName)
    {
        if (currentscene != SceneName)
        {
            return true;
        }
        return false;
    }
}