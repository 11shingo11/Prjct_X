using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
//using UnityEngine.UIElements;

public class LevelUpMenu : MonoBehaviour
{
    [SerializeField] private Button Choose1;
    [SerializeField] private Button Choose2;
    [SerializeField] private Button Choose3;
    [SerializeField] private GameObject skillSelectionMenu;
    [SerializeField] private Player player;
    [SerializeField] private FireMagic fireball;
    private List<Action> availableSkills;
    private List<Action> updateSkills = new List<Action>();
    private List<Action> currentSkills;
    private int fireTier = 1;
    private int explodeTier = 1;
    public bool explTru = false;

    protected Animator anim;
    private string skillText;

    private void Start()
    {
        availableSkills = new List<Action>() {
            IncreaseHealth,
            IncreaseArmor,
            IncreaseDamage,
            UpgradeToExplode
        };
        
        Choose1.onClick.AddListener(() => OnSkillSelected(1));
        Choose2.onClick.AddListener(() => OnSkillSelected(2));
        Choose3.onClick.AddListener(() => OnSkillSelected(3));
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player.leveling)
        {
            anim.SetTrigger("LvlUp");
            SetSkillList();            
            if (availableSkills.Count <= 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    switch (currentSkills[i].Method.Name)
                    {
                        case "IncreaseHealth":
                            skillText = "Getting Bigger";
                            break;
                        case "IncreaseArmor":
                            skillText = "Stone Skin";
                            break;
                        case "IncreaseDamage":
                            skillText = "Damage Tier " + fireTier.ToString();
                            break;
                        case "UpgradeToExplode":
                            skillText = "Explode Tier " + explodeTier.ToString();
                            break;
                    }
                    string chooseName = "Choose" + (i+1).ToString();                   
                    GameObject chooseObject = GameObject.Find(chooseName);
                    if (chooseObject != null)
                    {
                        Button chooseButton = chooseObject.GetComponent<Button>();
                        chooseButton.GetComponentInChildren<Text>().text = skillText;
                        Choose3.GetComponentInChildren<Text>().text = "";
                    }
                    
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    switch (currentSkills[i].Method.Name)
                    {
                        case "IncreaseHealth":
                            skillText = "Getting Bigger";
                            break;
                        case "IncreaseArmor":
                            skillText = "Stone Skin";
                            break;
                        case "IncreaseDamage":
                            skillText = "Damage Tier " + fireTier.ToString();
                            break;
                        case "UpgradeToExplode":
                            skillText = "Explode Tier " + explodeTier.ToString();
                            break;
                    }
                    string chooseName = "Choose" + (i+1).ToString();                   
                    GameObject chooseObject = GameObject.Find(chooseName);
                    if (chooseObject != null)
                    {
                        Button chooseButton = chooseObject.GetComponent<Button>();
                        chooseButton.GetComponentInChildren<Text>().text = skillText;
                    }
                }
            }
            Time.timeScale = 0.00001f;
        }

    }

    private void OnSkillSelected(int skillIndex)
    {
        Action selectedSkill = currentSkills[skillIndex-1];

        selectedSkill.Invoke();
        // Обработка выбранного скилла
        Time.timeScale = 1f;
        // Скрыть меню выбора
        anim.SetTrigger("choose");
    }

    private void IncreaseHealth()
    {
        player.maxHitpoint += 5;
    }

    private void IncreaseArmor()
    {
        player.armor++;
    }

    private void IncreaseDamage()
    {
        GameManager.instance.fire.fireballDamage += 1;
        fireTier++;
        if(fireTier>5) 
            availableSkills.RemoveAll(skill => skill.Method.Name == "IncreaseDamage");             
    }

    private void UpgradeToExplode()
    {
        explTru = true;
        explodeTier += 1;
        GameManager.instance.fire.explosionRadius += 1;
        switch (explodeTier)
        {
            case 1:
                GameManager.instance.fire.percentOfExpload += 1.5f;
                break;
            case 2:
                GameManager.instance.fire.percentOfExpload += 3f;
                break;
            case 3:
                GameManager.instance.fire.percentOfExpload += 10f;
                break;
            case 4:
                GameManager.instance.fire.percentOfExpload += 15f;
                break;
            case 5:
                GameManager.instance.fire.percentOfExpload += 25f;
                break;
        
        }
        if (explodeTier > 5)
            availableSkills.RemoveAll(skill => skill.Method.Name == "UpgradeToExplode");

    }

    private void SetSkillList()
    {
        currentSkills = new List<Action>();
        for (int i = 0; i < availableSkills.Count; )
        {
            int index = UnityEngine.Random.Range(0, availableSkills.Count);
            if (!currentSkills.Contains(availableSkills[index]))
            {
                currentSkills.Add(availableSkills[index]);
                i++;
            }
        }
        string skillsList = string.Join(", ", currentSkills.Select(skill => skill.Method.Name).ToArray());
        //Debug.Log(skillsList);
    }
}
