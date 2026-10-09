using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Task0Manager : MonoBehaviour, ITask
{
    // Public UI elements
    public GameObject mixButton;
    public GameObject finishButton;
    public TextMeshProUGUI ingredientList;
    public GameObject taskUI;

    // Public Audio
    public AudioSource keyTaskAudio;
    public AudioClip successfulBrew;
    public AudioClip failedBrew;

    // Candle meter object
    public Candle candle;

    // Array of ingredient objects currently in the scene
    [SerializeField]
    private GameObject[] ingredients;

    // List of needed ingredients
    private List<string> neededIngredients = new List<string>();

    // Recipe Library script
    private RecipeLibrary recipeLibrary;

    // Game Manager script
    private GameManager gameManager;

    // Difficulty setting index value
    private int diffRating;

    // Number of correct ingredients in the cauldron
    private int correctIngredients = 0;

    private void Awake()
    {
        // Clear the needed ingredients list
        neededIngredients.Clear();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   // Get the game manager script
        diffRating = (int)gameManager.difficulty;                                   // Get the index value of the difficulty setting
        recipeLibrary = GetComponent<RecipeLibrary>();                              // Get the recipe library script
        
        // Set the mix and finish buttons as inactive
        mixButton.SetActive(false);
        finishButton.SetActive(false);

        // Set everything to default layer
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i].layer = LayerMask.NameToLayer("Default");
        }
    }

    public void ResetTask()
    {
        // Reset all the ingredients to their start positions
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i].GetComponent<Ingredient>().ResetPosition();
            ingredients[i].layer = LayerMask.NameToLayer("Default");
        }

        // Reset the correct ingredients counter
        correctIngredients = 0;
    }

    public void CheckIngredient(GameObject ingredient)
    {
        // Compare the name of the checked ingredient to the list of ingredients. If name is not present, reset the ingredient's position. 
        bool matchFound = false;

        for (int i = 0; i < neededIngredients.Count; i++)
        {
            if (ingredient.name == neededIngredients[i])    // If the ingredient matches one in the needed list, remove it from the list
            {
                matchFound = true;
                neededIngredients.Remove(neededIngredients[i]);
                UpdateListText();
                break;
            }
        }

        if (matchFound)     // If a match is found, increment the correct ingredients counter
        {
            correctIngredients++;
        }
        else                // If no match is found, return the ingredient to the cauldron
        {
            ingredient.GetComponent<Ingredient>().ResetPosition();
            //keyTaskAudio.PlayOneShot(failedBrew); JACOB: plays "PotionFailSound" if ingredient is incorrect
        }

        if (correctIngredients == (diffRating + 1))   // Once all ingredients are added, make the mix button visible
        {
            mixButton.SetActive(true);
        }
    }

    private void SetList()
    {
        // Clear the needed ingredients list
        neededIngredients.Clear();

        // Get a random list of ingredients from the recipe library, based on the difficulty
        neededIngredients = recipeLibrary.GetRecipe(diffRating).ToList();

        if (neededIngredients == null)
            return;

        // Update the list text as shown in game
        UpdateListText();
    }

    private void UpdateListText()
    {
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
        //keyTaskAudio.Play(); JACOB: Plays background bubbling which is sloted into audio source. Not "PlayOneShot" in order to enable proper looping.
    }

    public void SwitchFromTask()
    {
        taskUI.SetActive(false);
        //keyTaskAudio.Pause(); JACOB: Plays background bubbling which is sloted into audio source. Not "PlayOneShot" in order to enable proper looping.
    }

    public void MixPotion()
    {
       StartCoroutine(PotionMixWait());
       mixButton.SetActive(false);
       //keyTaskAudio.PlayOneShot(successfulBrew); JACOB: plays "PototionSucessSound" once potion mixing begins
    }

    private IEnumerator PotionMixWait()
    {
        // Wait a few seconds for the potion to finish mixing (should be made longer)
        float waitTime = 3f;
        yield return new WaitForSeconds(waitTime);
        finishButton.SetActive(true);
    }

    public void FinishTask()
    {
        // Put the candle out of danger
        candle.PutOutOfDanger();

        // Disable the finish and mix buttons
        finishButton.SetActive(false);
        mixButton.SetActive(false);

        // Reset the task back to it's idle state
        ResetTask();
    }

    public void StartTask()
    {
        SetList();
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i].layer = LayerMask.NameToLayer("Draggable");
        }
    }
}
