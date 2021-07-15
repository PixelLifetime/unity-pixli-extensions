using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi
{
#if UNITY_EDITOR
	public static class InspectorUtility 
    {
		public const float DEFAULT_FIELD_WIDTH = 50f;
		public const float DEFAULT_FIELD_HEIGHT = 16f;

		public const float ADDITIONAL_SPACE_HORIZONTAL_WIDTH_SMALL = 2f;
		public const float ADDITIONAL_SPACE_HORIZONTAL_WIDTH = 5f;

		public const float ADDITIONAL_SPACE_VERTICAL_HEIGHT_SMALL = 2f;
		public const float ADDITIONAL_SPACE_VERTICAL_HEIGHT = 6f;
		public const float ADDITIONAL_SPACE_VERTICAL_HEIGHT_BIG = 10f;

		public const float BURGER_WIDTH = 12f;
		public const float BURGER_HEIGHT = 8f;

		public const float MINIMAL_COMPACT_WIDTH = 325f;

		public const float BUTTON_WIDTH_SMALL = 20f;
		public const float BUTTON_WIDTH = 25f;
	}
#endif
}