using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public static class InitialConfig
{
    public static void RestoreConfiguration()
    {
        if (PlayerPrefs.HasKey("volumeEffects"))
        {
            GameInfo.volumeEffects = PlayerPrefs.GetFloat("volumeEffects");
        }

        if (PlayerPrefs.HasKey("mouseSensivity"))
        {
            Debug.Log(PlayerPrefs.HasKey("mouseSensivity"));
            GameInfo.mouseSensivity = PlayerPrefs.GetFloat("mouseSensivity");
        }

        AudioListener.volume = GameInfo.volumeEffects;

		Player p = Player.GetInstance ();

        if (p != null)
        {
            Player.GetInstance().GetComponent<FirstPersonController>().ChangeMouseSensitivity(GameInfo.mouseSensivity, GameInfo.mouseSensivity);
        }

    }
}
