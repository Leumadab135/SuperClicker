using UnityEngine;

public class SpeedBoostAgent : MonoBehaviour
{

    private static float globalClickSpeedMultiplier = 1f;
    [SerializeField] private float _speedMultiplier = 1.7f;

    private void Start()
    {
        ApplyBoost();
    }

    private void ApplyBoost()
    {
        globalClickSpeedMultiplier *= _speedMultiplier;

        AutoClickerAgent[] autoClickers = FindObjectsByType<AutoClickerAgent>(FindObjectsSortMode.None);
        foreach (var autoClicker in autoClickers)
        {
            autoClicker.SetClickSpeed(autoClicker.ClicksPerSec * _speedMultiplier);
        }
    }
    public static float GetGlobalMultiplier()
    {
        return globalClickSpeedMultiplier;
    }
}


