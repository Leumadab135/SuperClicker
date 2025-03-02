using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class SlotButtonUI : MonoBehaviour
{
    public int ClicksLeft
    {
        get
        {
            return _clicksLeft;
        }
        set
        {
            _clicksLeft = value;
            if (_clicksLeft <= 0)
            {
                _stock--;
                if (_stock > 0)
                {
                    _initialClicks = Mathf.CeilToInt(_initialClicks * 1.15f);
                    _clicksLeft = _initialClicks;
                }
                else
                    Destroy(gameObject);

                Reward();
            }
        }
    }



    public event Action OnSlotClicked;

    [Header("GameController")]
    [Header("UI")]
    [SerializeField] Button _clickButton;
    [SerializeField] TextMeshProUGUI _clicksText;
    [SerializeField] ParticleSystem _particles;
    [Header("Prefab Points")]
    [SerializeField] PointsElementUI _pointsPrefab;
    [SerializeField] private int _initialClicks = 10;

    private int _stock = 5;
    private int _clicksLeft;
    private GameController _game;

    private void Awake()
    {
        _game = FindFirstObjectByType<GameController>();
    }

    void Start()
    {
        ClicksLeft = _initialClicks;
        RefreshClicksUI();
        int clickRatio = Mathf.RoundToInt(_game.ClickRatio);
        _clickButton.onClick.AddListener(() => Click(clickRatio));
    }

    public void Click(int clickCount)
    {
        _particles.Emit(clickCount);
        ClicksLeft -= clickCount;
        RefreshClicksUI();
        Instantiate(_pointsPrefab, transform);
        Camera.main.DOShakePosition(0.1f);
    }

    private void RefreshClicksUI()
    {
        _clicksText.text = ClicksLeft.ToString();
    }

    private void Reward()
    {
        throw new NotImplementedException();
    }
}
