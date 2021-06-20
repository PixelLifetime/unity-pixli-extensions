using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

using Object = UnityEngine.Object;

public static class AssetDatabaseUtility
{
#if UNITY_EDITOR
	public static T[] LoadAssetsAtPath<T>(string path, string extension = null)
		where T : Object
	{
		string[] fileEntries;

		try
		{
			fileEntries = Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Application.dataPath), path), "*" + extension);
		}
		catch (DirectoryNotFoundException)
		{
			return null;
		}

		if (!string.IsNullOrEmpty(extension))
		{
			// This is required. Because *xls will return .xls and .xlsx files, for example.
			fileEntries = fileEntries.Where((el) => { return Path.GetExtension(el) == extension; }).ToArray();
		}

		T[] assets = new T[fileEntries.Length];

		for (int i = 0; i < fileEntries.Length; i++)
			assets[i] = AssetDatabase.LoadAssetAtPath<T>(Path.Combine(path, Path.GetFileName(fileEntries[i])));

		//Array.Sort(assets, (el, other) => string.Compare(el.name, other.name));

		return assets;
	}

	public static T LoadAssetOfType<T>(string directoryPath, string extension = null)
		where T : Object
	{
		T[] assets = LoadAssetsAtPath<T>(directoryPath, extension);

		for (int a = 0; a < assets.Length; a++)
		{
			if (assets[a] != null)
				return assets[a];
		}

		return null;
	}
#endif
}