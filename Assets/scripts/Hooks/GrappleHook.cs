using UnityEngine;
using System.Collections;

public class GrappleHook : AbstractHook {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		Speed = 12f;
		RetractSpeed = 9f;
	}
	
//	// Update is called once per frame
//	void Update () {
//	
//	}

}
