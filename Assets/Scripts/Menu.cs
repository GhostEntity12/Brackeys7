using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
	public RectTransform selector;
	public CanvasGroup fade;
	bool optionSelected = false;
	delegate void MenuAction();
	event MenuAction MenuActionEvent;
	public AudioSource audio;

	// Start is called before the first frame update
	void Start()
	{
		fade.alpha += Time.deltaTime * 2;
		Cursor.lockState = CursorLockMode.None;
	}

	private void Update()
	{
		if (optionSelected)
		{
			fade.alpha += Time.deltaTime * 1.5f;
			audio.volume = 1 - fade.alpha;
			if (fade.alpha == 1)
			{
				MenuActionEvent?.Invoke();
			}
		}
		else if (fade.alpha > 0)
		{
			fade.alpha -= Time.deltaTime * 1.5f;
		}
	}

	public void Quit()
	{
		MenuActionEvent = () =>
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
		optionSelected = true;
	}
	public void LoadScene(int i)
	{
		MenuActionEvent = () => SceneManager.LoadScene(i);
		optionSelected = true;
	}

	public void Select(TextMeshProUGUI button)
	{
		selector.transform.position = button.transform.position;
		selector.gameObject.SetActive(true);
	}
	public void Deselect()
	{
		selector.gameObject.SetActive(false);
	}
}
