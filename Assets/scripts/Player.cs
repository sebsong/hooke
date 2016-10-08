using UnityEngine;
using System.Collections;

public class Player : AbstractCharacter {

	private float _x;
	private float _y;

	Rigidbody2D rigidBody;

	public GameObject hookObj;
	AbstractHook hook;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		hook = hookObj.GetComponent<GrappleHook> ();
		_x = transform.position.x;
		_y = transform.position.y;
		Speed = 3f;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hook.isHooked && !hook.isFired) {
			Move ();
			Rotate ();
			if (Input.GetKeyDown ("space")) {
				hook.Fire ();
				rigidBody.velocity = Vector2.zero;
				rigidBody.angularVelocity = 0;
			}
		}
		if (Health <= 0) {
			Die ();
		}
	}

	protected override void Move() {
		_x = Input.GetAxisRaw ("Horizontal");
		_y = Input.GetAxisRaw ("Vertical");
		Vector2 movementDir = new Vector2(_x, _y);
		movementDir.Normalize ();
		rigidBody.velocity = movementDir * Speed;
	}

	void Rotate() {
		Vector3 pos = Camera.main.WorldToScreenPoint (transform.position);
		Vector3 dir = Input.mousePosition - pos;
		float angle = Mathf.Atan2 (dir.x, dir.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (angle, Vector3.back);
	}

	protected override void Die() {
	}

	protected override void Attack () {
	}

}
