using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemProviderMonoBehaviour<TProvidedData> : ProviderMonoBehaviour<TProvidedData>
{
	[SerializeField] private TProvidedData[] _items;
	public TProvidedData[] _Items => this._items;

	public override TProvidedData Provide() => this._items.Random();
}