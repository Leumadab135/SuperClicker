using DG.Tweening;
using UnityEngine;

public class AutoClickerAgent : MonoBehaviour
{
    #region Fields
    private SlotButtonUI _currentSlot;
    private float _clickTimer = 0f;
    private GameController _game;

    [field: SerializeField] private float _clicksPerSec = 0.5f;
    #endregion

    #region Properties
    public float ClicksPerSec
    {
        get => _clicksPerSec;
        private set => _clicksPerSec = value;
    }
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _game = FindFirstObjectByType<GameController>();
    }

    private void Start()
    {
        _clicksPerSec *= SpeedBoostAgent.GetGlobalMultiplier();
        FindNextAvailableSlot();
    }

    private void Update()
    {
        if (_currentSlot == null || _currentSlot.ClicksLeft <= 0)
        {
            FindNextAvailableSlot();
        }

        if (_currentSlot != null)
        {
            MoveToCurrentSlot();
            _clickTimer += Time.deltaTime;
            if (_clickTimer >= 1f / _clicksPerSec)
            {
                _clickTimer = 0f;
                _currentSlot.Click((int)_game.ClickRatio);
            }
        }
    }
    #endregion

    #region Public Methods
    public void SetClickSpeed(float newSpeed)
    {
        ClicksPerSec = newSpeed;
    }
    #endregion

    #region Private Methods
    private void FindNextAvailableSlot()
    {
        foreach (var slot in _game.Slots)
        {
            if (slot.ClicksLeft > 0)
            {
                _currentSlot = slot;
                MoveToCurrentSlot();
                return;
            }
        }
        _currentSlot = null;
    }

    private void MoveToCurrentSlot()
    {
        if (_currentSlot != null)
        {
            float randomOffsetX = Random.Range(-200f, 200);
            float randomOffsetY = Random.Range(-200f, 200f);

            Vector2 targetPosition = new Vector2(
                _currentSlot.transform.position.x + randomOffsetX,
                _currentSlot.transform.position.y - 90 + randomOffsetY
            );

            transform.DOMove(targetPosition, 1);
        }
    }
    #endregion
}



