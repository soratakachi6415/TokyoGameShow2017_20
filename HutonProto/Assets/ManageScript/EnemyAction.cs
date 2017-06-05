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


    //フィールド
    #region
    private ReAppearState reAppStatus;

    private float[] Cnt; //内部カウント    
    
    //０～１の間で使用
    private float[] t; //上エネミーアクティブタイム

    private List<GameObject> Parent_handleg1;  //上エネミー手足のパーツ
    private List<GameObject> Parent_handleg2;  //下エネミー手足のパーツ

    private GameObject prevParts; //上エネミーランダム選抜一個前のパーツ
    private GameObject prevParts2; //下エネミーランダム選抜一個前のパーツ

    private GameObject m_rand_parts; //ランダムパーツ格納1
    private GameObject m_rand_parts2; //ランダムパーツ格納2

    private float m_rand_rotate_min; //ランダム最小角度
    private float m_rand_rotate_max; //ランダム最大角度
    private Quaternion m_rand_euler;
    private Quaternion m_rand_euler2;

    private Quaternion m_rand_rotate_range;　//回転角度範囲

    public bool actionFlag_up;  //上エネミー行動中フラグ
    public bool actionFlag_down; //下エネミー行動中フラグ

    private int initCoolT; //初期共通クールタイム
    private float coolTime; //上エネミークールタイム
    private float coolTime2; //下エネミークールタイム

    //確認用
    public GameObject topEnemy;
    public GameObject bottomEnemy;
    //
    private Vector3 topPos;
    private Vector3 bottomPos;

    private int [] Reappear;
    #endregion



    void Start () {
        //確認用
        topPos = topEnemy.transform.position;
        bottomPos = bottomEnemy.transform.position;
        //
        initCoolT = 0;
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
        Parent_handleg1.Add(GameObject.Find("Enemy_LeftHand1"));     //0
        Parent_handleg1.Add(GameObject.Find("Enemy_LeftHand2"));     //1
        Parent_handleg1.Add(GameObject.Find("Enemy_RightHand1"));    //2
        Parent_handleg1.Add(GameObject.Find("Enemy_RightHand2"));    //3
        Parent_handleg1.Add(GameObject.Find("Enemy_LeftLeg1"));      //4
        Parent_handleg1.Add(GameObject.Find("Enemy_LeftLeg2"));      //5
        Parent_handleg1.Add(GameObject.Find("Enemy_RightLeg1"));     //6
        Parent_handleg1.Add(GameObject.Find("Enemy_RightLeg2"));     //7

        Parent_handleg2.Add(GameObject.Find("Enemy2_LeftHand1"));     //0
        Parent_handleg2.Add(GameObject.Find("Enemy2_LeftHand2"));     //1
        Parent_handleg2.Add(GameObject.Find("Enemy2_RightHand1"));    //2
        Parent_handleg2.Add(GameObject.Find("Enemy2_RightHand2"));    //3
        Parent_handleg2.Add(GameObject.Find("Enemy2_LeftLeg1"));      //4
        Parent_handleg2.Add(GameObject.Find("Enemy2_LeftLeg2"));      //5
        Parent_handleg2.Add(GameObject.Find("Enemy2_RightLeg1"));     //6
        Parent_handleg2.Add(GameObject.Find("Enemy2_RightLeg2"));     //7
        #endregion

        m_rand_rotate_min = 0.2f;
        m_rand_rotate_max = 0.3f;
        SetParts_up();
        SetParts_down();
    }

    // Update is called once per frame
    void Update () {
        initCoolT++;
        if (initCoolT < 120) return; //最初の2秒はカウントしない

        Cnt[0] += Time.deltaTime;
        Cnt[1] += Time.deltaTime;

        //上のエネミー行動
        #region
        if (t[0] * 4 < 1f)
        {
            actionFlag_up = true;
            m_rand_parts.GetComponent<MeshRenderer>().material.color = Color.blue;  //debug
            m_rand_euler = new Quaternion(m_rand_euler.x, m_rand_euler.y, m_rand_euler.z, m_rand_parts.transform.rotation.w);
            m_rand_parts.transform.rotation = Quaternion.Slerp(m_rand_parts.transform.rotation, m_rand_euler, t[0]);
        }
        else 
        {
            actionFlag_up = false;
            m_rand_parts.GetComponent<MeshRenderer>().material.color = Color.yellow;  //debug
        }

        t[0] += Time.deltaTime * 0.05f;
        #endregion

        //下のエネミー行動
        #region
        if(t[1] * 4 < 1f)
        {
            if (prevParts == null && t[0] * 4< 1f) return;  //初回のみ上エネミ―の行動が終了するまでreturn

            m_rand_parts2.GetComponent<MeshRenderer>().material.color = Color.blue;  //debug
            actionFlag_down = true;
            m_rand_euler2 = new Quaternion(m_rand_euler2.x, m_rand_euler2.y, m_rand_euler2.z, m_rand_parts2.transform.rotation.w);
            m_rand_parts2.transform.rotation = Quaternion.Slerp(m_rand_parts2.transform.rotation, m_rand_euler2, t[1]);
        }
        else 
        {
            actionFlag_down = false;
            m_rand_parts2.GetComponent<MeshRenderer>().material.color = Color.yellow;  //debug
        }

        t[1] += Time.deltaTime * 0.05f;
        #endregion

        //クールタイム経過で初期化処理
        if ((coolTime < Cnt[0] || Reappear[0] > 0) && !actionFlag_up)
        {
           t[0] = 0;
           SetParts_up();
           Cnt[0] = 0;
            if (Reappear[0] > 0) Reappear[0]--;
        }
        else if((coolTime2 < Cnt[1] || Reappear[1] > 0) && !actionFlag_down)
        {
            t[1] = 0;
            SetParts_down();
            Cnt[1] = 0;
            if (Reappear[1] > 0) Reappear[1]--;
        }
	}

    private void SetParts_up()  //複数のパーツから1つを選抜し、ランダムで角度を決める
    {
        //クールタイムを決める
        coolTime = Random.Range(15f, 20f);

        //現在のパーツがNULLじゃなければ格納
        if (m_rand_parts != null)
        {
            prevParts = m_rand_parts;
        }
        //前パーツと被らないまでループ処理
        while (prevParts == m_rand_parts) 
        {
            //パーツをセット
            m_rand_parts = Parent_handleg1[Random.Range(0, 8)];  
        }

        //ランダムで回転角度決定
        m_rand_rotate_range = new Quaternion(m_rand_parts.transform.rotation.x, Random.Range(m_rand_rotate_min, m_rand_rotate_max),
                            m_rand_parts.transform.rotation.z, 0);

        if ((int)Random.Range(0, 2) == 0)
        {
            m_rand_euler = Quaternion.Euler(0, m_rand_parts.transform.rotation.y - m_rand_rotate_range.y, 0);
        }
        else
        {
            m_rand_euler = Quaternion.Euler(0, m_rand_parts.transform.rotation.y + m_rand_rotate_range.y, 0);
        }

    }

    private void SetParts_down() //上下エネミー両対応のコードは時間がかかるので普通にメソッド分け。時間に余裕あれば修正
    {
        //クールタイムを決める
        coolTime2 = Random.Range(15f, 20f);

        //現在のパーツがNULLじゃなければ格納
        if (m_rand_parts2 != null)
        {
            prevParts2 = m_rand_parts2;
        }
        //前パーツと被らないまでループ処理
        while (prevParts2 == m_rand_parts2)
        {
            //パーツをセット
            m_rand_parts2 = Parent_handleg2[Random.Range(0, 8)];
        }

        //ランダムで角度決定
        m_rand_rotate_range = new Quaternion(m_rand_parts2.transform.rotation.x, Random.Range(m_rand_rotate_min, m_rand_rotate_max),
                            m_rand_parts2.transform.rotation.z, 0);

        //体の左側と右側で角度調整
        if (m_rand_parts2.ToString().Contains("Left"))
        {
            //ランダムで回転方向を決める
            if (Random.Range(0, 2) == 0)
            {
                m_rand_euler2 = Quaternion.Euler(0, 180 - m_rand_rotate_range.y, 0);
            }
            else
            {
                m_rand_euler2 = Quaternion.Euler(0, -(180 - m_rand_rotate_range.y), 0);
            }
        }
        else if (m_rand_parts2.ToString().Contains("Right"))
        {
            if ((int)Random.Range(0, 2) == 0)
            {
                m_rand_euler2 = Quaternion.Euler(0, m_rand_rotate_range.y, 0);
            }
            else
            {
                m_rand_euler2 = Quaternion.Euler(0, -(m_rand_rotate_range.y), 0);
            }
        }
    }

    public void ReAppear(GameObject obj) //再出現時の行動判別
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

        if(obj.name == "Model_enemyTest") //上エネミ―
        {
            ReAppear_top(obj);
            Reappear[0] = 3;
        }
        else if(obj.name == "Model_enemy2Test") //下エネミー
        {
            ReAppear_bottom(obj);
            Reappear[1] = 3;
        }
    }

    private void ReAppear_top(GameObject obj)　////再出現時の回転と座標
    {
        switch (reAppStatus)
        {
            case ReAppearState.reApp12:
                obj.transform.rotation = new Quaternion(0, 90, 0,0);
                obj.transform.position = topPos;
                break;

            case ReAppearState.reApp34:
                obj.transform.rotation = new Quaternion(0, -180, 0, 0);
                obj.transform.position = new Vector3(-28,obj.transform.position.y,6);
                break;

            case ReAppearState.reApp56:
                obj.transform.rotation = new Quaternion(0, 0, 0, 0);
                obj.transform.position = new Vector3(28,obj.transform.position.y,6);
                break;

            case ReAppearState.reApp78:
                obj.transform.rotation = new Quaternion(0, 0, 0, 0);
                obj.transform.position = topPos;
                break;

            case ReAppearState.reApp910:
                obj.transform.rotation = new Quaternion(0, -180, 0, 0);
                obj.transform.position = topPos;
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
                break;

            case ReAppearState.reApp34:
                obj.transform.rotation = new Quaternion(0, 0, 0, 0);
                obj.transform.position = new Vector3(-14,obj.transform.position.y,5);
                break;

            case ReAppearState.reApp56:
                obj.transform.rotation = new Quaternion(0, 90, 0,0);
                obj.transform.position = bottomPos;
                break;

            case ReAppearState.reApp78:
                obj.transform.rotation = new Quaternion(0, -180, 0, 0);
                obj.transform.position = bottomPos;
                break;

            case ReAppearState.reApp910:
                obj.transform.rotation = new Quaternion(0, 0, 0, 0);
                obj.transform.position = bottomPos;
                break;
        }
    }
}
