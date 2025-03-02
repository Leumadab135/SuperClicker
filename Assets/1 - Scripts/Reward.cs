using System;
using UnityEngine;
public enum RewardType
{
    Plus,
    Multi
}

[Serializable]
public class Reward
{
    [field: SerializeField] public RewardType RewardType { get; set; }
    [field: SerializeField] public float Value { get; set; }
}
