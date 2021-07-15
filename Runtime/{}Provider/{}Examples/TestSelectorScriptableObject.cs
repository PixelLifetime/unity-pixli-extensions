using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "[Test] Selector Scriptable Object", menuName = "[TESTs]/[Test] Selector Scriptable Object")]
public class TestSelectorScriptableObject : SelectorScriptableObject<TestProvider, string, int>
{
	public override int Select()
	{
		return 0;
	}
}