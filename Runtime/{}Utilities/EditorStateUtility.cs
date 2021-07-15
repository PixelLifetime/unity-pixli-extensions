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

		//switch (playModeStateChange)
		//{
		//	case PlayModeStateChange.EnteredEditMode:

		//		if (EditorStateUtility.OnEnteredEditMode != null)
		//			EditorStateUtility.OnEnteredEditMode.Invoke();

		//		//foreach (InteractionTrigger interactionTrigger in GameObject.FindObjectsOfType<InteractionTrigger>())
		//		//{
		//		//	interactionTrigger.CheckForTriggerColliders();
		//		//}

		//		break;
		//	case PlayModeStateChange.ExitingEditMode:

		//		if (EditorStateUtility.OnExitingEditMode != null)
		//			EditorStateUtility.OnExitingEditMode.Invoke();

		//		break;
		//	case PlayModeStateChange.EnteredPlayMode:

		//		if (EditorStateUtility.OnEnteredPlayMode != null)
		//			EditorStateUtility.OnEnteredPlayMode.Invoke();

		//		//TODO: This is total shit, but I have no choise. The script reinitializes. If not using [InitializeOnLoad] it actually takes the references set and tries to call them. But by that time they are all null, thus it gives an error. Only solution would be to do the required things in Awake or Start and subscribe to Exiting to do what I was hoping to do. But it still doesn't work because I lose the reference to call on exiting play mode. So I will just do that in OnDrawGizmos.
		//		//foreach (InteractionTrigger interactionTrigger in GameObject.FindObjectsOfType<InteractionTrigger>())
		//		//{
		//		//	interactionTrigger.CheckForTriggerColliders();
		//		//}

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