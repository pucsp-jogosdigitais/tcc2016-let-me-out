using UnityEngine;
using System.Collections;

public static class GameInfo {
    public static bool gameOver = false;

	public static bool paused = false;

	public static KeyCode forward = KeyCode.W;
	public static KeyCode backward = KeyCode.S;
	public static KeyCode left = KeyCode.A;
	public static KeyCode right = KeyCode.D;
	public static KeyCode light = KeyCode.F;

	public static float mouseSensivity = 2;
	public static float volumeEffects = 1;

	public static bool fullScreen = false;

    public static float brightness = 0;
}
