using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ScriptReplacer : MonoBehaviour
{
    [SerializeField]
    GameObject gameObj;

    [ContextMenu("Replace")]
    void Replace()
    {
        var replays = gameObj.GetComponentsInChildren<TransformReplay>();
        foreach (var replay in replays)
        {
            DestroyImmediate(replay);
        }

        var ables = gameObj.GetComponentsInChildren<Replayable>();
        foreach (var able in ables)
        {
            able.gameObject.AddComponent<TransformRecorder>();
            DestroyImmediate(able);
        }
    }
}