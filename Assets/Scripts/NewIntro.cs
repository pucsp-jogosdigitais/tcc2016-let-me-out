using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewIntro : MonoBehaviour {

	public string oldYear;
	public string newYear;

	Text textYear;
	Image image;

	public AudioClip babySound;
	public AudioClip womanSound;

	// Use this for initialization
	void Start () {
		textYear = GameObject.Find("Text").GetComponent<Text>();
		textYear.GetComponent<Text> ().text = oldYear;

		image = GameObject.Find("Image").GetComponent<Image>();

		Invoke ("activeText", 2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void activeText()
	{
		CanvasGroup cg = textYear.GetComponent<CanvasGroup>();

		cg.alpha = 0;

		InvokeRepeating("fadeInText", 0, 0.10f);
	}

	private void fadeInText()
	{
		CanvasGroup cg = textYear.GetComponent<CanvasGroup>();

		cg.alpha += 0.10f;

		if (cg.alpha > 0.99f) {
			CancelInvoke("fadeInText");
			InvokeRepeating("fadeOutText", 2, 0.10f);
		}
	}

	private void fadeOutText()
	{
		CanvasGroup cg = textYear.GetComponent<CanvasGroup>();

		cg.alpha -= 0.10f;

		if (cg.alpha < 0.09) {
			CancelInvoke("fadeOutText");


			if (textYear.GetComponent<Text> ().text != newYear) {
				InvokeRepeating ("fadeInImage", 0.8f, 0.10f);
				Invoke ("playWoman", 1.4f);
				//Invoke ("playBaby", 5f);
			}
		}
	}

	private void fadeInImage()
	{
		CanvasGroup cg = image.GetComponent<CanvasGroup>();

		cg.alpha += 0.10f;

		Debug.Log ("dfsdfdsfd");

		if (cg.alpha > 0.99f) {
			CancelInvoke ("fadeInImage");
			InvokeRepeating ("fadeOutImage", 2, 0.10f);
		}
	}

	private void fadeOutImage()
	{
		CanvasGroup cg = image.GetComponent<CanvasGroup>();

		cg.alpha -= 0.10f;

		if (cg.alpha < 0.09) {
			CancelInvoke ("fadeOutImage");
			textYear.text = newYear;
			InvokeRepeating("fadeInText", 0, 0.10f);
		}
		/*
		if (cg.alpha > 0) {
			Invoke ("fadeOutImage", 0.10f);
		} else {
				textYear.GetComponent<Text> ().text = newYear;
				Invoke ("activeText", 0.10f);
				Invoke ("fadeOutText", 2f);
		}*/
	}

	private void playBaby()
	{
		AudioSource audio = GameObject.Find ("Canvas").GetComponent<AudioSource> ();

		if (!audio.isPlaying) {
			audio.clip = babySound;
			audio.Play ();
		}
	}

	private void playWoman()
	{
		AudioSource audio = GameObject.Find ("Canvas").GetComponent<AudioSource> ();

		if (!audio.isPlaying) {
			audio.clip = womanSound;
			audio.Play ();
		}
	}
}
