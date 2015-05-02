using UnityEngine;
using System.Collections;

public class MapMaker : MonoBehaviour {

    public int[,] map;
    public int width;
    public int height;

    public GameObject ceiling;
    public GameObject floor;

    public Material material;

    private MeshFilter filter;

    public int tile_count_y = 4;
    public int tile_count_x = 3;
    public int tileset_x = 0;
    public int tileset_y = 0;

    private static Vector3[] basePoints = {
		new Vector3(-0.5f, -0.5f, 0.5f),
		new Vector3(-0.5f, 0.5f,  0.5f),
		new Vector3(0.5f,  -0.5f, 0.5f),
		new Vector3(0.5f,   0.5f, 0.5f)
	};

    private static int[] baseTris = { 0, 2, 1, 2, 3, 1 };


	// Use this for initialization
	void Start () {
	    //map = new map[height,width];

        height = 12;
        width = 12;
        map = new int[,] {{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                          {1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1},
                          {1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 1},
                          {1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 1},
                          {1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 1},
                          {1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1},
                          {1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1},
                          {1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1},
                          {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                          {1, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0, 1},
                          {1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1},
                          {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}};
        for (int h = 0; h < height; h++)
        {
            for (int w = 0; w < width; w++)
            {
                if (map[h, w] != 0)
                {
                    createWall(w, h);
                }
            }
        }

        floor.transform.position = new Vector3(width/2, 0, height/2);
        ceiling.transform.position = new Vector3(width/2, 1, height/2);

        floor.transform.localScale = new Vector3(width, height, 1);
        ceiling.transform.localScale = new Vector3(width, height, 1);

        floor.GetComponent<Renderer>().material.renderQueue = 1;
        ceiling.GetComponent<Renderer>().material.renderQueue = 2;

        float tsize_x = 1f / tile_count_x /width;
        float tsize_y = 1f / tile_count_y /height;
        floor.GetComponent<MeshFilter>().mesh.uv = new Vector2[] {
			new Vector2(tsize_x * this.tileset_x, 		tsize_y * this.tileset_y),
			new Vector2(tsize_x * (this.tileset_x + 1), tsize_y * this.tileset_y),
			new Vector2(tsize_x * this.tileset_x, 		tsize_y * (this.tileset_y + 1)),
			new Vector2(tsize_x * (this.tileset_x + 1), tsize_y * (this.tileset_y + 1))
		};
	}

    private void createWall(int x, int y)
    {
        createWallSide(x, y, 0);
        createWallSide(x, y, 90);
        createWallSide(x, y, 180);
        createWallSide(x, y, 270);
    }

    private void createWallSide(int x, int y, int rot)
    {
        float tsize_x = 1f / tile_count_x;
        float tsize_y = 1f / tile_count_y;

        Debug.Log(tsize_x + "\t" + tsize_y);

        Mesh mesh = new Mesh();
        mesh.name = "Wall";

        mesh.vertices = basePoints;
        mesh.triangles = baseTris;
        Vector2[] uv = new Vector2[] {
			new Vector2(tsize_x * this.tileset_x, 		tsize_y * this.tileset_y),
			new Vector2(tsize_x * (this.tileset_x + 1), tsize_y * this.tileset_y),
			new Vector2(tsize_x * this.tileset_x, 		tsize_y * (this.tileset_y + 1)),
			new Vector2(tsize_x * (this.tileset_x + 1), tsize_y * (this.tileset_y + 1))
		};
        mesh.uv = uv;
        GameObject meshObj = new GameObject("Wall");
        MeshFilter meshFilter = (MeshFilter)meshObj.AddComponent(typeof(MeshFilter));
        meshFilter.mesh = mesh;
        MeshRenderer renderer = meshObj.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material = material;
        meshObj.transform.position = new Vector3(x, 0.5f, y);
        meshObj.transform.rotation = Quaternion.Euler(0, rot, 270);

        meshObj.AddComponent(typeof(BoxCollider));
    }


    // Update is called once per frame
    void Update()
    {
	
	}
}
