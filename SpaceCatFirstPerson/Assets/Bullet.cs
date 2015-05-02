using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int damage;
	public float maxLife;
	public Vector3 step;

	private float initialTime;
	public Animator animator;

	// Use this for initialization
	void Start () {
		this.initialTime = Time.realtimeSinceStartup;
		if (this.animator == null) {
			this.animator = this.GetComponent<Animator>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.realtimeSinceStartup - this.initialTime > maxLife) {
			Destroy(this.gameObject);
			//Debug.Log("killing bullet of old age");
		}
		this.transform.position += this.step * Time.deltaTime;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.GetComponentInParent<Bullet>() != null) return;
		Debug.Log("Collide!");

		LivingEntity e = other.GetComponentInParent<LivingEntity>();
		if (e!=null) e.damageFor(this.damage);
		
		
		animator.SetBool("explode", true);
		this.transform.position -= Time.deltaTime * this.step;
		this.step = Vector3.zero;
		Debug.Log("ResolveCollide");
	}
	
	public void Suicide() {
		Destroy(this.gameObject);
	}
}
