using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if UNITY_EDITOR
public static class SerializedPropertyExtensions
{
	public static object GetParent(this SerializedProperty prop)
	{
		var path = prop.propertyPath.Replace(".Array.data[", "[");
		object obj = prop.serializedObject.targetObject;
		var elements = path.Split('.');
		foreach (var element in elements.Take(elements.Length - 1))
		{
			if (element.Contains("["))
			{
				var elementName = element.Substring(0, element.IndexOf("["));
				var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
				obj = GetValue(obj, elementName, index);
			}
			else
			{
				obj = GetValue(obj, element);
			}
		}
		return obj;
	}

	private static object GetValue(object source, string name)
	{
		if (source == null)
			return null;
		var type = source.GetType();
		var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
		if (f == null)
		{
			var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
			if (p == null)
				return null;
			return p.GetValue(source, null);
		}
		return f.GetValue(source);
	}

	private static object GetValue(object source, string name, int index)
	{
		var enumerable = GetValue(source, name) as IEnumerable;
		var enm = enumerable.GetEnumerator();
		while (index-- >= 0)
			enm.MoveNext();
		return enm.Current;
	}

	public static object GetValue(this SerializedProperty prop, object source, string name, int index) => SerializedPropertyExtensions.GetValue(source, name, index);

	/// <summary>
	/// 
	/// </summary>
	/// <param name="serializedProperty"></param>
	/// <returns>Enum name value</returns>
	public static string GetEnumName(this SerializedProperty serializedProperty)
	{
		return serializedProperty.enumNames[serializedProperty.enumValueIndex];
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="serializedProperty"></param>
	/// <returns>Enum capitalized and formatted name string value</returns>
	public static string GetEnumDisplayName(this SerializedProperty serializedProperty)
	{
		return serializedProperty.enumDisplayNames[serializedProperty.enumValueIndex];
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TEnum"></typeparam>
	/// <param name="serializedProperty"></param>
	/// <returns>Enum value</returns>
	public static TEnum GetEnumValue<TEnum>(this SerializedProperty serializedProperty)
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		return serializedProperty.enumNames[serializedProperty.enumValueIndex].ToEnum<TEnum>(false);
	}

	//TODO: move it to its own extensions.
	public static T Invoke<T>(this MethodInfo methodInfo, object @object, params object[] arguments) =>
		(T)methodInfo.Invoke(@object, arguments);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static IEnumerable<SerializedProperty> InlineGetChildren(SerializedProperty serializedProperty, int depth, Func<bool, bool> traversalFunction)
	{
		SerializedProperty currentProperty = serializedProperty.Copy();

		int targetDepth = currentProperty.depth + depth;

		//SerializedProperty nextSiblingProperty = serializedProperty.Copy();
		//{
		//	nextSiblingProperty.Next(false);
		//}
			
		if (traversalFunction.Method.Invoke<bool>(currentProperty, true))
		{
			do
			{
				if (currentProperty.depth <= serializedProperty.depth) //(SerializedProperty.EqualContents(currentProperty, nextSiblingProperty))
					break;

				//Debug.Log(currentProperty.name);

				//if (currentProperty.name == "data")
				//{
				//	foreach (var item in currentProperty.GetChildren())
				//	{
				//		Debug.Log("Children of data: " + item.name);
				//	}
				//}

				yield return currentProperty;
			}
			while (traversalFunction.Method.Invoke<bool>(currentProperty, currentProperty.depth < targetDepth));
		}
	}

	internal static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty serializedProperty, int depth, Func<bool, bool> traversalFunction) =>
		SerializedPropertyExtensions.InlineGetChildren(serializedProperty, depth, traversalFunction);

	public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty serializedProperty, int depth) =>
		SerializedPropertyExtensions.InlineGetChildren(serializedProperty, depth, serializedProperty.Next);

	public static IEnumerable<SerializedProperty> GetVisibleChildren(this SerializedProperty serializedProperty, int depth) =>
		SerializedPropertyExtensions.InlineGetChildren(serializedProperty, depth, serializedProperty.NextVisible);

	/// <summary>
	/// Gets all children of `SerializedProperty` at 1 level depth.
	/// </summary>
	/// <param name="serializedProperty">Parent `SerializedProperty`.</param>
	/// <returns>Collection of `SerializedProperty` children.</returns>
	public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty serializedProperty)
	{
		SerializedProperty currentProperty = serializedProperty.Copy();
		SerializedProperty nextSiblingProperty = serializedProperty.Copy();
		{
			nextSiblingProperty.Next(false);
		}

		if (currentProperty.Next(true))
		{
			do
			{
				if (SerializedProperty.EqualContents(currentProperty, nextSiblingProperty))
					break;

				yield return currentProperty;
			}
			while (currentProperty.Next(false));
		}
	}

	/// <summary>
	/// Gets visible children of `SerializedProperty` at 1 level depth.
	/// </summary>
	/// <param name="serializedProperty">Parent `SerializedProperty`.</param>
	/// <returns>Collection of `SerializedProperty` children.</returns>
	public static IEnumerable<SerializedProperty> GetVisibleChildren(this SerializedProperty serializedProperty)
	{
		SerializedProperty currentProperty = serializedProperty.Copy();
		SerializedProperty nextSiblingProperty = serializedProperty.Copy();
		{
			nextSiblingProperty.NextVisible(false);
		}

		if (currentProperty.NextVisible(true))
		{
			do
			{
				if (SerializedProperty.EqualContents(currentProperty, nextSiblingProperty))
					break;

				yield return currentProperty;
			}
			while (currentProperty.NextVisible(false));
		}
	}
		
	public static SerializedProperty GetVisibleChildAt(this SerializedProperty serializedProperty, int index)
	{
		int currentIndex = 0;

		SerializedProperty currentProperty = serializedProperty.Copy();
		SerializedProperty nextSiblingProperty = serializedProperty.Copy();
		{
			nextSiblingProperty.NextVisible(false);
		}

		if (currentProperty.NextVisible(true))
		{
			do
			{
				if (SerializedProperty.EqualContents(currentProperty, nextSiblingProperty))
					break;

				if (currentIndex++ == index)
					return currentProperty;
			}
			while (currentProperty.NextVisible(false));
		}

		return null;
	}

	//TODO: add getchildrenToDepth - make use of srProperty.depth.

	public static SerializedProperty FindProperty(this SerializedProperty serializedProperty, string propertyName)
	{
		SerializedProperty currentProperty = serializedProperty.Copy();
		SerializedProperty nextSiblingProperty = serializedProperty.Copy();
		{
			nextSiblingProperty.NextVisible(false);
		}

		//int initialDepth = currentProperty.depth;

		if (currentProperty.NextVisible(true))
		{
			do
			{
				if (SerializedProperty.EqualContents(currentProperty, nextSiblingProperty))
					break;

				if (currentProperty.name == propertyName)
					return currentProperty;
			}
			while (currentProperty.NextVisible(false));
		}

		return null;
	}

	public static void AddToObjectArray(this SerializedProperty serializedProperty, UnityEngine.Object item)
	{
		if (!serializedProperty.isArray)
			throw new ArgumentException("SerializedProperty " + serializedProperty.name + " is not an array.");

		serializedProperty.serializedObject.Update();

		// Add a null array item to the end of the array then populate it with the object parameter
		serializedProperty.InsertArrayElementAtIndex(serializedProperty.arraySize);
		serializedProperty.GetArrayElementAtIndex(serializedProperty.arraySize - 1).objectReferenceValue = item;

		serializedProperty.serializedObject.ApplyModifiedProperties();
	}

	public static void RemoveFromObjectArrayAt(this SerializedProperty serializedProperty, int index)
	{
		if (index < 0)
			throw new IndexOutOfRangeException("Index out of bounds");

		if (!serializedProperty.isArray)
			throw new ArgumentException("SerializedProperty " + serializedProperty.name + " is not an array");

		if (index > serializedProperty.arraySize - 1)
			throw new IndexOutOfRangeException("Index out of bounds");

		serializedProperty.serializedObject.Update();

		// If item at the index is non-null - ``delete it`` - make it null
		if (serializedProperty.GetArrayElementAtIndex(index).objectReferenceValue)
			serializedProperty.DeleteArrayElementAtIndex(index);

		// Delete a null array item at the index
		serializedProperty.DeleteArrayElementAtIndex(index);

		serializedProperty.serializedObject.ApplyModifiedProperties();
	}

	public static bool RemoveFromObjectArray(this SerializedProperty serializedProperty, UnityEngine.Object item)
	{
		if (!serializedProperty.isArray)
			throw new ArgumentException("SerializedProperty " + serializedProperty.name + " is not an array");

		if (item == null)
			throw new NullReferenceException("`item` cannot be null");

		serializedProperty.serializedObject.Update();

		for (int i = 0; i < serializedProperty.arraySize; i++)
		{
			SerializedProperty elementProperty = serializedProperty.GetArrayElementAtIndex(i);

			if (elementProperty.objectReferenceValue == item)
			{
				serializedProperty.RemoveFromObjectArrayAt(i);
				return true;
			}
		}

		return false;
	}

	public static void ClearObjectArray(this SerializedProperty serializedProperty)
	{
		serializedProperty.arraySize = 0;

		serializedProperty.serializedObject.ApplyModifiedProperties();
	}

	public static void DrawEditorGUILayoutArray(this SerializedProperty serializedProperty, bool includeChildren = true)
	{
		if (!serializedProperty.isArray)
			throw new TypeAccessException("Trying to draw an array, but SerializedProperty isn't an array.");

		for (int a = 0; a < serializedProperty.arraySize; a++)
			EditorGUILayout.PropertyField(serializedProperty.GetArrayElementAtIndex(a), includeChildren);
	}

	public static FieldInfo GetContainingObjectFieldInfo(this SerializedProperty serializedProperty)
	{
		Type containingObjectType = serializedProperty.serializedObject.targetObject.GetType();

		FieldInfo fieldInfo = containingObjectType.GetFieldInfoViaPath(serializedProperty.propertyPath);
		return fieldInfo;
	}

	public static FieldInfo GetParentObjectFieldInfo(this SerializedProperty serializedProperty)
	{
		Type containingObjectType = serializedProperty.serializedObject.targetObject.GetType();

		FieldInfo fieldInfo = containingObjectType.GetParentObjectFieldInfoViaPath(serializedProperty.propertyPath);
		return fieldInfo;
	}

	public static Type GetContainingObjectFieldType(this SerializedProperty serializedProperty)
	{
		Type containingObjectType = serializedProperty.serializedObject.targetObject.GetType();

		FieldInfo fieldInfo = containingObjectType.GetFieldInfoViaPath(serializedProperty.propertyPath);
		return fieldInfo.FieldType;
	}

	public static Type GetParentObjectFieldType(this SerializedProperty serializedProperty)
	{
		Type containingObjectType = serializedProperty.serializedObject.targetObject.GetType();

		FieldInfo fieldInfo = containingObjectType.GetParentObjectFieldInfoViaPath(serializedProperty.propertyPath);
		return fieldInfo.FieldType;
	}

	public static Type GetParentObjectFieldType(this SerializedProperty serializedProperty, string[] fieldNames)
	{
		Type containingObjectType = serializedProperty.serializedObject.targetObject.GetType();

		FieldInfo fieldInfo = containingObjectType.GetParentObjectFieldInfoViaPath(fieldNames);
		return fieldInfo.FieldType;
	}

	public static TElement[] GetValues<TElement>(this SerializedProperty serializedProperty, object @object)
	{
		Type containingObjectType = serializedProperty.serializedObject.targetObject.GetType();

		return containingObjectType.GetArrayElementsViaPath<TElement>(serializedProperty.propertyPath, @object);
	}

	public static TElement[] GetValues<TElement>(this SerializedProperty serializedProperty) =>
		SerializedPropertyExtensions.GetValues<TElement>(serializedProperty, serializedProperty.serializedObject.targetObject);

	public static object GetValue(this SerializedProperty serializedProperty)
	{
		return serializedProperty.GetContainingObjectFieldInfo().GetValue(serializedProperty.serializedObject.targetObject);
	}

	public static void SetValue(this SerializedProperty serializedProperty, object value)
	{
		serializedProperty.GetContainingObjectFieldInfo().SetValue(serializedProperty.serializedObject.targetObject, value);
	}

	public static object GetParentValue(this SerializedProperty serializedProperty)
	{
		return serializedProperty.GetParentObjectFieldInfo().GetValue(serializedProperty.serializedObject.targetObject);
	}

	public static void SetParentValue(this SerializedProperty serializedProperty, object value)
	{
		serializedProperty.GetParentObjectFieldInfo().SetValue(serializedProperty.serializedObject.targetObject, value);
	}

	public static object GetValueOfExternal(this SerializedProperty serializedProperty, SerializedProperty externalSerializedProperty)
	{
		return serializedProperty.GetContainingObjectFieldInfo().GetValue(externalSerializedProperty.GetValue());
	}

	public static void SetValueOfExternal(this SerializedProperty serializedProperty, SerializedProperty externalSerializedProperty, object value)
	{
		serializedProperty.GetContainingObjectFieldInfo().SetValue(externalSerializedProperty.GetValue(), value);
	}

	public static object GetParentValueOfExternal(this SerializedProperty serializedProperty, SerializedProperty externalSerializedProperty)
	{
		return serializedProperty.GetParentObjectFieldInfo().GetValue(externalSerializedProperty.GetParentValue());
	}

	public static void SetParentValueOfExternal(this SerializedProperty serializedProperty, SerializedProperty externalSerializedProperty, object value)
	{
		serializedProperty.GetParentObjectFieldInfo().SetValue(externalSerializedProperty.GetParentValue(), value);
	}
}
#endif