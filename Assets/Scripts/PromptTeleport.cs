using UnityEngine;

public class PromptTeleport : Prompt
{
	enum FadeStatus
	{
		In, Out, None
	}

	public Transform teleportPoint;
	float holdTime = 2f;
	float holdTimer = 0f;
	FadeStatus fading = FadeStatus.None;
	public float bsTimer = 0.3f;


	protected override void Awake()
	{
		base.Awake();

		prompt.text += "   Enter";
	}

	protected override void Update()
	{
		base.Update();

		if (holdTimer > 0)
		{
			holdTimer = Mathf.Max(0, holdTimer - Time.deltaTime);
		}

		if (active && Input.GetKeyDown(key) && holdTimer == 0)
		{
			fading = FadeStatus.In;
			holdTimer = holdTime;
		}

		switch (fading)
		{
			case FadeStatus.In:
				GameManager.Instance.canvas.alpha += Time.deltaTime * 2;
				GameManager.Instance.canMove = false;
				if (GameManager.Instance.canvas.alpha == 1)
				{
					GameManager.Instance.player.transform.position = teleportPoint.position;
					fading = FadeStatus.Out;
				}
				break;
			case FadeStatus.Out:
				if (bsTimer == 0)
				{
					GameManager.Instance.canvas.alpha -= Time.deltaTime * 2;
					if (GameManager.Instance.canvas.alpha == 0)
					{
						GameManager.Instance.player.transform.position = teleportPoint.position;
						fading = FadeStatus.None;
						GameManager.Instance.canMove = true;
						bsTimer = 0.3f;
					}
				}
				else
				{
					bsTimer = Mathf.Max(0, bsTimer - Time.deltaTime);
				}
				break;
			default:
				break;
		}
	}
}
