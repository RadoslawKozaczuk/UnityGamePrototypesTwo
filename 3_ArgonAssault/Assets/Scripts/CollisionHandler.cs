using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
	[SerializeField] float levelLoadDelay = 1f;
	[SerializeField] GameObject deathFx;

	private void OnTriggerEnter(Collider other)
	{
		PlayerDeathSequence();
	}

	void PlayerDeathSequence()
	{
		deathFx.SetActive(true);
		Invoke("ReloadLevel", levelLoadDelay);
		print("player dying");

		// we can send a message to other components in the same object
		// this will invoke a method OnPlayerDeath in every other script from those on the same game object
		SendMessage("OnPlayerDeath"); 
	}

	private void ReloadLevel()
	{
		SceneManager.LoadScene(1);
	}
}
