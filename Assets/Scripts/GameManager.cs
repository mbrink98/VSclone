using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public bool gameIsPaused{
        get;
        set;
    }

    public Queue<float> levels{
        get;
        set;
    }
    public float [] levelsArray{
        get;
        set;
    }

    public float playerEXP{
        get;
        set;
    }

    public float playerLVL{
        get;
        set;
    }

    public float playerHealth{
        get;
        set;
    }

    public float playerMaxHealth{
        get;
        set;
    }

    public float playerMovementSpeed{
        get;
        set;
    }

    public float playerRotationSpeed{
        get;
        set;
    }

    public float playerBulletSpeed{
        get;
        set;
    }

    public float playerAttackDelay{
        get;
        set;
    }

    public float playerReloadSpeed{
        get;
        set;
    }

    public float playerAmmo{
        get;
        set;
    }

    public float playerMaxAmmo{
        get;
        set;
    }

    public UnityEvent playerUpgradeEvent;
    public UnityEvent gameManagerInit;

    public Image healthBar;
    public Image ExpBar;
    public float expCap = 4f;
    public float expMulti = 1.7f;
    

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemySpawnerPrefab;

    [SerializeField] private GameObject enemyBomberSpawnerPrefab;

    [SerializeField] private GameObject enemySharpshooterSpawnerPrefab;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is NULL");
            }
            return _instance;
        }
    }

    void Awake()
    {
        Queue<float> levels = new Queue<float>();
        
        levels.Enqueue(expCap);
        for (int i = 1; i < 50; i++) 
        {
            expCap = (float) Math.Round(expCap * expMulti);
            levels.Enqueue(expCap);
        }
        this.levels = levels;
        this.levelsArray = levels.ToArray();        //copy to array um pro lvl auf exp nötig zum aufsteigen zuzugreifen
        this.gameIsPaused = false;
        _instance = this;

        gameManagerInit.Invoke();
    }

    void Start()
    {
        // Player spawn
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        
        // Enemy Spawner spawn
        Vector3 spawnLeftBorder = new Vector3(transform.position.x - 15, transform.position.y,transform.position.z);
        Instantiate(enemySpawnerPrefab, spawnLeftBorder, Quaternion.identity);
        Vector3 spawnRightBorder = new Vector3(transform.position.x + 15, transform.position.y,transform.position.z);
        Instantiate(enemySpawnerPrefab, spawnRightBorder, Quaternion.identity);
        Vector3 spawnBottomBorder = new Vector3(transform.position.x, transform.position.y  - 10,transform.position.z);
        Instantiate(enemySpawnerPrefab, spawnBottomBorder, Quaternion.identity);
        Vector3 spawnTopBorder = new Vector3(transform.position.x, transform.position.y + 10,transform.position.z);
        Instantiate(enemySpawnerPrefab, spawnTopBorder, Quaternion.identity);

        //Bomber Enemy Spawner spawn

        Instantiate(enemyBomberSpawnerPrefab, spawnLeftBorder, Quaternion.identity);

        //SharpShooter Enemy Spawner spawn
        
        Instantiate(enemySharpshooterSpawnerPrefab, spawnRightBorder, Quaternion.identity);

    }

    public void PlayerLevelUp()
    {
        _instance.gameIsPaused = true;
        playerUpgradeEvent.Invoke();
    }

    public void UpgradeChosen()
    {
        _instance.gameIsPaused = false;
    }

    void Update(){
        healthBar.fillAmount = Mathf.Clamp(playerHealth/playerMaxHealth,0,1);               //könnte vllt ins playergetshitevent
        ExpBar.fillAmount = Mathf.Clamp(playerEXP/levelsArray[(int)playerLVL],0,1);         //könnte vllt ins player gets lvl up event
        
       // Debug.Log("Levelsarray "+ levelsArray[(int)playerLVL]);
        
    }

}
