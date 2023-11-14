using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Upgrades : MonoBehaviour
{
    //define list of possible upgrade cards
    Upgrade[] UpgradesList = new Upgrade[]
    {
        new Upgrade{Name = "Extra Cannon", Description = "Adds an additional cannon (Max Ammo +1)"},
        new Upgrade{Name = "Extra Deckhands", Description = "reload 10% faster"},
        new Upgrade{Name = "Stronger Hull", Description = "+25% max HP"},
        new Upgrade{Name = "Bigger Sails", Description = "increase Speed by 10%"},
        new Upgrade{Name = "Barrage", Description = " Fire A Barrage of 5 shots of the side of your Ship"},

    };
    [SerializeField] private Button Upgrade_button1;
    [SerializeField] private Button Upgrade_button2;
    [SerializeField] private Button Upgrade_button3;
    [SerializeField] private Button Upgrade_button4;

    [SerializeField] private Text Upgrade_DescriptionText1;
    [SerializeField] private Text Upgrade_DescriptionText2;
    [SerializeField] private Text Upgrade_DescriptionText3;
    [SerializeField] private Text Upgrade_DescriptionText4;

    // Start is called before the first frame update
    void Start()
    {
        ButtonsSet();
    }

    public void ButtonsSet(){
        List<int> availableUpgrades =new List<int>();
        for (int i = 0; i < UpgradesList.Length; i++)
        {
            availableUpgrades.Add(i);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public class Upgrade{
        public string Name{get;set;}
        public string Description{get;set;}


    }
}
