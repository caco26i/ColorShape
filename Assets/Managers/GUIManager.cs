using UnityEngine;

public class GUIManager : MonoBehaviour {

	private static GUIManager instance;

	public GUIText ruleText, boostsText, distanceText, gameOverText, instructionsText, runnerText;
	public string[] rules = new string[] { "Shape", "F*ck da Shape" };
	public int ruleNumber;


	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverText.enabled = false;
	}

	void Update () {
		if(Input.GetButtonDown("Jump")){
			GameEventManager.TriggerGameStart();
		}
	}
	
	private void GameStart () {
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		runnerText.enabled = false;
		enabled = false;
	}
	
	private void GameOver () {
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		enabled = true;
	}
	
	public static void SetBoosts(int boosts){
		instance.boostsText.text = boosts.ToString();
	}

	public static void SetDistance(float distance){
		instance.distanceText.text = distance.ToString("f0");
	}

	public static void SetRandomRule()
	{
		instance.ruleNumber = (int)Random.Range(0, instance.rules.Length);
		instance.ruleText.text = instance.rules[instance.ruleNumber];
	}

	public static int getRuleNumber()
	{
		return instance.ruleNumber;
	}
}