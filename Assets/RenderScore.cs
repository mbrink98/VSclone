using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RenderScore : MonoBehaviour
{
    void OnEnable(){
        int score = GameManager.Instance.score;
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        tmp.SetText("Your score: {0}", score * 10);
    }
}
