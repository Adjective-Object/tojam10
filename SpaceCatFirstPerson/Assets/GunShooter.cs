using UnityEngine;
using System.Collections;

public class GunShooter : MonoBehaviour {

	public int gunIndex;
	static Gun[] guns = {
		new CatGun()
	};

	// Use this for initialization
	void Start () {
		guns[gunIndex].Init(this);
	}
	
	public void SpewBullet() {
		guns[gunIndex].Shoot(this);
	}
}
