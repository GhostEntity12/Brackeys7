using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public GameObject player;
    public int textboxDistance = 50;
	public float textboxBuffer = 3;
	public CanvasGroup canvas;
	public bool canMove = true;

	[HideInInspector]
    new public Camera camera;

	[HideInInspector]
	public TextboxPool textboxPool;

	protected override void Awake()
	{
		base.Awake();
		Cursor.lockState = CursorLockMode.Locked;
		camera = Camera.main;
		textboxPool = FindObjectOfType<TextboxPool>();
		player = FindObjectOfType<Player>().gameObject;
	}
}
