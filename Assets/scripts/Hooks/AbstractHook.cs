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
	protected virtual void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		playerRb = player.GetComponent<Rigidbody2D> ();
		minDist = 0.75f;
	}

	public void Fire() {
		isFired = true;
		dirHook = hookGun.transform.up;
		offset = dirHook * 0.25f;
		transform.position = hookGun.transform.position + offset;
		transform.rotation = hookGun.transform.rotation * Quaternion.Euler (0, 0, 180f);
		gameObject.SetActive (true);
		rb.velocity = dirHook * Speed;
	}

	void ReturnToHook() {
		dirPlayer = (transform.position - player.transform.position).normalized;
		playerRb.velocity = dirPlayer * RetractSpeed;
		rb.velocity = Vector2.zero;
	}

	void PullTogether(GameObject pulledObj) {
		pulledRb = pulledObj.GetComponent<Rigidbody2D> ();
		dirHook = (player.transform.position - pulledObj.transform.position).normalized;
		dirPlayer = -dirHook;
		rb.velocity = dirHook * RetractSpeed;
		playerRb.velocity = dirPlayer * RetractSpeed;
		pulledRb.velocity = rb.velocity;
	}

	void PullToPlayer(GameObject pulledObj) {
		pulledRb = pulledObj.GetComponent<Rigidbody2D> ();
		dirHook = (player.transform.position - pulledObj.transform.position).normalized;
		playerRb.velocity = Vector2.zero;
		rb.velocity = dirHook * RetractSpeed;
		pulledRb.velocity = rb.velocity;
	}

	// Update is called once per frame
	void Update () {
		if (isHooked) {
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
		if (tag != "player") {
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
				print ("woops");
			}
		}
	}

}
