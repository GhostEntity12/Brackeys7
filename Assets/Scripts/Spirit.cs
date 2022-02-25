using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
	[SerializeField, Multiline] private string dialogue;
	public string Dialogue => dialogue;

	Textbox textbox;

	void Update()
	{
		if (!textbox && Vector3.Distance(transform.position, GameManager.Instance.camera.transform.position) < GameManager.Instance.textboxDistance)
		{
			textbox = GameManager.Instance.textboxPool.Retrieve() as Textbox;
			textbox.Setup(this);
			textbox.OnFadeComplete += Deregister;
		}
		else if (textbox && Vector3.Distance(transform.position, GameManager.Instance.camera.transform.position) > GameManager.Instance.textboxDistance)
		{
			textbox.state = State.Shrinking;
		}
	}

	void Deregister()
	{
		textbox.OnFadeComplete -= Deregister;
		textbox = null;
	}
}
