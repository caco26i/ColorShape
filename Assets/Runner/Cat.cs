using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

	public static Animator animations;

	public enum Trail { First, Second, Third };
	public enum Colors { Red, Green, Blue, Normal };

	public enum Shapes { Circle, Square, Triangle, Normal = -1, Sad = -2};
	public static Shapes shape;
	public Trail trail;
	public static Colors color;
	public Renderer renderer;


	// Use this for initialization
	void Start () {
		renderer = GetComponent<Renderer>();
		animations = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		UpdateTrail();
		UpdateMaterial();
		animations.SetInteger("state", (int) shape);
	}


	public void UpdateMaterial() {
		renderer.material = ColorManager.GetMaterial((int) color);
	}

	void OnTriggerEnter()
	{
		//RandomColor();
	}

	private void UpdateTrail() {
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			UpTrail();
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			DownTrail();
		}
	}

	public void UpTrail()
	{
		if (trail != Trail.First)
		{
			if (trail == Trail.Second)
			{
				trail = Trail.First;
			}
			if (trail == Trail.Third)
			{
				trail = Trail.Second;
			}
			transform.Translate(0, 0, PlatformManager.depth);
		}
	}

	public void DownTrail()
	{
		if (trail != Trail.Third)
		{
			if (trail == Trail.Second)
			{
				trail = Trail.Third;
			}
			if (trail == Trail.First)
			{
				trail = Trail.Second;
			}
			transform.Translate(0, 0, -PlatformManager.depth);
		}
	}

	public void RandomColor()
	{
		color = (Colors) (int) Random.Range(0, 3);
	}

	public void RandomShape()
	{
		shape = (Shapes) (int) Random.Range(0, 3);
	}
}
