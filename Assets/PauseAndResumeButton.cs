using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PauseAndResumeButton : MonoBehaviour
{
    public UnityEvent gamePausedEvent;

    public UnityEvent gameResumedEvent;

    [SerializeField] private GameObject buttonTextObject;

    private TextMeshProUGUI tmpComponent;

    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        tmpComponent = buttonTextObject.GetComponent<TextMeshProUGUI>();
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")){
            OnButtonClicked();
        }
    }

    public void OnButtonClicked(){
        if (paused){
            resume();
        } else {
            pause();
        }
        paused = !paused;
        GameManager.Instance.gameIsPaused = paused;
    }

    private void pause(){
        gamePausedEvent.Invoke();
        tmpComponent.text = "Resume (ESC)";
    }

    private void resume(){
        gameResumedEvent.Invoke();
        tmpComponent.text = "Pause (ESC)";
    }
}
