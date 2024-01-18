using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameRate : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public GameObject statDisplay;

    private DisplayStats statsScript;

    private float timeSinceUpdate;

    private int frameCount;

    // Start is called before the first frame update
    void Start()
    {
        statsScript = statDisplay.GetComponent<DisplayStats>();
        timeSinceUpdate = 0.0f;
        frameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (statsScript.showStats){
            frameCount++;
            timeSinceUpdate += Time.unscaledDeltaTime;
            if (timeSinceUpdate >= .5f) {
                textMeshPro.SetText("{0}", frameCount*2);
                timeSinceUpdate -= .5f;
                frameCount = 0;
            }
        } else {
            textMeshPro.text = "";
            timeSinceUpdate = 0.0f;
            frameCount = 0;
        }
    }
}
