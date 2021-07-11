using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class SceneManagerUtility
{
	public static string GetSceneNameFromBuildIndex(int buildIndex) => Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(buildIndex));

#if UNITY_EDITOR
	public static int GetSceneBuildIndex(SceneAsset sceneAsset)
	{
		for (int a = 0; a < SceneManager.sceneCountInBuildSettings; a++)
			if (Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(a)) == sceneAsset.name)
				return a;

		Debug.LogError("Make sure that the scene you are trying get index of is in the Build Settings.");

		return -1;
	}
#endif
}