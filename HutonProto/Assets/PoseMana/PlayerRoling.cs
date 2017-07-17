using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoling : MonoBehaviour
{
    private Vector3 _startPos = Vector3.zero;

    private Dictionary<int, Transform> _dragObjDic;
    // Use this for initialization
    void Start()
    {
        _dragObjDic = new Dictionary<int, Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool OnTouchDown()
    {
        // タッチされたとき
        if(0 < Input.touchCount)
        {
            // タッチされている指の数だけ処理
            for(int i = 0; i < Input.touchCount; i++)
            {
                // タッチ情報をコピー
                Touch t = Input.GetTouch(i);

                switch (t.phase)
                {
                    case TouchPhase.Began: DragBegan(t); break;

                    case TouchPhase.Moved: DragMove(t); break;

                    case TouchPhase.Ended: DragEnd(t); break;

                    default: break;
                }
                return true;
            }
        }
        return false;
    }
    
    void DragBegan(Touch touch)
    {
        _startPos = touch.position;
        // タッチ下位置からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit)) return;

        // Rayを飛ばして当たったのが目当てのオブジェクトか
        if (hit.collider.tag != "OverallPlayer") return;

        Transform dragObj = hit.collider.transform;

        if (_dragObjDic.ContainsValue(dragObj)) return;

        _dragObjDic.Add(touch.fingerId, dragObj);
        
    }

    void DragMove(Touch touch)
    {
        Transform dragObj = _dragObjDic[touch.fingerId];

        if (dragObj == null) return;

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        Vector3 move = hit.point;
        move.z = dragObj.position.z;

        dragObj.position = move;
    }

    void DragEnd(Touch touch)
    {
        _dragObjDic.Remove(touch.fingerId);
    }
}
