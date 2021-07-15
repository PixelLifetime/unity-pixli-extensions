using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "[Test] Selector Adapter", menuName = "[TESTs]/[Test] Selector Adapter")]
public class TestSelectorAdapter : SelectorAdapter<TestProvider.TestSelector, TestProvider, string, int>
{
}