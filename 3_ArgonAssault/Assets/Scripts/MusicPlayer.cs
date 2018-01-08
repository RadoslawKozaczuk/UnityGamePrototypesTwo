using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	void Awake()
	{
		// if more than one music player in scene destroy yourself
		// Unity Singleton Pattern
		if (FindObjectsOfType<MusicPlayer>().Length > 1)
		{
			Destroy(this);
		}
		else
		{
			// this means don't destroy the object that this script is attached to
			DontDestroyOnLoad(this);
		}
	}
}
