using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ReAppearState  //再出現時の行動パターン
{
    reApp12,
    reApp34,
    reApp56,
    reApp78,
    reApp910
}

public class EnemyAction : MonoBehaviour {
    /*難易度変動時に使われるpublic変数(ここから)*/
    //移動にかかる時間のランダム範囲(秒) 
    public float moveSpeed_sec_Min;
    public float moveSpeed_sec_Max;

    //クールタイムランダム範囲
    public float coolTime_Min;
    public float coolTime_Max;

　　//エネミーの角度(0 ～ 360°)
    public float m_rand_rotate_Min; //ランダム最小角度
    public float m_rand_rotate_Max; //ランダム最大角度
    /*難易度変動時に使われるpublic変数(ここまで)*/

    //フィールド
    #region
    public bool is_Action = true;
    private EnemyManager enemyManager;
    private ReAppearState reAppStatus;
    private float[] Cnt; //内部カウント    
    
    //０～１の間で使用
    private float[] t; //上エネミーアクティブタイム

    //パーツ関連
    private List<GameObject> Parent_handleg1;  //上エネミー手足のパーツ
    private List<GameObject> Parent_handleg2;  //下エネミー手足のパーツ

    private GameObject prevParts; //上エネミーランダム選抜一個前のパーツ
    private GameObject prevParts2; //下エネミーランダム選抜一個前のパーツ

    private GameObject m_rand_parts; //ランダムパーツ格納1
    private GameObject m_rand_parts2; //ランダムパーツ格納2

    //ランダム角度関連
    private float m_rotate_min;
    private float m_rotate_max;

    //クォータニオン型角度関連
    private Quaternion m_rand_euler;
    private Quaternion m_rand_euler2;

    private Quaternion m_rand_rotate_range;　//回転角度範囲

    public bool actionFlag_top;  //上エネミー行動中フラグ
    public bool actionFlag_bottom; //下エネミー行動中フラグ

    //クールタイム関連
    private int initCoolT; //初期共通クールタイム
    private float coolTime_Top; 
    private float coolTime_Bottom;

    //移動にかかる時間
    private float moveSpeed_sec_Top;
    private float moveSpeed_sec_Bottom;

    //エネミー関連
    private GameObject topEnemy;
    private GameObject bottomEnemy;

    private Vector3 topPos;
    private Vector3 bottomPos;

    private int [] Reappear;
    #endregion

    void Start () {
        enemyManager = GetComponent<EnemyManager>();

        topEnemy = GameObject.Find("Enemy_Top_mixamorig:Hips");
        bottomEnemy = GameObject.Find("Enemy_Bottom_mixamorig:Hips");

        //敵の初期ポジションを格納
        topPos = topEnemy.transform.position;
        bottomPos = bottomEnemy.transform.position;
        //敵共有クールタイム
        initCoolT = 0;
        //快眠ポイントに応じたステータス
        reAppStatus = ReAppearState.reApp910;

        t = new float[2];
        t[0] = t[1] = 0;

        Cnt = new float[2];
        Cnt[0] = Cnt[1] = 0;

        Reappear = new int[2];
        Reappear[0] = Reappear[1] = 0;

        Parent_handleg1 = new List<GameObject>();  //手足のリスト
        Parent_handleg2 = new List<GameObject>();

        //パーツの追加
        #region
        Parent_handleg1.Add(GameObject.Find("Enemy_Top_mixamorig:LeftArm"));     //0
        Parent_handleg1.Add(GameObject.Find("Enemy_Top_mixamorig:LeftForeArm")); //1
        Parent_handleg1.Add(GameObject.Find("Enemy_Top_mixamorig:RightArm"));    //2
        Parent_handleg1.Add(GameObject.Find("Enemy_Top_mixamorig:RightForeArm"));//3
        Parent_handleg1.Add(GameObject.Find("Enemy_Top_mixamorig:LeftUpLeg"));   //4
        Parent_handleg1.Add(GameObject.Find("Enemy_Top_mixamorig:LeftLeg"));     //5
        Parent_handleg1.Add(GameObject.Find("Enemy_Top_mixamorig:RightUpLeg"));  //6
        Parent_handleg1.Add(GameObject.Find("Enemy_Top_mixamorig:RightLeg"));    //7

        Parent_handleg2.Add(GameObject.Find("Enemy_Bottom_mixamorig:LeftArm"));     //0
        Parent_handleg2.Add(GameObject.Find("Enemy_Bottom_mixamorig:LeftForeArm")); //1
        Parent_handleg2.Add(GameObject.Find("Enemy_Bottom_mixamorig:RightArm"));    //2
        Parent_handleg2.Add(GameObject.Find("Enemy_Bottom_mixamorig:RightForeArm"));//3
        Parent_handleg2.Add(GameObject.Find("Enemy_Bottom_mixamorig:LeftUpLeg"));   //4
        Parent_handleg2.Add(GameObject.Find("Enemy_Bottom_mixamorig:LeftLeg"));     //5
        Parent_handleg2.Add(GameObject.Find("Enemy_Bottom_mixamorig:RightUpLeg"));  //6
        Parent_handleg2.Add(GameObject.Find("Enemy_Bottom_mixamorig:RightLeg"));    //7
        #endregion

        m_rotate_min = m_rand_rotate_Min / 360;  //ランダム最小角度
        m_rotate_max = m_rand_rotate_Max / 360;  //ランダム最大角度
        //難易度設定
        ChangeLevel();
    }

    // Update is called once per frame
    void Update () {
        initCoolT++;
        //最初の2秒は敵が行動しない
        if (initCoolT < 120 || !is_Action) return;
        else if(initCoolT == 120)
        {
            SetParts_Top();
            SetParts_Bottom();
        }

        Cnt[0] += Time.deltaTime;
        Cnt[1] += Time.deltaTime;

        //上のエネミー行動
        #region
        if (t[0] < 1f)
        {
            actionFlag_top = true;
            m_rand_euler = new Quaternion(m_rand_parts.transform.rotation.x, m_rand_euler.y,
                                          m_rand_parts.transform.rotation.z, m_rand_parts.transform.rotation.w);
            m_rand_parts.transform.rotation = Quaternion.Slerp(m_rand_parts.transform.rotation, m_rand_euler, t[0]);
            MovingEnemy(m_rand_parts);
        }
        else 
        {
            actionFlag_top = false;
        }

        t[0] += moveSpeed_sec_Top;
        #endregion

        //下のエネミー行動
        #region
        if(t[1] < 1f)
        {
            if (prevParts == null && t[0]< 1f) return;  //初回のみ上エネミ―の行動が終了するまでreturn

            actionFlag_bottom = true;
            m_rand_euler2 = new Quaternion(m_rand_parts2.transform.rotation.x, m_rand_euler2.y,
                                           m_rand_parts2.transform.rotation.z, m_rand_parts2.transform.rotation.w);
            m_rand_parts2.transform.rotation = Quaternion.Slerp(m_rand_parts2.transform.rotation, m_rand_euler2, t[1]);
            MovingEnemy(m_rand_parts2);
        }
        else 
        {
            actionFlag_bottom = false;
        }

        t[1] += moveSpeed_sec_Bottom;
        #endregion

        //クールタイム経過で初期化処理。再行動時には3回動くまで連続で初期化。
        #region
        if ((coolTime_Top < Cnt[0] || Reappear[0] > 0) && !actionFlag_top)
        {        
            t[0] = 0;
            SetParts_Top();
            Cnt[0] = 0;
            if (Reappear[0] > 0) Reappear[0]--;
        }
        else if((coolTime_Bottom < Cnt[1] || Reappear[1] > 0) && !actionFlag_bottom)
        {
            t[1] = 0;
            SetParts_Bottom();
            Cnt[1] = 0;
            if (Reappear[1] > 0) Reappear[1]--;
        }
        #endregion
    }
    //難易度に応じて敵のステータスが変動
    private void ChangeLevel()
    {
        string s = "hard";
        switch(s)
        {
            case "easy":
                moveSpeed_sec_Min = 9.0f;
                moveSpeed_sec_Max = 10.0f;
                coolTime_Min = 15.0f;
                coolTime_Max = 20.0f;
                m_rand_rotate_Min = 15.0f;
                m_rand_rotate_Max = 20.0f;
                break;
            case "normal":
                moveSpeed_sec_Min = 7.0f;
                moveSpeed_sec_Max = 10.0f;
                coolTime_Min = 10.0f;
                coolTime_Max = 20.0f;
                m_rand_rotate_Min = 15.0f;
                m_rand_rotate_Max = 20.0f;
                break;
            case "hard":
                moveSpeed_sec_Min = 5.0f;
                moveSpeed_sec_Max = 10.0f;
                coolTime_Min = 5.0f;
                coolTime_Max = 20.0f;
                m_rand_rotate_Min = 15.0f;
                m_rand_rotate_Max = 20.0f;
                break;
        }
    }
    //[上エネミ―]複数のパーツから1つを選抜し、ランダムで角度を決める
    private void SetParts_Top()  
    {
        //クールタイムを決める
        coolTime_Top = Random.Range(coolTime_Min, coolTime_Max);

        //移動時間を決める 
        moveSpeed_sec_Top = (1 / Random.Range(moveSpeed_sec_Min, moveSpeed_sec_Max)) / 60;

        //現在のパーツがNULLじゃなければ格納
        if (m_rand_parts != null)
        {
            prevParts = m_rand_parts;
        }

        //前パーツと被らないまでループ処理
        while (prevParts == m_rand_parts)
        {
            ActivateParts_Top();
        }

        //ランダムで回転角度決定
        m_rand_rotate_range = new Quaternion(0, Random.Range(m_rotate_min, m_rotate_max),0, 0);

        //基本ランダムで回転方向を決定
        //プレイヤーの状態次第でも回転方向が決定する
        if ((int)Random.Range(0, 2) == 0)
        {
            m_rand_euler.y = m_rand_parts.transform.rotation.y - m_rand_rotate_range.y;
        }
        else
        {
            m_rand_euler.y = m_rand_parts.transform.rotation.y + m_rand_rotate_range.y;
        }
    }
    //[下エネミー]上エネミ―と同じソースコード。今後変更が無ければひとまとめに。
    private void SetParts_Bottom() 
    {
        //クールタイムを決める
        coolTime_Bottom = Random.Range(coolTime_Min, coolTime_Max);
        
        //移動時間を決める 
        moveSpeed_sec_Bottom = (1 / Random.Range(moveSpeed_sec_Min, moveSpeed_sec_Max)) / 60;

        //現在のパーツがNULLじゃなければ格納
        if (m_rand_parts2 != null)
        {
            prevParts2 = m_rand_parts2;
        }

        //前パーツと被らないまでループ処理
        while (prevParts == m_rand_parts2)
        {
            ActivateParts_Bottom();
        }

        //ランダムで角度決定
        m_rand_rotate_range = new Quaternion(0, Random.Range(m_rotate_min, m_rotate_max),0, 0);

        if ((int)Random.Range(0, 2) == 0)
        {
            m_rand_euler2.y = m_rand_parts2.transform.rotation.y - m_rand_rotate_range.y;
        }
        else
        {
            m_rand_euler2.y = m_rand_parts2.transform.rotation.y + m_rand_rotate_range.y;
        }
    }
    //上エネミーの関節可動分岐
    private void ActivateParts_Top()
    {
        if(enemyManager == null)
        {
            m_rand_parts = Parent_handleg1[Random.Range(0, 8)];
            return;
        }
        switch (enemyManager.IsAvoid())
        {
            //プレイヤーが画面端(左)
            case "left":
                switch (enemyManager.IsRotation_Top)
                {
                    case Rotation_Top.None:
                        //腕のみ
                        m_rand_parts = Parent_handleg1[Random.Range(0, 4)];
                        break;
                    case Rotation_Top.Rotation90:
                        //左腕、左脚のみ
                        while (m_rand_parts == (Parent_handleg1[2] || Parent_handleg1[3]))
                        {
                            m_rand_parts = Parent_handleg1[Random.Range(0, 6)];
                        }
                        break;
                    case Rotation_Top.Rotation180:
                        //脚のみ
                        m_rand_parts = Parent_handleg1[Random.Range(4, 8)];
                        break;
                    case Rotation_Top.Rotation270:
                        //右腕、右脚のみ
                        while (m_rand_parts == (Parent_handleg1[4] || Parent_handleg1[5]))
                        {
                            m_rand_parts = Parent_handleg1[Random.Range(2, 8)];
                        }
                        break;
                }
                break;
            //プレイヤーが画面端(右)
            case "right":
                switch (enemyManager.IsRotation_Top)
                {
                    case Rotation_Top.None:
                        //脚のみ
                        m_rand_parts = Parent_handleg1[Random.Range(4, 8)];
                        break;
                    case Rotation_Top.Rotation90:
                        //左腕、左脚のみ
                        while (m_rand_parts == (Parent_handleg1[2] || Parent_handleg1[3]))
                        {
                            m_rand_parts = Parent_handleg1[Random.Range(0, 6)];
                        }
                        break;
                    case Rotation_Top.Rotation180:
                        //腕のみ
                        m_rand_parts = Parent_handleg1[Random.Range(0, 4)];
                        break;
                    case Rotation_Top.Rotation270:
                        //右腕、右脚のみ
                        while (m_rand_parts == (Parent_handleg1[4] || Parent_handleg1[5]))
                        {
                            m_rand_parts = Parent_handleg1[Random.Range(2, 8)];
                        }
                        break;
                }
                break;

            //条件に引っかからなかったらランダム対象を全パーツに
            default:
                m_rand_parts = Parent_handleg1[Random.Range(0, 8)];
                break;
        }
    }
    //下エネミーの関節可動分岐
    /*上エネミ―と同じソースコード。今後変更が無ければひとまとめに*/
    private void ActivateParts_Bottom()
    {
        switch(enemyManager.IsAvoid())
        {
            //プレイヤーが画面端(左)
            case "left":
                switch (enemyManager.IsRotation_Bottom)
                {
                    case Rotation_Bottom.None:
                        //腕のみ
                        m_rand_parts2 = Parent_handleg2[Random.Range(0, 4)];
                        break;
                    case Rotation_Bottom.Rotation90:
                        //左腕、左脚のみ
                        while (m_rand_parts2 == (Parent_handleg2[2] || Parent_handleg2[3]))
                        {
                            m_rand_parts2 = Parent_handleg2[Random.Range(0, 6)];
                        }
                        break;
                    case Rotation_Bottom.Rotation180:
                        //脚のみ
                        m_rand_parts2 = Parent_handleg2[Random.Range(4, 8)];
                        break;
                    case Rotation_Bottom.Rotation270:
                        //右腕、右脚のみ
                        while (m_rand_parts2 == (Parent_handleg2[4] || Parent_handleg2[5]))
                        {
                            m_rand_parts2 = Parent_handleg2[Random.Range(2, 8)];
                        }
                        break;
                }
                break;
            //プレイヤーが画面端(右)
            case "right":
                switch (enemyManager.IsRotation_Bottom)
                {
                    case Rotation_Bottom.None:
                        //脚のみ
                        m_rand_parts2 = Parent_handleg2[Random.Range(4, 8)];
                        break;
                    case Rotation_Bottom.Rotation90:
                        //左腕、左脚のみ
                        while (m_rand_parts2 == (Parent_handleg2[2] || Parent_handleg2[3]))
                        {
                            m_rand_parts2 = Parent_handleg2[Random.Range(0, 6)];
                        }
                        break;
                    case Rotation_Bottom.Rotation180:
                        //腕のみ
                        m_rand_parts2 = Parent_handleg2[Random.Range(0, 4)];
                        break;
                    case Rotation_Bottom.Rotation270:
                        //右腕、右脚のみ
                        while (m_rand_parts2 == (Parent_handleg2[4] || Parent_handleg2[5]))
                        {
                            m_rand_parts2 = Parent_handleg2[Random.Range(2, 8)];
                        }
                        break;
                }
                break;

            //条件に引っかからなかったらランダム対象を全パーツに
            default:
                m_rand_parts2 = Parent_handleg2[Random.Range(0, 8)];
                break;
        }
    }

    /*(未完)敵が関節をフル回転させてもプレイヤーに届かない場合
    敵が関節を動かしながら、関節が可動してプレイヤーに届
    く距離までにじり寄ってくる*/
    private void MovingEnemy(GameObject gObj)
    {
        //簡易的な条件判定と仮の動き
        if(enemyManager.IsAway() == "top" || enemyManager.IsAway() == "bottom")
        {
            Vector3 direction = enemyManager.GetPlayer.transform.position - gObj.transform.position;
            direction.Normalize();
            gObj.transform.position += direction * 0.06f;
        }
    }
    //再出現時の行動判別
    public void ReAppear(GameObject obj) 
    {
        //快眠ポイント取得
        int point = GameObject.Find("ScriptController").GetComponent<SleepGageScript>().sleepPoint;
        

        if(point >= 1 && point <= 2) //快眠ポイントが１～２のとき
        {
            reAppStatus = ReAppearState.reApp12;
        }
        else if(point >= 3 && point <= 4) //快眠ポイントが３～４のとき
        {
            reAppStatus = ReAppearState.reApp34;
        }
        else if (point >= 5 && point <= 6) //快眠ポイントが５～６のとき
        {
            reAppStatus = ReAppearState.reApp56;
        }
        else if (point >= 7 && point <= 8) //快眠ポイントが７～８のとき
        {
            reAppStatus = ReAppearState.reApp78;
        }
        else if (point <= 10) //快眠ポイントが９～１０のとき
        {
            reAppStatus = ReAppearState.reApp910;
        }

        if(obj.name == topEnemy.name) //上エネミ―
        {
            ReAppear_top(obj);
            Reappear[0] = 3;  //再出現後の行動回数
        }
        else if(obj.name == bottomEnemy.name) //下エネミー
        {
            ReAppear_bottom(obj);
            Reappear[1] = 3;  //再出現後の行動回数
        }
    }

    private void ReAppear_top(GameObject obj)　//再出現時の回転と座標
    {
        switch (reAppStatus)
        {
            case ReAppearState.reApp12:
                obj.transform.rotation = new Quaternion(0, 90, 0,0);
                obj.transform.position = topPos;
                enemyManager.IsRotation_Top = Rotation_Top.Rotation180;
                break;

            case ReAppearState.reApp34:
                obj.transform.rotation = new Quaternion(0, -180, 0, 0);
                obj.transform.position = new Vector3(-28,obj.transform.position.y,6);
                enemyManager.IsRotation_Top = Rotation_Top.Rotation270;
                break;

            case ReAppearState.reApp56:
                obj.transform.rotation = new Quaternion(0, 0, 0, 0);
                obj.transform.position = new Vector3(28,obj.transform.position.y,6);
                enemyManager.IsRotation_Top = Rotation_Top.Rotation90;
                break;

            case ReAppearState.reApp78:
                obj.transform.rotation = new Quaternion(0, 0, 0, 0);
                obj.transform.position = topPos;
                enemyManager.IsRotation_Top = Rotation_Top.Rotation90;
                break;

            case ReAppearState.reApp910:
                obj.transform.rotation = new Quaternion(0, -180, 0, 0);
                obj.transform.position = topPos;
                enemyManager.IsRotation_Top = Rotation_Top.Rotation270;
                break;
        }
    }

    private void ReAppear_bottom(GameObject obj) //再出現時の回転と座標
    {
        switch (reAppStatus)
        {
            case ReAppearState.reApp12:
                obj.transform.rotation = new Quaternion(0, -90, 0, 0);
                obj.transform.position = bottomPos;
                enemyManager.IsRotation_Bottom = Rotation_Bottom.None;
                break;

            case ReAppearState.reApp34:
                obj.transform.rotation = new Quaternion(0, 0, 0, 0);
                obj.transform.position = new Vector3(-14,obj.transform.position.y,5);
                enemyManager.IsRotation_Bottom = Rotation_Bottom.Rotation90;
                break;

            case ReAppearState.reApp56:
                obj.transform.rotation = new Quaternion(0, 90, 0,0);
                obj.transform.position = bottomPos;
                enemyManager.IsRotation_Bottom = Rotation_Bottom.Rotation180;
                break;

            case ReAppearState.reApp78:
                obj.transform.rotation = new Quaternion(0, -180, 0, 0);
                obj.transform.position = bottomPos;
                enemyManager.IsRotation_Bottom = Rotation_Bottom.Rotation270;
                break;

            case ReAppearState.reApp910:
                obj.transform.rotation = new Quaternion(0, 0, 0, 0);
                obj.transform.position = bottomPos;
                enemyManager.IsRotation_Bottom = Rotation_Bottom.Rotation90;
                break;
        }
    }
}
