using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary> 現在のスライダーの値を可視化するコンポーネントです </summary>
public class SlideViewer : MonoBehaviour
{
    [SerializeField, Tooltip("画像コンポーネント")]
    Image[]        images;

    [SerializeField, Tooltip("有効時の色")]
    Color          activeColor;

    [SerializeField, Tooltip("無効時の色")]
    Color          deactiveColor;

    [SerializeField, Tooltip("参照するSlideExtensionコンポーネント")]
    SlideExtension slide;

    int            index;  //現在有効な画像

    void Start()
    {
        index = slide.currentPage;
        foreach (Image image in images)
        {
            image.color = deactiveColor;
        }
        images[index].color = activeColor;
    }

    void Update()
    {
        if (index == slide.currentPage) { return; }

        //アクティブの切り替え
        images[index            ].color = deactiveColor;
        images[slide.currentPage].color = activeColor;

        index = slide.currentPage;
    }
}
