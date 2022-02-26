using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptPickup : Prompt
{
	bool disabled;
	public GameManager.PickupsFlag type;

	// Start is called before the first frame update
	protected override void Awake()
	{
		base.Awake();

		prompt.text += "   Pick Up";
	}

	protected override void Update()
	{
		base.Update();
		if (!disabled && active && Input.GetKeyDown(key))
		{

			GameManager.Instance.SetFlag(type);
			disabled = true;
			state = State.Shrinking;
		}
	}

	protected override void OnShrinkComplete()
	{
		gameObject.SetActive(false);
	}
}
