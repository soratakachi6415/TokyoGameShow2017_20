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
    /*難易度変動時に使われる変数(ここから)*/
    //移動にかかる時間のランダム範囲(秒) 
    private float moveSpeed_sec_Min;
    private float moveSpeed_sec_Max;

    //クールタイムランダム範囲
    private float coolTime_Min;
    private float coolTime_Max;

　　//エネミーの角度(0 ～ 360°)
    private float m_rand_rotate_Min; //ランダム最小角度
    private float m_rand_rotate_Max; //ランダム最大角度
    /*難易度変動時に使われる変数(ここまで)*/

    //難易度変動時の移動時間を割る数値
    private float level; 

    //フィールド
    #region
    public bool is_Action = true;
    private EnemyManager enemyManager;
    private ReAppearState reAppStatus;
    private float[] Cnt; //内部カウント    
    
    //０～１の間で使用
    private float[] t; //エネミーアクティブタイム

    //パーツ関連
    private List<GameObject> Parent_handleg_top;  //上エネミー手足のパーツ
    private List<GameObject> Parent_handleg_bottom;  //下エネミー手足のパーツ

    private GameObject prevPart_top; //上エネミーランダム選抜一個前のパーツ
    private GameObject prevPart_bottom; //下エネミーランダム選抜一個前のパーツ

    private GameObject m_rand_parts_top; //ランダムパーツ格納1
    private GameObject m_rand_parts_bottom; //ランダムパーツ格納2

    //ランダム角度関連
    private float m_rotate_min;
    private float m_rotate_max;

    //クォータニオン型角度関連
    private Quaternion m_rand_euler_top;
    private Quaternion m_rand_euler_bottom;

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
        //難易度設定
        ChangeLevel();
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

        Parent_handleg_top = new List<GameObject>();  //手足のリスト
        Parent_handleg_bottom = new List<GameObject>();

        //パーツの追加
        #region
        Parent_handleg_top.Add(GameObject.Find("Enemy_Top_mixamorig:LeftArm"));     //0
        Parent_handleg_top.Add(GameObject.Find("Enemy_Top_mixamorig:LeftForeArm")); //1
        Parent_handleg_top.Add(GameObject.Find("Enemy_Top_mixamorig:RightArm"));    //2
        Parent_handleg_top.Add(GameObject.Find("Enemy_Top_mixamorig:RightForeArm"));//3
        Parent_handleg_top.Add(GameObject.Find("Enemy_Top_mixamorig:LeftUpLeg"));   //4
        Parent_handleg_top.Add(GameObject.Find("Enemy_Top_mixamorig:LeftLeg"));     //5
        Parent_handleg_top.Add(GameObject.Find("Enemy_Top_mixamorig:RightUpLeg"));  //6
        Parent_handleg_top.Add(GameObject.Find("Enemy_Top_mixamorig:RightLeg"));    //7

        Parent_handleg_bottom.Add(GameObject.Find("Enemy_Bottom_mixamorig:LeftArm"));     //0
        Parent_handleg_bottom.Add(GameObject.Find("Enemy_Bottom_mixamorig:LeftForeArm")); //1
        Parent_handleg_bottom.Add(GameObject.Find("Enemy_Bottom_mixamorig:RightArm"));    //2
        Parent_handleg_bottom.Add(GameObject.Find("Enemy_Bottom_mixamorig:RightForeArm"));//3
        Parent_handleg_bottom.Add(GameObject.Find("Enemy_Bottom_mixamorig:LeftUpLeg"));   //4
        Parent_handleg_bottom.Add(GameObject.Find("Enemy_Bottom_mixamorig:LeftLeg"));     //5
        Parent_handleg_bottom.Add(GameObject.Find("Enemy_Bottom_mixamorig:RightUpLeg"));  //6
        Parent_handleg_bottom.Add(GameObject.Find("Enemy_Bottom_mixamorig:RightLeg"));    //7
        #endregion

        m_rotate_min = m_rand_rotate_Min / 360;  //ランダム最小角度
        m_rotate_max = m_rand_rotate_Max / 360;  //ランダム最大角度

        t[1] -= 0.4f;
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
        if (t[0] < 0.8f)
        {
            //上エネミ―行動フラグ
            actionFlag_top = true;
            m_rand_euler_top = new Quaternion(m_rand_parts_top.transform.rotation.x, m_rand_euler_top.y,
                                          m_rand_parts_top.transform.rotation.z, m_rand_parts_top.transform.rotation.w);
            m_rand_parts_top.transform.rotation = Quaternion.Slerp(m_rand_parts_top.transform.rotation, m_rand_euler_top, t[0]);
        }
        else actionFlag_top = false;

        t[0] += moveSpeed_sec_Top;
        #endregion

        //下のエネミー行動
        #region
        if(t[1] > 0.0f && t[1] < 0.8f)
        {
            actionFlag_bottom = true;
            m_rand_euler_bottom = new Quaternion(m_rand_parts_bottom.transform.rotation.x, m_rand_euler_bottom.y,
                                           m_rand_parts_bottom.transform.rotation.z, m_rand_parts_bottom.transform.rotation.w);
            m_rand_parts_bottom.transform.rotation = Quaternion.Slerp(m_rand_parts_bottom.transform.rotation, m_rand_euler_bottom, t[1]);
        }
        else actionFlag_bottom = false;

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
        switch(GameObject.Find("GameDate").GetComponent<GameData>().GameLevel)
        {
            case "Easy":
                moveSpeed_sec_Min = 5.0f;
                moveSpeed_sec_Max = 7.0f;
                coolTime_Min = 12.0f;
                coolTime_Max = 15.0f;
                m_rand_rotate_Min = 37.0f;
                m_rand_rotate_Max = 50.0f;
                level = 180;
                break;
            case "Normal":
                moveSpeed_sec_Min = 4.0f;
                moveSpeed_sec_Max = 5.0f;
                coolTime_Min = 5.0f;
                coolTime_Max = 10.0f;
                m_rand_rotate_Min = 37.0f;
                m_rand_rotate_Max = 50.0f;
                level = 120;
                break;
            case "Hard":
                moveSpeed_sec_Min = 1.0f;
                moveSpeed_sec_Max = 2.0f;
                coolTime_Min = 2.0f;
                coolTime_Max = 3.0f;
                m_rand_rotate_Min = 37.0f;
                m_rand_rotate_Max = 50.0f;
                level = 60;
                break;
            default:
                moveSpeed_sec_Min = 1.0f;
                moveSpeed_sec_Max = 2.0f;
                coolTime_Min = 2.0f;
                coolTime_Max = 3.0f;
                m_rand_rotate_Min = 37.0f;
                m_rand_rotate_Max = 50.0f;
                level = 60;
                break;
        }
    }
    //[上エネミ―]複数のパーツから1つを選抜し、ランダムで角度を決める
    private void SetParts_Top()  
    {
        //クールタイムを決める
        coolTime_Top = Random.Range(coolTime_Min, coolTime_Max);

        //移動時間を決める 
        moveSpeed_sec_Top = (1 / Random.Range(moveSpeed_sec_Min, moveSpeed_sec_Max)) / level;

        //現在のパーツがNULLじゃなければ格納
        if (m_rand_parts_top != null)
        {
            prevPart_top = m_rand_parts_top;
        }

        //前パーツと被らないまでループ処理
        while (prevPart_top == m_rand_parts_top)
        {
            ActivateParts_Top();
        }

        //ランダムで回転角度決定
        m_rand_rotate_range = new Quaternion(0, Random.Range(m_rotate_min, m_rotate_max),0, 0);

        //基本ランダムで回転方向を決定
        //****メモ****
        //+は画面上で左回転。-は画面上で右回転
        if ((m_rand_parts_top.GetComponent<HingeJoint>().limits.min / 360) + 0.2f > m_rand_rotate_range.y)
        {
            m_rand_euler_top.y = m_rand_parts_top.transform.rotation.y - m_rand_rotate_range.y;
        }
        else if ((m_rand_parts_top.GetComponent<HingeJoint>().limits.max / 360) - 0.2f < m_rand_rotate_range.y)
        {
            m_rand_euler_top.y = m_rand_parts_top.transform.rotation.y + m_rand_rotate_range.y;
        }
        else
        {
            if ((int)Random.Range(0, 2) == 0) m_rand_euler_top.y = m_rand_parts_top.transform.rotation.y + m_rand_rotate_range.y;
            else m_rand_euler_top.y = m_rand_parts_top.transform.rotation.y - m_rand_rotate_range.y;
        }
    }
    //[下エネミー]上エネミ―と同じソースコード。今後変更が無ければひとまとめに。
    private void SetParts_Bottom() 
    {
        //クールタイムを決める
        coolTime_Bottom = Random.Range(coolTime_Min, coolTime_Max);
        
        //移動時間を決める 
        moveSpeed_sec_Bottom = (1 / Random.Range(moveSpeed_sec_Min, moveSpeed_sec_Max)) / level;

        //現在のパーツがNULLじゃなければ格納
        if (m_rand_parts_bottom != null)
        {
            prevPart_bottom = m_rand_parts_bottom;
        }

        //前パーツと被らないまでループ処理
        while (prevPart_bottom == m_rand_parts_bottom)
        {
            ActivateParts_Bottom();
        }

        //ランダムで角度決定
        m_rand_rotate_range = new Quaternion(0, Random.Range(m_rotate_min, m_rotate_max),0, 0);

        //基本ランダムで回転方向を決定
        //****メモ****
        //+はMax,画面上で左回転。-はMin,画面上で右回転
        //if ((m_rand_parts_bottom.GetComponent<HingeJoint>().limits.min / 360) + 0.2f > m_rand_rotate_range.y)
        //{
        //   m_rand_euler_bottom.y = m_rand_parts_bottom.transform.rotation.y - m_rand_rotate_range.y;
        //}
        //else if ((m_rand_parts_bottom.GetComponent<HingeJoint>().limits.max / 360) - 0.2f < m_rand_rotate_range.y)
        //{
        //   m_rand_euler_bottom.y = m_rand_parts_bottom.transform.rotation.y + m_rand_rotate_range.y;
        //}
        //else
        //{
            if((int)Random.Range(0,2) == 0) m_rand_euler_bottom.y = m_rand_parts_bottom.transform.rotation.y + m_rand_rotate_range.y;
            else m_rand_euler_bottom.y = m_rand_parts_bottom.transform.rotation.y - m_rand_rotate_range.y;
        //}
    }
    //上エネミーの関節可動分岐
    private void ActivateParts_Top()
    {
        m_rand_parts_top = Parent_handleg_top[Random.Range(0, 8)];
        ////エラー回避
        //if(enemyManager == null)
        //{
        //    m_rand_parts_top = Parent_handleg_top[Random.Range(0, 8)];
        //    return;
        //}
        ////プレイヤーの状態に応じてランダムで選抜される可動パーツが分かれる
        //switch (enemyManager.IsAvoid())
        //{
        //    //プレイヤーが画面端(左)
        //    case "left":
        //        switch (enemyManager.IsRotation_Top)
        //        {
        //            case Rotation_Top.None:
        //                //腕のみ
        //                m_rand_parts_top = Parent_handleg_top[Random.Range(0, 4)];
        //                break;
        //            case Rotation_Top.Rotation90:
        //                //左腕、左脚のみ
        //                while (m_rand_parts_top == (Parent_handleg_top[2] || Parent_handleg_top[3]))
        //                {
        //                    m_rand_parts_top = Parent_handleg_top[Random.Range(0, 6)];
        //                }
        //                break;
        //            case Rotation_Top.Rotation180:
        //                //脚のみ
        //                m_rand_parts_top = Parent_handleg_top[Random.Range(4, 8)];
        //                break;
        //            case Rotation_Top.Rotation270:
        //                //右腕、右脚のみ
        //                while (m_rand_parts_top == (Parent_handleg_top[4] || Parent_handleg_top[5]))
        //                {
        //                    m_rand_parts_top = Parent_handleg_top[Random.Range(2, 8)];
        //                }
        //                break;
        //        }
        //        break;
        //    //プレイヤーが画面端(右)
        //    case "right":
        //        switch (enemyManager.IsRotation_Top)
        //        {
        //            case Rotation_Top.None:
        //                //脚のみ
        //                m_rand_parts_top = Parent_handleg_top[Random.Range(4, 8)];
        //                break;
        //            case Rotation_Top.Rotation90:
        //                //左腕、左脚のみ
        //                while (m_rand_parts_top == (Parent_handleg_top[2] || Parent_handleg_top[3]))
        //                {
        //                    m_rand_parts_top = Parent_handleg_top[Random.Range(0, 6)];
        //                }
        //                break;
        //            case Rotation_Top.Rotation180:
        //                //腕のみ
        //                m_rand_parts_top = Parent_handleg_top[Random.Range(0, 4)];
        //                break;
        //            case Rotation_Top.Rotation270:
        //                //右腕、右脚のみ
        //                while (m_rand_parts_top == (Parent_handleg_top[4] || Parent_handleg_top[5]))
        //                {
        //                    m_rand_parts_top = Parent_handleg_top[Random.Range(2, 8)];
        //                }
        //                break;
        //        }
        //        break;

        //    //条件に引っかからなかったらランダム対象を全パーツに
        //    default:
        //        m_rand_parts_top = Parent_handleg_top[Random.Range(0, 8)];
        //        break;
        //}
    }
    //下エネミーの関節可動分岐
    /*上エネミ―と同じソースコード。今後変更が無ければひとまとめに*/
    private void ActivateParts_Bottom()
    {
        m_rand_parts_bottom = Parent_handleg_bottom[Random.Range(0, 8)];
        //switch (enemyManager.IsAvoid())
        //{
        //    //プレイヤーが画面端(左)
        //    case "left":
        //        switch (enemyManager.IsRotation_Bottom)
        //        {
        //            case Rotation_Bottom.None:
        //                //腕のみ
        //                m_rand_parts_bottom = Parent_handleg_bottom[Random.Range(0, 4)];
        //                break;
        //            case Rotation_Bottom.Rotation90:
        //                //左腕、左脚のみ
        //                while (m_rand_parts_bottom == (Parent_handleg_bottom[2] || Parent_handleg_bottom[3]))
        //                {
        //                    m_rand_parts_bottom = Parent_handleg_bottom[Random.Range(0, 6)];
        //                }
        //                break;
        //            case Rotation_Bottom.Rotation180:
        //                //脚のみ
        //                m_rand_parts_bottom = Parent_handleg_bottom[Random.Range(4, 8)];
        //                break;
        //            case Rotation_Bottom.Rotation270:
        //                //右腕、右脚のみ
        //                while (m_rand_parts_bottom == (Parent_handleg_bottom[4] || Parent_handleg_bottom[5]))
        //                {
        //                    m_rand_parts_bottom = Parent_handleg_bottom[Random.Range(2, 8)];
        //                }
        //                break;
        //        }
        //        break;
        //    //プレイヤーが画面端(右)
        //    case "right":
        //        switch (enemyManager.IsRotation_Bottom)
        //        {
        //            case Rotation_Bottom.None:
        //                //脚のみ
        //                m_rand_parts_bottom = Parent_handleg_bottom[Random.Range(4, 8)];
        //                break;
        //            case Rotation_Bottom.Rotation90:
        //                //左腕、左脚のみ
        //                while (m_rand_parts_bottom == (Parent_handleg_bottom[2] || Parent_handleg_bottom[3]))
        //                {
        //                    m_rand_parts_bottom = Parent_handleg_bottom[Random.Range(0, 6)];
        //                }
        //                break;
        //            case Rotation_Bottom.Rotation180:
        //                //腕のみ
        //                m_rand_parts_bottom = Parent_handleg_bottom[Random.Range(0, 4)];
        //                break;
        //            case Rotation_Bottom.Rotation270:
        //                //右腕、右脚のみ
        //                while (m_rand_parts_bottom == (Parent_handleg_bottom[4] || Parent_handleg_bottom[5]))
        //                {
        //                    m_rand_parts_bottom = Parent_handleg_bottom[Random.Range(2, 8)];
        //                }
        //                break;
        //        }
        //        break;

        //    //条件に引っかからなかったらランダム対象を全パーツに
        //    default:
        //        m_rand_parts_bottom = Parent_handleg_bottom[Random.Range(0, 8)];
        //        break;
        //}
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
