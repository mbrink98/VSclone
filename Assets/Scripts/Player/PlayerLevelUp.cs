using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerLevelUp : MonoBehaviour
{
    public UnityEvent playerLevelUpEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        GameManager.Instance.playerEXP = 0f;
        GameManager.Instance.playerLVL = 1f;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "EXP") {
            GameManager.Instance.playerEXP++;
            if (GameManager.Instance.playerEXP >= GameManager.Instance.levelsArray[(int) GameManager.Instance.playerLVL]) {
                GameManager.Instance.playerLVL++;
                GameManager.Instance.playerEXP = 0;         //vllt geht beim lvlup überschüssige ep verloren
                playerLevelUpEvent.Invoke();
               // if (GameManager.Instance.levels.Count != 0) {                 //habs auf Array umgestellt, hoffe es gibt keine bugs 
                //    GameManager.Instance.levels.Dequeue();
              //  }
            }
        }
    }

}
