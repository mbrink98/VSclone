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

    [SerializeField] private float planetLayerScaleQuotient;

    [SerializeField] private GameObject borderPrefab;

    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private GameObject nebulaPrefab;

    [SerializeField] private float tileZCoordinate;

    public int size;

    [SerializeField] private int tilesPerObstacle;

    [SerializeField] private int scaleFactorPlanets;

    private List<GameObject> rootPlanets = new List<GameObject>();

    private System.Random rand;

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
            Instantiate(obstaclePrefab, randomPositionOnGrid(size, size, 1f, 0f), Quaternion.identity);
        }
        //Border
        for (int i = -size - 1; i <= size + 1; i++){
            Instantiate(borderPrefab, new Vector3(i, size+1, 0), Quaternion.identity);
            Instantiate(borderPrefab, new Vector3(i, -size-1, 0), Quaternion.identity);
            Instantiate(borderPrefab, new Vector3(size+1, i, 0), Quaternion.identity);
            Instantiate(borderPrefab, new Vector3(-size-1, i, 0), Quaternion.identity);
        }
        //Planets
        Instantiate(planetPrefabs[14], new Vector3(0f, 0f, 0f), Quaternion.identity);
        Vector3 cameraHalfDiagonal = Camera.main.ScreenToWorldPoint(
            new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, 0)
            ) - Camera.main.transform.position;
        float additionalSpaceX = cameraHalfDiagonal.x;
        float additionalSpaceY = cameraHalfDiagonal.y;
        float scale = planetLayerScaleQuotient;
        for (int layer = 1; layer <= numberOfPlanetLayers; layer++, scale *= planetLayerScaleQuotient){
            Vector3 unitX = scale * Vector3.right;
            Vector3 unitY = scale * Vector3.up;
            int sizeX = (int) (additionalSpaceX / scale) + size + 1;
            int sizeY = (int) (additionalSpaceY / scale) + size + 1;
            int numberOfScaledTiles = (2 * sizeX / scaleFactorPlanets + 1) * (2 * sizeY / scaleFactorPlanets + 1);
            GameObject root = Instantiate(
                planetPrefabs[rand.Next(planetPrefabs.Length)], 
                randomPositionOnGrid(sizeX, sizeY, scale, layer), 
                Quaternion.identity);
            root.transform.localScale *= scale * scaleFactorPlanets;
            for (int i = 1; i < numberOfScaledTiles / tilesPerPlanet; i++){
                GameObject planet = Instantiate(
                    planetPrefabs[rand.Next(planetPrefabs.Length)], 
                    randomPositionOnGrid(sizeX, sizeY, scale, layer), 
                    Quaternion.identity);
                planet.transform.localScale *= scale * scaleFactorPlanets;
                planet.transform.parent = root.transform;
            }
            rootPlanets.Add(root);
        }
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
        float scale = planetLayerScaleQuotient;
        for (int i = 0; i < numberOfPlanetLayers; i++, scale *= planetLayerScaleQuotient){
            float factor = 1.0f - scale;
            rootPlanets[i].transform.Translate(factor * cameraMovement);
        }
    }
}
