using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EscapeMenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject instructionsPanel;
    private bool isMenuOpen = false;

    private void Start()
    {
        menuPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (instructionsPanel.activeSelf)
                CloseInstructions();
            else
                ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        menuPanel.SetActive(isMenuOpen);
        Time.timeScale = isMenuOpen ? 0f : 1f;
    }

    public void OnResume() => ToggleMenu();

    public void OnInstructions()
    {
        menuPanel.SetActive(false);
        instructionsPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseInstructions()
    {
        instructionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void OnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void OnQuit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
