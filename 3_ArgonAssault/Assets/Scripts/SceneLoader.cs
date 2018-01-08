using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	[SerializeField] private float levelLoadDelay = 4f;

	void Start()
	{
		Invoke("LoadNextLevel", levelLoadDelay);
	}

	private void LoadNextLevel()
	{
		SceneManager.LoadScene(1);
	}
}
