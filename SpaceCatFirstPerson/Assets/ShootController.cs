using UnityEngine;
using System.Collections;

public class ShootController : MonoBehaviour {

	public string shootButton;
	public Animator animatedThing;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool shooting = Input.GetButton(shootButton);
		animatedThing.SetBool("shoot", shooting);
	}
}
