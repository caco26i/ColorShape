using UnityEngine;

public class Runner : MonoBehaviour
{

	public static float distanceTraveled;
	public static float score;
	private static int boosts;
	public static int bonuses;

	public static float acceleration;

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
        acceleration = 3;

    }

	void Update()
	{
		/*if (Input.GetButtonDown("Jump"))
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
		}*/
		distanceTraveled = transform.localPosition.x;
		score = distanceTraveled + bonuses;
		GUIManager.SetDistance(score);

		if (transform.localPosition.y < gameOverY)
		{
			GameEventManager.TriggerGameOver();
		}
		maxSpeed = distanceTraveled/100 + 10; //se va aumentando la velocidad conforme avanza
	}

	void FixedUpdate()
	{
		if (GetComponent<Rigidbody>().velocity.x <= maxSpeed)
		{
			GetComponent<Rigidbody>().AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
		}
	}


	private void GameStart()
	{
		boosts = 0;
		GUIManager.SetBoosts(boosts);
		distanceTraveled = 0f;
		score = 0f;
		GUIManager.SetDistance(distanceTraveled);
		transform.localPosition = startPosition;
		GetComponent<Renderer>().enabled = true;
		GetComponent<Rigidbody>().isKinematic = false;
		enabled = true;
		//GetComponent<Rigidbody>().AddForce(acceleration*2f, 0f, 0f, ForceMode.Impulse);
		//GetComponent<Rigidbody>().velocity.magnitude
	}

	private void GameOver()
	{
		//GetComponent<Renderer>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		LeaderBoardSample.gs = LeaderBoardSample.gameState.enterscore;
		//enabled = false;
	}

	public static void AddBoost()
	{
		boosts += 1;
		GUIManager.SetBoosts(boosts);
	}
}
