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

    [SerializeField] private GameObject tilePrefab;

    [SerializeField] private GameObject borderPrefab;

    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private float tileZCoordinate;

    public int size;

    [SerializeField] private int obstacleNumber;

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        System.Random rand = new System.Random();
        HashSet<int> obstaclePositions = new HashSet<int>();
        for (int i = 0; i < obstacleNumber; i++){
            obstaclePositions.Add(rand.Next((2*size+1)*(2*size+1)));
        }
        for (int i = -size; i <= size; i++){
            for (int j = -size; j <= size; j++){
                // Instantiate(tilePrefab, new Vector3(i, j, tileZCoordinate), Quaternion.identity);
                if (obstaclePositions.Contains( (size + i) * (2*size + 1) + size + j )){
                    Instantiate(obstaclePrefab, new Vector3(i, j, 0), Quaternion.identity);
                }
            }
        }
        for (int i = -size - 1; i <= size + 1; i++){
            Instantiate(borderPrefab, new Vector3(i, size+1, 0), Quaternion.identity);
            Instantiate(borderPrefab, new Vector3(i, -size-1, 0), Quaternion.identity);
            Instantiate(borderPrefab, new Vector3(size+1, i, 0), Quaternion.identity);
            Instantiate(borderPrefab, new Vector3(-size-1, i, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
