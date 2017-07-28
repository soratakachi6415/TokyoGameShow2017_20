using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class EventSender : MonoBehaviour
{
    enum EventType
    {
        RecordStart,
        ReplayStart,
        Stop
    }

    [SerializeField]
    EventType startSend;

    [SerializeField]
    EventType endSend;

    public string objectName;
    GameObject gameObj;

    TransformRecorder[] recorders;

    private void Awake()
    {
        gameObj = GameObject.Find(objectName);
        recorders = gameObj.GetComponentsInChildren<TransformRecorder>();
    }

    private void Start()
    {
        switch (startSend)
        {
            case EventType.RecordStart: RecordStart(); break;
            case EventType.ReplayStart: ReplayStart(); break;
            case EventType.Stop:        Stop();        break;
        }
    }

    private void OnDestroy()
    {
        switch (endSend)
        {
            case EventType.RecordStart: RecordStart(); break;
            case EventType.ReplayStart: ReplayStart(); break;
            case EventType.Stop:        Stop();        break;
        }
    }

    private void RecordStart()
    {
        foreach (var rec in recorders)
        {
            ExecuteEvents.Execute<IRecordEvent>(rec.gameObject, null,
                    (o, v) => { o.OnRecord(); }
                );
        }
    }

    private void ReplayStart()
    {
        foreach (var rec in recorders)
        {
            ExecuteEvents.Execute<IRecordEvent>(rec.gameObject, null,
                    (o, v) => { o.OnReplay(); }
                );
        }
    }

    private void Stop()
    {
        foreach (var rec in recorders)
        {
            ExecuteEvents.Execute<IRecordEvent>(rec.gameObject, null,
                    (o, v) => { o.OnStop(); }
                );
        }
    }
}
