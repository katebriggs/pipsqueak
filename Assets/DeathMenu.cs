using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void GoToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void Continue() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
