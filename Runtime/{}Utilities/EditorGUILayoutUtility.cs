using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
public static class EditorGUILayoutUtility
{
	public static readonly Color DEFAULT_COLOR = new Color(0f, 0f, 0f, 0.4f);
	public static readonly Vector2 DEFAULT_LINE_MARGIN = new Vector2(2f, 2f);

	public const float DEFAULT_LINE_HEIGHT = 1.2f;

	//! HorizontalLine
	public static void HorizontalLine(Color color, float height, Vector2 margin)
	{
		GUILayout.Space(margin.x);

		EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, height), color);

		GUILayout.Space(margin.y);
	}
	public static void HorizontalLine(Color color, float height) => EditorGUILayoutUtility.HorizontalLine(color, height, DEFAULT_LINE_MARGIN);
	public static void HorizontalLine(Color color, Vector2 margin) => EditorGUILayoutUtility.HorizontalLine(color, DEFAULT_LINE_HEIGHT, margin);
	public static void HorizontalLine(float height, Vector2 margin) => EditorGUILayoutUtility.HorizontalLine(DEFAULT_COLOR, height, margin);

	public static void HorizontalLine(Color color) => EditorGUILayoutUtility.HorizontalLine(color, DEFAULT_LINE_HEIGHT, DEFAULT_LINE_MARGIN);
	public static void HorizontalLine(float height) => EditorGUILayoutUtility.HorizontalLine(DEFAULT_COLOR, height, DEFAULT_LINE_MARGIN);
	public static void HorizontalLine(Vector2 margin) => EditorGUILayoutUtility.HorizontalLine(DEFAULT_COLOR, DEFAULT_LINE_HEIGHT, margin);

	public static void HorizontalLine() => EditorGUILayoutUtility.HorizontalLine(DEFAULT_COLOR, DEFAULT_LINE_HEIGHT, DEFAULT_LINE_MARGIN);

	//! ButtonMini
	public static bool ButtonMini(GUIContent content, GUIStyle style) => GUILayout.Button(
		content, 
		style, 
		GUILayout.Width(EditorGUIUtility.singleLineHeight), GUILayout.Height(EditorGUIUtility.singleLineHeight)
	);

	public static bool ButtonMini(GUIContent content) => GUILayout.Button(
		content,
		GUILayout.Width(EditorGUIUtility.singleLineHeight), GUILayout.Height(EditorGUIUtility.singleLineHeight)
	);

	//! Button HelpBox
	public static bool ButtonHelpBox(GUIContent content, SerializedProperty holdingStateSerializedProperty, string message)
	{
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();

		bool buttonPressed = GUILayout.Button(content);

		if (EditorGUILayoutUtility.ButtonMini(new GUIContent("!")))
			holdingStateSerializedProperty.boolValue = !holdingStateSerializedProperty.boolValue;

		EditorGUILayout.EndHorizontal();

		if (holdingStateSerializedProperty.boolValue)
			EditorGUILayout.HelpBox(message, MessageType.Info, true);

		EditorGUILayout.EndVertical();

		return buttonPressed;
	}

	public static bool ButtonHelpBox(string text, SerializedProperty holdingStateSerializedProperty, string message) => EditorGUILayoutUtility.ButtonHelpBox(
		new GUIContent(text), 
		holdingStateSerializedProperty, 
		message
	);

	public static bool ButtonHelpBox(GUIContent content, ref bool messageVisible, string message)
	{
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();

		bool buttonPressed = GUILayout.Button(content);

		if (EditorGUILayoutUtility.ButtonMini(new GUIContent("!")))
			messageVisible = !messageVisible;

		EditorGUILayout.EndHorizontal();

		if (messageVisible)
			EditorGUILayout.HelpBox(message, MessageType.Info, true);

		EditorGUILayout.EndVertical();

		return buttonPressed;
	}

	public static bool ButtonHelpBox(string text, ref bool messageVisible, string message) => EditorGUILayoutUtility.ButtonHelpBox(
		new GUIContent(text), 
		ref messageVisible, 
		message
	);

	public static T HelpBoxAdditive<T>(Func<T> mainAction, ref bool messageVisible, string message)
	{
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();

		T mainActionResult = mainAction.Invoke();

		if (EditorGUILayoutUtility.ButtonMini(new GUIContent("!")))
			messageVisible = !messageVisible;

		EditorGUILayout.EndHorizontal();

		if (messageVisible)
			EditorGUILayout.HelpBox(message, MessageType.Info, true);

		EditorGUILayout.EndVertical();

		return mainActionResult;
	}

	public static void HelpBoxAdditive(Action mainAction, ref bool messageVisible, string message)
	{
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();

		mainAction.Invoke();

		if (EditorGUILayoutUtility.ButtonMini(new GUIContent("!")))
			messageVisible = !messageVisible;

		EditorGUILayout.EndHorizontal();

		if (messageVisible)
			EditorGUILayout.HelpBox(message, MessageType.Info, true);

		EditorGUILayout.EndVertical();
	}

	//public static void DrawRect()
	//{
	//}
}
#endif