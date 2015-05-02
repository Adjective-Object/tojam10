using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour {

	public int health;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void damageFor(int dmg) {
		this.health -= dmg;
	}
}
