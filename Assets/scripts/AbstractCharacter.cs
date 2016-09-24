using UnityEngine;
using System.Collections;

public abstract class AbstractCharacter : MonoBehaviour {

	public int Health { get; set; }
	public float Speed { get; set; }

	// Use this for initialization
	void Start () {
	
	}

	protected abstract void Move();

	protected abstract void Die();

	protected abstract void Attack ();

	// Update is called once per frame
	void Update () {
		Move ();
	}
}
