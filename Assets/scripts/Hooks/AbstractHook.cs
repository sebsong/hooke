using UnityEngine;
using System.Collections;

public abstract class AbstractHook : MonoBehaviour {

	public float Speed { get; set; }
	public GameObject hookPrefab;
	public GameObject hookGun;

	private GameObject hook;

	private bool isFired;
	private Vector3 offset;
	private Vector3 dir;
	private Rigidbody2D rb;

	// Use this for initialization
	protected void Start () {
		isFired = false;
		hook = (GameObject) Instantiate (hookPrefab);
		hook.SetActive (false);
		rb = hook.GetComponent<Rigidbody2D> ();
	}

	public void Fire() {
		offset = transform.up * .25f;
		hook.transform.position = hookGun.transform.position + offset;
		hook.transform.rotation = hookGun.transform.rotation * Quaternion.Euler(0, 0, 180f);
		hook.SetActive (true);
		isFired = true;
		dir = transform.up;
	}

	// Update is called once per frame
	void Update () {
		if (isFired) {
			//hook.transform.position += dir * (Speed * Time.deltaTime);
			rb.velocity = dir * Speed;
		}
	}

	void OnCollisionEnter2d (Collision2D coll) {
		isFired = false;
		//rb.velocity = Vector2.zero;
		print("HIT");
	}
}
