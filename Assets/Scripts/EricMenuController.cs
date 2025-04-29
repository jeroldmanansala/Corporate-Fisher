using UnityEngine;
using UnityEngine.SceneManagement;

public class EricMenuController : MonoBehaviour {
    public void PlayGame() {
        SceneManager.LoadScene("SampleScene");
    }
    public void ShowInstructions() {
        SceneManager.LoadScene("Instructions");
    }
    public void BackToMenu() {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame() {
        Application.Quit();
    }
}
