using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour {

	public int health;

	public string[] gibSpritePaths;
	private Sprite[] gibSprites;
	private Object gibPrefab = null;
	
	public float hspread = 0.1f;
	public float vmin = 1;
	public float vmax = 3;

	// Use this for initialization
	void Start () {
		gibSprites = new Sprite[gibSpritePaths.Length];
		for(int i=0; i<gibSpritePaths.Length; i++) {
			gibSprites[i] = Resources.Load<Sprite>(gibSpritePaths[i]);
			Debug.Log(gibSpritePaths[i] +"\t"+gibSprites[i]);
		}
		gibPrefab = Resources.Load("gibPrefab");
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void damageFor(int dmg) {
		this.health -= dmg;
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
