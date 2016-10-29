using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform player;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = new Vector3(0, 0, 2 * (transform.position.z - player.position.z));
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.position + offset;
	}
}
