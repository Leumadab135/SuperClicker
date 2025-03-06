using UnityEngine;
using System.Collections.Generic;
using System.Data;

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
        Initialize(_particlesRain, materialParticleIndex); // Aplicar el frame de la textura
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
        float segment = 1f / 28f;  // Divide la textura en 28 frames
        float frame = segment * materialParticleIndex;
        var tex = particleSystem.textureSheetAnimation;
        tex.startFrame = frame;  // Asigna el frame correcto
    }

    private void GetReward(Reward reward)
    {
        _textUpgrader.ShowTextReward(reward);

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
    #endregion
}
