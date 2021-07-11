using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

using PixLi;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public class ScriptableObjectSingleton<T> : ScriptableObject
	where T : ScriptableObjectSingleton<T>
{
#if UNITY_EDITOR
	//[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected virtual string GetInstanceDirectoryPath() => PathUtility.GetScriptFileDirectoryPath();
#endif

	private static T s_instance;
	public static T _Instance
	{
		get
		{
			if (s_instance == null)
			{
				s_instance = Container._Instance.Resolve<T>();

#if UNITY_EDITOR
				if (s_instance == null)
				{
					T temporaryInstance = ScriptableObject.CreateInstance<T>();

					s_instance = AssetDatabaseUtility.LoadAssetOfType<T>(
						temporaryInstance.GetInstanceDirectoryPath(),
						".asset"
					);

					if (s_instance == null)
					{
						s_instance = ScriptableObjectUtility.CreateAsset<T>(
							path: temporaryInstance.GetInstanceDirectoryPath(),
							name: $"[{typeof(T).Name.ToDisplayValue()}]"
						);
					}

					Container._Instance.Register(s_instance);

					// Dispose of temporary ScriptableObject.
					if (!EditorApplication.isPlaying)
						Object.DestroyImmediate(temporaryInstance);
					else
						Object.Destroy(temporaryInstance);
				}
#endif
			}

			return s_instance;
		}
	}

#if UNITY_EDITOR
	static ScriptableObjectSingleton()
	{
		EditorApplication.delayCall += () =>
		{
			if (_Instance == null)
				Debug.LogError($"Couldn't get or create instance of {typeof(T)}.");	
		};
	}
#endif
}