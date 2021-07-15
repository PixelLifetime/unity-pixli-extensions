using System.Collections;
using System.Collections.Generic;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi
{
    public static class GizmosUtility
    {
		private const float DEFAULT_TRANSPARENCY_ALPHA = 0.5f;

		public static void SetColor(Color color, float alpha)
		{
			color.a = alpha;
			Gizmos.color = color;
		}

		#region Line
		public static void DrawLine(Vector3 from, Vector3 to, Color color)
		{
			Gizmos.color = color;
			Gizmos.DrawLine(from, to);
		}

		public static void DrawLine(Vector3 from, Vector3 to, Color color, float alpha)
		{
			GizmosUtility.SetColor(color, alpha);
			Gizmos.DrawLine(from, to);
		}
		#endregion

		#region Single Cube
		public static void DrawCombinedCube(Vector3 center, Vector3 size, Color color)
		{
			Gizmos.color = color;
			Gizmos.DrawWireCube(center, size);
			Gizmos.DrawCube(center, size);
		}

		public static void DrawCombinedCube(Vector3 center, Vector3 size, Color color, Transform transform)
		{
			GizmosUtility.DrawCombinedCube(center, Vector3.Scale(size, transform.localScale), color);
		}

		public static void DrawCombinedCube(Vector3 center, Vector3 size, Color color, float alpha)
		{
			Gizmos.color = color;
			Gizmos.DrawWireCube(center, size);

			GizmosUtility.SetColor(color, alpha);
			Gizmos.DrawCube(center, size);
		}

		public static void DrawCombinedCube(Vector3 center, Vector3 size, Color color, Transform transform, float alpha)
		{
			GizmosUtility.DrawCombinedCube(center, Vector3.Scale(size, transform.localScale), color, alpha);
		}
		#endregion

		#region Multiple Cubes
		// Single

		public static void DrawCombinedCube(Vector3[] centers, Vector3 size, Color color)
		{
			Gizmos.color = color;

			for (int i = 0; i < centers.Length; i++)
			{
				Gizmos.DrawWireCube(centers[i], size);
				Gizmos.DrawCube(centers[i], size);
			}
		}

		public static void DrawCombinedCube(Vector3[] centers, Vector3 size, Color color, Transform transform)
		{
			GizmosUtility.DrawCombinedCube(centers, Vector3.Scale(size, transform.localScale), color);
		}

		public static void DrawCombinedCube(Vector3[] centers, Vector3 size, Color color, float alpha)
		{
			Gizmos.color = color;

			for (int i = 0; i < centers.Length; i++)
			{
				Gizmos.DrawWireCube(centers[i], size);
			}
			
			GizmosUtility.SetColor(color, alpha);

			for (int i = 0; i < centers.Length; i++)
			{
				Gizmos.DrawCube(centers[i], size);
			}
		}

		public static void DrawCombinedCube(Vector3[] centers, Vector3 size, Color color, Transform transform, float alpha)
		{
			GizmosUtility.DrawCombinedCube(centers, Vector3.Scale(size, transform.localScale), color, alpha);
		}

		// Double

		public static void DrawCombinedCube(Vector3[,] centers, Vector3 size, Color color)
		{
			Gizmos.color = color;

			for (int i = 0; i < centers.GetLength(0); i++)
			{
				for (int a = 0; a < centers.GetLength(2); a++)
				{
					Gizmos.DrawWireCube(centers[i, a], size);
					Gizmos.DrawCube(centers[i, a], size);
				}
			}
		}

		public static void DrawCombinedCube(Vector3[,] centers, Vector3 size, Color color, Transform transform)
		{
			GizmosUtility.DrawCombinedCube(centers, Vector3.Scale(size, transform.localScale), color);
		}

		public static void DrawCombinedCube(Vector3[,] centers, Vector3 size, Color color, float alpha)
		{
			Gizmos.color = color;

			for (int i = 0; i < centers.GetLength(0); i++)
			{
				for (int a = 0; a < centers.GetLength(2); a++)
				{
					Gizmos.DrawWireCube(centers[i, a], size);
				}
			}

			GizmosUtility.SetColor(color, alpha);

			for (int i = 0; i < centers.GetLength(0); i++)
			{
				for (int a = 0; a < centers.GetLength(2); a++)
				{
					Gizmos.DrawCube(centers[i, a], size);
				}
			}
		}

		public static void DrawCombinedCube(Vector3[,] centers, Vector3 size, Color color, Transform transform, float alpha)
		{
			GizmosUtility.DrawCombinedCube(centers, Vector3.Scale(size, transform.localScale), color, alpha);
		}

		// Tripple

		public static void DrawCombinedCube(Vector3[,,] centers, Vector3 size, Color color)
		{
			Gizmos.color = color;

			for (int i = 0; i < centers.GetLength(0); i++)
			{
				for (int a = 0; a < centers.GetLength(1); a++)
				{
					for (int b = 0; b < centers.GetLength(2); b++)
					{
						Gizmos.DrawWireCube(centers[i, a, b], size);
						Gizmos.DrawCube(centers[i, a, b], size);
					}
				}
			}
		}

		public static void DrawCombinedCube(Vector3[,,] centers, Vector3 size, Color color, Transform transform)
		{
			GizmosUtility.DrawCombinedCube(centers, Vector3.Scale(size, transform.localScale), color);
		}

		public static void DrawCombinedCube(Vector3[,,] centers, Vector3 size, Color color, float alpha)
		{
			Gizmos.color = color;

			for (int i = 0; i < centers.GetLength(0); i++)
			{
				for (int a = 0; a < centers.GetLength(1); a++)
				{
					for (int b = 0; b < centers.GetLength(2); b++)
					{
						Gizmos.DrawWireCube(centers[i, a, b], size);
					}
				}
			}

			GizmosUtility.SetColor(color, alpha);

			for (int i = 0; i < centers.GetLength(0); i++)
			{
				for (int a = 0; a < centers.GetLength(1); a++)
				{
					for (int b = 0; b < centers.GetLength(2); b++)
					{
						Gizmos.DrawCube(centers[i, a, b], size);
					}
				}
			}
		}

		public static void DrawCombinedCube(Vector3[,,] centers, Vector3 size, Color color, Transform transform, float alpha)
		{
			GizmosUtility.DrawCombinedCube(centers, Vector3.Scale(size, transform.localScale), color, alpha);
		}
		#endregion

		#region Mesh
		public static void DrawCombinedMesh(Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale, Color color, float alpha)
		{
			Gizmos.color = color;
			Gizmos.DrawWireMesh(mesh, position, rotation, scale);

			color.a = alpha;

			Gizmos.color = color;
			Gizmos.DrawMesh(mesh, position, rotation, scale);
		}

		public static void DrawCombinedMesh(Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale, Color color)
		{
			GizmosUtility.DrawCombinedMesh(mesh, position, rotation, scale, color, DEFAULT_TRANSPARENCY_ALPHA);
		}
		#endregion

		#region Sphere
		public static void DrawCombinedSphere(Vector3 center, float radius, Color color, float alpha)
		{
			Gizmos.color = color;
			Gizmos.DrawWireSphere(center, radius);

			color.a = alpha;

			Gizmos.color = color;
			Gizmos.DrawSphere(center, radius);
		}

		public static void DrawCombinedSphere(Vector3 center, float radius, Color color)
		{
			GizmosUtility.DrawCombinedSphere(center, radius, color, DEFAULT_TRANSPARENCY_ALPHA);
		}
		#endregion
	}
}