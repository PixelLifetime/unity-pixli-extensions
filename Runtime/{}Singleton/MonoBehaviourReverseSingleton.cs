using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MonoBehaviourReverseSingleton<T> : MonoBehaviour
	where T : MonoBehaviourReverseSingleton<T>
{
	public static MonoBehaviourReverseSingletonEmbedded<T> S_Singleton_ { get; private set; }
	public static T _Instance { get { return S_Singleton_._Instance; ; } }

	protected virtual void Awake()
	{
		S_Singleton_ = new MonoBehaviourReverseSingletonEmbedded<T>(this as T);
	}
}