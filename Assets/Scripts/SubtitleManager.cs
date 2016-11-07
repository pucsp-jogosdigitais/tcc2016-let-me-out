using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour {

	static SubtitleManager instance;

	[SerializeField]
	Text subText;

	public static SubtitleManager GetInstance() {
		return instance;
	}

	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetText(string text)
	{
		subText.text = text;

		StartCoroutine ("DesactivateText");
	}

	public IEnumerator DesactivateText()
	{
		yield return new WaitForSeconds (4);
		subText.text = "";
	}
}
