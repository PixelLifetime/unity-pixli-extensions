using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[InitializeOnLoad]
public static class EditorStateUtility
{
	public static event Action OnEnteredEditMode;
	public static event Action OnExitingEditMode;
	public static event Action OnEnteredPlayMode;
	public static event Action OnExitingPlayMode;

	private static void OnPlaymodeStateChanged(PlayModeStateChange playModeStateChange)
	{
		//switch (playModeStateChange)
		//{
		//	case PlayModeStateChange.EnteredEditMode:

		//		if (EditorStateUtility.OnEnteredEditMode != null)
		//			EditorStateUtility.OnEnteredEditMode.Invoke();

		//		break;
		//	case PlayModeStateChange.ExitingEditMode:

		//		if (EditorStateUtility.OnExitingEditMode != null)
		//			EditorStateUtility.OnExitingEditMode.Invoke();

		//		break;
		//	case PlayModeStateChange.EnteredPlayMode:

		//		if (EditorStateUtility.OnEnteredPlayMode != null)
		//			EditorStateUtility.OnEnteredPlayMode.Invoke();

		//		break;
		//	case PlayModeStateChange.ExitingPlayMode:

		//		if (EditorStateUtility.OnExitingPlayMode != null)
		//			EditorStateUtility.OnExitingPlayMode.Invoke();

		//		break;
		//}
	}

	static EditorStateUtility()
	{
		//EditorApplication.playModeStateChanged -= EditorStateUtility.OnPlaymodeStateChanged;
		//EditorApplication.playModeStateChanged += EditorStateUtility.OnPlaymodeStateChanged;

		//BuildTargetGroup buildTargetGroup = BuildTargetGroup.Standalone | BuildTargetGroup.Android | BuildTargetGroup.iOS;

		//List<string> scriptingDefineSymbols = new List<string>(PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup).Split(';'));

		//string unity_3d_scriptingDefineSymbol = "UNITY_3D";

		//if (!scriptingDefineSymbols.Contains(unity_3d_scriptingDefineSymbol))
		//	PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, unity_3d_scriptingDefineSymbol);

		//string global_debug_scriptingDefineSymbol = "GLOBAL_DEBUG";

		//if (!scriptingDefineSymbols.Contains(global_debug_scriptingDefineSymbol))
		//	PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, global_debug_scriptingDefineSymbol);
	}
}
#endif