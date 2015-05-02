using UnityEngine;
using System.Collections;

public class ShootController : MonoBehaviour {

	public string shootButton;
	public Animator animatedThing;
	public Gun gun = new CatSpreadGun();

	// Use this for initialization
	void Start () {
		this.gun.Init();
	}
	
	// Update is called once per frame
	void Update () {
		bool shooting = Input.GetButton(shootButton);
		animatedThing.SetBool("shoot", shooting);
	}
	
	public void ShootBullet () {
		Debug.Log("Shoot!");
		this.gun.Shoot(this);
	}
}
