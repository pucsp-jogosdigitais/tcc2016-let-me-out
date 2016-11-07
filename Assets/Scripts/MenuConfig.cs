using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuConfig : MonoBehaviour {

	private string waitingText;
	private string fullScreenText;
	private string windowedScreenText;

	private Command currCommand;
	private KeyCode currKeyPressed;

	public Text fowardText;
	public Text backwardText;
	public Text leftText;
	public Text rightText;
	public Text lightText;
	public Text resolutionText;

	private static MenuConfig ocurrency;

	public static MenuConfig GetOcurrency()
	{
		return ocurrency;
	}

	public enum Command
	{
		Empty,
		Foward,
		Backward,
		Left,
		Right,
		Light
	}

	// Use this for initialization
	void Start () {
		MenuConfig.ocurrency = this;

		currCommand = Command.Empty;
		currKeyPressed = KeyCode.None;
		waitingText = "Pressione uma tecla...";
		windowedScreenText = "JANELA";
		fullScreenText = "TELA CHEIA";

		fowardText.text = GameInfo.forward.ToString();
		backwardText.text = GameInfo.backward.ToString();
		leftText.text = GameInfo.left.ToString ();
		rightText.text = GameInfo.right.ToString ();
		lightText.text = GameInfo.light.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (MenuConfig.GetOcurrency());

		if(currCommand != Command.Empty)
		{
			if (Input.anyKey)
			{
				KeyCode keyCode = GetKeyDown ();

				if (keyCode != KeyCode.None) {
					currKeyPressed = keyCode;
				}
			}

			if (currKeyPressed != KeyCode.None) {

				switch (currCommand) {
				case Command.Foward: 
					GameInfo.forward = currKeyPressed;
					fowardText.text = currKeyPressed.ToString ();
					SaveNewKeyConfiguration ("forward", currKeyPressed);
					break;

				case Command.Backward:
					GameInfo.backward = currKeyPressed;
					backwardText.text = currKeyPressed.ToString ();
					SaveNewKeyConfiguration ("backward", currKeyPressed);
					break;

				case Command.Left:
					GameInfo.left = currKeyPressed;
					leftText.text = currKeyPressed.ToString ();
					SaveNewKeyConfiguration ("left", currKeyPressed);
					break;

				case Command.Right:
					GameInfo.right = currKeyPressed;
					rightText.text = currKeyPressed.ToString ();
					SaveNewKeyConfiguration ("right", currKeyPressed);
					break;

				case Command.Light:
					GameInfo.light = currKeyPressed;
					lightText.text = currKeyPressed.ToString ();
					SaveNewKeyConfiguration ("light", currKeyPressed);
					break;
				}

				currKeyPressed = KeyCode.None;
				currCommand = Command.Empty;;
			}
		}
	}

	public KeyCode GetKeyDown()
	{
		System.Array values = System.Enum.GetValues(typeof(KeyCode));

		foreach(KeyCode kcode in values)
		{
			if (Input.GetKeyDown (kcode)) {
				return kcode;
			}
		}

		return KeyCode.None;
	}

	public void ChangeButtonFoward()
	{
		currCommand = Command.Foward;
		fowardText.text = waitingText;
	}

	public void ChangeButtonBackward()
	{
		currCommand = Command.Backward;
		backwardText.text = waitingText;
	}

	public void ChangeButtonLeft()
	{
		currCommand = Command.Left;
		leftText.text = waitingText;
	}

	public void ChangeButtonRight()
	{
		currCommand = Command.Right;
		rightText.text = waitingText;
	}

	public void ChangeButtonLight()
	{
		currCommand = Command.Light;
		lightText.text = waitingText;
	}

	public void ChangeScreenSize()
	{
		Screen.fullScreen = !Screen.fullScreen;

		Debug.Log (Screen.fullScreen);

		if (Screen.fullScreen) {
			resolutionText.text = fullScreenText;
		} else {
			resolutionText.text = windowedScreenText;
		}
	}

	private void SaveNewKeyConfiguration(string keyName, KeyCode currKey)
	{
		PlayerPrefs.SetInt (keyName, (int)currKey);
		PlayerPrefs.Save ();
	}
}
