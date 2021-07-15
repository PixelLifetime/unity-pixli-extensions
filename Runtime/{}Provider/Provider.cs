using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

//TODO: write summaries.
public interface IProvider<TProvidedData>
{
	TProvidedData Provide();
}

public abstract class Provider<TProvidedData> : IProvider<TProvidedData>
{
	public abstract TProvidedData Provide();
}

public abstract class ProviderMonoBehaviour<TProvidedData> : MonoBehaviour, IProvider<TProvidedData>
{
	public abstract TProvidedData Provide();
}

public abstract class ProviderScriptableObject<TProvidedData> : ScriptableObject, IProvider<TProvidedData>
{
	public abstract TProvidedData Provide();
}