using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRaycast : MonoBehaviour
{
    //UIを操作する用のRaycast

    string selectedGameObjectname;

    void Start()
    {

    }

    void Update()
    {
        //マウスが押されたか
        if (Input.GetMouseButtonDown(0))
        {
            //マウスのクリックしたスクリーン座標をrayに変換
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit))
            {
                selectedGameObjectname =
                    hit.collider.gameObject.name;
                Debug.Log("name=" + selectedGameObjectname);
            }
        }


    }
}
