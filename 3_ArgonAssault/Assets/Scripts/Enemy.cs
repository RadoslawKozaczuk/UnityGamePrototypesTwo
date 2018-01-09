using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] GameObject deathFX;
	[SerializeField] Transform parent;

	[SerializeField] int score = 12;
	ScoreBoard scoreBoard;

	// Use this for initialization
	void Start()
	{
		// run at runtime 
		scoreBoard = FindObjectOfType<ScoreBoard>();

		Collider collider = gameObject.AddComponent<BoxCollider>();
		collider.isTrigger = false;
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnParticleCollision(GameObject other)
	{
		scoreBoard.IncreaseScore(score);

		GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
		fx.transform.parent = parent;
		
		Destroy(gameObject);
	}
}
