using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
	PlayerController controller;

	private void Awake()
	{
		controller = GetComponent<PlayerController>();
	}
}
