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
		renderer.material = ColorManager.GetMaterial((int)color);
	}

	void OnTriggerEnter(Collider collider)
	{
		print(Cat.shape);
		print(this.shape);
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
		color = (Colors)(int)Random.Range(0, 3);
	}
}