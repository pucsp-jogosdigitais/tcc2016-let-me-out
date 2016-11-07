using UnityEngine;
using System.Collections;

public static class InitialConfig {

	public static void Configure()
	{
		RestoreKeyboardConfiguration();
	}

	public static void RestoreKeyboardConfiguration()
	{
		if (PlayerPrefs.HasKey ("forward")) {
			GameInfo.forward = (KeyCode)(PlayerPrefs.GetInt("forward"));
		}

		if (PlayerPrefs.HasKey ("backward")) {
			GameInfo.backward = (KeyCode)(PlayerPrefs.GetInt("backward"));
		}

		if (PlayerPrefs.HasKey ("left")) {
			GameInfo.left = (KeyCode)(PlayerPrefs.GetInt("left"));
		}

		if (PlayerPrefs.HasKey ("right")) {
			GameInfo.right = (KeyCode)(PlayerPrefs.GetInt("right"));
		}

		if (PlayerPrefs.HasKey ("light")) {
			GameInfo.light = (KeyCode)(PlayerPrefs.GetInt("light"));
		}

		if (PlayerPrefs.HasKey ("mouseSensivity")) {
			GameInfo.mouseSensivity = PlayerPrefs.GetFloat("mouseSensivity");
		}

		if (PlayerPrefs.HasKey ("volumeEffects")) {
			GameInfo.volumeEffects = PlayerPrefs.GetFloat("volumeEffects");
		}

		if (PlayerPrefs.HasKey ("fullScreen")) {
			GameInfo.volumeEffects = PlayerPrefs.GetFloat("volumeEffects");
		}
	}
}
