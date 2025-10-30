using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region Properties
    [field: SerializeField] public float ClickRatio { get; set; }

    [Header("Rain")]
    [field: SerializeField] public ParticleSystem _particlesRain;
    [Header("Slots en Escena")]
    public List<SlotButtonUI> Slots => _slots;
    #endregion

    #region Fields
    [SerializeField] public List<SlotButtonUI> _slots = new List<SlotButtonUI>();
    [Header("Rewards")]
    [field: SerializeField] private RewardsPanelTextUpdate _textUpgrader;
    [field: SerializeField] private GameObject[] _agentPrefabs;
    [field: SerializeField] private RectTransform _agentsPanel;
    #endregion

    #region Unity Callbacks

    void Start()
    {
        SoundManager.Instance.PlayMusic(SoundManager.Instance.BackgroundMusic);

        SlotButtonUI.OnSlotReward += GetReward;
        SlotButtonUI.OnSlotAgent += GetAgent;
    }

    private void OnDestroy()
    {
        SlotButtonUI.OnSlotReward -= GetReward;
        SlotButtonUI.OnSlotAgent -= GetAgent;
    }
    #endregion

    #region Public Methods
    public void RainParticles(int materialParticleIndex)
    {
        Initialize(_particlesRain, materialParticleIndex);
        _particlesRain.Play();
        Invoke(nameof(StopRainParticles), 2f);
    }
    #endregion

    #region Private Methods
    private void StopRainParticles()
    {
        _particlesRain.Stop();
    }

    private void Initialize(ParticleSystem particleSystem, int materialParticleIndex)
    {
        float segment = 1f / 28f;
        float frame = segment * materialParticleIndex;
        var tex = particleSystem.textureSheetAnimation;
        tex.startFrame = frame;
    }

    private void GetReward(Reward reward)
    {
        _textUpgrader.ShowTextReward(reward);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.RewardSound);
        CheckAllSlotsEmpty();

        if (reward.RewardType == RewardType.Plus)
        {
            ClickRatio += reward.Value;
            return;
        }

        if (reward.RewardType == RewardType.Multi)
        {
            ClickRatio *= reward.Value;
            return;
        }
    }

    private void GetAgent(Agent agent)
    {
        _textUpgrader.ShowTextAgent(agent);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.OutOfStockSound);
        CheckAllSlotsEmpty();

        if (agent.AgentType == AgentType.AutoClicker)
        {
            Instantiate(_agentPrefabs[0], transform.position, Quaternion.identity);
            return;
        }

        if (agent.AgentType == AgentType.SpeedBoost)
        {
            Instantiate(_agentPrefabs[1], _agentsPanel);
            return;
        }
        
        if (agent.AgentType == AgentType.ClickMultiplier)
        {
            Instantiate(_agentPrefabs[2], _agentsPanel);
            return;
        }
        
        if (agent.AgentType == AgentType.AreaClick)
        {
            Instantiate(_agentPrefabs[3], _agentsPanel);
            return;
        }
    }

    public void CheckAllSlotsEmpty()
    {
        bool allSlotsEmpty = true;

        foreach (var slot in _slots)
        {
            if (slot.ClicksLeft > 0)
            {
                allSlotsEmpty = false;
                break;
            }
        }

        if (allSlotsEmpty)
        {
            SceneManager.LoadScene("EndGame");
        }
    }
    #endregion
}
