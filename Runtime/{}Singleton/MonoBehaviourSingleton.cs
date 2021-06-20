/* Created by Max.K.Kimo */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class MonoBehaviourSingleton<T> : MonoBehaviour
	where T : MonoBehaviourSingleton<T>
{
	public static MonoBehaviourSingletonEmbedded<T> S_Singleton_ { get; private set; }
	public static T _Instance => S_Singleton_._Instance;

	protected virtual void Awake()
	{
		S_Singleton_ = new MonoBehaviourSingletonEmbedded<T>(this as T);
	}
}