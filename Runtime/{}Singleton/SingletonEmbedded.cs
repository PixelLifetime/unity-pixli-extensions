using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public sealed class SingletonEmbedded<T>
	where T : class
{
	private static readonly object _mutex = new object();

	private static volatile T s_instance;
	public T _Instance { get { return s_instance; } }

	public SingletonEmbedded(T creator)
	{
#if UNITY_EDITOR
		if (creator.GetType() != typeof(T))
			Debug.LogWarning("Instance type: " + creator.GetType().Name + " and encapsulation type: " + typeof(T).Name + " are different. This may lead to errors or unexpected behavior.");
#endif

		if (s_instance == null)
		{
			lock (_mutex)
			{
				if (s_instance == null)
				{
					s_instance = creator;
				}
			}
		}
	}
}

///<summary>
/// Snippet
/// 
/// public static volatile SingletonEmbedded<$classname$> S_Singleton_;
/// public static $classname$ _Instance { get { return S_Singleton_._Instance; } }
/// S_Singleton_ = new SingletonEmbedded<$classname$>(this);
///</summary>