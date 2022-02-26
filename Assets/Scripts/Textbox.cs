using TMPro;
using UnityEngine;
public enum State
{
    Growing,
    Shrinking,
    Inactive
}

public class Textbox : Poolable
{


    [SerializeField]
    private GameObject character;

    new private Camera camera;

    private TextMeshProUGUI textbox;
    private CanvasGroup canvasGroup;
    [HideInInspector]
    public State state = State.Inactive;

    public float lerpTime = 0.3f;
    private float lerpTimer;


    public delegate void FadeComplete();
    public event FadeComplete OnFadeComplete;

    void Awake()
    {
        camera = Camera.main;
        GetComponent<Canvas>().worldCamera = camera;
        textbox = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Setup(Spirit spirit)
	{
        textbox.text = spirit.Dialogue;
        character = spirit.gameObject;
        transform.position = character.transform.position + Vector3.up * 1.2f;
        UpdateRotation();
        state = State.Growing;
	}

	private void Update()
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
                sourcePool.Return(this);
                OnFadeComplete();
            }
        }
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * 0.01f, lerpTimer / lerpTime);

        canvasGroup.alpha = Mathf.Min((Vector3.Distance(transform.position, camera.transform.position) - GameManager.Instance.textboxBuffer), 1);
	}

	private void FixedUpdate()
	{
        UpdateRotation();
	}

    void UpdateRotation() => transform.rotation = camera.transform.rotation;
}
