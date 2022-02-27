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
