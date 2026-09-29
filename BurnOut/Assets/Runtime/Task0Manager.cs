using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Task0Manager : MonoBehaviour
{
    // Public UI elements
    public Button resetButton;
    public TextMeshProUGUI ingredientList;
    public GameObject taskUI;

    // Array of ingredient objects currently in the scene
    [SerializeField]
    private GameObject[] ingredients;

    // Array of ingredient names
    private string[] ingredientNames = new string[8];

    // List of needed ingredients
    private List<string> neededIngredients = new List<string>();

    private RecipeLibrary recipeLibrary;

    private GameManager gameManager;
    private int diffRating;

    private void Awake()
    {
        // Add the ingredient names to the name array and save their starting positions
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredientNames[i] = ingredients[i].name;
        }

        neededIngredients.Clear();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        diffRating = (int)gameManager.difficulty;
        recipeLibrary = GetComponent<RecipeLibrary>();

        SetList();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetTask()
    {
        // Reset all the ingredients to their start positions
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i].GetComponent<Ingredient>().ResetPosition();
        }

        SetList();
    }

    public void CheckIngredient(string ingredientName)
    {
        // Compare the name of the checked ingredient to the list of ingredients. If name is not present, reset the ingredient's position. 
    }

    private void SetList()
    {
        neededIngredients.Clear();

        neededIngredients = recipeLibrary.GetRecipe(diffRating).ToList();

        if (neededIngredients == null)
            return;

        string listText = "Need:";

        for (int i = 0; i < neededIngredients.Count; i++)
        {
            listText += "\n" + neededIngredients[i];
        }

        ingredientList.text = listText;
    }

    public void SwitchToTask()
    {
        taskUI.SetActive(true);
    }

    public void SwitchFromTask()
    {
        taskUI.SetActive(false);
    }

    public void MixPotion()
    {

    }
}
