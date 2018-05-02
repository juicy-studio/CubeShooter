using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    private float time;
    public Map[] maps;
    public int mapIndex;

	public Transform ShopPrefab;
    public Transform tilePrefab;

    public Transform navmeshFloor;
    public Transform navmeshMaskPrefab;
    public Vector2 maxMapSize;
	int Cycle=1;
	public int seed;
	private int randomObstacle;

    [Range(0, 1)]
    public float outlinePercent;


    public float tileSize;
    List<Coord> allTileCoords;
    Queue<Coord> shuffledTileCoords;
    Queue<Coord> shuffledOpenTileCoords;
    Transform[,] tileMap;
	private Vector3 gap =new Vector3(0,-0.5f,0);
	//private int WaveChange=0;
	int currentStage;

    Map currentMap;

    void Awake()
    {
        GenerateMap();
		currentStage = ScoreManager.stage;
    }

    void Update()
	{
		
		if (ScoreManager.stage > currentStage) {
			StartCoroutine (OnNewWave ());
			currentStage = ScoreManager.stage;
		}
		

    }

	IEnumerator OnNewWave()
    {
		yield return new WaitForSeconds (2.0f);
		if (ScoreManager.stage < 8) {
			mapIndex = ScoreManager.stage;
			GenerateMap ();
		} else {
			if ((ScoreManager.stage - 8 * Cycle)==8) {
				Cycle++;
			}
			/*
			if ((ScoreManager.stage - 8 * Cycle) == 2 || ScoreManager.stage - 8 * Cycle == 5) {
				ScoreManager.stage++;
			}
			*/
			mapIndex = ScoreManager.stage - 8*Cycle;
			GenerateMap ();

		}
    }

    public void GenerateMap()
	{
		seed = Random.Range (0, 3000);

		currentMap = maps [mapIndex];
		tileMap = new Transform[currentMap.mapSize.x, currentMap.mapSize.y];
		System.Random prng = new System.Random (seed);
		GetComponent<BoxCollider> ().size = new Vector3 (currentMap.mapSize.x * tileSize, .05f, currentMap.mapSize.y * tileSize);

		// Generating coords
		allTileCoords = new List<Coord> ();
		for (int x = 0; x < currentMap.mapSize.x; x++) {
			for (int y = 0; y < currentMap.mapSize.y; y++) {
				allTileCoords.Add (new Coord (x, y));
			}
		}
		shuffledTileCoords = new Queue<Coord> (Utility.ShuffleArray (allTileCoords.ToArray (), seed));

		// Create map holder object
		string holderName = "Generated Map";
		if (transform.Find (holderName)) {
			DestroyImmediate (transform.Find (holderName).gameObject);
		}

		Transform mapHolder = new GameObject (holderName).transform;
		mapHolder.parent = transform;


//		Debug.Log (null == transform.Find ("Tiles"));

		if (null == transform.Find ("Tiles")) {

			Transform mapHolder2 = new GameObject ("Tiles").transform;
			mapHolder2.parent = transform;

			// Spawning tiles
			for (int x = 0; x < currentMap.mapSize.x; x++) {
				for (int y = 0; y < currentMap.mapSize.y; y++) {
					Vector3 tilePosition = CoordToPosition (x, y);
					Transform newTile = Instantiate (tilePrefab, tilePosition, Quaternion.Euler (Vector3.right * 90)) as Transform;
					newTile.localScale = Vector3.one * (1 - outlinePercent) * tileSize;
					newTile.parent = mapHolder2;
					tileMap [x, y] = newTile;
				}
			}
		}
		


			// Spawning obstacles
			bool[,] obstacleMap = new bool[(int)currentMap.mapSize.x, (int)currentMap.mapSize.y];

			int obstacleCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y * currentMap.obstaclePercent);
			int currentObstacleCount = 0;
			List<Coord> allOpenCoords = new List<Coord> (allTileCoords);
      
			//Spawn Shop

		for (int i = 0; i < currentMap.ShopNumber; i++) {
			Coord ShopCoord = GetRandomCoord ();
			while (ShopCoord.y == 0) {
				ShopCoord = GetRandomCoord ();
			}
			obstacleMap [ShopCoord.x, ShopCoord.y] = true;
			obstacleMap [ShopCoord.x, ShopCoord.y - 1] = true;

//			Debug.Log (ShopCoord.x + ShopCoord.y);
			currentObstacleCount++;
			currentObstacleCount++;
			Vector3 ShopPosition = CoordToPosition (ShopCoord.x, ShopCoord.y);
			Transform newShop = Instantiate (ShopPrefab, (ShopPosition + Vector3.down * 3f) + Vector3.down, Quaternion.Euler (0, 180, 0)) as Transform;
			newShop.parent = mapHolder;
			Coord Entrance = new Coord (ShopCoord.x, ShopCoord.y - 1);
			/*for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				int neighbourX = ShopCoord.x + x;
				int neighbourY = ShopCoord.y + y;
				obstacleMap [neighbourX, neighbourY] = true;
				currentObstacleCount++;
				Coord NeighbourShop = new Coord (neighbourX, neighbourY);
				allOpenCoords.Remove (NeighbourShop);
			}
		}*/

			allOpenCoords.Remove (Entrance);
			allOpenCoords.Remove (ShopCoord);
		}


			for (int i = 0; i < obstacleCount; i++) {
				Coord randomCoord = GetRandomCoord ();
				obstacleMap [randomCoord.x, randomCoord.y] = true;
				currentObstacleCount++;

				if (randomCoord != currentMap.mapCentre && MapIsFullyAccessible (obstacleMap, currentObstacleCount)) {
					float obstacleHeight = 0f;
					randomObstacle = Random.Range (0, currentMap.obstaclePrefabs.Length);
					/*
				foreach(GameObject ObPrefab in getChildren(currentMap.obstaclePrefabs[randomObstacle]))
				{
					Mesh mesh = ObPrefab.GetComponent<MeshFilter> ().mesh;
					Bounds bounds = mesh.bounds;
					if(obstacleHeight<bounds.size.y)
					{
						obstacleHeight = bounds.size.y;
					}
				}*/
					float randomRotation = Random.Range (0f, 360f);
					Transform obstaclePrefab = currentMap.obstaclePrefabs [randomObstacle].GetComponent<Transform> ();
					Vector3 obstaclePosition = CoordToPosition (randomCoord.x, randomCoord.y);
					Transform newObstacle = Instantiate (obstaclePrefab, (obstaclePosition + Vector3.down * obstacleHeight / 2) + Vector3.down / 10, Quaternion.Euler (0f, randomRotation, 0f)) as Transform;
					newObstacle.parent = mapHolder;
       	 
					allOpenCoords.Remove (randomCoord);
				} else {
					obstacleMap [randomCoord.x, randomCoord.y] = false;
					currentObstacleCount--;
				}
			}

			shuffledOpenTileCoords = new Queue<Coord> (Utility.ShuffleArray (allOpenCoords.ToArray (), seed));

			// Creating navmesh mask
			Transform maskLeft = Instantiate (navmeshMaskPrefab, Vector3.left * (currentMap.mapSize.x + maxMapSize.x) / 4f * tileSize, Quaternion.identity) as Transform;
			maskLeft.parent = mapHolder;
			maskLeft.localScale = new Vector3 ((maxMapSize.x - currentMap.mapSize.x) / 2f, 1, currentMap.mapSize.y) * tileSize;

			Transform maskRight = Instantiate (navmeshMaskPrefab, Vector3.right * (currentMap.mapSize.x + maxMapSize.x) / 4f * tileSize, Quaternion.identity) as Transform;
			maskRight.parent = mapHolder;
			maskRight.localScale = new Vector3 ((maxMapSize.x - currentMap.mapSize.x) / 2f, 1, currentMap.mapSize.y) * tileSize;

			Transform maskTop = Instantiate (navmeshMaskPrefab, Vector3.forward * (currentMap.mapSize.y + maxMapSize.y) / 4f * tileSize, Quaternion.identity) as Transform;
			maskTop.parent = mapHolder;
			maskTop.localScale = new Vector3 (maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y) / 2f) * tileSize;

			Transform maskBottom = Instantiate (navmeshMaskPrefab, Vector3.back * (currentMap.mapSize.y + maxMapSize.y) / 4f * tileSize, Quaternion.identity) as Transform;
			maskBottom.parent = mapHolder;
			maskBottom.localScale = new Vector3 (maxMapSize.x, 1, (maxMapSize.y - currentMap.mapSize.y) / 2f) * tileSize;

			navmeshFloor.localScale = new Vector3 (maxMapSize.x, maxMapSize.y) * tileSize;


	}

    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
    {
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(currentMap.mapCentre);
        mapFlags[currentMap.mapCentre.x, currentMap.mapCentre.y] = true;

        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if (x == 0 || y == 0)
                    {
                        if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlags[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }
        }

        int targetAccessibleTileCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y - currentObstacleCount);
        return targetAccessibleTileCount == accessibleTileCount;
    }

	Vector3 CoordToPosition(int x, int y)
	{
		return new Vector3(-currentMap.mapSize.x / 2f + 0.5f + x, 0, -currentMap.mapSize.y / 2f + 0.5f + y) * tileSize;
	}

    public Transform GetTileFromPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / tileSize + (currentMap.mapSize.x - 1) / 2f);
        int y = Mathf.RoundToInt(position.z / tileSize + (currentMap.mapSize.y - 1) / 2f);
        x = Mathf.Clamp(x, 0, tileMap.GetLength(0) - 1);
        y = Mathf.Clamp(y, 0, tileMap.GetLength(1) - 1);
        return tileMap[x, y];
    }

	public Coord GetRandomCoord()
	{
		Coord randomCoord = shuffledTileCoords.Dequeue();
		shuffledTileCoords.Enqueue(randomCoord);
		return randomCoord;
	}

    public Transform GetRandomOpenTile()
    {
        Coord randomCoord = shuffledOpenTileCoords.Dequeue();
        shuffledOpenTileCoords.Enqueue(randomCoord);
        return tileMap[randomCoord.x, randomCoord.y];
    }

	/*
	public GameObject[] getChildren(GameObject parent)
	{
		int i = 0;
		int max = 0;
		int max2;

		Transform[] Tparent = parent.GetComponentsInChildren<Transform> ();
		max = Tparent.Length;
		Transform[] ReParents = new Transform[max];
		foreach (Transform Obj in Tparent) {
		
			ReParents[i] = Obj;
			i++;
		}
		i = 0;
		max2 = ReParents.Length;
		GameObject[] ReReParents = new GameObject[max2];
		foreach (Transform Obj in ReParents) {

			ReReParents[i] = Obj.gameObject;
			i++;
		}

		return ReReParents;

	}
	*/

    [System.Serializable]
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }

    }




    [System.Serializable]
    public class Map
    {

        public Coord mapSize;
        [Range(0, 1)]
        public float obstaclePercent;
		public GameObject[] obstaclePrefabs;
		public int ShopNumber = 2;

        public Coord mapCentre
        {
            get
            {
                return new Coord(mapSize.x / 2, mapSize.y / 2);
            }
        }

    }
}