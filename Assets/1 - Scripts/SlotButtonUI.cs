using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class SlotButtonUI : MonoBehaviour
{
    #region Properties
    [field: SerializeField] public Reward Reward;
    [field: SerializeField] public Agent Agent;
    public static event Action<Reward> OnSlotReward;
    public static event Action<Agent> OnSlotAgent;

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
                    // Reward Event
                    OnSlotReward?.Invoke(Reward);
                    _initialClicks = Mathf.CeilToInt(_initialClicks * 3f);
                    _clicksLeft = _initialClicks;
                    _game.RainParticles(_materialParticleIndex);
                }
                else
                {
                    OnSlotAgent?.Invoke(Agent);

                    GetComponent<Image>().enabled = false;
                    _clickButton.interactable = false;
                    _clicksText.enabled = false;
                }
            }
        }
    }
    #endregion

    #region Fields
    [Header("GameController")]
    [Header("UI")]
    [SerializeField] public Button _clickButton;
    [SerializeField] private TextMeshProUGUI _clicksText;
    [SerializeField] private ParticleSystem _particles;
    [Header("Prefab Points")]
    [SerializeField] private PointsElementUI _pointsPrefab;
    [SerializeField] private int _initialClicks = 10;
    [SerializeField] private int _materialParticleIndex;

    private GameController _game;
    private int _stock = 5;
    private int _clicksLeft;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _game = FindFirstObjectByType<GameController>();
    }

    void Start()
    {
        Initialize(_particles);

        ClicksLeft = _initialClicks;

        _clickButton.onClick.AddListener(() =>
        {
            int clickRatio = Mathf.RoundToInt(_game.ClickRatio);
            Click(clickRatio);
        });

        RefreshClicksUI();
    }
    #endregion

    #region Public Methods
    public void Click(int clickCount)
    {
        _particles.Emit(1);
        ClicksLeft -= clickCount;
        RefreshClicksUI();
        Instantiate(_pointsPrefab, transform);
        Camera.main.DOShakePosition(0.1f);
    }
    public void Initialize(ParticleSystem particleSystem)
    {
        ClicksLeft = _initialClicks;

        //Particle frame
        float segment = 1f / 28f;
        float frame = segment * _materialParticleIndex;
        var tex = particleSystem.textureSheetAnimation;
        tex.startFrame = frame;
    }
    #endregion

    #region Private Methods
    private void RefreshClicksUI()
    {

        _clicksText.text = FormatNumber(ClicksLeft);
    }

    private string FormatNumber(int number)
    {
        string[] suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "O", "N", "D", "Ud", "Dd", "Td", "Qd", "Qid", "Sxd", "Spd", "Od", "Nd", "Y" };

        int order = 0;
        while (number >= 1000f && order < suffixes.Length - 1)
        {
            number /= 1000;
            order++;
        }

        return number.ToString("0.#") + suffixes[order];
    }
    #endregion
}