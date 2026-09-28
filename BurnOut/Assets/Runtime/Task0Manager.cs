using System.Collections;
using System.Collections.Generic;
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
    private List<Ingredient> neededIngredients = new List<Ingredient>();

    private void Awake()
    {
        // Add the ingredient names to the name array and save their starting positions
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredientNames[i] = ingredients[i].name;
        }

        neededIngredients.Clear();
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
        // Reset all the ingredients to their start position
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i].GetComponent<Ingredient>().ResetPosition();
        }
    }

    public void CheckIngredient(string ingredientName)
    {
        // Compare the name of the checked ingredient to the list of ingredients. If name is not present, reset the ingredient's position. 
    }

    private void SetList()
    {
        // Randomly take 4 ingredients and put them in the needed ingredient list
    }

    public void SwitchToTask()
    {
        taskUI.SetActive(true);
    }

    public void SwitchFromTask()
    {
        taskUI.SetActive(false);
    }

    public void MixPostion()
    {

    }
}
