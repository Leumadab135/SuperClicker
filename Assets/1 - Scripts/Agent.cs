using System;
using UnityEngine;

public enum AgentType
{
    AutoClicker,
    SpeedBoost,
    ClickMultiplier,
    AreaClick
}

[Serializable]
public class Agent
{
    [field: SerializeField] public AgentType AgentType { get; set; }
}
