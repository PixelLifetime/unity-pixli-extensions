using System.Collections;
using System.Collections.Generic;

using UnityEngine;
//using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MonoBehaviourSingletonEmbedded<T>
	where T : MonoBehaviour
{
	//? <Begin>
	//private static readonly object s_lock = new object();

	//private static bool? s_quitting;
	//? <End>

	public static T Instance_ { get; protected set; }

	public T _Instance
	{
		get
		{
			//? <Begin>
//			lock (s_lock)
//			{
//				if (Instance_ != null)
//					return Instance_;
//				else if (s_quitting.Value)
//					return null;

//#if UNITY_EDITOR
//				T[] instances = Object.FindObjectsOfType<T>();

//				if (instances.Length > 0)
//				{
//					Debug.LogWarning($"MonoBehaviourSingleton<{typeof(T)}> Multiple instances were detected. Make sure to fix this before building the application.");

//					if (instances.Length == 1)
//						Debug.LogWarning($"MonoBehaviourSingleton<{typeof(T)}> There was 1 instance found but it was never assigned. New instance will be created for general use.");
//					else
//						Debug.LogWarning($"MonoBehaviourSingleton<{typeof(T)}> There should never be more than one (Singleton) of type {typeof(T)} in the scene, but {instances.Length} were found. New instance will be created for general use.");

//					for (int a = 0; a < instances.Length; a++)
//					{
//						Debug.LogWarning($"Found MonoBehaviourSingleton<{typeof(T)}> instance on {instances[a].gameObject}.");
//					}
//				}
//#endif

//				Instance_ = new GameObject($"(Singleton) {typeof(T)}").AddComponent<T>();

//#if UNITY_EDITOR_DEBUG
//							Debug.LogWarning($"[INSTANTIATED] Instance of type - {typeof(T)} - was created because singleton _instance was null when requested. Make sure this is intended behaviour, if not - please create an instance of the object in your scene before using it.");
//#endif

//				return Instance_;
//			}
			//? <End>

			return Instance_;
		}
	}

	protected virtual void WarningMessage(T creator)
	{
#if UNITY_EDITOR
		// This is just a hint
		// If the types are different it will throw an error anyway in future steps
		if (creator.GetType() != typeof(T))
			Debug.LogWarning("Instance type: " + creator.GetType().Name + " and encapsulation type: " + typeof(T).Name + " are different. This may lead to errors or unexpected behavior.");
#endif
	}

	//? <Begin>
	//[RuntimeInitializeOnLoadMethod]
	//private static void RuntimeInitializeOnLoad() => Application.quitting += () => s_quitting = true;
	//? <End>

	protected virtual void InitializeInstance(T creator)
	{
		//? <Begin>
		//? Addressables and multithreading stuff...
//		if (Instance_ == null)
//			Instance_ = creator;
//		else if (Instance_ != creator)
//		{
//			Addressables.ReleaseInstance(creator.gameObject);

//#if UNITY_EDITOR_DEBUG
//					Debug.Log($"Another instance MonoBehaviourSingleton<{typeof(T)}> was trying to initialize and was deleted.");
//#endif
//		}

//		if (!s_quitting.HasValue)
//		{
//			s_quitting = false;

//			RuntimeInitializeOnLoad();
//		}
		//? </End>

		if (Instance_ == null)
		{
			Instance_ = creator;
			//Instance_.transform.SetParent(null);

			//Transform rootTransform = Instance_.transform;

			//while (rootTransform.parent != null)
			//	rootTransform = rootTransform.parent;

			//Object.DontDestroyOnLoad(Instance_.gameObject);
		}
		else if (Instance_ != creator)
		{
			Object.Destroy(creator.gameObject);
		}
	}

	public MonoBehaviourSingletonEmbedded(T creator)
	{
		this.WarningMessage(creator);
		this.InitializeInstance(creator);
	}
}

///<summary>
/// Snippet
/// 
/// public static MonoBehaviourSingletonEmbedded<$classname$> S_Singleton_ { get; private set; }
/// public static $classname$ _Instance { get { return S_Singleton_._Instance; ; } }
/// S_Singleton_ = new MonoBehaviourSingletonEmbedded<$classname$>(this);
///</summary>
///