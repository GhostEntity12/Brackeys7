using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public float rotSpeed = 1f;
    float[] timings = new float[]
    {
        4.019f,
        7.938f,
        11.75f,
        15.45f,
        19.09f,
        28.25f,
        29.52f,
        32.84f,
        36.10f,
        40.0f
    };

    public AudioSource music;
    public AudioClip roar;
    public AudioClip echo;
    int stage = 0;
    public ParticleSystem[] candles = new ParticleSystem[5];
    public Animator hand;

    public Light colorLight;
    public Renderer pentagram;

    public CanvasGroup group;
    bool fading = false;

    Color cyan = new Color(0, 1, 0.945098f);
    // Start is called before the first frame update
    void Start()
    {
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
		if (stage < timings.Length)
        {
            if (music.time > timings[stage])
            {
				switch (stage)
				{
                    case int i when i < 5:
                        candles[stage].Play();
                        break;
                    case 5:
                        music.PlayOneShot(roar);
                        break;
                    case 6:
                        hand.SetTrigger("Descend");
                        break;
                    case 7:
                        candles.ToList().ForEach(c => c.Stop());
                        break;
                    case 8:
                        colorLight.color = cyan;
                        pentagram.material.color = cyan;
                        pentagram.material.SetColor("_EmissionColor", cyan);
                        candles.ToList().ForEach(c => c.Play());
                        music.PlayOneShot(echo);
						break;
                    case 9:
                        fading = true;
						break;
                    default:
						break;
				}
                stage++;
            }
        }
		if (fading)
		{
            group.alpha += Time.deltaTime * 1.2f;
			if (group.alpha == 1)
			{
                SceneManager.LoadScene(0);
			}
		}
    }
}
