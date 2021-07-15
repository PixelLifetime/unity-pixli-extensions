using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProvider : ProviderMonoBehaviour<string>
{
	[SerializeField] private string[] _innerFieldAccessibleInNestedSelector;

	[SerializeField] private SelectorScriptableObject<TestProvider, string, int> _selector;

	public override string Provide()
	{
		return this._innerFieldAccessibleInNestedSelector[this._selector.Select()];
	}

	private void Awake()
	{
		this._selector.Initialize(this);
	}

	public class TestSelector : Selector<TestProvider, string, int>
	{
		public override int Select()
		{
			return this.provider._innerFieldAccessibleInNestedSelector.Length - 1;
		}

		public TestSelector(TestProvider provider) : base(provider)
		{
		}

		public TestSelector()
		{
		}
	}
}