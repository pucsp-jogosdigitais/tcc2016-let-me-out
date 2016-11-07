using UnityEngine;
using System.Collections;
using  UnityEngine.UI;

public class InitialIntroScene : MonoBehaviour {

	public RawImage[] introImages;
	public string nextScene = "menu";

	int  indexCurrImage = 0;
	RawImage introImage;

	int multAlpha = 1;

	// Use this for initialization
	void Start () {
		DisableImages ();
		ActivateNextImage ();
	}
	
	// Update is called once per frame
	void Update () {
		CanvasGroup cg = introImage.GetComponent<CanvasGroup>();
		cg.alpha += 0.01f * multAlpha;

		if(cg.alpha > 0.9f) {
			multAlpha = 0;
			Invoke ("SetMulAlpha", 0.5f);
		}
	}

	void SetMulAlpha()
	{
		multAlpha = -1;
	}

	void ActivateNextImage()
	{
		Debug.Log (indexCurrImage);

		if(indexCurrImage >= introImages.Length) {
			Debug.Log ("dsadsads");
			Application.LoadLevel(nextScene);
		}

		introImages [indexCurrImage].enabled = true;
		introImage = introImages [indexCurrImage];

		CanvasGroup cg = introImage.GetComponent<CanvasGroup>();
		cg.alpha = 0;

		multAlpha = 1;
		indexCurrImage += 1;

		Invoke ("ActivateNextImage", 4);
	}

	void DisableImages()
	{
		foreach(RawImage image in introImages)
		{
			image.enabled = false;
		}
	}
}
