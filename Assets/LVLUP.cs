using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LVLUP : MonoBehaviour
{
    public UnityEvent playerLevelUpEvent;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LvlUP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LvlUP() {
        playerLevelUpEvent.Invoke();
    }
}
