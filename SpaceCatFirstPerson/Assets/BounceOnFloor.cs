using UnityEngine;
using System.Collections;

public class BounceOnFloor : MonoBehaviour {

	public float floor = 0;
	public float falloff = 0.7f;
	public float velocity_min = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y < floor) {
			this.transform.position = new Vector3(
				this.transform.position.x,
				floor,
				this.transform.position.z);
			
			Rigidbody body = this.GetComponent<Rigidbody>();
			if (body != null) {
				Debug.Log("bounce");
				body.velocity = new Vector3(
					body.velocity.x * falloff,
					-body.velocity.y * falloff,
					body.velocity.z  * falloff);
				if (body.velocity.y < velocity_min) {
					Destroy(this);
					Destroy(body);
				}
			}
		}

	}
}
