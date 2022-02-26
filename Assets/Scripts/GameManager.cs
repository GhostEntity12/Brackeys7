using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
	[Flags] public enum PickupsFlag { Nothing = 0, Blood = 1, Footprint = 2, Drawings = 4, Hair = 8, Everything = 15 }
	public GameObject player;
    public int textboxDistance = 50;
	public float textboxBuffer = 3;
	public CanvasGroup canvas;
	public bool canMove = true;

	PickupsFlag pickups;
	public bool AllPickups => pickups.HasFlag(PickupsFlag.Everything);

	public Image[] pickupImages = new Image[4];
	public Sprite[] pickupSprites = new Sprite[4];

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

	public void SetFlag(PickupsFlag flag)
	{
		pickups |= flag;
		int index = (int)Mathf.Log((int)flag, 2);
		pickupImages[index].sprite = pickupSprites[index];
	}
}