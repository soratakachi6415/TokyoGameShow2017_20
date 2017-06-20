using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SlideViewer : MonoBehaviour
{
	[SerializeField]
	Image[] images;

	[SerializeField]
	Color activeColor;

	[SerializeField]
	Color deactiveColor;

	[SerializeField]
	SlideExtension slide;

	int index;

    void Awake()
    {

    }

    void Start()
    {
		index = slide.TargetNum;
		foreach (Image image in images)
		{
			image.color = deactiveColor;
		}
		images[index].color = activeColor;
    }

    void Update()
    {
		if (index == slide.TargetNum) return;

		images[index          ].color = deactiveColor;
		images[slide.TargetNum].color = activeColor;

		index = slide.TargetNum;
    }
}
