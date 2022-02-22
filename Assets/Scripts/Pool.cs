using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pool : MonoBehaviour
{
	public Poolable sourceObject;
	public int defaultPoolSize = 5;
	readonly Queue<Poolable> itemPool = new Queue<Poolable>();

	private void Awake()
	{
		Poolable[] p = new Poolable[defaultPoolSize];
		for (int i = 0; i < defaultPoolSize; i++)
		{
			p[i] = Retrieve();
		}
		for (int i = 0; i < defaultPoolSize; i++)
		{
			Return(p[i]);
		}

	}

	/// <summary>
	/// Gets a pooled object
	/// </summary>
	/// <returns></returns>
	public virtual Poolable Retrieve()
	{
		Poolable newObject;
		if (itemPool.Count <= 1)
		{
			newObject = CreateNewPooledObject();
		}
		else
		{
			newObject = itemPool.Dequeue();
		}

		newObject.gameObject.SetActive(true);
		return newObject;
	}

	/// <summary>
	/// Returns an object to the pool
	/// </summary>
	/// <param name="returningObject"></param>
	public virtual void Return(Poolable returningObject)
	{
		itemPool.Enqueue(returningObject);
		returningObject.gameObject.SetActive(false);
	}

	/// <summary>
	/// Creates a new poolable object. Does not add it to the pool.
	/// </summary>
	/// <returns></returns>
	public virtual Poolable CreateNewPooledObject()
	{
		Debug.LogWarning($"<b><color=#ad5f15>[Pooling]</color></b> Ran out of items in the {sourceObject.name} pool, instantiating a new instance");
		Poolable newObject = Instantiate(sourceObject.gameObject).GetComponent<Poolable>();
		newObject.sourcePool = this;
		newObject.gameObject.name = sourceObject.name + " (Pooled)";
		return newObject;
	}
}
