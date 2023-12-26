using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load the game scene when the start button is pressed
        SceneManager.LoadScene("SampleScene");
        Debug.Log("Start The Game");
    }

    public void Instruction()
    {
        // Load the game scene when the start button is pressed
        SceneManager.LoadScene("Instruction");
    }

    public void QuitGame()
    {
        // Quit the game when the quit button is pressed
        // Note: This will only work in a standalone build, not in the Unity Editor
        Application.Quit();
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }
}
