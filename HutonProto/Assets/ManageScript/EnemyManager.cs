using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState  //プレイヤーの状態
{
    None,             //何もなし
    LowHealth,        //プレイヤーのHPが低い
    HighHealth,       //プレイヤーのHPが高い
    AvoidLeft,        //プレイヤーが画面端(左)に逃げた
    AvoidRight,       //プレイヤーが画面端(右)に逃げた
    AvoidUp,          //プレイヤーが画面端(上)に逃げた
    AvoidDown,        //プレイヤーが画面端(下)に逃げた
    AwayEnemy_Top,    //プレイヤーが上エネミ―から離れた
    AwayEnemy_Bottom, //プレイヤーが下エネミ―から離れた
}

public enum Rotation_Top  //上エネミー回転情報
{
    None,
    Rotation90,
    Rotation180,
    Rotation270
}

public enum Rotation_Bottom  //下エネミー回転情報
{
    None,
    Rotation90,
    Rotation180,
    Rotation270
}

public class EnemyManager : MonoBehaviour {


    /*UdEnemyManageの移植フィールド*/
    #region
    private int topEnemyhit;  //上エネミーヒット回数
    private int downEnemeyHit;  //下エネミーヒット回数

    private int topCnt;
    private int downCnt;

    private float topCoolT;
    private float bottomCoolT;

    private SoundsManager soundsManager;

    //確認用
    private Vector3 upE;
    private Vector3 downE;
    #endregion

    private Rotation_Top rotation_top;
    private Rotation_Bottom rotation_bottom;
    private List<PlayerState> playerState = new List<PlayerState>();
    private GameObject enemy_Top;
    private GameObject enemy_Bottom;
    private GameObject player;
    private SleepGageScript sleepGauge;

    void Start()
    {
        enemy_Top = GameObject.Find("Enemy_Top_mixamorig:Hips");
        enemy_Bottom = GameObject.Find("Enemy_Bottom_mixamorig:Hips");
        //エネミー回転情報初期化
        IsRotation_Top = Rotation_Top.None;
        IsRotation_Bottom = Rotation_Bottom.None;
        /*UdEnemyManageの移植Start*/
        #region
        upE = enemy_Top.transform.position;
        downE = enemy_Bottom.transform.position;

        topEnemyhit = 0;
        downEnemeyHit = 0;
        topCnt = 0;
        downCnt = 0;

        soundsManager = GameObject.FindGameObjectWithTag("SoundsManager").GetComponent<SoundsManager>();
        #endregion
        player = GameObject.Find("Player_mixamorig:Hips");
        sleepGauge = GameObject.FindGameObjectWithTag("GameController").GetComponent<SleepGageScript>();
    }

    void Update()
    {
        IsGetUpEnemyTop();
        IsGetUpEnemyBottom();
    }
    //上エネミーの回転情報
    public Rotation_Top IsRotation_Top
    {
        get { return rotation_top; }
        set { rotation_top = value; }
    }
    //下エネミーの回転情報
    public Rotation_Bottom IsRotation_Bottom
    {
        get { return rotation_bottom; }
        set { rotation_bottom = value; }
    }
    //プレイヤー情報
    public GameObject GetPlayer
    {
        get { return player; }
    }

    //プレイヤーの状態:画面端にいるかを確認
    public string IsAvoid()
    {
        if(GetPlayerState().Contains(PlayerState.AvoidLeft))
        {
            return "left";
        }
        else if(GetPlayerState().Contains(PlayerState.AvoidRight))
        {
            return "right";
        }
        else if (GetPlayerState().Contains(PlayerState.AvoidUp))
        {
            return "up";
        }
        else if (GetPlayerState().Contains(PlayerState.AvoidDown))
        {
            return "down";
        }
        return null;
    }
    //プレイヤーの状態:どの敵と離れてるかを確認
    public string IsAway()
    {
        if (GetPlayerState().Contains(PlayerState.AwayEnemy_Top))
        {
            return "top";
        }
        else if (GetPlayerState().Contains(PlayerState.AwayEnemy_Bottom))
        {
            return "bottom";
        }
        return null;
    }
    public void CheckPlayerStatus()
    {
        GetPlayerState();
    }
    //プレイヤーの状態:HPの割合確認
    public string IsHighLow()
    {
        if (GetPlayerState().Contains(PlayerState.HighHealth))
        {
            return "high";
        }
        else if (GetPlayerState().Contains(PlayerState.LowHealth))
        {
            return "low";
        }
        return "middle";
    }
    //衝突した回数を数えて起きるかどうかの判定
    private void IsGetUpEnemyTop()
    {
        if (topEnemyhit > 2)
        {
            //効果音再生
            if (topCnt == 1) soundsManager.WalkFloor();

            if (topCnt > 2 && topCnt < 180)//起きるアニメーション　*現時点では指定座標に瞬間移動
            {
                enemy_Top.transform.position = new Vector3(upE.x, upE.y, upE.z * 3);
            }
            else if (topCnt >= 180)
            {
                GameObject.Find("ScriptController").GetComponent<EnemyAction>().ReAppear(enemy_Top);
                topEnemyhit = 0;
                topCoolT = 0;
                topCnt = 0;
            }
            topCnt++;
        }
        topCoolT += Time.deltaTime;
    }
    private void IsGetUpEnemyBottom()
    {
        if (downEnemeyHit > 1)
        {
            //効果音再生
            if (downCnt == 1) soundsManager.WalkFloor();

            if (downCnt > 2 && downCnt < 180)
            {
                enemy_Bottom.transform.position = new Vector3(downE.x, downE.y, downE.z * 3);
            }
            else if (downCnt >= 180)
            {
                GameObject.Find("ScriptController").GetComponent<EnemyAction>().ReAppear(enemy_Bottom);
                downEnemeyHit = 0;
                bottomCoolT = 0;
                downCnt = 0;
            }
            downCnt++;
        }
        bottomCoolT += Time.deltaTime;
    }
    public void upHit() //上エネミ―と衝突時カウントアップ
    {
        if (topCoolT > 0.5f && !(GameObject.Find("ScriptController").GetComponent<EnemyAction>().actionFlag_top))
        {
            topCoolT = 0;
            topEnemyhit++;
        }
    }
    public void downHit() //下エネミーと衝突時カウントアップ
    {
        if (bottomCoolT > 0.5f && !(GameObject.Find("ScriptController").GetComponent<EnemyAction>().actionFlag_bottom))
        {
            bottomCoolT = 0;
            downEnemeyHit++;
        }
    }
    private List<PlayerState> GetPlayerState() //す
    {
        List<PlayerState> m_Status = new List<PlayerState>();

        //プレイヤーが上エネミ―から逃げた
        if (Vector3.Distance(Camera.main.WorldToScreenPoint(player.transform.position),
            Camera.main.WorldToScreenPoint(enemy_Top.transform.position)) >= 900.0f)
        {
            m_Status.Add(PlayerState.AwayEnemy_Top);
        }
        //プレイヤーが下エネミ―から逃げた
        if (Vector3.Distance(Camera.main.WorldToScreenPoint(player.transform.position),
            Camera.main.WorldToScreenPoint(enemy_Bottom.transform.position)) >= 900.0f)
        {
            m_Status.Add(PlayerState.AwayEnemy_Bottom);
        }

        //プレイヤーが画面端(左)
        if (Camera.main.WorldToScreenPoint(player.transform.position).x < Camera.main.pixelWidth / 5)
        {
            m_Status.Add(PlayerState.AvoidLeft);
        }
        //プレイヤーが画面端(右)
        else if (Camera.main.WorldToScreenPoint(player.transform.position).x > Camera.main.pixelWidth - (Camera.main.pixelWidth / 5))
        {
            m_Status.Add(PlayerState.AvoidRight);
        }
        //プレイヤーのHPが８より高い
        if (sleepGauge.sleepPoint > 8)
        {
            m_Status.Add(PlayerState.HighHealth);
        }
        //プレイヤーのHPが４より低い
        else if (sleepGauge.sleepPoint < 4)
        {
            m_Status.Add(PlayerState.LowHealth);
        }

        //どの条件にも引っかからなければ
        //Noneで返す
        if (m_Status.Count == 0)
        {
            m_Status.Add(PlayerState.None);
        }
        return m_Status;
    }
}
