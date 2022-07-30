using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixLi
{
	//public class ListPriorityQueue<T> : IPriorityQueue<T>
	//	where T : class
	//{
	//	private List<Data> _items = new List<Data>(256);

	//	public int _Capacity => this._items.Capacity;

	//	public int Count_ => this._items.Count;

	//	public void Enqueue(T item, float priority)
	//	{
	//		this._items.Add(new Data(item, priority));

	//		this._items.Sort((a, b) => a.Priority < b.Priority ? -1 : 1);
	//	}

	//	public T Dequeue()
	//	{
	//		T item = this._items[0].Item;

	//		this._items.RemoveAt(0);

	//		return item;
	//	}

	//	public void Update(T item, float priority)
	//	{
	//		int index = this._items.FindIndex(data => data.Item == item);
	//		this._items[index] = new Data(this._items[index].Item, priority);

	//		this._items.Sort((a, b) => a.Priority < b.Priority ? -1 : 1);
	//	}

	//	public bool Contains(T item) => this._items.FindIndex(data => data.Item == item) > 0;

	//	public struct Data
	//	{
	//		public T Item { get; set; }
	//		public float Priority { get; set; }

	//		public Data(T item, float priority) : this()
	//		{
	//			this.Item = item;
	//			this.Priority = priority;
	//		}
	//	}
	//}

	public class PriorityQueue<T> : IPriorityQueue<T>
		where T : class, PriorityQueue<T>.IItem
	{
		private float[] _heuristics;
		private float[] _priorities;
		private T[] _items;
		public int _Capacity => this._items.Length;

		public int Count_ { get; private set; }

		private void Swap(T a, T b)
		{
			////? DBUG
			//float aPriority = this._priorities[a.Index];
			//float bPriority = this._priorities[b.Index];

			int aIndex = a.Index;
			a.Index = b.Index;
			b.Index = aIndex;

			this._items[a.Index] = a;
			this._items[b.Index] = b;

			//this._priorities[a.Index] = aPriority;
			//this._priorities[b.Index] = bPriority;

			float aPriority = this._priorities[b.Index];
			this._priorities[b.Index] = this._priorities[a.Index];
			this._priorities[a.Index] = aPriority;

			float aHeuristic = this._heuristics[b.Index];
			this._heuristics[b.Index] = this._heuristics[a.Index];
			this._heuristics[a.Index] = aHeuristic;
		}

		private void SortUp(T item)
		{
			int parentItemIndex = (item.Index - 1) / 2;

			while (true)
			{
				T parentItem = this._items[parentItemIndex];

				int priorityComparisonResult = this._priorities[item.Index].CompareTo(this._priorities[parentItem.Index]);

				if (priorityComparisonResult < 0 || (priorityComparisonResult == 0 && this._heuristics[item.Index] < this._heuristics[parentItem.Index]))
					this.Swap(a: item, b: parentItem);
				else
					break;

				parentItemIndex = (item.Index - 1) / 2;
			}
		}

		public void Enqueue(T item, float priority, float heuristic)
		{
			item.Index = this.Count_;
			this._items[item.Index] = item;
			this._priorities[item.Index] = priority;
			this._heuristics[item.Index] = heuristic;

			this.SortUp(item: item);

			++this.Count_;
		}

		public void SortDown(T item)
		{
			while (true)
			{
				int leftChildIndex = item.Index * 2 + 1;
				int rightChildIndex = item.Index * 2 + 2;

				int swapIndex = 0;

				if (leftChildIndex < this.Count_)
				{
					swapIndex = leftChildIndex;

					int priorityComparisonResult = this._priorities[leftChildIndex].CompareTo(this._priorities[rightChildIndex]);

					//TODO: This is not stable/ordered priority queue. Sometimes it chooses items not in the order they were added which makes path shortest but sometimes weird viusally, like going over instead of straight line.
					//if (priorityComparisonResult == 0)
					//	this.Swap(a: this._items[leftChildIndex], b: this._items[rightChildIndex]);

					if (rightChildIndex < this.Count_)
					{
						if (priorityComparisonResult > 0 || (priorityComparisonResult == 0 && this._heuristics[leftChildIndex] > this._heuristics[rightChildIndex]))
						{
							swapIndex = rightChildIndex;
						}
					}

					priorityComparisonResult = this._priorities[item.Index].CompareTo(this._priorities[swapIndex]);

					if (priorityComparisonResult > 0 || (priorityComparisonResult == 0 && this._heuristics[item.Index] > this._heuristics[swapIndex]))
						this.Swap(a: item, b: this._items[swapIndex]);
					else
						return;
				}
				else
					return;
			}
		}

		public T Dequeue()
		{
			T topItem = this._items[0];
			this._items[0] = null;

			--this.Count_;

			if (this.Count_ > 0)
			{
				// Put last item in the top place.
				this._items[0] = this._items[this.Count_];
				this._items[0].Index = 0;

				this._priorities[0] = this._priorities[this.Count_];
				this._heuristics[0] = this._heuristics[this.Count_];

				this.SortDown(this._items[0]);
			}

			return topItem;
		}

		//? Doesn't really update. We only have a scenario of sort up in pathfinding.
		public void Update(T item, float priority)
		{
			this._priorities[item.Index] = priority;

			this.SortUp(item: item);
		}

		public bool Contains(T item) => object.Equals(this._items[item.Index], item);

		public PriorityQueue(int capacity)
		{
			this._items = new T[capacity];
			this._priorities = new float[this._items.Length];
			this._heuristics = new float[this._items.Length];
		}

		public interface IItem
		{
			int Index { get; set; }
		}

		//public struct Data
		//{
		//	T Item { get; set; }
		//	float Priority { get; set; }
		//}
	}
}