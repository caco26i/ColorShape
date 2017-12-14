using UnityEngine;

public class Portal : MonoBehaviour
{

	public Vector3 offset, rotationVelocity;
	public float recycleOffset, spawnChance;

	public enum Colors { Red, Green, Blue };
	public Material[] materials;
	public Colors color;
	public Material material;

	public enum Shapes { Circle, Square, Triangle};
	public Shapes shape;

	public Renderer renderer;


	void Start()
	{
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(false);
		RandomColor();
	}

	void Update()
	{
		if (transform.localPosition.x + recycleOffset < Runner.distanceTraveled)
		{
			gameObject.SetActive(false);
			return;
		}
		transform.Rotate(rotationVelocity * Time.deltaTime);

		UpdateMaterial();
	}

	public void UpdateMaterial()
	{
		Material material = ColorManager.GetMaterial((int)color);
		foreach (Transform child in transform)
		{
			child.GetComponent<Renderer>().material = material;
			foreach (Transform child2 in child.GetComponent<Transform>())
				child2.GetComponent<Renderer>().material = material;
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (GUIManager.getRuleNumber() == 0) {
			if ((int) Cat.color != (int) this.color) {
				GameObject.FindWithTag("Player").BroadcastMessage("AnimateSad");
			}
		}
		else if (GUIManager.getRuleNumber() == 1) {
			if ((int)Cat.shape != (int)this.shape)
			{
				GameObject.FindWithTag("Player").BroadcastMessage("AnimateSad");
			}
		}
		else if (GUIManager.getRuleNumber() == 2) {
			if ((int)Cat.color == (int)this.color)
			{
				GameObject.FindWithTag("Player").BroadcastMessage("AnimateSad");
			}
		}
		else if (GUIManager.getRuleNumber() == 3) {
			if ((int)Cat.shape == (int)this.shape)
			{
				GameObject.FindWithTag("Player").BroadcastMessage("AnimateSad");
			}
		}
		print(Cat.shape);
		print(this.shape);

		print((int)Cat.color);
		print(this.color);
	}

	public void SpawnIfAvailable(Vector3 position)
	{
		position.z = offset.z - (PlatformManager.depth * (int) Random.Range(0, 3));
		if (gameObject.activeSelf || spawnChance <= Random.Range(0f, 100f))
		{
			return;
		}
		transform.localPosition = position + offset;
		gameObject.SetActive(true);
		RandomColor();
	}

	private void GameOver()
	{
		gameObject.SetActive(false);
	}

	public void RandomColor()
	{
		color = (Colors) (int)Random.Range(0, 3);
	}
}