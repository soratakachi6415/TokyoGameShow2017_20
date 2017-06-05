using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour {

    private float Cnt;
    private float lifeCnt; //ライフの減少する時間
    private bool touch;

    private Vector3 m_Offset;
    private Vector3 m_ScreenPoint;

    // Use this for initialization
    void Start()
    {
        lifeCnt = GameObject.Find("ScriptController").GetComponent<LifeScript>().lifeDownTime_sec;
        touch = false;
        Cnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var rb = GetComponent<Rigidbody>();

        if (!touch)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionY;
        }
        else if(touch)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;   
        }
    }

    void OnMouseDown()
    {
        touch = true;
        //PC画面上のマウス座標を取得
        m_ScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        //マウス座標のXとYだけ使用
        float mousePositionX = Input.mousePosition.x;
        float mousePositionY = Input.mousePosition.y;

        //オブジェクト座標とマウス座標の"ズレ"を取得
        m_Offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(mousePositionX, mousePositionY, m_ScreenPoint.z));
    }

    void OnMouseDrag()
    {
        float mousePositionX = Input.mousePosition.x;
        float mousePositionY = Input.mousePosition.y;

        //PC画面上のマウス座標を取得
        Vector3 currentScreenPoint = new Vector3(mousePositionX, mousePositionY, m_ScreenPoint.z);

        //オブジェクト座標をマウス座標に同期(≒追尾)
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + m_Offset;
        
        //オブジェクトを移動させる
        transform.position = currentPosition;

        //動かし続けるとライフが一つ減る
        if(lifeCnt <= Cnt)
        {
            GameObject.Find("SoundController").GetComponent<SleepGageScript>().hitEnemy(false);
            Cnt = 0;
        }
        Cnt++;
    }

    void OnMouseUp()
    {
        touch = false;
    }
}
