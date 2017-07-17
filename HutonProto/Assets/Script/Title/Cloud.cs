using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cloud : MonoBehaviour
{
    [SerializeField]
    float   speed;
    Vector3 vec;

    RectTransform rectTrans;

    void Awake()
    {
        vec       = new Vector3(speed, 0);
        rectTrans = GetComponent<RectTransform>();
    }

    void Start()
    {

    }

    void Update()
    {
        rectTrans.position -= vec;

        Vector3 pos = rectTrans.localPosition;
        if (pos.x <= 0)
        {
            Vector3 changePos = rectTrans.localPosition;
            changePos.x = Screen.width + 550;
            changePos.y = Random.Range(-50.0f, 500.0f);

            rectTrans.localPosition = changePos;
        }
    }
}
