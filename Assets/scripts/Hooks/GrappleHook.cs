using UnityEngine;
using System.Collections;

public class GrappleHook : AbstractHook {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		Speed = 9f;
		RetractSpeed = 3f;
	}
	
//	// Update is called once per frame
//	void Update () {
//	
//	}

}
