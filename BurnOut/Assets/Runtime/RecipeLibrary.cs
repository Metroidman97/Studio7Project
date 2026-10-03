using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeLibrary : MonoBehaviour
{
    /*
    private string[] easyPotion0 = { "Rosemary" };
    private string[] easyPotion1 = { "Jar Of Blood" };
    private string[] easyPotion2 = { "Lavender" };

    private string[] mediumPotion0 = { "Salt", "Crystals" };
    private string[] mediumPostion1 = { "Bay Leaves", "Rose Petals" };
    private string[] mediumPostion2 = { "Cinnamon Stick", "Salt" };

    private string[] hardPotion0 = { "Rosemary", "Crystals", "Lavender" };
    private string[] hardPotion1 = { "Jar Of Blood", "Bay Leaves", "Salt" };
    private string[] hardPotion2 = { "Rose Petals", "Cinnamon Stick", "Lavender" };

    private string[] expertPotion0 = { "Crystals", "Bay Leaves", "Rose Petals", "Lavender" };
    private string[] expertPotion1 = { "Cinnamon Stick", "Jar Of Blood", "Rosemary", "Salt" };
    private string[] expertPotion2 = { "Bay Leaves", "Salt", "Crystals", "Jar Of Blood" };
    */

    private string[][] easyPotions = new string[3][]
    {
        new string[] { "Rosemary" },
        new string[] { "Jar Of Blood" },
        new string[] { "Lavender" }
    };

    private string[][] mediumPotions = new string[3][]
    {
        new string[] { "Salt", "Crystals" },
        new string[] { "Bay Leaves", "Rose Petals" },
        new string[] { "Cinnamon Stick", "Salt" }
    };

    private string[][] hardPotions = new string[3][]
    {
        new string[] { "Rosemary", "Crystals", "Lavender" },
        new string[] { "Jar Of Blood", "Bay Leaves", "Salt" },
        new string[] { "Rose Petals", "Cinnamon Stick", "Lavender" }
    };

    private string[][] expertPotion = new string[3][]
    {
        new string[] { "Crystals", "Bay Leaves", "Rose Petals", "Lavender" },
        new string[] { "Cinnamon Stick", "Jar Of Blood", "Rosemary", "Salt" },
        new string[] { "Bay Leaves", "Salt", "Crystals", "Jar Of Blood" }
    };

    public string[] GetRecipe(int diffRating)
    {
        int index;
        switch(diffRating)
        {
            case 0:
                index = Random.Range(0, easyPotions.Length);
                return easyPotions[index];
            case 1:
                index = Random.Range(0, mediumPotions.Length);
                return mediumPotions[index];
            case 2:
                index = Random.Range(0, hardPotions.Length);
                return hardPotions[index];
            case 3:
                index = Random.Range(0, expertPotion.Length);
                return expertPotion[index];
            default:
                return null;
        }
    }
}
