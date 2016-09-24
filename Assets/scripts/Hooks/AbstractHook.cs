using UnityEngine;
using System.Collections;

public abstract class AbstractHook : MonoBehaviour {

	public float Speed { get; set; }
	public GameObject tip;

	private bool isFired;

	// Use this for initialization
	void Start () {
		isFired = false;
	}

	public void Fire() {
		tip.transform.position += transform.up * (Speed * Time.deltaTime);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			isFired = true;
		}
		if (isFired) {
			Fire ();
		}
	}
}
