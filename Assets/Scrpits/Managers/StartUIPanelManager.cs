using UnityEngine;

public class StartUIPanelManager : MonoBehaviour
{
    public GameObject startUIPanel; // Assign your start UI panel in the Inspector.
    private static bool startPanelShown = false; // This flag persists across scene loads.

    void Start()
    {
        if (startPanelShown)
        {
            // The panel has been shown before, so hide it.
            startUIPanel.SetActive(false);
        }
        else
        {
            // This is the first time, show the panel.
            startUIPanel.SetActive(true);
            startPanelShown = true;
        }
    }

    // Optionally, create a public method to hide the panel manually (e.g., when the player clicks "Start Game").
    public void HideStartPanel()
    {
        startUIPanel.SetActive(false);
    }
}
