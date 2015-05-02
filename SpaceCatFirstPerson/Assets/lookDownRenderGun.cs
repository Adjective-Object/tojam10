using UnityEngine;
using System.Collections;

public class lookDownRenderGun : MonoBehaviour {

	public Transform camTrans;
	public float lookRange = 10;
	public float posRange = -0.1f;

	private Vector3 init_pos;

	// Use this for initialization
	void Start () {
		this.init_pos = this.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		float angle = this.camTrans.rotation.eulerAngles.x;
		this.transform.localPosition = this.init_pos +
			new Vector3(0, angle / lookRange * posRange, 0);
	}
}
