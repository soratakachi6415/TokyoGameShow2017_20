using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SlideArrow : MonoBehaviour
{
    public enum HideFlag
    {
        Start,
        End
    }

    [SerializeField]
    private SlideExtension slide;

    [SerializeField]
    private HideFlag hideFlag;

    Image image;

    delegate bool CheckHide();
    CheckHide checkHide;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        //あらかじめ処理する関数を決定する
        if (hideFlag == HideFlag.Start)
        {
            checkHide = () => { return !slide.isScrollStart; };
        }
        else
        {
            checkHide = () => { return !slide.isScrollEnd;   };
        }
    }

    void LateUpdate()
    {
        image.enabled = checkHide();
    }
}
