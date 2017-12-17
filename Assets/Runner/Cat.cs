﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{

	public static Animator animations;

	public enum Trail { First, Second, Third };
	public enum Colors { Red, Green, Blue, Normal };

	public enum Shapes { Circle, Square, Triangle, Normal = -1, Sad = 3 };
	public static Shapes shape;
	public Trail trail;
	public static Colors color;
	public Renderer renderer;
	public static int lifes;

	public float DepthDamping = 2.0f;

	private Vector3 offset;

	// Use this for initialization
	void Start()
	{
		GameEventManager.GameStart += GameStart;
		renderer = GetComponent<Renderer>();
		animations = GetComponent<Animator>();
		offset = transform.position;
	}

	private void GameStart()
	{
		lifes = 3;
		shape = Shapes.Normal;
		color = Colors.Normal;
		trail = Trail.First;
		GameObject.FindWithTag("Life1").GetComponent<Animator>().SetBool("on", true);
		GameObject.FindWithTag("Life2").GetComponent<Animator>().SetBool("on", true);
		GameObject.FindWithTag("Life3").GetComponent<Animator>().SetBool("on", true);
	}

	// Update is called once per frame
	void Update()
	{
		UpdateTrail();
		UpdateMaterial();
		animations.SetInteger("state", (int)shape);

		UpdateLifes();

		if (lifes <= 0)
		{
			GameEventManager.TriggerGameOver();
		}

		float wantedDepth = -(PlatformManager.depth * (int)trail) + offset.z;
		float currentDepth = transform.position.z;

		// Damp the Depth
		currentDepth = Mathf.Lerp(currentDepth, wantedDepth, DepthDamping * Time.deltaTime);

		// Set the z of the cat
		transform.position = new Vector3(transform.position.x, transform.position.y, currentDepth);
	}


	public void UpdateMaterial()
	{
		//renderer.material = ColorManager.GetMaterial((int)color);
		renderer.material.SetColor("_Color", ColorManager.GetColor((int)color));
	}

	void OnTriggerEnter()
	{
		//RandomColor();
	}

	private void UpdateTrail()
	{
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
		}
	}

	public void RandomColor()
	{
		color = (Colors)(int)Random.Range(0, 3);
	}

	public void RandomShape()
	{
		shape = (Shapes)(int)Random.Range(0, 3);
	}

	public void AnimateSad()
	{
		color = Colors.Normal;
		shape = Shapes.Sad;

		GameObject.FindGameObjectWithTag("Item").SetActive(true);
	}

	public static void LifeLess()
	{
		lifes--;
	}

	private void UpdateLifes()
	{
		if (lifes == 2)
		{
			GameObject.FindWithTag("Life3").GetComponent<Animator>().SetBool("on", false);
		}
		else if (lifes == 1)
		{
			GameObject.FindWithTag("Life2").GetComponent<Animator>().SetBool("on", false);
		}
		else if (lifes == 0)
		{
			GameObject.FindWithTag("Life1").GetComponent<Animator>().SetBool("on", false);
		}
	}
}
