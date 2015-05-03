using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {

    public Texture[] wallTextures;
    private static System.Random random = new System.Random();
	// Use this for initialization
	void Start () {
        
        GetComponent<Renderer>().material.SetTexture("_MainTex", wallTextures[random.Next(0, wallTextures.Length)]);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
