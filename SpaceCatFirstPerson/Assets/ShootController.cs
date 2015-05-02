using UnityEngine;
using System.Collections;

public class ShootController : MonoBehaviour {

	public string 	shootButton = "Fire1", 
					prevButton = "PrevGun",
					nextButton = "NextGun";
	public Animator animatedThing;
	public int equipped;
	public Gun[] guns = {
		new CatGun(),
		new CatSpreadGun()
	};

	// Use this for initialization
	void Start () {
		foreach (Gun g in guns) {
			g.Init(this);
		}
		this.EquipGun(1);
	}
	
	// Update is called once per frame
	void Update () {
		animatedThing.SetBool(
			"shoot", 
			Input.GetButton(shootButton));
		if(Input.GetButtonDown(prevButton)) {
			this.equipped = 
				(this.equipped + this.guns.Length - 1) %
					this.guns.Length;
			this.EquipGun(this.equipped);
		} else if (Input.GetButtonDown(nextButton)) {
			this.equipped = 
				(this.equipped + 1) % this.guns.Length;
			this.EquipGun(this.equipped);
		}
	}
	
	public void EquipGun(int index) {
		Debug.Log("Equipping gun "+index);
		this.equipped = index;
		this.guns[this.equipped].Equip(this);
	}
	
	public void ShootBullet () {
		Debug.Log("Shoot!");
		this.guns[this.equipped].Shoot(this);
	}
}
