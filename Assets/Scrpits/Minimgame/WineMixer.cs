using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


public class WineMixer : MonoBehaviour
{
    public DropZone dropZone;
    public TextMeshProUGUI resultText;

    void Start()
    {
        resultText.gameObject.SetActive(false); // Hide the result text initially
    }

    public void MixWine()
    {
        Debug.Log(" Mixing ingredients...");

        if (dropZone == null)
        {
            Debug.LogError(" dropZone is not assigned!");
            return;
        }

        Dictionary<string, int> ingredients = dropZone.GetFinalIngredients();

        if (ingredients == null || ingredients.Count == 0)
        {
            Debug.LogWarning(" No ingredients were added! Try adding ingredients before mixing.");
            return;
        }

        foreach (var ingredient in ingredients)
        {
            Debug.Log($" Ingredient: {ingredient.Key}, Amount: {ingredient.Value}");
        }

        string result = CheckWineQuality(ingredients);
        Debug.Log($" Wine Quality: {result}");


        // Show result in UI
        if (resultText != null)
        {
            resultText.text = result;
            resultText.gameObject.SetActive(true); // Show the result text
        }
        else
        {
            Debug.LogError(" resultText is not assigned in the Inspector!");
        }
    }


    string CheckWineQuality(Dictionary<string, int> ingredients)
    {
        int grapes = ingredients.ContainsKey("Grapes") ? ingredients["Grapes"] : 0;
        int yeast = ingredients.ContainsKey("Yeast") ? ingredients["Yeast"] : 0;
        int honey = ingredients.ContainsKey("Honey") ? ingredients["Honey"] : 0;
        int water = ingredients.ContainsKey("Water") ? ingredients["Water"] : 0;
        int herbs = ingredients.ContainsKey("Herbs") ? ingredients["Herbs"] : 0;

        if (grapes == 3 && yeast == 2 && honey == 1 && water == 1 && herbs == 1)
        {
            return "Perfect Wine! ";
        }
        else if (water >= 3)
        {
            return "The wine is too weak. ";
        }
        else if (water == 0 && yeast >= 3)
        {
            return "The wine is too strong! ";
        }
        else if (yeast >= 3 || herbs >= 3)
        {
            return "The wine is overly fermented and tastes strange. ";
        }
        else
        {
            return "The wine is unbalanced. Try again! ";
        }
    }
}
