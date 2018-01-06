using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
	[Tooltip("In m^-1")]
	[SerializeField] private float xSpeed = 20f;

	[Tooltip("In m^-1")]
	[SerializeField] private float ySpeed = 14f;

	[Tooltip("Horizontal max lean range in m")]
	[SerializeField] private float xRange = 9f;

	[Tooltip("Vertical max lean range in m")]
	[SerializeField] private float yRange = 4f;
	
	[SerializeField] private float positionPitchFactor = -3f;
	[SerializeField] private float positionYawFactor = 3f;

	[SerializeField] private float controlPitchFactor = -20f;
	[SerializeField] private float controlRollFactor = -20f;

	// how much a button was pressed
	float xThrow, yThrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		ProcessTranslation();

		ProcessRotation();
	}

	void ProcessTranslation()
	{
		xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
		yThrow = CrossPlatformInputManager.GetAxis("Vertical");

		float xOffset = xThrow * xSpeed * Time.deltaTime;
		float yOffset = yThrow * ySpeed * Time.deltaTime;

		float rawXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange);
		float rawYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -yRange, yRange);

		transform.localPosition = new Vector3(rawXPos, rawYPos, transform.localPosition.z);
	}

	void ProcessRotation()
	{
		float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
		float yaw = transform.localPosition.x * positionYawFactor;
		float roll = xThrow * controlRollFactor;
		
		transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
	}
}
