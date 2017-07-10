using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    /*フェードの管理*/
    //trueがロード中,falseがロード中じゃない
    private bool loadingnow = false;

    void Start()
    {
        BaseScene();
    }

    void Update()
    {
        //現在どのシーンか
        currentscene = SceneManager.GetActiveScene().name;

        ////シーンの読み込み
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
            ResultScene();
        }
    }
    void BaseScene()
    {
        //シーンの状態をタイトルシーンに変更
        Scene_state = Scenestate.TitleScene;
        //タイトルシーン呼び出し
       // TitleScene();
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
            //Debug.Log("アクティブ");
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(Title));
        }
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
        loadingnow = false;
    }

    //リザルトシーン
    public void ResultScene()
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
        loadingnow = false;
    }

    //次のシーンへ移動
    public void NextScene()
    {
        if (loadingnow == false)
        {
            //ローディング開始
            loadingnow = true;


            //タイトルシーンステートからゲームシーンステートへ
            if (Scene_state == Scenestate.TitleScene)
            {
                //タイトルシーンのアンロード
                if (ContainsScene(Title))
                {
                    SceneManager.UnloadSceneAsync(Title);
                }

                //シーンステートをゲームシーン変更
                Scene_state = Scenestate.LevelSelect;
                //LevelSelectScene();
            }

            //レベルセレクトシーンからゲームシーンステートへ
            else if (Scene_state == Scenestate.LevelSelect)
            {
                //シーンのアンロード
                if (ContainsScene(LevelSelect))
                {
                    SceneManager.UnloadSceneAsync(LevelSelect);
                }

                //シーンステートをゲームシーン変更
                Scene_state = Scenestate.GameScene;
               // Gamescene();
            }

            //ゲームシーンステートからリザルトシーンステートへ
            else if (Scene_state == Scenestate.GameScene)
            {
                //シーンがあれば
                if (ContainsScene(Game))
                {
                    //シーンのアンロード
                    SceneManager.UnloadSceneAsync(Game);
                }

                //シーンステートをリザルトシーンに変更
                Scene_state = Scenestate.ResultScene;
                //ResultScene();
            }

            //リザルトシーンステートからタイトルシーンステートへ
            else if (Scene_state == Scenestate.ResultScene)
            {
                //追加ではなく読み込みし直しをしてスコア等の初期化
                SceneManager.LoadScene("BaseScene");
            }
        }
    }

    private void SceneDelete()
    {
        //自身以外のシーンを削除する保険

        if (Scene_state == Scenestate.TitleScene)
        {
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
            //ゲームシーンがあれば
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
        }
        else if (Scene_state == Scenestate.LevelSelect)
        {
#if UNITY_EDITOR
            //タイトルシーンがあれば
            if (ContainsScene(Title))
            {
                SceneManager.UnloadSceneAsync(Title);
            }
#endif
            //ゲームシーンがあれば
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
        }
        else if (Scene_state == Scenestate.GameScene)
        {
            //タイトルシーンがあれば
            if (ContainsScene(Title))
            {
                SceneManager.UnloadSceneAsync(Title);
            }
#if UNITY_EDITOR
            //レベルシーンがあれば
            if (ContainsScene(LevelSelect))
            {
                SceneManager.UnloadSceneAsync(LevelSelect);
            }
            
            #endif
            
            //リザルトシーンのアンロード
            if (ContainsScene(Result))
            {
                SceneManager.UnloadSceneAsync(Result);
            }
        }
        else if (Scene_state == Scenestate.ResultScene)
        {
            //タイトルシーンがあれば
            if (ContainsScene(Title))
            {
                SceneManager.UnloadSceneAsync(Title);
            }

            if (ContainsScene(LevelSelect))
            {
                SceneManager.UnloadSceneAsync(LevelSelect);
            }

#if UNITY_EDITOR
            //シーンがあれば
            if (ContainsScene(Game))
            {
                //シーンのアンロード
                SceneManager.UnloadSceneAsync(Game);
            }
#endif
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