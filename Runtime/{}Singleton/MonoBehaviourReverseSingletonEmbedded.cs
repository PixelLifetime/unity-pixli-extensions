using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class MonoBehaviourReverseSingletonEmbedded<T> : MonoBehaviourSingletonEmbedded<T>
	where T : MonoBehaviour
{
	protected override void InitializeInstance(T creator)
	{
		if (Instance_ != null)
			Object.Destroy(Instance_.gameObject);

		Instance_ = creator;
		Instance_.transform.SetParent(null);

		//Object.DontDestroyOnLoad(Instance_.gameObject);
	}

	public MonoBehaviourReverseSingletonEmbedded(T creator) : base(creator)
	{
	}
}