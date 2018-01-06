using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		PlayerDeathSequence();
	}

	void PlayerDeathSequence()
	{
		print("player dying");

		// we can send a message to other components in the same object
		SendMessage("OnPlayerDeath"); // this will invoke a method OnPlayerDeath in every other script
	}
}
