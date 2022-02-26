using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	Rigidbody rb;

	[Header("Movement")]
	public float movementSpeed = 5f;
	public float rotateSpeed = 5f;
	private Vector3 movement;


	[Header("Camera")]
	public Transform cameraFollow;
	public Transform rotator;

	[Space(10)]
	public float yawSpeed = 2.0f;
	public float pitchSpeed = 2.0f;
	private float yaw = 0.0f;
	private float pitch = 0.0f;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		movement = GameManager.Instance.canMove ? UniformInput.GetAnalogStick() : Vector3.zero;
		yaw += yawSpeed * Input.GetAxis("Mouse X");
		pitch -= pitchSpeed * Input.GetAxis("Mouse Y");
		pitch = Mathf.Clamp(pitch, -40, 80);
	}

	private void FixedUpdate()
	{
		rb.MovePosition(transform.position + (movementSpeed * Time.fixedDeltaTime * rotator.TransformVector(movement)));
	}

	private void LateUpdate()
	{
		cameraFollow.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
		rotator.rotation = Quaternion.Euler(0, cameraFollow.transform.eulerAngles.y, 0);
		rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, rotator.rotation.normalized, rotateSpeed));
	}
}

/// <summary>
///  Via https://amorten.com/blog/2017/mapping-square-input-to-circle-in-unity/, modified to use Vector3
/// </summary>
public static class UniformInput
{
	/// <summary>
	/// Remaps the horizontal/vertical input to a perfect circle instead of a square.
	/// This prevents the issue of characters speeding up when moving diagonally, but maintains the analog stick sensivity.
	/// </summary>
	public static Vector3 GetAnalogStick(string horizontal = "Horizontal", string vertical = "Vertical")
	{
		// apply some error margin, because the analog stick typically does not
		// reach the corner entirely
		const float error = 1.1f;

		// clamp input with error margin
		var input = new Vector3(
			Mathf.Clamp(Input.GetAxisRaw(horizontal) * error, -1f, 1f),
			0,
			Mathf.Clamp(Input.GetAxisRaw(vertical) * error, -1f, 1f)
		);

		// map square input to circle, to maintain uniform speed in all
		// directions
		return new Vector3(
			input.x * Mathf.Sqrt(1 - input.z * input.z * 0.5f),
			0,
			input.z * Mathf.Sqrt(1 - input.x * input.x * 0.5f)
		);
	}
}
