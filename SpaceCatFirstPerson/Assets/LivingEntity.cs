using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour {

	public int health;

	public Sprite[] gibSprites;
	public GameObject gibPrefab;
	
	public float hspread = 0.1f;
	public float vmin = 1;
	public float vmax = 3;
	
	private Animator stateMachine;

	// Use this for initialization
	void Start () {
		this.stateMachine = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void damageFor(int dmg) {
		this.health -= dmg;
		if (this.health <= 0) {
			stateMachine.SetBool("alive", false);
		}
	}
	
	public void SpewGibs(int gibsCount) {
		for (int i=0; i<gibsCount; i++) {
			GameObject instance = (GameObject) Object.Instantiate(gibPrefab);
			instance.transform.position = this.transform.position;
			instance.GetComponent<SpriteRenderer>().sprite = 
				gibSprites[Random.Range(0, gibSprites.Length)] ;
			instance.GetComponent<Rigidbody>().velocity = 
				Vector3.forward * Random.Range(-hspread, hspread) +
				Vector3.right * Random.Range(-hspread, hspread) +
				Vector3.up * Random.Range(vmin,vmax);
		}
	}
}
