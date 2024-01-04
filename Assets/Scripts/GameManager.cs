using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public GameObject player{
        get;
        private set;
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

    public string playerGun{
        get;
        set;
    }

    public int enemyMaxHealth{
        get;
        set;
    }

    private List<string> weapons = new List<string>(){
        "Gun", "Shotgun", "Laser"
    };

    private List<GameObject> generatedChoices = new List<GameObject>();

    public UnityEvent selectWeaponEvent;
    public UnityEvent playerUpgradeEvent;
    public UnityEvent gameManagerInit;
    public UnityEvent gameManagerStarted;

    public Image healthBar;
    public Image ExpBar;
    public float expCap = 3f;
    public float expMulti = 1.4f;
    

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemySpawnerPrefab;
    [SerializeField] private GameObject enemyBomberSpawnerPrefab;
    [SerializeField] private GameObject enemySharpshooterSpawnerPrefab;
    [SerializeField] private GameObject upgradePrefab;

    private GameObject enemySpawner1;
    private GameObject enemySpawner2;
    private GameObject enemySpawner3;
    private GameObject enemySpawner4;

    private float spawnerTimer = 0.0f;
    private float EnemyTimer = 0.0f;
    private float period = 3f;
    public float EnemyHealthCycle = 60f;

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

        this.playerGun = "Gun";
     
        this.gameIsPaused = false;

        _instance = this;
        gameManagerInit.Invoke();
    }

    void Start()
    {
        // Player spawn
        _instance.player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        GenerateWeapons();

        // Enemy Spawner spawn
        Vector3 spawnLeftBorder = new Vector3(transform.position.x - 5, transform.position.y,transform.position.z);
        Vector3 spawnRightBorder = new Vector3(transform.position.x + 5, transform.position.y,transform.position.z);
        Vector3 spawnBottomBorder = new Vector3(transform.position.x, transform.position.y  - 5,transform.position.z);
        Vector3 spawnTopBorder = new Vector3(transform.position.x, transform.position.y + 5,transform.position.z);

        _instance.enemySpawner1 = Instantiate(enemySpawnerPrefab, spawnLeftBorder, Quaternion.identity);
        _instance.enemySpawner2 = Instantiate(enemySpawnerPrefab, spawnRightBorder, Quaternion.identity);
        _instance.enemySpawner3 = Instantiate(enemySpawnerPrefab, spawnBottomBorder, Quaternion.identity);
        _instance.enemySpawner4 = Instantiate(enemySpawnerPrefab, spawnTopBorder, Quaternion.identity);
        _instance.enemyMaxHealth = 1;

        // //Bomber Enemy Spawner spawn

        // Instantiate(enemyBomberSpawnerPrefab, spawnLeftBorder, Quaternion.identity);

        // //SharpShooter Enemy Spawner spawn
        
        // Instantiate(enemySharpshooterSpawnerPrefab, spawnRightBorder, Quaternion.identity);
        gameManagerStarted.Invoke();
    } 

    void Update()
    {
        spawnerTimer += Time.deltaTime;
        EnemyTimer += Time.deltaTime;
        if(spawnerTimer >= period ) { // reset position to borders
            spawnerTimer = spawnerTimer % 1f;
            Vector3 bottomLeft = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(-1, -1, Camera.main.nearClipPlane));
            Vector3 topLeft = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(-1, Camera.main.pixelHeight+1, Camera.main.nearClipPlane));
            Vector3 topRight = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth+1, Camera.main.pixelHeight+1, Camera.main.nearClipPlane));
            Vector3 bottomRight = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth+1, -1, Camera.main.nearClipPlane));

            _instance.enemySpawner1.transform.position = bottomLeft;
            _instance.enemySpawner2.transform.position = topLeft;
            _instance.enemySpawner3.transform.position = topRight;
            _instance.enemySpawner4.transform.position = bottomRight;
        }

        if(EnemyTimer >= EnemyHealthCycle ) {
            EnemyTimer = spawnerTimer % 1f;
            _instance.enemyMaxHealth += 1;
        }
        
        healthBar.fillAmount = Mathf.Clamp(_instance.playerHealth/_instance.playerMaxHealth,0,1);               //könnte vllt ins playergetshitevent
        ExpBar.fillAmount = Mathf.Clamp(_instance.playerEXP/_instance.levels.Peek(),0,1);         //könnte vllt ins player gets lvl up event
    }

    public void GenerateWeapons()
    {
        _instance.gameIsPaused = true;
        Queue<int> positionsX = new Queue<int>(new List<int>() { -550, 0, 550 });

        // Generate the Upgrades
        for (int i = 0; i < weapons.Count; i++)
        {
            GameObject canvas = GameObject.Find("UpgradeCardCanvas");
            Vector3 weaponPosition = new Vector3(canvas.transform.position.x + positionsX.Dequeue(), canvas.transform.position.y, canvas.transform.position.z);

            GameObject weaponChoice = _instance.upgradePrefab;
            weaponChoice.name = "Weapon-"+ weapons[i];
            Sprite weaponSprite = Resources.Load<Sprite>("Sprites/" + weapons[i]);
            weaponChoice.GetComponent<Image>().sprite = weaponSprite;
            weaponChoice.GetComponentInChildren<TMP_Text>().text = weapons[i];

            _instance.generatedChoices.Add(Instantiate(weaponChoice, weaponPosition, Quaternion.identity, canvas.transform));
        }
    }

    public void WeaponChosen(string upgradeName)
    {
        // upgradeName = upgradeName.Substring(0, upgradeName.IndexOf('('));
        _instance.playerEXP = 0f;
        _instance.playerLVL = 1f;

        
        switch (upgradeName)
        {
            // Weapons
            case "Gun": 
                _instance.playerBulletSpeed = 10f;
                _instance.playerAttackDelay = 0.0f;
                _instance.playerMaxAmmo = 10f;
                _instance.playerReloadSpeed = 1.5f;
                _instance.playerGun = "Gun";

                _instance.playerMovementSpeed = 3f;
                _instance.playerRotationSpeed = 100f;
                _instance.playerMaxHealth = 7;
                _instance.playerHealth = 7;

                break;
            case "Shotgun":
                _instance.playerBulletSpeed = 4f;
                _instance.playerAttackDelay = 0.0f;
                _instance.playerMaxAmmo = 3f;
                _instance.playerReloadSpeed = 3f;
                _instance.playerGun = "Shotgun";

                _instance.playerMovementSpeed = 1.5f;
                _instance.playerRotationSpeed = 80f;
                _instance.playerMaxHealth = 10;
                _instance.playerHealth = 10;

                break;
            case "Laser":
                _instance.playerBulletSpeed = 5f;
                _instance.playerAttackDelay = 1.5f;
                _instance.playerMaxAmmo = 5f;
                _instance.playerReloadSpeed = 5f;
                _instance.playerGun = "Laser";

                _instance.playerMovementSpeed = 4f;
                _instance.playerRotationSpeed = 150f;
                _instance.playerMaxHealth = 3;
                _instance.playerHealth = 3;

                break;
        }

        _instance.playerHealth = _instance.playerMaxHealth;
        _instance.playerAmmo = _instance.playerMaxAmmo;

        DestroyChoices();
    }

    public void DestroyChoices()
    {
        for (int i = 0; i < _instance.generatedChoices.Count; i++)
        {
            Destroy(_instance.generatedChoices[i]);
        }
        _instance.gameIsPaused = false;
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

}
