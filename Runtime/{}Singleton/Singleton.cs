using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class Singleton<T>
	where T : Singleton<T>
{
	public static volatile SingletonEmbedded<T> S_Singleton_;
	public static T _Instance { get { return S_Singleton_._Instance; } }

	public Singleton()
	{
		S_Singleton_ = new SingletonEmbedded<T>(this as T);
	}
}