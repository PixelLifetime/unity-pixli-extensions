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

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

//TODO: write summaries.
public abstract class SelectorAdapter<TSelector, TProvider, TProvidedData, TSelectedData> : SelectorScriptableObject<TProvider, TProvidedData, TSelectedData>
	where TSelector : Selector<TProvider, TProvidedData, TSelectedData>, new()
	where TProvider : IProvider<TProvidedData>
{
	public TSelector Selector;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override TSelectedData Select() => this.Selector.Select();

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override void Initialize(TProvider provider)
	{
		base.Initialize(provider);

		this.Selector = new TSelector();
		this.Selector.Initialize(provider);
	}

#if UNITY_EDITOR
#endif
}