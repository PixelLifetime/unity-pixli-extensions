using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class TypeExtensions
{
#if UNITY_EDITOR
	public static FieldInfo GetFieldInfoViaPath(this Type type, string path)
	{
		FieldInfo fieldInfo = null;

		string[] fieldNames = path.Split('.');
		for (int a = 0; a < fieldNames.Length; a++)
		{
			if (type.IsArray)
			{
				type = type.GetElementType();
					
				a += 2;

				if (a >= fieldNames.Length)
				{
					Debug.LogError("It has reached array elements [behaviour is not supported yet].");
					break;
				}
			}
			else if (type.IsGenericType) //! Basically if it's a list. For more generic types [<,...,,,>] it won't work.
			{
				type = type.GetGenericArguments()[0]; //! Thus this way we take only 0 argument as List<T>.

				a += 2;

				if (a >= fieldNames.Length)
				{
					Debug.LogError("It has reached array elements [behaviour is not supported yet].");
					break;
				}
			}

			fieldInfo = type.GetField(fieldNames[a], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				
			type = fieldInfo.FieldType;
		}

		return fieldInfo;
	}
		
	public static TElement[] GetArrayElementsViaPath<TElement>(this Type type, string path, object @object)
	{
		FieldInfo fieldInfo = null;
		TElement[] elements = null;

		string[] fieldNames = path.Split('.');
		for (int a = 0; a < fieldNames.Length; a++)
		{
			if (type.IsArray)
			{
				elements = fieldInfo.GetValue(@object) as TElement[];

				return elements;
			}
			else if (type.IsGenericType) //! Basically if it's a list. For more generic types [<,...,,,>] it won't work.
			{
				IList<TElement> elementsList = fieldInfo.GetValue(@object) as IList<TElement>;

				elements = new TElement[elementsList.Count];

				for (int b = 0; b < elementsList.Count; b++)
					elements[b] = elementsList[b];

				return elements;
			}

			fieldInfo = type.GetField(fieldNames[a], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

			type = fieldInfo.FieldType;
		}

		return elements;
	}
#endif

	public static readonly string[] PATH_FIELD_NAMES_EXCEPTIONS_SUBSRTINGS = { "Array", "data[" };

	private static bool IsException(string fieldName)
	{
		for (int j = 0; j < TypeExtensions.PATH_FIELD_NAMES_EXCEPTIONS_SUBSRTINGS.Length; j++)
			if (fieldName.Substring(0, TypeExtensions.PATH_FIELD_NAMES_EXCEPTIONS_SUBSRTINGS[j].Length) == TypeExtensions.PATH_FIELD_NAMES_EXCEPTIONS_SUBSRTINGS[j])
				return true;

		return false;
	}

	/// <summary>
	/// Search to top of the hierarchy via fieldNames
	/// </summary>
	/// <param name="type"></param>
	/// <param name="fieldNames"></param>
	/// <returns>Parent object FieldInfo || null if has no parent</returns>
	public static FieldInfo GetParentObjectFieldInfoViaPath(this Type type, string[] fieldNames)
	{
		FieldInfo fieldInfo = null;

		for (int i = 0; i < fieldNames.Length - 1; i++)
		{
			//if (TypeExtensions.IsException(fieldNames[i]))
			//	continue;
			if (type.IsArray)
			{
				type = type.GetElementType();

				i += 2;
			}

			fieldInfo = type.GetField(fieldNames[i], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
				
			if (fieldInfo.FieldType.IsArray && i + 3 >= fieldNames.Length)
				break;

			type = fieldInfo.FieldType;
		}

		return fieldInfo;
	}

	public static FieldInfo GetParentObjectFieldInfoViaPath(this Type type, string path)
	{
		string[] fieldNames = path.Split('.');

		return type.GetParentObjectFieldInfoViaPath(fieldNames);
	}
}