using UnityEngine;
using System.Collections;

public class StopOnFloor : MonoBehaviour {

	public float floor = 0;

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
			if (body != null) body.velocity = Vector3.zero;
			Destroy(this);
			Destroy(body);
		}

	}
}
