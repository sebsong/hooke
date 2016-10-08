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
	private Vector3 dirHook;
	private Rigidbody2D rb;

	public bool isHooked = false;
	private Vector3 dirPlayer;
	private float minDist;

	private bool isPulling = false;
	private Rigidbody2D pulledRb;

	// Use this for initialization
	protected virtual void Start () {
		rb = GetComponent<Rigidbody2D> ();
		playerRb = player.GetComponent<Rigidbody2D> ();
		minDist = 0.75f;
	}

	public void Fire() {
		if (!isFired && !isHooked) {
			dirHook = hookGun.transform.up;
			offset = dirHook * 0.25f;
			transform.position = hookGun.transform.position + offset;
			transform.rotation = hookGun.transform.rotation * Quaternion.Euler (0, 0, 180f);
			gameObject.SetActive (true);
			isFired = true;
		}
	}

	void ReturnToHook() {
		dirHook = Vector2.zero;
		dirPlayer = (transform.position - player.transform.position).normalized;
	}

	void PullTogether(GameObject pulledObj) {
		dirHook = (player.transform.position - pulledObj.transform.position).normalized;
		dirPlayer = -dirHook;
		pulledRb = pulledObj.GetComponent<Rigidbody2D> ();
	}

	void PullToPlayer(GameObject pulledObj) {
		dirHook = (player.transform.position - pulledObj.transform.position).normalized;
		dirPlayer = Vector2.zero;
		pulledRb = pulledObj.GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () {
		if (isFired) {
			rb.velocity = dirHook * Speed;
		}
		if (isHooked) {
			rb.velocity = dirHook * RetractSpeed;
			playerRb.velocity = dirPlayer * RetractSpeed;
			if (isPulling) {
				pulledRb.velocity = dirHook * RetractSpeed;
			}
			if (Vector2.Distance(player.transform.position, transform.position) < minDist) {
				isHooked = false;
				gameObject.SetActive (false);
				if (isPulling) {
					isPulling = false;
					pulledRb.velocity = Vector2.zero;
				}
			}
		}
	}

	void OnCollisionEnter2D (Collision2D coll) {
		string tag = coll.gameObject.tag;
		if (tag == "player") {
		} else {
			isHooked = true;
			isFired = false;
			if (tag == "big") {
				ReturnToHook ();
			} else if (tag == "medium") {
				isPulling = true;
				PullTogether (coll.gameObject);
			} else if (tag == "small") {
				isPulling = true;
				PullToPlayer (coll.gameObject);
			} else {
			}

		}
	}

}
