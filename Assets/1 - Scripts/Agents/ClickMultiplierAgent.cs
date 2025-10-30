using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickMultiplierAgent : MonoBehaviour
{
    #region Fields
    [field: SerializeField] private Image _timerImage;
    private GameController _game;
    private float _multiplier = 3f;
    private float _duration = 30f;
    private float _increment;
    #endregion

    #region UnityCallbacks
    private void Start()
    {
        _game = FindFirstObjectByType<GameController>();

        //Get increment and apply the multiplier
        _increment = _game.ClickRatio * (_multiplier - 1);
        _game.ClickRatio = _game.ClickRatio * _multiplier;

        StartCoroutine(RestoreAfterDelay());
    }

    private void Update()
    {
        _timerImage.fillAmount -= Time.deltaTime / _duration;
    }
    #endregion

    #region Private Methods
    private IEnumerator RestoreAfterDelay()
    {
        yield return new WaitForSeconds(_duration);

        //ClickRatio restauration
        _game.ClickRatio = _game.ClickRatio - _increment;

        Destroy(gameObject);
    }
    #endregion
}


