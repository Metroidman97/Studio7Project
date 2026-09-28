using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Task0Manager : MonoBehaviour
{
    // Public UI elements
    public Button resetButton;
    public TextMeshProUGUI ingredientList;

    // Array of ingredient objects currently in the scene
    [SerializeField]
    private GameObject[] ingredients;

    // Array of ingredient names
    private string[] ingredientNames = new string[6];

    // Array of ingredient start positions
    private Vector2[] ingredientStartPositions = new Vector2[6];

    private void Awake()
    {
        // Add the ingredient names to the name array and save their starting positions
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredientNames[i] = ingredients[i].name;
            ingredientStartPositions[i] = ingredients[i].transform.position;
        }
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
            ingredients[i].transform.position = ingredientStartPositions[i];
        }
    }
}
