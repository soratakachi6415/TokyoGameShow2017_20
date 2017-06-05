using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RayPointTouch : MonoBehaviour
{
    private Dictionary<int, Transform> dragObjDic;

    // Use this for initialization
    void Awake()
    {
        dragObjDic = new Dictionary<int, Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //いくつタッチされているか取得
        int touchCount = Input.touchCount;
        for (int i = 0; i < touchCount; i++)
        {
            //タッチの数だけ更新
            TouchUpdate(Input.GetTouch(i));
        }
    }

    void TouchUpdate(Touch touch)
    {
        switch (touch.phase)
        {
            //押した瞬間
            case TouchPhase.Began: DragBegin(touch); break;

            //ドラッグしている間
            case TouchPhase.Moved: DragMove (touch); break;

            //離した瞬間
            case TouchPhase.Ended: DragEnd  (touch); break;

            //ここではなにもしない
            default: break;
        }
    }

    void DragBegin(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;

        //ヒットしてるか
        if (!Physics.Raycast(ray, out hit)) return;
        
        //目当てのオブジェクトか
        if (hit.collider.tag != "Player") return;   

        Transform dragObject = hit.collider.transform;

        //既に他の指でドラッグされてたらそっちを優先にする
        if (dragObjDic.ContainsValue(dragObject)) return; 

        //fingerIDで識別するため
        dragObjDic.Add(touch.fingerId, dragObject);
    }

    void DragMove(Touch touch)
    {
        Transform dragObject = dragObjDic[touch.fingerId];

        //何も見つからなかった -> null
        if (dragObject == null) return;

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);
        
        //y軸の座標を固定して代入
        Vector3 move = hit.point;
        move.y = dragObject.position.y;

        dragObject.position = move;
    }

    void DragEnd(Touch touch)
    {
        //登録を削除
        dragObjDic.Remove(touch.fingerId);
    }
}
