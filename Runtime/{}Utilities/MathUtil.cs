using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtil
{
	public static float GetProjectionValue(Vector2 lhs, Vector2 rhs) => Vector2.Dot(lhs.normalized, rhs.normalized);
	public static float GetProjectionValue(Vector3 lhs, Vector3 rhs) => Vector3.Dot(lhs.normalized, rhs.normalized);

	public static Vector3 GetDirectionFromAngle(float angleInDegrees, float localRotationAngle = 0f)
	{
		angleInDegrees += localRotationAngle;
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0f, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}