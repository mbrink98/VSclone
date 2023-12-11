using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;
using UnityEngine.Events;
using Unity.VisualScripting;

// To Add an Upgrade just add the entries in dictionaries and the switch case + 
// add an Image with upgradeImage as name + chagne upgradeNames variable name once???
public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager _instance;

    [SerializeField] private GameObject upgradePrefab;

    public Dictionary<string, float> upgradeValues = new Dictionary<string, float>()
    {
        {"AttackSpeed", 0.7f}, // Better : DOWN THE DELAY
        {"MovementSpeed", 1.5f}, // Better : UP
        {"Health", 1.6f}, // Better : UP
        {"Ammo", 1f}, // Better : UP
        {"ReloadSpeed", 0.8f}, // Better : DOWN
    };

    public Dictionary<string, string> upgradeDescription = new Dictionary<string, string>()
    {
        {"AttackSpeed", "Increase Attackspeed"},
        {"MovementSpeed", "Increase Movementspeed"},
        {"Health", "Increase Health"},
        {"Ammo", "Increase Ammo"},
        {"ReloadSpeed", "Decrease Reloadtime"},
    };

    public Dictionary<string, string> upgradeImage = new Dictionary<string, string>()
    {
        {"AttackSpeed", "AttackSpeedUp"},
        {"MovementSpeed", "MovementSpeedUp"},
        {"Health", "HealthUp"},
        {"Ammo", "AmmoUp"},
        {"ReloadSpeed", "ReloadSpeedUp"},
    };

    private List<string> upgradeNames2 = new List<string>(){
        "AttackSpeed","MovementSpeed","Health","Ammo", "ReloadSpeed",
    };

    private List<string> weapons = new List<string>(){
        "Gun", "Shotgun",
    };


    private List<GameObject> generatedUpgrades = new List<GameObject>();

    public static UpgradeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UpgradeManager is NULL");
            }
            return _instance;
        }
    }

    private void Awake()
    {
    }

    // public void GenerateWeapons()
    // {
    //     Queue<int> positionsX = new Queue<int>(new List<int>() { -550, 0, 550 });

    //     // Generate the Upgrades
    //     for (int i = 0; i < weapons.Count; i++)
    //     {
    //         GameObject canvas = GameObject.Find("UpgradeCardCanvas");
    //         Vector3 weaponPosition = new Vector3(canvas.transform.position.x + positionsX.Dequeue(), canvas.transform.position.y, canvas.transform.position.z);

    //         GameObject weaponChoice = upgradePrefab;
    //         weaponChoice.name = weapons[i];
    //         weaponChoice.GetComponentInChildren<TMP_Text>().text = upgradeDescription[weaponChoice.name];

    //         generatedUpgrades.Add(Instantiate(weaponChoice, weaponPosition, Quaternion.identity, canvas.transform));
    //     }
    // }

    public void GenerateUpgrades()
    {
        Queue<int> positionsX = new Queue<int>(new List<int>() { -550, 0, 550 });
        List<string> upgrades = new List<string>();
        
        for (int i = 0; i < positionsX.Count; i++)
        {
            string upgradeName = upgradeNames2[Range(0, upgradeNames2.Count)];
            while (upgrades.Contains(upgradeName))
            {
                upgradeName = upgradeNames2[Range(0, upgradeNames2.Count)];
            }
            upgrades.Add(upgradeName); 
        }

        // Generate the Upgrades
        for (int i = 0; i < upgrades.Count; i++)
        {
            GameObject canvas = GameObject.Find("UpgradeCardCanvas");
            Vector3 upgradePosition = new Vector3(canvas.transform.position.x + positionsX.Dequeue(), canvas.transform.position.y, canvas.transform.position.z);

            GameObject upgrade = upgradePrefab;
            upgrade.name = upgrades[i];
            Sprite upgradeSprite = Resources.Load<Sprite>("Sprites/" + upgradeImage[upgrade.name]);
            upgrade.GetComponent<Image>().sprite = upgradeSprite;
            upgrade.GetComponentInChildren<TMP_Text>().text = upgradeDescription[upgrade.name];

            generatedUpgrades.Add(Instantiate(upgrade, upgradePosition, Quaternion.identity, canvas.transform));
        }
    }

    public void DestroyUpgrades()
    {
        for (int i = 0; i < generatedUpgrades.Count; i++)
        {
            Destroy(generatedUpgrades[i]);
        }
    }

    public void UpgradeChosen(string upgradeName)
    {
        upgradeName = upgradeName.Substring(0, upgradeName.IndexOf('('));

        switch (upgradeName)
        {
            case "AttackSpeed":
                GameManager.Instance.playerAttackDelay *= upgradeValues[upgradeName];
                break;
            case "MovementSpeed":
                GameManager.Instance.playerMovementSpeed *= upgradeValues[upgradeName];
                GameManager.Instance.playerRotationSpeed *= upgradeValues[upgradeName];
                break;
            case "Health":
                GameManager.Instance.playerMaxHealth = Mathf.Round(GameManager.Instance.playerMaxHealth * upgradeValues[upgradeName]);
                break;
            case "Ammo":
                GameManager.Instance.playerMaxAmmo += upgradeValues[upgradeName];
                break;
            case "ReloadSpeed":
                GameManager.Instance.playerReloadSpeed *= upgradeValues[upgradeName];
                break;
        }
        GameManager.Instance.playerHealth = GameManager.Instance.playerMaxHealth;
        GameManager.Instance.playerAmmo = GameManager.Instance.playerMaxAmmo;
    }
}

