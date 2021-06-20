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

public enum PivotPreset
{
	TopLeft, TopCenter, TopRight,
	MiddleLeft, MiddleCenter, MiddleRight,
	BottomLeft, BottomCenter, BottomRight
}

public enum AnchorPreset
{
	TopLeft = PivotPreset.TopLeft, TopCenter = PivotPreset.TopCenter, TopRight = PivotPreset.TopRight,
	MiddleLeft = PivotPreset.MiddleLeft, MiddleCenter = PivotPreset.MiddleCenter, MiddleRight = PivotPreset.MiddleRight,
	BottomLeft = PivotPreset.BottomLeft, BottomCenter = PivotPreset.BottomCenter, BottomRight = PivotPreset.BottomRight,
	// Order matters.
	HorizontalStretchTop, HorizontalStretchMiddle, HorizontalStretchBottom,
	VerticalStretchLeft, VerticalStretchCenter, VerticalStretchRight,
	FullRectStretch
}

public static class RectTransformExtensions
{
	private static readonly Dictionary<int, Action<RectTransform>> s_anchorPreset_applyAnchorPresetAction_relations = new Dictionary<int, Action<RectTransform>>()
	{
		{
			(int)AnchorPreset.TopLeft,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(0f, 1f);
				rectTransform.anchorMax = new Vector2(0f, 1f);
			}
		},
		{
			(int)AnchorPreset.TopCenter,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(0.5f, 1f);
				rectTransform.anchorMax = new Vector2(0.5f, 1f);
			}
		},
		{
			(int)AnchorPreset.TopRight,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(1f, 1f);
				rectTransform.anchorMax = new Vector2(1f, 1f);
			}
		},
		{
			(int)AnchorPreset.MiddleLeft,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(0f, 0.5f);
				rectTransform.anchorMax = new Vector2(0f, 0.5f);
			}
		},
		{
			(int)AnchorPreset.MiddleCenter,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
				rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
			}
		},
		{
			(int)AnchorPreset.MiddleRight,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(1f, 0.5f);
				rectTransform.anchorMax = new Vector2(1f, 0.5f);
			}
		},
		{
			(int)AnchorPreset.BottomLeft,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(0f, 0f);
				rectTransform.anchorMax = new Vector2(0f, 0f);
			}
		},
		{
			(int)AnchorPreset.BottomCenter,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(0.5f, 0f);
				rectTransform.anchorMax = new Vector2(0.5f, 0f);
			}
		},
		{
			(int)AnchorPreset.BottomRight,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(1f, 0f);
				rectTransform.anchorMax = new Vector2(1f, 0f);
			}
		},
		{
			(int)AnchorPreset.HorizontalStretchTop,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(0f, 1f);
				rectTransform.anchorMax = new Vector2(1f, 1f);
			}
		},
		{
			(int)AnchorPreset.HorizontalStretchMiddle,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(0f, 0.5f);
				rectTransform.anchorMax = new Vector2(1f, 0.5f);
			}
		},
		{
			(int)AnchorPreset.HorizontalStretchBottom,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(0f, 0f);
				rectTransform.anchorMax = new Vector2(1f, 0f);
			}
		},
		{
			(int)AnchorPreset.VerticalStretchLeft,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(0f, 0f);
				rectTransform.anchorMax = new Vector2(0f, 1f);
			}
		},
		{
			(int)AnchorPreset.VerticalStretchCenter,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(0.5f, 0f);
				rectTransform.anchorMax = new Vector2(0.5f, 1f);
			}
		},
		{
			(int)AnchorPreset.VerticalStretchRight,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(1f, 0f);
				rectTransform.anchorMax = new Vector2(1f, 1f);
			}
		},
		{
			(int)AnchorPreset.FullRectStretch,
			delegate (RectTransform rectTransform)
			{
				rectTransform.anchorMin = new Vector2(0f, 0f);
				rectTransform.anchorMax = new Vector2(1f, 1f);
			}
		}
	};
	private static readonly Dictionary<int, Action<RectTransform>> s_pivotPreset_applyPivotPresetAction_relations = new Dictionary<int, Action<RectTransform>>()
	{
		{
			(int)PivotPreset.TopLeft,
			delegate (RectTransform rectTransform) { rectTransform.pivot = new Vector2(0f, 1f); }
		},
		{
			(int)PivotPreset.TopCenter,
			delegate (RectTransform rectTransform) { rectTransform.pivot = new Vector2(0.5f, 1f); }
		},
		{
			(int)PivotPreset.TopRight,
			delegate (RectTransform rectTransform) { rectTransform.pivot = new Vector2(1f, 1f); }
		},
		{
			(int)PivotPreset.MiddleLeft,
			delegate (RectTransform rectTransform) { rectTransform.pivot = new Vector2(0f, 0.5f); }
		},
		{
			(int)PivotPreset.MiddleCenter,
			delegate (RectTransform rectTransform) { rectTransform.pivot = new Vector2(0.5f, 0.5f); }
		},
		{
			(int)PivotPreset.MiddleRight,
			delegate (RectTransform rectTransform) { rectTransform.pivot = new Vector2(1f, 0.5f); }
		},
		{
			(int)PivotPreset.BottomLeft,
			delegate (RectTransform rectTransform) { rectTransform.pivot = new Vector2(0f, 0f); }
		},
		{
			(int)PivotPreset.BottomCenter,
			delegate (RectTransform rectTransform) { rectTransform.pivot = new Vector2(0.5f, 0f); }
		},
		{
			(int)PivotPreset.BottomRight,
			delegate (RectTransform rectTransform) { rectTransform.pivot = new Vector2(1f, 0f); }
		}
	};

	private static readonly Dictionary<int, PivotPreset> s_anchorPreset_pivotPreset_relations = new Dictionary<int, PivotPreset>()
	{
		{ (int)AnchorPreset.TopLeft, PivotPreset.TopLeft },
		{ (int)AnchorPreset.TopCenter, PivotPreset.TopCenter },
		{ (int)AnchorPreset.TopRight, PivotPreset.TopRight },
		{ (int)AnchorPreset.MiddleLeft, PivotPreset.MiddleLeft },
		{ (int)AnchorPreset.MiddleCenter, PivotPreset.MiddleCenter },
		{ (int)AnchorPreset.MiddleRight, PivotPreset.MiddleRight },
		{ (int)AnchorPreset.BottomLeft, PivotPreset.BottomLeft },
		{ (int)AnchorPreset.BottomCenter, PivotPreset.BottomCenter },
		{ (int)AnchorPreset.BottomRight, PivotPreset.BottomRight },
		{ (int)AnchorPreset.HorizontalStretchTop, PivotPreset.TopCenter },
		{ (int)AnchorPreset.HorizontalStretchMiddle, PivotPreset.MiddleCenter },
		{ (int)AnchorPreset.HorizontalStretchBottom, PivotPreset.BottomCenter },
		{ (int)AnchorPreset.VerticalStretchLeft, PivotPreset.MiddleLeft },
		{ (int)AnchorPreset.VerticalStretchCenter, PivotPreset.MiddleCenter },
		{ (int)AnchorPreset.VerticalStretchRight, PivotPreset.MiddleRight },
		{ (int)AnchorPreset.FullRectStretch, PivotPreset.MiddleCenter }
	};

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetAnchor(this RectTransform rectTransform, AnchorPreset anchorPreset) =>
		RectTransformExtensions.s_anchorPreset_applyAnchorPresetAction_relations[(int)anchorPreset].Invoke(rectTransform);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void SetPivot(this RectTransform rectTransform, PivotPreset pivotPreset) =>
		RectTransformExtensions.s_pivotPreset_applyPivotPresetAction_relations[(int)pivotPreset].Invoke(rectTransform);

	public static void Reset(this RectTransform rectTransform, AnchorPreset anchorPreset, bool setPivot = true, bool setPosition = true)
	{
		RectTransformExtensions.SetAnchor(rectTransform, anchorPreset);

		if (setPivot)
			RectTransformExtensions.SetPivot(rectTransform, RectTransformExtensions.s_anchorPreset_pivotPreset_relations[(int)anchorPreset]); // We want to be sure that mapping is done based on integral value.
			
		if (setPosition)
		{
			switch (anchorPreset)
			{
				case AnchorPreset.HorizontalStretchTop:
				case AnchorPreset.HorizontalStretchMiddle:
				case AnchorPreset.HorizontalStretchBottom:

					rectTransform.offsetMin = new Vector2(0f, rectTransform.offsetMin.y);
					rectTransform.offsetMax = new Vector2(0f, rectTransform.offsetMax.y);

					break;
				case AnchorPreset.VerticalStretchLeft:
				case AnchorPreset.VerticalStretchCenter:
				case AnchorPreset.VerticalStretchRight:

					rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, 0f);
					rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, 0f);

					break;
				case AnchorPreset.FullRectStretch:

					rectTransform.offsetMin = Vector2.zero;
					rectTransform.offsetMax = Vector2.zero;

					break;
			}

			rectTransform.anchoredPosition = Vector2.zero;
		}
	}

#if UNITY_EDITOR
#endif
}