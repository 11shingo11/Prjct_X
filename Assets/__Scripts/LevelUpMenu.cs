using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;
using UnityEditor;
using Unity.VisualScripting;
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
    private List<Action> currentSkills;
    private int fireTier = 1;
    private int explodeTier = 1;
    public bool explTru = false;
    private string description;
    private bool isChoosingSkill = true;

    protected Animator anim;
    private string skillText;

    private void Start()
    {
        availableSkills = new List<Action>() {
            IncreaseHealth,
            IncreaseArmor,
            IncreaseDamage,
            UpgradeToExplode,
            MoveSpeedUpgrade,
            ManaRecovery,
            IncreaseMana
        };
        
        Choose1.onClick.AddListener(() => OnSkillSelected(1));
        Choose2.onClick.AddListener(() => OnSkillSelected(2));
        Choose3.onClick.AddListener(() => OnSkillSelected(3));
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        string skillBonusText = "";
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
                            description = " With this perk, you'll feel like you have an extra layer of bubble wrap around you," +
                                " protecting you from all of life's little bumps and bruises." +
                                " So go ahead, eat that extra slice of pizza, because with this perk, your body can handle anything!";
                            skillBonusText = "+5 Health";
                            break;
                        case "IncreaseArmor":
                            skillText = "Stone Skin";
                            description = "Introducing the latest in fashion trends: the Stone Skin! This stylish accessory is a must-have for any adventurer who wants to stay protected while looking their best." +
                                " Made from the finest minerals and enchanted by powerful wizards," +
                                " the Stone Skin adds an extra layer of defense that even the toughest enemies will envy." +
                                " Plus, it's a great conversation starter at parties. Trust us, with the Stone Skin on your side, you'll feel like a rockstar!";
                            skillBonusText = "+10% to your armour";
                            break;
                        case "IncreaseDamage":
                            skillText = "Damage Tier " + fireTier.ToString();
                            description = "Unleash the fury of the fire! Your fireballs now deal 1 extra damage. That's right, one whole extra damage. Watch out, enemies!" +
                                "Your fireballs now travel 15% faster, making you the Usain Bolt of spellcasting. Zoom, zoom!" +
                                "The good news has ended. Mana cost increase for 12%, sorry, you've got to pay for all this stuff.";
                            skillBonusText = "+1 Damage\n+15% fireball speed\n+12% mana cost";
                            break;
                        case "UpgradeToExplode":
                            skillText = "Explode Tier " + explodeTier.ToString();
                            description = "Introducing the explosive upgrade for Fireball! Now you can light up the whole room with just one shot!" +
                                " Enemies will be begging for mercy as they feel the heat from the radius damage. And the best part?" +
                                " You can do it all from a safe distance, without getting your hands dirty. So sit back, relax, and enjoy the fireworks!" +
                                "Causes a radius explosion, damaging all enemies within a 6-meter range for 15% of the fireball's damage.";
                            skillBonusText = "+1 Range\n +15% mana cost";
                            break;
                        case "MoveSpeedUpgrade":
                            skillText = "Fast Legs";
                            description = "Congratulations! You've could earn the \"Fast Legs\" perk! " +
                                "Your legs are now officially the Usain Bolt of the fantasy world." +
                                " You'll move faster than a hungry dragon chasing a sheep. " +
                                "Just don't forget to stretch before running, or you might pull a muscle and end up hobbling slower than a snail on a Sunday stroll.";
                            skillBonusText = "+20% Move speed";
                            break;
                        case "ManaRecovery":
                            skillText = "Concentration";
                            description = "Feeling drained after casting spells?" +
                                " Not anymore! With the Concentration perk, you'll recover your mana faster than ever." +
                                " Say goodbye to those pesky moments when you're left defenseless. " +
                                "Keep your enemies at bay and your mana pool full with Concentration!";
                            skillBonusText = "+15% Mana recovery";
                            break;
                        case "IncreaseMana":
                            skillText = "Trained mind";
                            description = "Upgrade your mind with the Trained Mind perk and expand your mana pool by 15%!";
                            skillBonusText = "+15% to Maximum mana";
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
                            description = " With this perk, you'll feel like you have an extra layer of bubble wrap around you," +
                                " protecting you from all of life's little bumps and bruises." +
                                " So go ahead, eat that extra slice of pizza, because with this perk, your body can handle anything!";
                            skillBonusText = "+5 Health";
                            break;
                        case "IncreaseArmor":
                            skillText = "Stone Skin";
                            description = "Introducing the latest in fashion trends: the Stone Skin! This stylish accessory is a must-have for any adventurer who wants to stay protected while looking their best." +
                                " Made from the finest minerals and enchanted by powerful wizards," +
                                " the Stone Skin adds an extra layer of defense that even the toughest enemies will envy." +
                                " Plus, it's a great conversation starter at parties. Trust us, with the Stone Skin on your side, you'll feel like a rockstar!";
                            skillBonusText="+10% to your armour";
                            break;
                        case "IncreaseDamage":
                            skillText = "Damage Tier " + fireTier.ToString();
                            description = "Unleash the fury of the fire! Your fireballs now deal 1 extra damage. That's right, one whole extra damage. Watch out, enemies!" +
                                "Your fireballs now travel 15% faster, making you the Usain Bolt of spellcasting. Zoom, zoom!" +
                                "The good news has ended. Mana cost increase for 12%, sorry, you've got to pay for all this stuff.";
                            skillBonusText = "+1 Damage\n+15% fireball speed\n+12% mana cost";
                            break;
                        case "UpgradeToExplode":
                            skillText = "Explode Tier " + explodeTier.ToString();
                            description = "Introducing the explosive upgrade for Fireball! Now you can light up the whole room with just one shot!" +
                                " Enemies will be begging for mercy as they feel the heat from the radius damage. And the best part?" +
                                " You can do it all from a safe distance, without getting your hands dirty. So sit back, relax, and enjoy the fireworks!" +
                                "Causes a radius explosion, damaging all enemies within a 6-meter range for 15% of the fireball's damage.";
                            skillBonusText = "+1 Range\n +15% mana cost";
                            break;
                        case "MoveSpeedUpgrade":
                            skillText = "Fast Legs";
                            description = "Congratulations! You've could earn the \"Fast Legs\" perk! " +
                                "Your legs are now officially the Usain Bolt of the fantasy world." +
                                " You'll move faster than a hungry dragon chasing a sheep. " +
                                "Just don't forget to stretch before running, or you might pull a muscle and end up hobbling slower than a snail on a Sunday stroll.";
                            skillBonusText = "+20% Move speed";
                            break;
                        case "ManaRecovery":
                            skillText = "Concentration";
                            description = "Feeling drained after casting spells?" +
                                " Not anymore! With the Concentration perk, you'll recover your mana faster than ever." +
                                " Say goodbye to those pesky moments when you're left defenseless. " +
                                "Keep your enemies at bay and your mana pool full with Concentration!";
                            skillBonusText = "+15% Mana recovery";
                            break;
                        case "IncreaseMana":
                            skillText = "Trained mind";
                            description = "Upgrade your mind with the Trained Mind perk and expand your mana pool by 15%!";
                            skillBonusText = "+15% to Maximum mana";
                            break;
                    }
                    string chooseName = "Choose" + (i+1).ToString();                   
                    GameObject chooseObject = GameObject.Find(chooseName);
                    if (chooseObject != null)
                    {
                        Button chooseButton = chooseObject.GetComponent<Button>();
                        chooseButton.GetComponentInChildren<Text>().text = skillText;                       
                    }
                    string skillName = "Skill_"+(i+1).ToString();
                    GameObject skillObject = GameObject.Find(skillName);
                    if (skillObject != null)
                    {
                        skillObject.transform.Find("Description").GetComponent<Text>().text = description;
                        skillObject.transform.Find("SkillBonus").GetComponent<Text>().text = skillBonusText;
                    }
                }
            }
            Time.timeScale = 0.00001f;
            isChoosingSkill = true;
        }

    }

    private void OnSkillSelected(int skillIndex)
    {
        Action selectedSkill = currentSkills[skillIndex-1];
        
        Time.timeScale = 1f;
        if (isChoosingSkill)
        {
            selectedSkill.Invoke();
            // Обработка выбранного скилла

            // Скрыть меню выбора
            anim.SetTrigger("choose");
            isChoosingSkill = false;
        }

            

        
        
    }

    private void IncreaseHealth()
    {
        player.maxHitpoint += 5;
        player.hitpoint = player.maxHitpoint;

    }

    private void IncreaseArmor()
    {
        player.armor*=1.1f;
    }

    private void IncreaseDamage()
    {
        GameManager.instance.fire.fireballDamage += 1;
        GameManager.instance.fire.fireballSpeed *= 1.15f;
        GameManager.instance.fire.manacost *= 1.12f;

        fireTier++;
        if(fireTier>5) 
            availableSkills.RemoveAll(skill => skill.Method.Name == "IncreaseDamage");             
    }

    private void UpgradeToExplode()
    {
        explTru = true;
        explodeTier += 1;
        GameManager.instance.fire.explosionRadius += 1;
        GameManager.instance.fire.manacost *= 1.15f;
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

    private void MoveSpeedUpgrade()
    {
       player.moveSpeed *= 1.20f;
    }

    private void ManaRecovery()
    {
       player.manaRecovery *= 1.15f;
    }

    private void IncreaseMana()
    {
        player.maxMana *= 1.15f;
        player.currMana = player.maxMana;
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
