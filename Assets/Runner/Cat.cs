using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
	public enum Trail { First, Second, Third };
	public enum Colors { Red, Green, Blue, Normal};
	public Material[] colors;
	public Trail trail;
	public Colors color;

	private Material material;


	// Use this for initialization
	void Start () {
		trail = Trail.First;
		material = GetComponent<Renderer>().material;
	}

	// Update is called once per frame
	void Update () {
		UpdateTrail();
		ChangeColor();
	}

	void ChangeColor() { //r = 0, g = 2, b =3
		if (color == Colors.Red)
		{
			material = colors[0];
		}
		else if (color == Colors.Green)
		{
			material = colors[1];
		}
		else if (color == Colors.Blue)
		{
			material = colors[2];
		}
		else {
			material = colors[3];
		}
	}

	private Trail UpdateTrail() {
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			UpTrail();
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			DownTrail();
		}
		return trail;
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
}
