using UnityEngine;

public class Runner : MonoBehaviour
{

	public static float distanceTraveled;
	private static int boosts;

	public float acceleration;

	public float maxSpeed = 10f;

	public Vector3 boostVelocity, jumpVelocity;
	public float gameOverY;

	private bool touchingPlatform;
	private Vector3 startPosition;

	void Start()
	{
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		GetComponent<Renderer>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		enabled = false;
	}

	void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			if (touchingPlatform)
			{
				GetComponent<Rigidbody>().AddForce(jumpVelocity, ForceMode.VelocityChange);
				touchingPlatform = false;
			}
			else if (boosts > 0)
			{
				GetComponent<Rigidbody>().AddForce(boostVelocity, ForceMode.VelocityChange);
				boosts -= 1;
				GUIManager.SetBoosts(boosts);
			}
		}
		distanceTraveled = transform.localPosition.x;
		GUIManager.SetDistance(distanceTraveled);

		if (transform.localPosition.y < gameOverY)
		{
			GameEventManager.TriggerGameOver();
		}
	}

	void FixedUpdate()
	{
		if (GetComponent<Rigidbody>().velocity.magnitude <= maxSpeed)
		{
			GetComponent<Rigidbody>().AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}


	private void GameStart()
	{
		boosts = 0;
		GUIManager.SetBoosts(boosts);
		distanceTraveled = 0f;
		GUIManager.SetDistance(distanceTraveled);
		transform.localPosition = startPosition;
		GetComponent<Renderer>().enabled = true;
		GetComponent<Rigidbody>().isKinematic = false;
		enabled = true;
	}

	private void GameOver()
	{
		//GetComponent<Renderer>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		//enabled = false;
	}

	public static void AddBoost()
	{
		boosts += 1;
		GUIManager.SetBoosts(boosts);
	}

}