using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Example: a dictionary to track choice values
    private Dictionary<string, int> choices = new Dictionary<string, int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RecordChoice(string choiceKey, int value)
    {
        if (choices.ContainsKey(choiceKey))
            choices[choiceKey] += value;
        else
            choices[choiceKey] = value;
    }

    public int GetChoiceValue(string choiceKey)
    {
        return choices.ContainsKey(choiceKey) ? choices[choiceKey] : 0;
    }

    public void CheckEnding()
    {
        if (GetChoiceValue("morality") > 5)
        {
            Debug.Log("Good ending");
            // Trigger the good ending (e.g., load a scene or show UI)
        }
        else
        {
            Debug.Log("Bad ending");
            // Trigger the bad ending
        }
    }
}
