using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeLibrary
{
    private int diffRating;

    //private string[] ingredients = new string[] {"Rosemary", "Jar Of Blood", "Salt", "Rose Petals", "Bay Leaves", "Crystals", "Cinnamon Stick", "Lavender" };

    private string[][] easyPotions = new string[4][]
    {
        new string[] { "Crystals", "Rose Petals" },
        new string[] { "Bay Leaves", "Rosemary" },
        new string[] { "Cinnamon Stick", "Salt" },
        new string[] { "Jar Of Blood", "Lavender" }
    };

    private string[][] mediumPotions = new string[6][]
    {
        new string[] { "Rosemary", "Salt", "Jar Of Blood" },
        new string[] { "Lavender", "Rose Petals", "Cinnamon Stick" },
        new string[] { "Rosemary", "Crystals", "Jar Of Blood" },
        new string[] { "Cinnamon Stick", "Crystals", "Rose Petals" },
        new string[] { "Cinnamon Stick", "Bay Leaves", "Rosemary" },
        new string[] { "Lavender", "Rosemary", "Salt" }
    };

    private string[][] hardPotions = new string[4][]
    {
        new string[] { "Crystals", "Jar Of Blood", "Lavender", "Salt" },
        new string[] { "Salt", "Rose Petals", "Bay Leaves", "Rosemary" },
        new string[] { "Bay Leaves", "Cinnamon Stick", "Crystals", "Rose Petals" },
        new string[] { "Bay Leaves", "Lavender", "Salt", "Jar Of Blood" }
    };

    public RecipeLibrary(int rating)
    {
        diffRating = rating;
    }

    public string[] GetRecipe()
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
            default:
                return null;
        }
    }
}
