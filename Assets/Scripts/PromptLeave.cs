using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptLeave : Prompt
{
	// Update is called once per frame
	void Update()
	{
		base.Update();

		if (active && Input.GetKeyDown(key))
		{
			active = false;

			if (GameManager.Instance.AllPickups)
			{
				DoWin("good");
			}
			else
			{
				DoWin("bad");
			}
		}

	}

	void DoWin(string a) { }
}
