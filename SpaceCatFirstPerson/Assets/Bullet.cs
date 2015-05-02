using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int damage;
	public float maxLife;
	public Vector3 step;

	private float initialTime;

	// Use this for initialization
	void Start () {
		this.initialTime = Time.realtimeSinceStartup;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup - this.initialTime > maxLife) {
			Destroy(this.gameObject);
			Debug.Log("killing bullet of old age");
		}
		this.transform.position += this.step * Time.deltaTime;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.GetComponentInParent<Bullet>() != null) return;
		LivingEntity e = other.GetComponentInParent<LivingEntity>();
		if (e!=null) e.damageFor(this.damage);
		Debug.Log("bullet collided");
		Destroy(this.gameObject);
	}
}
