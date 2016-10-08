using UnityEngine;
using System.Collections;

public abstract class AbstractHook : MonoBehaviour {

	public float Speed { get; set; }
	public float RetractSpeed { get; set; }

	public GameObject player;
	private Rigidbody2D playerRb;

	public GameObject hookGun;

	public bool isFired = false;
	private Vector3 offset;
	private Vector3 dirFired;
	private Rigidbody2D rb;

	public bool isHooked = false;
	private Vector2 anchorPt;
	private Vector3 dirToHook;

	// Use this for initialization
	protected virtual void Start () {
		rb = GetComponent<Rigidbody2D> ();
		playerRb = player.GetComponent<Rigidbody2D> ();
	}

	public void Fire() {
		if (!isFired && !isHooked) {
			dirFired = hookGun.transform.up;
			offset = dirFired * .25f;
			transform.position = hookGun.transform.position + offset;
			transform.rotation = hookGun.transform.rotation * Quaternion.Euler (0, 0, 180f);
			gameObject.SetActive (true);
			isFired = true;
		}
	}

	void ReturnToHook() {
		isHooked = true;
	}

	// Update is called once per frame
	void Update () {
		if (isFired) {
			rb.velocity = dirFired * Speed;
		}
		if (isHooked) {
			dirToHook = transform.position - player.transform.position;
			playerRb.velocity = dirToHook * RetractSpeed;
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "player") {
			isHooked = false;
			gameObject.SetActive (false);
		} else {
			ReturnToHook ();
			isFired = false;
			rb.velocity = Vector2.zero;
		}
	}

}
