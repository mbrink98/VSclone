using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAmmoAsBars : MonoBehaviour
{
    public Image chargeBar;
    private int maxammo;
    private List<Image> Bars;
    private Color full;
    private Color empty;
    [SerializeField]
    public GameManager gameManager;    
    private RectTransform rt;
    private Transform barPos;
    private float addedHeight = 0f;
    void Start()
    {
        /**
        barPos = transform;
        barPos.position = transform.position;
        barPos.rotation = transform.rotation;
        barPos.localScale = transform.localScale;

        rt = GetComponent<RectTransform>();
        full= new Color(34,180,11,255);
        empty = new Color(22,19,19,255);
        maxammo = (int) gameManager.playerMaxAmmo;
        */
       // UpdateMaxAmmo(maxammo);
    }

    // Update is called once per frame
    void Update()
    {
        chargeBar.fillAmount = Mathf.Clamp(gameManager.playerAmmo/gameManager.playerMaxAmmo,0,1);
        Debug.Log("maxammo: "+ gameManager.playerMaxAmmo);
        Debug.Log("ammo: "+ gameManager.playerAmmo);
        /**
        int counter = 0;
        foreach (Image bar in Bars){
            Debug.Log("Drini");
            counter++;
            if(counter<gameManager.playerAmmo){
                bar.color = full;
            }
            else{
                bar.color = empty;
            }

        }
            */
        //setcolours = green
        //vllt nur colors switchen von green nach black und wenn maxammo changed ui changen
    }

    public void UpdateMaxAmmo(int newMaxAmmo){          //muss eingebunden werden in GamemangerLVLupevent
        float maxSpace = rt.rect.height;
        float newHeight = maxSpace/newMaxAmmo;   //imagehöhe ausrechnen durch teilen der höhe mit abständen durch new maxammo  
        Bars = new List<Image>();
        Debug.Log("asdasd");        //log die liste next
        for(int i= 0; i<newMaxAmmo;i++){
                            //vllt abstände durch raender der bars?
            //Bars[i] = instantiate 
            addedHeight += newHeight;
            barPos.position = transform.position + new Vector3(0,addedHeight,0);
            Image newImage = Instantiate(chargeBar, barPos);
            Bars.Add(newImage);
            //y coordinate der transforms growth um höhe der image, ausgerechnet durch teilen oben, +ABSTAND 
        }
         
        //instantiate ammobars nach maxammo
    }
}