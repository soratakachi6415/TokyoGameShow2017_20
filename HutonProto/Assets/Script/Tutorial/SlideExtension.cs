using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Scrollbar))]
public class SlideExtension : MonoBehaviour
{
	[Header("分割する数"), SerializeField]
	int splitValue = 1;

	[Header("現在のターゲットから下の数値以上離れたら次のターゲットに遷移")]
	[Range(0.0f, 1.0f), SerializeField]
	float distance;

	[SerializeField]
	[Range(0.0f, 60.0f)]
	float scrollSpeed;

	int target;
	public int TargetNum
	{
		get
		{
			return target;
		}
	}

	float targetValue;

	Scrollbar scroll;
	bool isScrolls = false;

	void OnValidate()
	{
		splitValue = Mathf.Max(splitValue, 1);
	}

	void Awake()
    {
		scroll = GetComponent<Scrollbar>();

		float value = scroll.value;
		target = (int)Mathf.Floor(value * splitValue);

		//splitValue -> 0以下にはならない
		targetValue = target / splitValue;
    }

    void Start()
    {

    }

    void Update()
    {
		if (Input.GetMouseButtonDown(0) || Input.touchCount != 0)
		{
			isScrolls = false;
		}

		if (isScrolls)
		{
			Scroll();
		}

#if UNITY_EDITOR
		if (Input.GetMouseButtonUp(0))
		{
			CheckDistance();
		}
#else
		if (Input.touchCount == 0)
		{
			CheckDistance();
		}
#endif
    }

	void CheckDistance()
	{
		float dis = scroll.value - targetValue;
		if (Mathf.Abs(dis) >= distance)
		{
			target += (int)Mathf.Sign(dis);
			target = Mathf.Clamp(target, 0, splitValue - 1);
			Debug.Log(target);

			if (target == 0)
			{
				targetValue = 0.0f;
			}
			else
			if (target == splitValue - 1)
			{
				targetValue = 1.0f;
			}
			else
			{
				targetValue = (float)target / (float)(splitValue - 1);
			}

		}
		isScrolls = true;
	}

	void Scroll()
	{
		scroll.value = Mathf.Lerp(scroll.value, targetValue, scrollSpeed*Time.deltaTime);

		if (Mathf.Abs(scroll.value - targetValue) < 0.01f)
		{
			isScrolls = false;
		}
	}
}
