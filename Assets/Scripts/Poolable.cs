using UnityEngine;

public abstract class Poolable : MonoBehaviour
{
	protected bool active;
	[System.NonSerialized]
	public Pool sourcePool;

	protected void ReturnToPool() => sourcePool.Return(this);
}
