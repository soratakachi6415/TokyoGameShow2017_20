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
#if UNITY_EDITOR

		if (Input.GetMouseButtonDown(0))
		{
			DragBegin(0, Input.mousePosition);
			return;
		}

		if (Input.GetMouseButtonUp(0))
		{
			DragEnd(0);
			return;
		}

		if (Input.GetMouseButton(0))
		{
			DragMove(0, Input.mousePosition);
		}
#else
		//いくつタッチされているか取得
		int touchCount = Input.touchCount;
        for (int i = 0; i < touchCount; i++)
        {
            //タッチの数だけ更新
            TouchUpdate(Input.GetTouch(i));
        }
#endif
	}

    void TouchUpdate(Touch touch)
    {
        switch (touch.phase)
        {
            //押した瞬間
            case TouchPhase.Began: DragBegin(touch.fingerId, touch.position); break;

            //ドラッグしている間
            case TouchPhase.Moved: DragMove (touch.fingerId, touch.position); break;

            //離した瞬間
            case TouchPhase.Ended: DragEnd  (touch.fingerId); break;

            //ここではなにもしない
            default: break;
        }
    }

    void DragBegin(int id, Vector3 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        //ヒットしてるか
        if (!Physics.Raycast(ray, out hit)) return;

        //目当てのオブジェクトか
        if (hit.collider.tag != "Player") return;

        Transform dragObject = hit.collider.transform;

        //既に他の指でドラッグされてたらそっちを優先にする
        if (dragObjDic.ContainsValue(dragObject)) return;

        //fingerIDで識別するため
        dragObjDic.Add(id, dragObject);
    }

    void DragMove(int id, Vector3 position)
    {
        Transform dragObject = dragObjDic[id];

        //何も見つからなかった -> null
        if (dragObject == null) return;

        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        //y軸の座標を固定して代入
        Vector3 move = hit.point;
        move.y = dragObject.position.y;

        dragObject.position = move;

		Rigidbody rig = dragObject.GetComponent<Rigidbody>();
		rig.AddForce(move - dragObject.position, ForceMode.Acceleration);
    }

    void DragEnd(int id)
    {
        //登録を削除
        dragObjDic.Remove(id);
    }
}
