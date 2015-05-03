using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthTracker : MonoBehaviour {

	public LivingEntity target;
	public Texture[] sprites;
	
	private int maxHP;
	private RawImage img;

	// Use this for initialization
	void Start () {
		this.maxHP = target.health;
		this.img = this.GetComponent<RawImage>();
	}
	
	// Update is called once per frame
	void Update () {
		float hpFrac = ((float) Mathf.Max(target.health, 0)) / this.maxHP;
		int num = (int) Mathf.Ceil(hpFrac * (sprites.Length-1));
		this.img.texture = this.sprites[num];
	}
}
