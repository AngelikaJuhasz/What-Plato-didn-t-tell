using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton

    private Dictionary<string, bool> playerChoices = new Dictionary<string, bool>();

    private void Awake()
    {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                // Load all saved choices at start
                LoadChoices();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        //Load saved choices from PlayerPrefs
        private void LoadChoices()
        {
            string[] keys = { "HelpedNPC" }; // Add more choice keys here if needed

            foreach (string key in keys)
            {
                if (PlayerPrefs.HasKey(key))
                {
                    playerChoices[key] = PlayerPrefs.GetInt(key) == 1;
                    Debug.Log($"Loaded choice: {key} = {playerChoices[key]}");
                }
            }

        }

        public void SetChoice(string choiceKey, bool value)
    {
        playerChoices[choiceKey] = value;
        Debug.Log($"Choice saved: {choiceKey} = {value}");
    }

    public bool GetChoice(string choiceKey)
    {
        bool exists = playerChoices.ContainsKey(choiceKey);
        Debug.Log($"Checking choice: {choiceKey}, Exists: {exists}, Value: {(exists ? playerChoices[choiceKey] : false)}");
        return exists && playerChoices[choiceKey];
    }
}
