using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solution2 : MonoBehaviour
{
    //Input variables
    public bool isHillDwarf, hasToughFeat, isRollingHealth;
    public string characterName, characterClass;
    public int characterLevel, characterConScore;

    int playerTotalHealth;
    int dwarfMod = 0, toughMod = 0;

    public class CharacterData
    {
        public string name;
        public int level;
        public int health;
        public string characterClass;
        public int conMod;
        public CharacterData(string name, int level, int health, string characterClass)
        {
            this.name = name;
            this.level = level;
            this.health = health;
            this.characterClass = characterClass;
        }



    };

    CharacterData newCharacter;


    public Dictionary<string, int> classDieSizes = new Dictionary<string, int>
    {
        {"artificer", 8},{"barbarian", 12},{"bard", 8},{"cleric", 8},{"druid", 8},{"fighter", 10},{"monk", 10},{"paladin", 10},{"ranger", 10},{"rogue", 8},{"sorcerer", 6},{"warlock", 8},{"wizard", 6}
    };



    private void Start()
    {
        newCharacter = new CharacterData(characterName, characterLevel, playerTotalHealth, characterClass);

        if (isHillDwarf)
        {
            dwarfMod = 1;
        }
        if(hasToughFeat)
        {
            toughMod = 1;
        }

        CalculatePlayerHealth(newCharacter);
        Debug.LogFormat("Character Name: {0}, Character Class {1} \n Character Level: {2}, Character ConMod {3} \n Character Health: {4}", newCharacter.name, newCharacter.characterClass, newCharacter.level, newCharacter.conMod, newCharacter.health);
    }

    int CalculateConMod(int conScore)
    {
        //Find the con modifier based on the initial score
        Debug.Log((conScore - 10) / 2);
        switch (conScore)
        {
            case 9: return -1;
            case 7: return -2;
            case 5: return -3;
            case 3: return -4;
            case 1: return -5;
        }
        return (conScore-10)/2;
    }

    int RollHealthDice(string playerClass)
    {
        int classDieSize = classDieSizes[playerClass];
        int healthFromDieRoll = Random.Range(1, classDieSize);
        return healthFromDieRoll;
    }

    void CalculatePlayerHealth(CharacterData newCharacter)
    {
        int healthFromDieRoll = 0;
        newCharacter.conMod = CalculateConMod(characterConScore);

        for(int i = 1; i <= characterLevel; i++)
        {

            if (isRollingHealth)
            {
                healthFromDieRoll = RollHealthDice(newCharacter.characterClass); //Generate a random roll
            }
            else
            {
                healthFromDieRoll = (1 + classDieSizes[newCharacter.characterClass]) / 2; //Take the average of the die size
            }

            if (i == 1) //On the first iteration, add the starting level amount (dieSize + conMod)
            {
                newCharacter.health += classDieSizes[newCharacter.characterClass] + newCharacter.conMod + dwarfMod + toughMod;
            }
            else{
                newCharacter.health += healthFromDieRoll + newCharacter.conMod + dwarfMod + toughMod;
            }
            Debug.LogFormat("Player health at level {0} is {1}", i, newCharacter.health);

        }

    }



}
