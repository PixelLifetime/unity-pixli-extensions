using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class PathUtility
{
	public const string ASSETS_PATH_NAME = "Assets";
	public const string RESOURCES_PATH_NAME = "Resources";
	public const string PROJECT_SETTINGS_PATH_NAME = "ProjectSettings";
	public const string PACKAGES_PATH_NAME = "Packages";

	public const string SPECIFIC_PATH_NAME = "#_Specific";
	public const string SPECIFIC_RELATIVE_PATH = "Assets/#_Specific";

	public const string AUTO_GENERATED_DIRECTORY_NAME = "^Auto-Generated DO NOT DELETE";

#if UNITY_EDITOR
	/// <summary>
	/// 
	/// </summary>
	/// <param name="fullPath"></param>
	/// <returns></returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static string GetRelativePath(string fullPath) => 
		Path.Combine(PathUtility.ASSETS_PATH_NAME, fullPath.Substring(Application.dataPath.Length + 1));

	/// <summary>
	/// Get relative path to the file where call to this function happens.
	/// </summary>
	/// <returns>Relative path to the invoking file</returns>
	public static string GetScriptFilePath()
	{
		// It's pizdiec kostyl but whateva! It works, though.
		string scriptFilePath = new System.Diagnostics.StackTrace(true).GetFrame(1).GetFileName();

		return Path.Combine(
			PathUtility.ASSETS_PATH_NAME,
			scriptFilePath.Substring(Application.dataPath.Length + 1)
		);
	}

	/// <summary>
	/// Get relative path to the file folder where call to this function happens.
	/// </summary>
	/// <returns>Relative path to the invoking file folder</returns>
	public static string GetScriptFileDirectoryPath()
	{
		string scriptFilePath = new System.Diagnostics.StackTrace(true).GetFrame(1).GetFileName();

		return PathUtility.GetRelativePath(scriptFilePath.Substring(0, scriptFilePath.LastIndexOf(Path.DirectorySeparatorChar)));
	}

	public static string GetScriptFilePath(MonoBehaviour monoBehaviour)
	{
		MonoScript ms = MonoScript.FromMonoBehaviour(monoBehaviour);

		string scriptFilePath = AssetDatabase.GetAssetPath(ms);
		return scriptFilePath.Replace('\\', '/');
	}

	public static string GetScriptFilePath(ScriptableObject scriptableObject)
	{
		MonoScript ms = MonoScript.FromScriptableObject(scriptableObject);

		string scriptFilePath = AssetDatabase.GetAssetPath(ms);
		return scriptFilePath.Replace('\\', '/');
	}

	public static string GetScriptFileDirectoryPath(MonoBehaviour monoBehaviour)
	{
		string scriptFilePath = GetScriptFilePath(monoBehaviour);

		return scriptFilePath.Substring(0, scriptFilePath.LastIndexOf("/"));
	}

	public static string GetScriptFileDirectoryPath(ScriptableObject scriptableObject)
	{
		string scriptFilePath = GetScriptFilePath(scriptableObject);

		return scriptFilePath.Substring(0, scriptFilePath.LastIndexOf("/"));
	}
#endif
}