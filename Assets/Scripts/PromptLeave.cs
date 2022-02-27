using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PromptLeave : MonoBehaviour
{
	bool? endingType;
	float timer = 0f;
	public CanvasGroup text;
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
			if (GameManager.Instance.canvas.alpha == 1)
			{
				if (endingType == true)
				{
					SceneManager.LoadScene(2);
				}
				else
				{
					text.alpha += Time.deltaTime * 1.2f;
					if (timer > 4f)
					{
						SceneManager.LoadScene(0);
					}
				}
			}
		}
	}
}
