using UnityEngine;
using System.Collections;

public class MapquadMaker : MonoBehaviour {

	private MeshFilter filter;
	
	public int TILESIZE = 32;
	public int tileset_x = 0;
	public int tileset_y = 0;

	private static Vector3[] points = {
		new Vector3(0,0),
		new Vector3(0,1),
		new Vector3(1,0),
		new Vector3(1,1)
	};

	// Use this for initialization
	void Start () {
		this.filter = GetComponent<MeshFilter>();
		
		mesh.verticies = 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
