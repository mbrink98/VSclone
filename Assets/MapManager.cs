using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;

    public static MapManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("MapManager is NULL");
            }
            return _instance;
        }
    }

    [SerializeField] private GameObject[] planetPrefabs;

    [SerializeField] private int numberOfPlanetLayers;

    [SerializeField] private int tilesPerPlanet;

    [SerializeField] private float planetScaleFactor;

    [SerializeField] private GameObject borderPrefab;

    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private GameObject nebulaPrefab;

    [SerializeField] private float tileZCoordinate;

    public int size;

    [SerializeField] private int tilesPerObstacle;

    private List<GameObject> rootPlanets = new List<GameObject>();

    private System.Random rand;

    public int nebulaSkipBackAmount{
        get;
        private set;
    }

    public float nebulaMaxX{
        get;
        private set;
    }

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Obstacles
        rand = new System.Random();
        HashSet<int> obstaclePositions = new HashSet<int>();
        int numberOfTiles = (2*size+1)*(2*size+1);
        for (int i = 0; i < numberOfTiles / tilesPerObstacle; i++){
            obstaclePositions.Add(rand.Next(numberOfTiles));
        }
        for (int i = -size; i <= size; i++){
            for (int j = -size; j <= size; j++){
                if (obstaclePositions.Contains( (size + i) * (2*size + 1) + size + j )){
                    Instantiate(obstaclePrefab, new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }
        //Border
        for (int i = -size - 1; i <= size + 1; i++){
            Instantiate(borderPrefab, new Vector3(i, size+1, 0), Quaternion.identity);
            Instantiate(borderPrefab, new Vector3(i, -size-1, 0), Quaternion.identity);
            Instantiate(borderPrefab, new Vector3(size+1, i, 0), Quaternion.identity);
            Instantiate(borderPrefab, new Vector3(-size-1, i, 0), Quaternion.identity);
        }
        //Planets
        Vector3 cameraHalfDiagonal = Camera.main.ScreenToWorldPoint(
            new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0)
            ) - Camera.main.transform.position;
        float additionalSpaceX = cameraHalfDiagonal.x;
        float additionalSpaceY = cameraHalfDiagonal.y;
        float scale = planetScaleFactor;
        for (int layer = 1; layer <= numberOfPlanetLayers; layer++, scale *= planetScaleFactor){
            Vector3 unitX = scale * Vector3.right;
            Vector3 unitY = scale * Vector3.up;
            int sizeX = (int) (additionalSpaceX / scale) + size;
            int sizeY = (int) (additionalSpaceY / scale) + size;
            int numberOfScaledTiles = (2 * sizeX + 1) * (2 * sizeY + 1);
            GameObject root = Instantiate(
                planetPrefabs[rand.Next(planetPrefabs.Length)], 
                randomPositionOnGrid(sizeX/2, sizeY/2, scale*2, layer), 
                Quaternion.identity);
            root.transform.localScale *= scale;
            for (int i = 1; i < numberOfScaledTiles / tilesPerPlanet; i++){
                GameObject planet = Instantiate(
                    planetPrefabs[rand.Next(planetPrefabs.Length)], 
                    randomPositionOnGrid(sizeX/2, sizeY/2, scale*2, layer), 
                    Quaternion.identity);
                planet.transform.localScale *= scale;
                planet.transform.parent = root.transform;
            }
            rootPlanets.Add(root);
        }
        //Nebula
        int maxX = size + 1 + (int) additionalSpaceX;
        int maxY = size + 1 + (int) additionalSpaceY;
        int i_start = -maxX;
        int j_start = -maxY;
        while (i_start <= maxX || j_start != -maxY){
            int i = i_start;
            int j = j_start;
            while (j <= maxY){
                Vector3 pos = new Vector3(i + UnityEngine.Random.Range(-.5f, .5f), j + UnityEngine.Random.Range(-.5f, .5f), .5f);
                Instantiate(nebulaPrefab, pos, Quaternion.identity);
                i += 1;
                j += 3;
            }
            i_start += 3;
            j_start -= 1;
            if (j_start < -maxY){
                i_start += 1;
                j_start += 3;
            }
        }
        nebulaSkipBackAmount = i_start + maxX;
        nebulaMaxX = maxX;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 randomPositionOnGrid(int gridSizeX, int gridSizeY, float gridConstant, float z){
        Vector3 result = z * Vector3.forward;
        result += gridConstant * ( rand.Next(-gridSizeY, gridSizeY + 1) * Vector3.up + rand.Next(-gridSizeX, gridSizeX + 1) * Vector3.right );
        return result;
    }

    public void movePlanets(Vector3 cameraMovement){
        float scale = planetScaleFactor;
        for (int i = 0; i < numberOfPlanetLayers; i++, scale *= planetScaleFactor){
            float factor = 1.0f - scale;
            rootPlanets[i].transform.Translate(factor * cameraMovement);
        }
    }
}
