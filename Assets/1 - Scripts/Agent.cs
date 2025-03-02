using System;
using UnityEngine;

public enum AgentType
{
    AutoClicker,
    SpeedBoost,
    x10ClickSpeed,
    AreaClick
}

[Serializable]
public class Agent
{
    [field: SerializeField] public AgentType AgentType { get; set; }
}
