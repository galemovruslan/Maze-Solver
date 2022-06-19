using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _resume;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _mainMenu;
    [SerializeField] private Button _exit;

    private void Awake()
    {
        _menuButton.onClick.AddListener(() => Show());
        _resume.onClick.AddListener(() => Hide());
        _restart.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        _mainMenu.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        _exit.onClick.AddListener(() => Application.Quit());
        Hide();
    }

    public void Show()
    {
        Time.timeScale = 0;
        _menuButton.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        _menuButton.gameObject.SetActive(true);
    }
}
