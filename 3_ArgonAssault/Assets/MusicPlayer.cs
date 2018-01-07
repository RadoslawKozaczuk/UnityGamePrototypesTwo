using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

	[SerializeField] private float levelLoadDelay = 4f;

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
			DontDestroyOnLoad(gameObject);
		}
	}

	// Use this for initialization
	void Start()
	{
		Invoke("LoadNextLevel", levelLoadDelay);
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void LoadNextLevel()
	{
		SceneManager.LoadScene(1);
	}
}
