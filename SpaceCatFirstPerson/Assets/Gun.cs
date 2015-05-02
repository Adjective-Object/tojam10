using UnityEngine;
using System.Collections;

public abstract class Gun {
	public abstract void Init();
	public abstract void Shoot(MonoBehaviour parent);
	
	
	public static void  SpewBullet(
			Object prefab,
			MonoBehaviour parent,
			Vector2 xSpread,
			Vector2 speedSpread) {
		GameObject g = (GameObject) Object.Instantiate(prefab);
		
		float xDrift = Random.Range(xSpread.x, xSpread.y);
		float speedDrift = Random.Range(speedSpread.x, speedSpread.y);
		
		g.transform.position = parent.transform.position + 
			parent.transform.rotation * Vector3.forward * 0.3f;
		g.GetComponent<Bullet>().step = 
			Quaternion.AngleAxis(
				parent.transform.rotation.eulerAngles.y + xDrift,
				Vector3.up) * 
			Vector3.forward * 
			(speedDrift);
	}
}

public class CatGun : Gun {
	
	protected Object bulletPrefab;
	protected static int shootSpeed = 8;
	public override void Init(){
		this.bulletPrefab = Resources.Load("bullet");
	}
		
	public override void Shoot(MonoBehaviour parent){
		CatGun.SpewBullet(
			bulletPrefab, 
			parent, 
			new Vector2(0,0), 
			new Vector2(shootSpeed, shootSpeed));
	}
}

public class CatSpreadGun : CatGun {
	
	public override void Shoot(MonoBehaviour parent){
		for(int i=0; i<10; i++) {
			CatGun.SpewBullet(
				this.bulletPrefab, 
				parent, 
				new Vector2(-10,10), 
				new Vector2(shootSpeed-3, shootSpeed+3));			
		}
	}
	
}

