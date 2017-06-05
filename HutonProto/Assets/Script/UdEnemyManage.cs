using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UdEnemyManage : MonoBehaviour {

    private int topEnemyhit;  //上エネミーヒット回数
    private int downEnemeyHit;  //下エネミーヒット回数

    private int topCnt;
    private int downCnt;

    private float topCoolT;
    private float bottomCoolT;

    public GameObject topEnemy;
    public GameObject bottomEnemy;

    //確認用
    private Vector3 upE;
    private Vector3 downE;

    // Use this for initialization
    void Start () {
        //確認用
        upE = topEnemy.transform.position;
        downE = bottomEnemy.transform.position;

        topEnemyhit = 0;
        downEnemeyHit = 0;
        topCnt = 0;
        downCnt = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if(topEnemyhit > 2)
        {
            //効果音再生
            if (topCnt == 1) GameObject.Find("SoundController").GetComponent<SoundsManager>().WalkFloor();

            if (topCnt > 2 && topCnt < 120)//起きるアニメーション　*現時点では指定座標に瞬間移動
            {
                topEnemy.transform.position = new Vector3(upE.x, upE.y, upE.z * 3);       
            }
            else if(topCnt >= 120)
            {
                GameObject.Find("ScriptController").GetComponent<EnemyAction>().ReAppear(topEnemy);
                topEnemyhit = 0;
                topCoolT = 0;
                topCnt = 0;
            }
            topCnt++;
        }
        if(downEnemeyHit > 1)
        {
            //効果音再生
            if (downCnt == 1) GameObject.Find("SoundController").GetComponent<SoundsManager>().WalkFloor();

            if (downCnt > 2 && downCnt < 120)
            {
                bottomEnemy.transform.position = new Vector3(downE.x, downE.y, downE.z * 3);
            }
            else if (downCnt >= 120)
            {
                GameObject.Find("ScriptController").GetComponent<EnemyAction>().ReAppear(bottomEnemy);
                downEnemeyHit = 0;
                bottomCoolT = 0;
                downCnt = 0;
            }
            downCnt++;
        }
        topCoolT += Time.deltaTime;
        bottomCoolT += Time.deltaTime;
    }

    public void upHit() //上エネミ―と衝突時カウントアップ
    {
        if (topCoolT > 0.5f && !(GameObject.Find("ScriptController").GetComponent<EnemyAction>().actionFlag_up))
        {
            topCoolT = 0;
            topEnemyhit++;
        }
    }

    public void downHit() //下エネミーと衝突時カウントアップ
    {
        if (bottomCoolT > 0.5f && !(GameObject.Find("ScriptController").GetComponent<EnemyAction>().actionFlag_down))
        {
            bottomCoolT = 0;
            downEnemeyHit++;
        }
    }
}
