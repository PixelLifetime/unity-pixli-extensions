using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEditor.Events
{
#if UNITY_EDITOR
	public static class UnityEventToolsUtility
	{
		public static void AddPersistentListener(UnityEventBase unityEventBase, UnityAction unityAction)
		{
			UnityEventTools.AddPersistentListener(unityEventBase);
			UnityEventTools.RegisterVoidPersistentListener(
				unityEventBase,
				unityEventBase.GetPersistentEventCount() - 1,
				unityAction
			);
		}
	}
#endif
}