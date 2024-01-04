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

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "EXP") {
            GameManager.Instance.playerEXP++;
            // if (GameManager.Instance.playerEXP >= GameManager.Instance.levelsArray[(int) GameManager.Instance.playerLVL]) {
                if (GameManager.Instance.playerEXP >= GameManager.Instance.levels.Peek()) {
                    GameManager.Instance.levels.Dequeue();
                    GameManager.Instance.playerLVL++;
                    GameManager.Instance.playerEXP = 0;         //vllt geht beim lvlup überschüssige ep verloren
                    playerLevelUpEvent.Invoke();
                    Debug.Log("LVL: "+ GameManager.Instance.levels.Peek());
               // if (GameManager.Instance.levels.Count != 0) {                 //habs auf Array umgestellt, hoffe es gibt keine bugs 
                //    GameManager.Instance.levels.Dequeue();
              //  }
            }
        }
    }

}
