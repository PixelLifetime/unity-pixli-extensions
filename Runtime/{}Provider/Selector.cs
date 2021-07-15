using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

//TODO: write summaries.
public interface ISelector<TSelectedData>
{
	TSelectedData Select();
}

[Serializable]
public abstract class Selector<TSelectedData> : ISelector<TSelectedData>
{
	public abstract TSelectedData Select();

	public Selector()
	{
	}
}

[Serializable]
public abstract class Selector<TProvider, TProvidedData, TSelectedData> : Selector<TSelectedData>
	where TProvider : IProvider<TProvidedData>
{
	[SerializeField] protected TProvider provider;

	public virtual void Initialize(TProvider provider) => this.provider = provider;

	public Selector(TProvider provider)
	{
		this.Initialize(provider);
	}

	public Selector() : base()
	{
	}
}

public abstract class SelectorMonoBehaviour<TSelectedData> : MonoBehaviour, ISelector<TSelectedData>
{
	public abstract TSelectedData Select();

#if UNITY_EDITOR
#endif
}

public abstract class SelectorMonoBehaviour<TProvider, TProvidedData, TSelectedData> : SelectorMonoBehaviour<TSelectedData>
	where TProvider : IProvider<TProvidedData>
{
	[SerializeField] protected TProvider provider;

	public virtual void Initialize(TProvider provider) => this.provider = provider;

#if UNITY_EDITOR
#endif
}

public abstract class SelectorScriptableObject<TSelectedData> : ScriptableObject, ISelector<TSelectedData>
{
	public abstract TSelectedData Select();

#if UNITY_EDITOR
#endif
}

public abstract class SelectorScriptableObject<TProvider, TProvidedData, TSelectedData> : SelectorScriptableObject<TSelectedData>
	where TProvider : IProvider<TProvidedData>
{
	[SerializeField] protected TProvider provider;

	public virtual void Initialize(TProvider provider) => this.provider = provider;

#if UNITY_EDITOR
#endif
}