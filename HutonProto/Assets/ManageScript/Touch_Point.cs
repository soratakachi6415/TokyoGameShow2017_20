using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Point : MonoBehaviour
{
    private bool hit;
    //
    private Vector3 m_touchPoint;
    private Vector3 touch_Offset;
    private float Cnt;
    private float lifeCnt; //ライフの減少する時間
    //

    private Vector3 m_Offset;
    private Vector3 m_ScreenPoint;
    private static Vector3 m_TouchPosition = Vector3.zero;
    private static Vector3 m_PreviousPosition = Vector3.zero;


    void Start()
    {
        lifeCnt = GameObject.Find("ScriptController").GetComponent<LifeScript>().lifeDownTime_sec;
        Cnt = 0;
    }

    public static int touchCount
    {
        get
        {
            if (Application.isEditor)
            {
                if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return Input.touchCount;
            }
        }
    }

    /// <summary>
    /// タッチ情報、Unity内のTouch判定情報に空の情報として追加拡張。
    /// </summary>
    public enum TouchInfo
    {
        // 空 又は なし
        None = 99,
        // タッチ開始
        Began = 0,
        // タッチ移動
        Moved = 1,
        // タッチ静止
        Stationary = 2,
        // タッチ終了
        Ended = 3,
        // タッチキャンセル
        Cancle = 4,
    }

    ///*
    void OnCollisionStay(Collision other)
    {
        if(other.collider.tag == "Enemy")
        {
            hit = true;
        }
    }
    //*/


    /// <summary>
    /// PC上のマウスで何か押した(振れた？)場合
    /// </summary>
    void OnMouseDown()
    {
        // カメラのワールド空間のポジションをスクリーン空間に変える
        m_ScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        // 
        float mousePositionX = Input.mousePosition.x;
        float mousePositionY = Input.mousePosition.y;

        m_Offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(mousePositionX, mousePositionY, m_ScreenPoint.z));
    }

    void OnMouseDrag()
    {
        float mousePositionX = Input.mousePosition.x;
        float mousePositionY = Input.mousePosition.y;

        //Debug.Log(mousePositionX.ToString() + " - " + mousePositionY.ToString());

        Vector3 currentScreenPoint = new Vector3(mousePositionX, mousePositionY, m_ScreenPoint.z);

        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + m_Offset;

        transform.position = currentPosition;

        //動かし続けるとライフが一つ減る
        if (lifeCnt <= Cnt)
        {
            GameObject.Find("ScriptController").GetComponent<SleepGageScript>().hitEnemy(false);
            Cnt = 0;
        }
        Cnt++;
    }

    public static TouchInfo GetTouch(int n)
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))    { return TouchInfo.Began; }
            if (Input.GetMouseButton(0))        { return TouchInfo.Moved; }
            if (Input.GetMouseButtonUp(0))      { return TouchInfo.Ended; }
        }
        else
        {
            if (Input.touchCount >= n)
            {
                return (TouchInfo)((int)Input.GetTouch(n).phase);
            }
        }
        return TouchInfo.None;
    } 

    public static Vector3 GetTouchPosition(int n)
    {
        if (Application.isEditor)
        {
            TouchInfo info = Touch_Point.GetTouch(n);
            if (info != TouchInfo.None)
            {
                Vector3 currentPosition = Input.mousePosition;
                Vector3 delta = currentPosition - m_PreviousPosition;
                m_PreviousPosition = currentPosition;
                return delta;
            }
        }
        else
        {
            if (Input.touchCount >= n)
            {
                Touch touch = Input.GetTouch(n);
                m_PreviousPosition.x = touch.deltaPosition.x;
                m_PreviousPosition.y = touch.deltaPosition.y;
                return m_PreviousPosition;
            }
        }
        return Vector3.zero;
    }

    public static int getFingerId(int i)
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            return Input.GetTouch(i).fingerId;
        }
    }

    public static Vector3 GetTouchWorldPosition(Camera  cameram, int i)
    {
        return cameram.ScreenToWorldPoint(GetTouchPosition(i));
    }

    void Update()
    {

        //タッチ操作
        //if (Input.touchCount > 0)
        //{
        //    TouchInfo info = Touch_Point.GetTouch(0);

        //    if (GameObject.Find("ScriptController").GetComponent<SleepGageScript>().sleepImage == null) return;


        //    if (info == TouchInfo.Began)
        //    {
        //        // タッチ開始
        //        m_touchPoint = Camera.main.WorldToScreenPoint(transform.position);

        //        float touchPositionX = Input.GetTouch(0).position.x;
        //        float touchPositionY = Input.GetTouch(0).position.y;

        //        touch_Offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(touchPositionX, touchPositionY, m_touchPoint.z));
        //    }
        //    else
        //    {
        //        if (info == TouchInfo.Moved)
        //        {
        //            // タッチ移動
        //            float touchPositionX = Input.GetTouch(0).position.x;
        //            float touchPositionY = Input.GetTouch(0).position.y;

        //            Vector3 currentScreenPoint = new Vector3(touchPositionX, touchPositionY, m_touchPoint.z);

        //            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + touch_Offset;

        //            transform.position = currentPosition;

        //            if (lifeCnt <= Cnt)
        //            {
        //                GameObject.Find("ScriptController").GetComponent<SleepGageScript>().hitEnemy();
        //                Cnt = 0;
        //            }
        //            Cnt++;
        //        }
        //    }
        //}
    }
}
