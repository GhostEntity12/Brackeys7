using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptPickup : Prompt
{
	bool disabled;

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

			Debug.Log("Picked Up");
			disabled = true;
			state = State.Shrinking;
		}
		if (disabled && lerpTime == 0)
		{
			gameObject.SetActive(false);
		}
	}
}
