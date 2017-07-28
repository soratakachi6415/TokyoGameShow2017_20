using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TransformRecorder : MonoBehaviour, IRecordEvent
{
    enum State
    {
        None,
        Record,
        Play,
    }

    State m_State = State.None;

    [System.Serializable]
    class Data
    {

        public Data(Vector3 pos, Quaternion rot)
        {
            position = pos;
            rotation = rot;
        }

        public Vector3    position;
        public Quaternion rotation;
    }

    List<Data> recordData = new List<Data>();


    int m_FrameIndex = 0;

    /// <summary> 何フレームに1回記録するのか </summary>
    const int X = 4;

    private void Update()
    {
        switch (m_State)
        {
            case State.Record: Record(); break;
            case State.Play:   Play();   break;
        }
    }

    private void Record()
    {
        if (m_FrameIndex % X == 0)
        {
            recordData.Add(new Data(transform.localPosition, transform.localRotation));
        }
        m_FrameIndex++;
    }

    private void Play()
    {
        int   index = m_FrameIndex / X;
        float rate  = ((float)(m_FrameIndex % X) / X);

        if (recordData.Count - 1 <= index)
        {
            //終了
            Debug.Log("Finish");
            m_State = State.None;
            return;
        }

        transform.localPosition = Vector3    .Lerp(recordData[index].position, recordData[index + 1].position, rate);
        transform.localRotation = Quaternion.Slerp(recordData[index].rotation, recordData[index + 1].rotation, rate);

        m_FrameIndex++;
    }

    public void OnRecord()
    {
        m_State = State.Record;
        m_FrameIndex = 0;
    }

    public void OnReplay()
    {
        m_State = State.Play;
        m_FrameIndex = 0;
    }

    public void OnStop()
    {
        m_State = State.None;
    }
}
