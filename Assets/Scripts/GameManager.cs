using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
	[Flags] public enum PickupsFlag { Nothing = 0, Blood = 1, Footprint = 2, Drawings = 4, Hair = 8, Everything = 15 }
	public GameObject player;
	public int textboxDistance = 50;
	public float textboxBuffer = 3;
	public CanvasGroup canvas;
	[HideInInspector] public bool canMove = true;
	[HideInInspector] public bool canLook = true;

	public RectTransform clues;

	bool cluesMoving = false;
	float moveTimeCache;
	float cluesCacheX;

	PickupsFlag pickups;
	public bool AllPickups => pickups.HasFlag(PickupsFlag.Everything);

	public Image[] pickupImages = new Image[4];
	public Sprite[] pickupSprites = new Sprite[4];

	[HideInInspector]
	new public Camera camera;

	[HideInInspector]
	public TextboxPool textboxPool;


	bool isPaused;
	public CanvasGroup pauseMenu;

	bool quitting = false;

	protected override void Awake()
	{
		base.Awake();
		Cursor.lockState = CursorLockMode.Locked;
		camera = Camera.main;
		textboxPool = FindObjectOfType<TextboxPool>();
		player = FindObjectOfType<Player>().gameObject;
		cluesCacheX = clues.position.x;
	}

	public void SetFlag(PickupsFlag flag)
	{
		pickups |= flag;
		int index = (int)Mathf.Log((int)flag, 2);
		pickupImages[index].sprite = pickupSprites[index];
		moveTimeCache = Time.time;
		cluesMoving = true;
	}

	private void Update()
	{
		if (quitting)
		{
			canMove = false;
			canvas.alpha += Time.deltaTime * 2;
			if (canvas.alpha == 1)
			{
				SceneManager.LoadScene(0);
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SetPause(!isPaused);
		}

		if (isPaused && pauseMenu.alpha < 1)
		{
			pauseMenu.alpha += Time.deltaTime * 2;
		}
		else if (!isPaused && pauseMenu.alpha > 0)
		{
			pauseMenu.alpha -= Time.deltaTime * 2;
		}

		if (cluesMoving)
		{
			float t = Time.time - moveTimeCache;

			switch (t)
			{
				case float f when f < 1:
					clues.position = new Vector2(cluesCacheX, Mathf.Lerp(-120, 0, t));
					break;
				case float f when f > 3:
					clues.position = new Vector2(cluesCacheX, Mathf.Lerp(0, -120, t - 3));
					if (clues.position.y == -120)
					{
						cluesMoving = false;
					}
					break;
				default:
					break;
			}
		}
	}

	public void SetPause(bool paused)
	{
		Cursor.lockState = (CursorLockMode)(paused ? 0 : 1);
		isPaused = paused;
		pauseMenu.interactable = paused;
		canMove = !paused;
		canLook = !paused;
	}

	public void ToMenu() => quitting = true;
}