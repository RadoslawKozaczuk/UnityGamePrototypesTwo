﻿using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField] GameObject deathFX;
	[SerializeField] Transform parent;

	// Use this for initialization
	void Start()
	{
		Collider collider = gameObject.AddComponent<BoxCollider>();
		collider.isTrigger = false;
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnParticleCollision(GameObject other)
	{
		GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
		fx.transform.parent = parent;
		
		Destroy(gameObject);
	}
}
