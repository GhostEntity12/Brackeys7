using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PromptLeave : MonoBehaviour
{
	bool? endingType;
	float timer = 0f;
	public CanvasGroup text;
	public AudioSource audio;
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (GameManager.Instance.AllPickups)
			{
				GameManager.Instance.canvas.GetComponent<Image>().color = Color.white;
				endingType = true;
			}
			else
			{
				endingType = false;
			}
		}
	}

	private void Update()
	{
		if (endingType != null)
		{
			timer += Time.deltaTime;
			GameManager.Instance.canvas.alpha += Time.deltaTime * 1.5f;
			audio.volume -= Time.deltaTime;
			if (GameManager.Instance.canvas.alpha == 1)
			{
				if (endingType == true)
				{
					SceneManager.LoadScene(2);
				}
				else
				{
					Debug.Log(timer);
					switch (timer)
					{
						case float i when i < 4f:
							text.alpha += Time.deltaTime * 1.2f;
							break;
						case float i when i > 7.5f:
							SceneManager.LoadScene(0);
							break;
						case float i when i > 6f:
							text.alpha -= Time.deltaTime;
							break;
					}
				}
			}
		}
	}
}
