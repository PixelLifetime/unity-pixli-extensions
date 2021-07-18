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

public static class CoroutineProcessorsCollection
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static IEnumerator InvokeAfterInline(YieldInstruction yieldInstruction, Action action)
	{
		yield return yieldInstruction;

		action.Invoke();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static IEnumerator InvokeAfterInline(CustomYieldInstruction customYieldInstruction, Action action)
	{
		yield return customYieldInstruction;

		action.Invoke();
	}

	public static IEnumerator InvokeAfter(YieldInstruction yieldInstruction, Action action) => 
		CoroutineProcessorsCollection.InvokeAfterInline(yieldInstruction, action);
		
	public static IEnumerator InvokeAfter(CustomYieldInstruction customYieldInstruction, Action action) => 
		CoroutineProcessorsCollection.InvokeAfterInline(customYieldInstruction, action);

	public static IEnumerator InvokeAfter(float seconds, Action action) => 
		CoroutineProcessorsCollection.InvokeAfterInline(new WaitForSeconds(seconds), action);

	public static IEnumerator InvokeAfterRealtime(float seconds, Action action) => 
		CoroutineProcessorsCollection.InvokeAfterInline(new WaitForSecondsRealtime(seconds), action);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static IEnumerator InvokeIndefinitelyInline(YieldInstruction yieldInstruction, Action action)
	{
		while (true)
		{
			action.Invoke();

			yield return yieldInstruction;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static IEnumerator InvokeIndefinitelyInline(CustomYieldInstruction customYieldInstruction, Action action)
	{
		while (true)
		{
			action.Invoke();

			yield return customYieldInstruction;
		}
	}

	public static IEnumerator InvokeIndefinitely(YieldInstruction yieldInstruction, Action action) =>
		CoroutineProcessorsCollection.InvokeIndefinitelyInline(yieldInstruction, action);

	public static IEnumerator InvokeIndefinitely(CustomYieldInstruction customYieldInstruction, Action action) =>
		CoroutineProcessorsCollection.InvokeIndefinitelyInline(customYieldInstruction, action);

#if UNITY_EDITOR
#endif
}