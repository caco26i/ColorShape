using UnityEngine;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {

	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public Vector3 size, minGap, maxGap;
	public float minY, maxY;
	public Material[] materials;
	public PhysicMaterial physicMaterial;
	public Booster booster;

	private Vector3 nextPosition;
	private Queue<Transform> objectQueue;
	private Queue<Transform> objectQueue2;
	private Queue<Transform> objectQueue3;

	public static float depth;

	void Start () {
		depth = 5f;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		objectQueue = new Queue<Transform>(numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++)
		{
			objectQueue.Enqueue((Transform)Instantiate(
				prefab, new Vector3(0f, 1000f, 0f), Quaternion.identity));
		}
		objectQueue2 = new Queue<Transform>(numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++)
		{
			objectQueue2.Enqueue((Transform)Instantiate(
				prefab, new Vector3(0f, 1000f, 0f), Quaternion.identity));
		}
		objectQueue3 = new Queue<Transform>(numberOfObjects);
		for (int i = 0; i < numberOfObjects; i++)
		{
			objectQueue3.Enqueue((Transform)Instantiate(
				prefab, new Vector3(0f, 1000f, 0f), Quaternion.identity));
		}
		enabled = false;
	}

	void Update () {
		if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled)
		{
			Recycle();
		}
	}

	private void Recycle () {
		Vector3 scale = new Vector3(size.x, depth, depth);

		Vector3 position = nextPosition;
		position.x += scale.x;
		booster.SpawnIfAvailable(position);

		Transform o = objectQueue.Dequeue();
		o.localScale = scale;
		o.localPosition = position;
		o.GetComponent<Renderer>().material = materials[0];
		o.GetComponent<Collider>().material = physicMaterial;
		objectQueue.Enqueue(o);

		Transform o2 = objectQueue2.Dequeue();
		o2.localScale = scale;
		position.z = -depth;
		o2.localPosition = position;
		o2.GetComponent<Renderer>().material = materials[1];
		o2.GetComponent<Collider>().material = physicMaterial;
		objectQueue2.Enqueue(o2);

		Transform o3 = objectQueue3.Dequeue();
		o3.localScale = scale;
		position.z = -depth * 2;
		o3.localPosition = position;
		o3.GetComponent<Renderer>().material = materials[2];
		o3.GetComponent<Collider>().material = physicMaterial;
		objectQueue3.Enqueue(o3);

		nextPosition += new Vector3(scale.x, 0, 0);


	}
	
	private void GameStart () {
		nextPosition = startPosition;
		for(int i = 0; i < numberOfObjects; i++){
			Recycle();
		}
		enabled = true;
	}

	private void GameOver () {
		enabled = false;
	}
}