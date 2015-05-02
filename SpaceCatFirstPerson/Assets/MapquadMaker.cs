using UnityEngine;
using System.Collections;

public class MapquadMaker : MonoBehaviour {

	private MeshFilter filter;
	
	public int tile_count_y = 4;
	public int tile_count_x = 3;
	public int tileset_x = 0;
	public int tileset_y = 0;

	private static Vector3[] basePoints = {
		new Vector3(-0.5f, 0f ,-0.5f),
		new Vector3(-0.5f, 0f, 0.5f),
		new Vector3(0.5f,  0f, -0.5f),
		new Vector3(0.5f,  0f, 0.5f)
	};

	private static int[] baseTris = {0,1,2,2,1,3};

	// Use this for initialization
	void Start () {
		float tsize_x = 1f / tile_count_x;
		float tsize_y = 1f / tile_count_y;
		
		Debug.Log(tsize_x +"\t"+ tsize_y);
		
		this.filter = GetComponent<MeshFilter>();

		this.filter.mesh.vertices = MapquadMaker.basePoints;
		this.filter.mesh.triangles  = MapquadMaker.baseTris;
		Vector2[] uv = new Vector2[] {
			new Vector2(tsize_x * this.tileset_x, 		tsize_y * this.tileset_y),
			new Vector2(tsize_x * (this.tileset_x + 1), tsize_y * this.tileset_y),
			new Vector2(tsize_x * this.tileset_x, 		tsize_y * (this.tileset_y + 1)),
			new Vector2(tsize_x * (this.tileset_x + 1), tsize_y * (this.tileset_y + 1))
		};
		this.filter.mesh.uv = uv;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
