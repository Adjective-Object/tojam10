using UnityEngine;
using System.Collections;

public class MapMaker : MonoBehaviour {


    public enum TileTypes
    {
        Empty = 0,
        Blocked = 1,
        PlayerSpawn = 2
    }

    public int[,] map;
    public int width;
    public int height;

    public GameObject ceiling;
    public GameObject floor;
    public GameObject player;

    public Material material;

    private MeshFilter filter;

    public int tile_count_y = 4;
    public int tile_count_x = 4;
    public int tileset_x = 0;
    public int tileset_y = 0;

    const float tilesize_x = 0.25f;
    const float tilesize_y = 0.25f;

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
                          {1, (int)TileTypes.PlayerSpawn, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1},
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
                if (map[h, w] == (int)TileTypes.Blocked)
                {
                    createWall(w, h);
                }
                else if (map[h, w] == (int)TileTypes.PlayerSpawn)
                {
                    player.transform.position = new Vector3(w, 0.5f, h);
                }
            }
        }

        floor.transform.position = new Vector3(width / 2 - 0.5f, 0, height / 2 - 0.5f);
        ceiling.transform.position = new Vector3(width / 2 - 0.5f, 1, height / 2 - 0.5f);

        floor.transform.localScale = new Vector3(width, height, 1);
        ceiling.transform.localScale = new Vector3(width, height, 1);

        floor.GetComponent<Renderer>().material.renderQueue = 1;
        ceiling.GetComponent<Renderer>().material.renderQueue = 2;

        float tsize_x = (1f / tile_count_x);
        float tsize_y = (1f / tile_count_y);
        floor.GetComponent<MeshFilter>().mesh.uv = new Vector2[] {
			new Vector2(tsize_x * this.tileset_x, 		tsize_y * this.tileset_y),
			new Vector2(tsize_x * (this.tileset_x + 1), tsize_y * this.tileset_y),
			new Vector2(tsize_x * this.tileset_x, 		tsize_y * (this.tileset_y + 1)),
			new Vector2(tsize_x * (this.tileset_x + 1), tsize_y * (this.tileset_y + 1))
		};
	}

    /// <summary>
    /// Create a wall cube at given tile position.
    /// </summary>
    private void createWall(int x, int y)
    {
        //Create collision box
        GameObject wall = new GameObject("Wall");
        wall.transform.position = new Vector3(x, 0.5f, y);
        BoxCollider box = (BoxCollider)wall.AddComponent(typeof(BoxCollider));
        box.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        //Create 4 sides 
        createWallSide(x, y, 0);
        createWallSide(x, y, 90);
        createWallSide(x, y, 180);
        createWallSide(x, y, 270);
    }

    /// <summary>
    /// Create one side of a wall cube
    /// </summary>
    private GameObject createWallSide(int x, int y, int rot)
    {
        Mesh mesh = new Mesh();
        mesh.name = "Wall";

        mesh.vertices = basePoints;
        mesh.triangles = baseTris;
        Vector2[] uv = new Vector2[] {
			new Vector2(tilesize_x * this.tileset_x, 		tilesize_y * this.tileset_y),
			new Vector2(tilesize_x * (this.tileset_x + 1), tilesize_y * this.tileset_y),
			new Vector2(tilesize_x * this.tileset_x, 		tilesize_y * (this.tileset_y + 1)),
			new Vector2(tilesize_x * (this.tileset_x + 1), tilesize_y * (this.tileset_y + 1))
		};
        mesh.uv = uv;
        GameObject meshObj = new GameObject("WallSide");
        MeshFilter meshFilter = (MeshFilter)meshObj.AddComponent(typeof(MeshFilter));
        meshFilter.mesh = mesh;
        MeshRenderer renderer = meshObj.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        renderer.material = material;
        meshObj.transform.position = new Vector3(x, 0.5f, y);
        meshObj.transform.rotation = Quaternion.Euler(0, rot, 270);

        return meshObj;
    }


    // Update is called once per frame
    void Update()
    {
	
	}
}
