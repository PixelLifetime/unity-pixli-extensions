/* Created by Pixel Lifetime */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi.RandomDistribution
{
	////public interface IRandom
	////{
	////	int Range(int min, int max);
	////	float Range(float min, float max);
	////}

	////public abstract class RandomBase : System.Random, IRandom
	////{
	////	private static RandomBase GlobalRandom { get; set; } = new Random();

	////	/// <summary>
	////	/// Returns random number between min and max.
	////	/// </summary>
	////	/// <param name="min">[inclusive]</param>
	////	/// <param name="max">[exclusive]</param>
	////	/// <returns></returns>
	////	public static int Range(int min, int max) => RandomBase.GlobalRandom.Next(min, max);

	////	/// <summary>
	////	/// Returns random number between min and max.
	////	/// </summary>
	////	/// <param name="min">[inclusive]</param>
	////	/// <param name="max">[inclusive]</param>
	////	/// <returns></returns>
	////	public static float Range(float min, float max) => (float)(min + RandomBase.GlobalRandom.NextDouble() * (max - min));

	////	///// <summary>
	////	///// Returns random number between min and max.
	////	///// </summary>
	////	///// <param name="min">[inclusive]</param>
	////	///// <param name="max">[exclusive]</param>
	////	///// <returns></returns>
	////	//int IRandom.Range(int min, int max) => this.Next(min, max);

	////	///// <summary>
	////	///// Returns random number between min and max.
	////	///// </summary>
	////	///// <param name="min">[inclusive]</param>
	////	///// <param name="max">[inclusive]</param>
	////	///// <returns></returns>
	////	//float IRandom.Range(float min, float max) => (float)(min + this.NextDouble() * (max - min));

	////	public RandomBase()
	////	{
	////	}

	////	public RandomBase(int Seed) : base(Seed)
	////	{
	////	}
	////}
	
	public class Random : System.Random
	{
		public static Random _GlobalRandom { get; } = new Random();
		public static Random GetGlobalRandom() => Random._GlobalRandom;

		/// <summary>
		/// Seed that was set for this random.
		/// </summary>
		public int Seed_ { get; private set; }

		/// <summary>
		/// Returns random number between min and max.
		/// </summary>
		/// <param name="min">[inclusive]</param>
		/// <param name="max">[exclusive]</param>
		/// <returns></returns>
		public int Range(int min, int max) => this.Next(min, max);

		/// <summary>
		/// Returns random number between min and max.
		/// </summary>
		/// <param name="min">[inclusive]</param>
		/// <param name="max">[inclusive]</param>
		/// <returns></returns>
		public float Range(float min, float max) => (float)(min + this.NextDouble() * (max - min));

		/// <summary>
		/// Returns a random number between 0f and 1f.
		/// </summary>
		public float Value => this.Range(0f, 1f);

		/// <summary>
		/// Returns a random number between -1f and 1f.
		/// </summary>
		public float SectionValue => this.Range(-1f, 1f);

		/// <summary>
		/// Returns a random number between -1f and 1f.
		/// </summary>
		public float Sign => this.SectionValue >= 0 ? 1f : -1f;

		/// <summary>
		/// Returns a random point inside a circle with radius 1f.
		/// </summary>
		public Vector2 InsideUnitCircle
		{
			get
			{
				float angle = 2 * Mathf.PI * this.Value;

				return new Vector2(Mathf.Sin(angle), Mathf.Cos(angle));
			}
		}

		public Random()
		{
		}

		public Random(int seed) : base(seed)
		{
			this.Seed_ = seed;
		}

#if UNITY_EDITOR
#endif
	}
}