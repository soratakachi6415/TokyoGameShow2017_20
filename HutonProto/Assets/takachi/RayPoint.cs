using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RayPoint : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    private GameObject dragObj;

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseBegin();
            return;
        }
        if (Input.GetMouseButton(0))
        {
            DragMove();
            return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseBottonEnded();
        }
    }

    void DragMove()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);

        Vector3 move = hit.point;
        move.y = dragObj.transform.position.y;
        dragObj.transform.position = move;
    }

    void MouseBegin()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Player")
            {
                dragObj = hit.collider.gameObject;
            }
        }
    }

    void MouseBottonEnded()
    {
        dragObj = null;
    }
#else
    //このコンポーネントを削除しているのにも関わらずエラーが出る場合
    //下の行を削除してください
#endif
}
