using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SkinnedMeshToMeshRenderer : MonoBehaviour
{
	public Transform target;
	public Component[] list;

    void Awake()
    {

    }

    void Start()
    {

    }

	[ContextMenu("Generate")]
	void Convert()
	{
		transform.position   = Vector3.zero;
		transform.rotation   = Quaternion.identity;
		transform.localScale = Vector3.one;

		list = target.GetComponentsInChildren<SkinnedMeshRenderer>();

		foreach (SkinnedMeshRenderer s in list)
		{
			GameObject child = new GameObject();
			child.name                    = s.gameObject.name;
			child.transform.localPosition = s.transform.localPosition;
			child.transform.localRotation = s.transform.localRotation;
			child.transform.localScale    = s.transform.localScale;


			//初期化
			var f = child.AddComponent<MeshFilter>();
			var r = child.AddComponent<MeshRenderer>();

			//Array.Copy(s.materials, r.materials, s.materials.Length);
			f.mesh = s.sharedMesh;

			r.materials = s.materials;
			r.lightmapIndex                 = s.lightmapIndex;
			r.lightmapScaleOffset           = s.lightmapScaleOffset;
			r.lightProbeProxyVolumeOverride = s.lightProbeProxyVolumeOverride;
			r.lightProbeUsage               = s.lightProbeUsage;
			r.motionVectorGenerationMode    = s.motionVectorGenerationMode;
			r.probeAnchor                   = s.probeAnchor;
			r.realtimeLightmapIndex         = s.realtimeLightmapIndex;
			r.realtimeLightmapScaleOffset   = s.realtimeLightmapScaleOffset;
			r.receiveShadows                = s.receiveShadows;
			r.reflectionProbeUsage          = s.reflectionProbeUsage;

			child.transform.parent = transform;
		}
	}

    void Update()
    {

    }
}
