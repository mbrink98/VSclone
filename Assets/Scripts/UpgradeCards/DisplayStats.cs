using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayStats : MonoBehaviour
{
    public bool showStats
    {
        get;
        private set;
    }

    // Start is called before the first frame update
    void Start()
    {
        showStats = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3)){
            showStats = !showStats;
        }
        if (showStats){
            GetComponent<TMP_Text>().text = 
                "Game stats (F3)\n" +
                $"Level:{GameManager.Instance.playerLVL}\n" +
                $"EXP:{GameManager.Instance.playerEXP}\n" +
                $"Max HP:{GameManager.Instance.playerMaxHealth}\n" +
                $"HP:{GameManager.Instance.playerHealth}\n" +
                $"MovementSpeed:{GameManager.Instance.playerMovementSpeed}\n" +
                $"Ammo:{GameManager.Instance.playerAmmo}\n" +
                $"ReloadLength:{GameManager.Instance.playerReloadSpeed}\n" +
                $"AttackDelay:{GameManager.Instance.playerAttackDelay}\n" +
                $"EnemyMaxHealth:{GameManager.Instance.enemyMaxHealth}\n"
            ;
        } else {
            GetComponent<TMP_Text>().text = "";
        }
    }
}
