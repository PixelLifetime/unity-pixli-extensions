using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


//TODO: Maybe redo this approach with Adressables.
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi
{
#if UNITY_EDITOR
	//[InitializeOnLoad]
#endif
	[CreateAssetMenu(fileName = "[Container]", menuName = "[Dependency Injection]/[Container]")]
	public class Container : ScriptableObject
	{
		private static readonly string S_CONTAINER_DEFAULT_ASSET_NAME = $"[{typeof(Container).Name.ToDisplayValue()}]";
		private static readonly string S_RELATIVE_PATH_TO_CONTAINER_INSTANCE = Path.Combine(PathUtility.AUTO_GENERATED_DIRECTORY_NAME, S_CONTAINER_DEFAULT_ASSET_NAME);
		private static readonly string S_RESOURCES_RELATIVE_PATH_TO_CONTAINER_INSTANCE = Path.Combine(PathUtility.RESOURCES_PATH_NAME, PathUtility.AUTO_GENERATED_DIRECTORY_NAME);

		private static Container s_instance;
		public static Container _Instance
		{
			get
			{
				if (s_instance == null)
				{
					s_instance = Resources.Load<Container>(
						path: S_RELATIVE_PATH_TO_CONTAINER_INSTANCE
					);

#if UNITY_EDITOR
					if (s_instance == null)
					{
						Directory.CreateDirectory(Path.Combine(Application.dataPath, S_RESOURCES_RELATIVE_PATH_TO_CONTAINER_INSTANCE));

						s_instance = ScriptableObjectUtility.CreateAsset<Container>(
							path: Path.Combine(PathUtility.ASSETS_PATH_NAME, S_RESOURCES_RELATIVE_PATH_TO_CONTAINER_INSTANCE),
							name: S_CONTAINER_DEFAULT_ASSET_NAME
						);
					}
#endif
				}

				return s_instance;
			}
		}

		private Dictionary<int, ScriptableObject> _scriptableObjectRegistry;

		//[Immutable]
		[SerializeField] private List<ScriptableObject> _scriptableObjects = new List<ScriptableObject>();
		public List<ScriptableObject> _ScriptableObjects => this._scriptableObjects;

		public void Register(ScriptableObject scriptableObject)
		{
			if (this._scriptableObjectRegistry == null)
			{
				this._scriptableObjectRegistry = new Dictionary<int, ScriptableObject>(capacity: this._scriptableObjects.Count);

				for (int a = 0; a < this._scriptableObjects.Count; a++)
				{
					this._scriptableObjectRegistry.Add(key: this._scriptableObjects[a].GetInstanceID(), value: this._scriptableObjects[a]);
				}
			}

			if (!this._scriptableObjectRegistry.ContainsKey(scriptableObject.GetInstanceID()))
			{
				this._scriptableObjectRegistry.Add(key: scriptableObject.GetInstanceID(), value: scriptableObject);

				this._scriptableObjects.Add(item: scriptableObject);
			}
		}

		public T Resolve<T>()
			where T : ScriptableObject
		{
			for (int a = 0; a < this._scriptableObjects.Count; a++)
			{
				if (this._scriptableObjects[a] is T scriptableObject)
				{
					return scriptableObject;
				}
			}

			return null;
		}

#if UNITY_EDITOR
		//static Container()
		//{
		//	EditorApplication.delayCall += () =>
		//	{
		//		if (_Instance == null)
		//			Debug.LogError($"Couldn't create instance of {typeof(Container)}.");
		//	};
		//}
#endif
	}
}