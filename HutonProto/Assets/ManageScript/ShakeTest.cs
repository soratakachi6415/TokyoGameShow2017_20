using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeTest : MonoBehaviour {

    public float shakeTime;

    public Vector3 shakeRange = new Vector3(0.4f, 0.4f, 0);

    private float _shakeTime;
    private float _timer;

    private Vector3 _originPos;
    private bool _onShakeEnd;
     

	// Use this for initialization
	void Start () {
        _shakeTime = -1f;
        _timer = 0f;
        _originPos = transform.position;
        _onShakeEnd = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(_timer <= _shakeTime)
        {
            _onShakeEnd = true;
            _timer += Time.deltaTime;
            transform.position = _originPos + mulVector3(shakeRange, Random.insideUnitSphere);
        }
        else
        {
            if (_onShakeEnd)
            {
                transform.position = _originPos;
                _onShakeEnd = false;
            }
            _originPos = transform.position;
        }
	}

    public void Shake()
    {
        _timer = 0f;
        _shakeTime = shakeTime;
    }

    private Vector3 mulVector3(Vector3 a,Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
}
