using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private Button _mainMenu;
    [SerializeField] private ParticleSystem _topConfeti;
    [SerializeField] private ParticleSystem _leftConfeti;
    [SerializeField] private ParticleSystem _rightConfeti;

    private void Awake()
    {
        _mainMenu.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        Hide();
    }

    [ContextMenu("Show")]
    public void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(ShowConfeti());
    }

    private IEnumerator ShowConfeti()
    {
        yield return StartCoroutine(Delay(0.5f));

        var topConfeti = Instantiate(_topConfeti);
        topConfeti.Play();

        yield return StartCoroutine(Delay(topConfeti.main.duration));

        var leftConfeti = Instantiate(_leftConfeti);
        leftConfeti.Play();

        var rightConfeti = Instantiate(_rightConfeti);
        rightConfeti.Play();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator Delay(float amount)
    {
        yield return new WaitForSeconds(amount);
    }

}
