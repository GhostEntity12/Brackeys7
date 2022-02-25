using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Prompt : MonoBehaviour
{
	public KeyCode key;
	protected TextMeshProUGUI prompt;
	Transform child;
	protected bool active;
	new Camera camera;

	public float lerpTime = 0.3f;
	protected float lerpTimer;
	protected State state = State.Inactive;

	protected virtual void Awake()
	{
		prompt = GetComponentInChildren<TextMeshProUGUI>();
		prompt.text = key.ToString();
		child = transform.GetChild(0);
		camera = GameManager.Instance.camera;
	}

	protected virtual void Update()
	{
		if (state == State.Growing)
		{
			lerpTimer += Time.deltaTime;
			if (lerpTimer >= lerpTime)
			{
				state = State.Inactive;
			}

		}
		else if (state == State.Shrinking)
		{
			lerpTimer -= Time.deltaTime;
			if (lerpTimer <= 0)
			{
				state = State.Inactive;
				active = false;
			}
		}
		child.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, lerpTimer / lerpTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			state = State.Growing;
			UpdateRotation();
			active = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			state = State.Shrinking;
		}
	}
	private void FixedUpdate()
	{
		if (active)
		{
			UpdateRotation();
		}
	}

	void UpdateRotation() => child.rotation = camera.transform.rotation;
}
