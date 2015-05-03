using UnityEngine;
using System.Collections;

public abstract class Gun {
	public abstract void Init(MonoBehaviour parent);
	public abstract void Equip(MonoBehaviour parent);
	public abstract void Shoot(MonoBehaviour parent);
	
	
	public static void  SpewBullet(
			Object prefab,
			MonoBehaviour parent,
			Vector2 xSpread,
			Vector2 speedSpread) {
		Debug.Log(prefab);
		GameObject g = (GameObject) Object.Instantiate(prefab);
		
		float xDrift = Random.Range(xSpread.x, xSpread.y);
		float speedDrift = Random.Range(speedSpread.x, speedSpread.y);
		
		g.transform.position = parent.transform.position + 
			parent.transform.rotation * Vector3.forward * 0.5f;
		g.GetComponent<Bullet>().step = 
			Quaternion.AngleAxis(
				parent.transform.rotation.eulerAngles.y + xDrift,
				Vector3.up) * 
			Vector3.forward * 
			(speedDrift);
	}
	
	
	public static void  SpewBullet(
			Object prefab,
			MonoBehaviour parent,
			float angle,
			float speed) {
		Debug.Log(prefab);
		GameObject g = (GameObject) Object.Instantiate(prefab);
		
		g.transform.position = parent.transform.position + 
			parent.transform.rotation * Vector3.forward * 0.5f;
		g.GetComponent<Bullet>().step = 
			Quaternion.AngleAxis(angle,Vector3.up) * 
				Vector3.forward * speed;
	}
}

public class CatGun : Gun {
	
	protected Object bulletPrefab;
	protected static int shootSpeed = 8;
	protected RuntimeAnimatorController animator;
	public string controllerName {
		get {return "catpistol/shooty";}
	}

	
	public override void Init(MonoBehaviour parent){
		this.bulletPrefab = Resources.Load("catpistol/pistolBullet");
		Debug.Log("loading "+ this.controllerName);
		this.animator = 
			(RuntimeAnimatorController) Resources.Load(this.controllerName);
	}
	
	public override void Equip(MonoBehaviour parent) {
		//Debug.Log(this.controllerName);
		parent.GetComponentInParent<Animator>()
			.runtimeAnimatorController = this.animator;
	}
		
	public override void Shoot(MonoBehaviour parent){
		Gun.SpewBullet(
			bulletPrefab, 
			parent, 
			new Vector2(0,0), 
			new Vector2(shootSpeed, shootSpeed));

	}
}


public class DogGun : Gun {
	
	protected Object bulletPrefab;
	protected static int shootSpeed = 8;
	protected RuntimeAnimatorController animator;
	public string controllerName {
		get {return "catpistol/shooty";}
	}

	
	public override void Init(MonoBehaviour parent){
		this.bulletPrefab = Resources.Load("catpistol/pistolBulletWeak");
		Debug.Log("loading "+ this.controllerName);
		this.animator = 
			(RuntimeAnimatorController) Resources.Load(this.controllerName);
	}
	
	public override void Equip(MonoBehaviour parent) {
		//Debug.Log(this.controllerName);
		parent.GetComponentInParent<Animator>()
			.runtimeAnimatorController = this.animator;
	}
		
	public override void Shoot(MonoBehaviour parent){
		Gun.SpewBullet(
			bulletPrefab, 
			parent, 
			new Vector2(0,0), 
			new Vector2(shootSpeed, shootSpeed));

	}
}

public class CatSpreadGun : Gun {
	
	protected Object bulletPrefab;
	protected static int shootSpeed = 8;
	protected RuntimeAnimatorController animator;
	public string controllerName {
		get {return "shotgun/shooty_shotgun";}
	}

	public override void Init(MonoBehaviour parent){
		this.bulletPrefab = Resources.Load("shotgun/shotgunBullet");
		Debug.Log("loading "+ this.controllerName);
		this.animator = 
			(RuntimeAnimatorController) Resources.Load(this.controllerName);
	}
	
	public override void Equip(MonoBehaviour parent) {
		//Debug.Log(this.controllerName);
		parent.GetComponentInParent<Animator>()
			.runtimeAnimatorController = this.animator;
	}
	
	public override void Shoot(MonoBehaviour parent){
		for(int i=0; i<10; i++) {
			Gun.SpewBullet(
				this.bulletPrefab, 
				parent, 
				new Vector2(-10,10), 
				new Vector2(shootSpeed-1, shootSpeed+1));
		}
	}
}


public class Knife : Gun {
	
	protected Object bulletPrefab;
	protected static int shootSpeed = 10;
	protected RuntimeAnimatorController animator;
	public string controllerName {
		get {return "ThrowingKnife/knife_animation_controller";}
	}

	public override void Init(MonoBehaviour parent){
		this.bulletPrefab = Resources.Load("ThrowingKnife/knife_bullet");
		Debug.Log("loading "+ this.controllerName);
		this.animator = 
			(RuntimeAnimatorController) Resources.Load(this.controllerName);
	}
	
	public override void Equip(MonoBehaviour parent) {
		//Debug.Log(this.controllerName);
		parent.GetComponentInParent<Animator>()
			.runtimeAnimatorController = this.animator;
	}
	
	public override void Shoot(MonoBehaviour parent){
		Gun.SpewBullet(
			this.bulletPrefab, 
			parent, 
			new Vector2(-3,3), 
			new Vector2(shootSpeed-1, shootSpeed+1));
	}
}


public class ABiggGun : Gun {
	
	protected Object bulletPrefabSmall, bulletPrefabBig;
	protected static float shootSpeed = 5;
	
	public override void Init(MonoBehaviour parent){
		this.bulletPrefabSmall = Resources.Load("biggBullet");
		this.bulletPrefabBig = Resources.Load("biggerBullet");
	}
	
	public override void Shoot(MonoBehaviour parent){
		for (int i=0; i<360; i+=30) {
			Gun.SpewBullet(
				this.bulletPrefabSmall, 
				parent, 
				(float) i, 
				shootSpeed);		
		}
		Gun.SpewBullet(
			this.bulletPrefabBig, 
			parent, 
			parent.transform.rotation.eulerAngles.y,
			shootSpeed * 0.8f);
	}
	
	public override void Equip(MonoBehaviour parent) {
	}
}