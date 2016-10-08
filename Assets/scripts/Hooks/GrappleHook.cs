using UnityEngine;
using System.Collections;

public class GrappleHook : AbstractHook {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		Speed = 7f;
		RetractSpeed = 5f;
	}
	
//	// Update is called once per frame
//	void Update () {
//	
//	}

}
