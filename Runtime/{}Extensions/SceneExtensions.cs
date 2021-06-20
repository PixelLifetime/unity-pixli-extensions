using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;

public static class SceneExtensions
{
	// Ideally - T : Object. But for now it only works for Component type of objects. Nonetheless we have access to GameObject only anyway, so it might make sense to do it T : Component.
	public static T FindObjectOfType<T>(this Scene scene)
		where T : Object
	{
		GameObject[] rootGameObjects = scene.GetRootGameObjects();

		for (int a = 0; a < rootGameObjects.Length; a++)
		{
			T component = rootGameObjects[a].GetComponentInChildren<T>();
			if (component != null)
				return component;
		}

		return null;
	}

	public static void GetAllGameObjects(this Scene scene, out List<GameObject> allGameObjects)
	{
		GameObject[] rootGameObjects = scene.GetRootGameObjects();

		allGameObjects = new List<GameObject>(rootGameObjects);

		for (int a = 0; a < rootGameObjects.Length; a++)
		{
			Transform[] childrenTransform = rootGameObjects[a].GetComponentsInChildren<Transform>();

			for (int b = 0; b < childrenTransform.Length; b++)
				allGameObjects.Add(childrenTransform[b].gameObject);
		}
	}

	public static GameObject[] GetAllGameObjects(this Scene scene)
	{
		scene.GetAllGameObjects(out List<GameObject> allGameObjects);

		return allGameObjects.ToArray();
	}

#if UNITY_EDITOR
#endif
}