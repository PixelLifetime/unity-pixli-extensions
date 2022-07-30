using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixLi
{
	public interface IPriorityQueue<T>
	{
		int _Capacity { get; }

		int Count_ { get; }

		void Enqueue(T item, float priority, float heuristic);
		T Dequeue();

		void Update(T item, float priority);
		bool Contains(T item);
	}
}