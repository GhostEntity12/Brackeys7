using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T Instance { get; private set; }
	protected virtual void Awake()
	{
		if (Instance != null)
		{
			Debug.LogError($"An instance of ${this.name} already exists");
			Destroy(gameObject);
			return;
		}
		Instance = this as T;
	}
}
