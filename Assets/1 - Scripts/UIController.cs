using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    [field: SerializeField] private Button _playAgainButton;
    [field: SerializeField] private Button _exitButton;

    private void Start()
    {
        _playAgainButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("SuperClicker");
        });

        _exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
