using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi
{
    public static class GUIStyleUtility
    {
		public static GUIStyle GetBoxStyle(Texture2D texture2D, RectOffset padding)
		{
			GUIStyle style = new GUIStyle(GUI.skin.box);
			style.normal.background = texture2D;
			style.padding = padding;

			return style;
		}
    }
}