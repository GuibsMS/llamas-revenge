using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
