using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {
	public enum Trail { First, Second, Third };
	public Trail trail;

	// Use this for initialization
	void Start () {
		Trail trail;
		trail = Trail.First;
	}

	// Update is called once per frame
	void Update () {
		updateTrail();
	}

	private Trail updateTrail() {
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
