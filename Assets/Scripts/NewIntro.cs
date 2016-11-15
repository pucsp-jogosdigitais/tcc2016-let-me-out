using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewIntro : MonoBehaviour {

	public string oldYear;
	public string newYear;

	Text textYear;
	Image image;

	// Use this for initialization
	void Start () {
		textYear = GameObject.Find("Text").GetComponent<Text>();
		textYear.GetComponent<Text> ().text = oldYear;

		image = GameObject.Find("Image").GetComponent<Image>();

		Invoke ("activeText", 2);
		Invoke ("fadeOutText", 4f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void activeText()
	{
		CanvasGroup cg = textYear.GetComponent<CanvasGroup>();

		cg.alpha = 0;

		fadeInText ();
	}

	private void fadeInText()
	{
		CanvasGroup cg = textYear.GetComponent<CanvasGroup>();

		cg.alpha += 0.10f;

		if (cg.alpha < 0.99f) {
			Invoke ("fadeInText", 0.10f);
		}
	}

	private void fadeOutText()
	{
		CanvasGroup cg = textYear.GetComponent<CanvasGroup>();

		cg.alpha -= 0.10f;

		if (cg.alpha > 0) {
			Invoke ("fadeOutText", 0.10f);

			if (textYear.GetComponent<Text> ().text != newYear) {
				Invoke ("fadeInImage", 2);
			}
		}
	}

	private void fadeInImage()
	{
		CanvasGroup cg = image.GetComponent<CanvasGroup>();

		cg.alpha += 0.10f;

		if (cg.alpha < 0.99f) {
			Invoke ("fadeInImage", 0.10f);
		} else {
			Invoke ("fadeOutImage", 4f);
		}
	}

	private void fadeOutImage()
	{
		CanvasGroup cg = image.GetComponent<CanvasGroup>();

		cg.alpha -= 0.10f;

		if (cg.alpha > 0) {
			Invoke ("fadeOutImage", 2);
		} else {
				textYear.GetComponent<Text> ().text = newYear;
				Invoke ("activeText", 0.10f);
				Invoke ("fadeOutText", 2f);
		}
	}
}
