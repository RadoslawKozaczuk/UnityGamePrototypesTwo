using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
	Rigidbody rigidbody;
	AudioSource audioSource;

	// serializeField can be changed from the inspector but can not be changed from the other scripts
	// public field can be changed both from the inspector and other scripts
	[SerializeField] float rotationPower = 100f;
	[SerializeField] float mainThrustPower = 100f;
	[SerializeField] AudioClip mainEngine;
	[SerializeField] AudioClip death;
	[SerializeField] AudioClip levelCompleted;
	[SerializeField] float levelLoadDelay = 2f;

	[SerializeField] ParticleSystem engineParticles;
	[SerializeField] ParticleSystem successParticles;
	[SerializeField] ParticleSystem deathParticles;

	bool collisionEnabled = true;

	enum State
	{
		Alive,
		Dying,
		Transcending
	}
	State state = State.Alive;

	// Use this for initialization
	void Start ()
	{
		// both this script and rocket's rigid body are part (component) of the same object
		// here we get a reference

		rigidbody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(state == State.Alive)
		{
			RespondToThrustInput();
			RespondToRotationInput();
		}


		if(Debug.isDebugBuild)
			RespondToDegubKey();
	}

	private void RespondToDegubKey()
	{
		if(Input.GetKeyDown(KeyCode.L))
		{
			LoadNextLevel();
		}
		else if(Input.GetKeyDown(KeyCode.C))
		{
			collisionEnabled = !collisionEnabled;
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (state != State.Alive || !collisionEnabled) return;

		switch(collision.gameObject.tag)
		{
			case "Friendly":
				// do nothing 
				break;
			case "Foe":
				StartDeathSequence();
				break;
			case "Finish":
				StartCompleteSequence();
				break;
			default:
				break;
		}
	}

	private void StartCompleteSequence()
	{
		state = State.Transcending;
		audioSource.PlayOneShot(levelCompleted);
		successParticles.Play();
		Invoke("LoadNextLevel", levelLoadDelay);
	}

	private void StartDeathSequence()
	{
		state = State.Dying;
		audioSource.Stop();
		audioSource.PlayOneShot(death);
		deathParticles.Play();
		Invoke("LoadFirstLevel", levelLoadDelay);
	}

	private void LoadNextLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = currentSceneIndex >= SceneManager.sceneCountInBuildSettings - 1 
			? 0 
			: currentSceneIndex + 1;

		SceneManager.LoadScene(nextSceneIndex);
		state = State.Alive;
	}

	private void LoadFirstLevel()
	{
		SceneManager.LoadScene(0);
	}

	private void RespondToThrustInput()
	{
		// GetKey applies all the time
		// GetKeyDown applie only first time
		if (Input.GetKey(KeyCode.Space))
		{
			ApplyThrust();
			print("THRUST!!");
		}
		else
		{
			audioSource.Pause();
			engineParticles.Stop();
		}
	}

	private void ApplyThrust()
	{
		float powerThisFrame = mainThrustPower * Time.deltaTime;

		// we use relative force because we want to add directional force
		rigidbody.AddRelativeForce(Vector3.up * powerThisFrame); // always up based on the local coordinates
		if (!audioSource.isPlaying)
		{
			audioSource.PlayOneShot(mainEngine);
		}
		engineParticles.Play();
	}

	private void RespondToRotationInput()
	{
		// we want our ship to react so we disable physics while we use rotation thrusts
		rigidbody.angularVelocity = Vector3.zero;
		
		if (Input.GetKey(KeyCode.A))
		{
			float rotationThisFrame = rotationPower * Time.deltaTime;
			transform.Rotate(Vector3.forward * rotationThisFrame);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			float rotationThisFrame = rotationPower * Time.deltaTime;
			transform.Rotate(-Vector3.forward * rotationThisFrame);
		}
	}
}
