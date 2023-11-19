using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = 
            $"Level:{GameManager.Instance.playerLVL}\n" +
            $"EXP:{GameManager.Instance.playerEXP}\n" +
            $"Max HP:{GameManager.Instance.playerMaxHealth}\n" +
            $"HP:{GameManager.Instance.playerHealth}\n" +
            $"MovementSpeed:{GameManager.Instance.playerMovementSpeed}\n" +
            $"Ammo:{GameManager.Instance.playerAmmo}\n" +
            $"ReloadLength:{GameManager.Instance.playerReloadSpeed}\n" +
            $"AttackDelay:{GameManager.Instance.playerAttackDelay}\n"
        ;
    }
}
