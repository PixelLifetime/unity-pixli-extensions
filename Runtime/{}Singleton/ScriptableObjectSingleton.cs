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

#if UNITY_EDITOR
[InitializeOnLoad]
public class ScriptableObjectSingleton<T> : ScriptableObject
	where T : ScriptableObjectSingleton<T>
{
	//[MethodImpl(MethodImplOptions.AggressiveInlining)]
	protected virtual string GetInstanceDirectoryPath() => PathUtility.GetScriptFileDirectoryPath();

	private static T s_instance;
	public static T _Instance
	{
		get
		{
			if (s_instance == null)
			{
				T temporaryInstance = ScriptableObject.CreateInstance<T>();

				s_instance = AssetDatabaseUtility.LoadAssetOfType<T>(
					temporaryInstance.GetInstanceDirectoryPath(),
					".asset"
				);
				
				//? Loading of instance by name is a bad idea. When reference to the script is lost - it starts to recreate the asset every time with different name.
				// s_instance = AssetDatabase.LoadAssetAtPath<T>(
				// 	Path.Combine(
				// 		temporaryInstance.GetInstanceDirectoryPath(),
				// 		$"[{typeof(T).Name.ToDisplayValue()}] Default.asset"
				// 	)
				// );

				if (s_instance == null)
				{
					s_instance = ScriptableObjectUtility.CreateAsset<T>(
						path: temporaryInstance.GetInstanceDirectoryPath(),
						name: $"[{typeof(T).Name.ToDisplayValue()}]"
					);
				}

				// Dispose of temporary ScriptableObject.
#if UNITY_EDITOR
				if (!EditorApplication.isPlaying)
					Object.DestroyImmediate(temporaryInstance);
				else
#endif
					Object.Destroy(temporaryInstance);
			}

			return s_instance;

			//return s_instance ??
			//	(s_instance = AssetDatabase.LoadAssetAtPath<T>(
			//		Path.Combine(
			//			ScriptableObject.CreateInstance<T>().GetInstanceDirectoryPath(),
			//			$"[{typeof(T).Name}] Default.asset"
			//		))) ??
			//	(s_instance = ScriptableObjectUtility.CreateAsset<T>(
			//		path: ScriptableObject.CreateInstance<T>().GetInstanceDirectoryPath(),
			//		name: $"[{typeof(T).Name}] Default")
			//	);
		}
	}

	static ScriptableObjectSingleton()
	{
		EditorApplication.delayCall += () =>
		{
			if (_Instance == null)
				Debug.LogError($"Couldn't create instance of {typeof(T)}.");	
		};
	}
}
#endif