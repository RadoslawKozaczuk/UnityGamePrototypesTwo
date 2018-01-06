using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
	[Header("General")]
	[Tooltip("In m^-1")] [SerializeField] private float xSpeed = 20f;
	[Tooltip("In m^-1")] [SerializeField] private float ySpeed = 14f;
	[Tooltip("Horizontal max lean range in m")]	[SerializeField] private float xRange = 9f;
	[Tooltip("Vertical max lean range in m")] [SerializeField] private float yRange = 4f;

	[Header("Screen-position Based")]
	[SerializeField] private float positionPitchFactor = -3f;
	[SerializeField] private float positionYawFactor = 3f;

	[Header("Control-throw Based")]
	[SerializeField] private float controlPitchFactor = -20f;
	[SerializeField] private float controlRollFactor = -20f;

	// how much a button was pressed
	float xThrow, yThrow;
	bool isControlEnabled = true;
	
	void Update ()
	{
		if (isControlEnabled)
		{
			ProcessTranslation();
			ProcessRotation();
		}
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

	void OnPlayerDeath()
	{
		isControlEnabled = false;
		print("Controller frozen");
	}
}
