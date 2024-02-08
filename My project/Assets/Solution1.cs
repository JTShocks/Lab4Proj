using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solution1 : MonoBehaviour
{
    //Input variables
    public bool isHillDwarf, hasToughFeat, isRollingHealth;
    public string characterName, characterClass;
    public int characterLevel, characterConScore;

    int playerTotalHealth;
    int dwarfMod = 0, toughMod = 0;


    public Dictionary<string, int> classDieSizes = new Dictionary<string, int>
    {
        {"artificer", 8},{"barbarian", 12},{"bard", 8},{"cleric", 8},{"druid", 8},{"fighter", 10},{"monk", 10},{"paladin", 10},{"ranger", 10},{"rogue", 8},{"sorcerer", 6},{"warlock", 8},{"wizard", 6}
    };



    private void Start()
    {
        if (isHillDwarf)
        {
            dwarfMod = 1;
        }
        if(hasToughFeat)
        {
            toughMod = 1;
        }

        CalculatePlayerHealth();
    }

    int CalculateConMod(int conScore)
    {
        //Find the con modifier based on the initial score
        Debug.Log((conScore - 10) / 2);
        return (conScore-10)/2;
    }

    int RollHealthDice(string playerClass)
    {
        int classDieSize = classDieSizes[playerClass];
        int healthFromDieRoll = Random.Range(1, classDieSize);
        return healthFromDieRoll;
    }

    void CalculatePlayerHealth()
    {
        int healthFromDieRoll = 0;

        for(int i = 1; i <= characterLevel; i++)
        {

            if (isRollingHealth)
            {
                healthFromDieRoll = RollHealthDice(characterClass); //Generate a random roll
            }
            else
            {
                healthFromDieRoll = (1 + classDieSizes[characterClass]) / 2; //Take the average of the die size
            }

            if (i == 1) //On the first iteration, add the starting level amount (dieSize + conMod)
            {
                playerTotalHealth += classDieSizes[characterClass] + CalculateConMod(characterConScore) + dwarfMod + toughMod;
            }
            else{
                playerTotalHealth += healthFromDieRoll + CalculateConMod(characterConScore) + dwarfMod + toughMod;
            }
            Debug.LogFormat("Player health at level {0} is {1}", i, playerTotalHealth);

        }

    }



}
