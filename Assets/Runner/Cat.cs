using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

	public static Animator animations;

	public enum Trail { First, Second, Third };
	public enum Colors { Red, Green, Blue, Normal};
	public Material[] materials;
	public Trail trail;
	public  Colors color;
	public Material material;


	// Use this for initialization
	void Start () {
		trail = Trail.First;
		material = GetComponent<Renderer>().material;
		animations = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		UpdateTrail();

		AnimatorStateInfo stateInfo = animations.GetCurrentAnimatorStateInfo(0);
		UpdateMaterial();
		//Debug.Log(stateInfo.nameHash);
	}


	public void UpdateMaterial() { //r = 0, g = 2, b =3

		Renderer rend = GetComponent<Renderer>();
		

		if (color == Colors.Red)
		{
			rend.material = materials[0];
		}
		else if (color == Colors.Green)
		{
			rend.material = materials[1];
		}
		else if (color == Colors.Blue)
		{
			rend.material = materials[2];
		}
		else {
			rend.material = materials[3];
		}
	}

	void OnTriggerEnter()
	{
		RandomColor();
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

	public void RandomColor()
	{
		switch ((int) Random.Range(0, 3)) {
			case 0: { color = Colors.Red; break; }
			case 1: { color = Colors.Green; break; }
			case 2: { color = Colors.Blue; break; }
		}
	}

	public static void RandomShape()
	{
		animations.SetInteger("state", (int)Random.Range(1, 4));
	}
}
