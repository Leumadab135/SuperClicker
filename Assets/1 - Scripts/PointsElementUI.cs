using UnityEngine;
using DG.Tweening;
using TMPro;

public class PointsElementUI : MonoBehaviour
{
    #region Fields

    [SerializeField] private float duration = 1.5f;
    [SerializeField] private TextMeshProUGUI pointsText;
    private string FormatNumber(float number)
    {
        string[] suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "O", "N", "D", "Ud", "Dd", "Td", "Qd", "Qid", "Sxd", "Spd", "Od", "Nd", "Y" };

        int order = 0;
        while (number >= 1000f && order < suffixes.Length - 1)
        {
            number /= 1000f;
            order++;
        }

        return number.ToString("0.#") + suffixes[order];
    }
    #endregion

    #region Unity Callbacks

    private void Start()
    {
        float points = FindFirstObjectByType<GameController>().ClickRatio;
        pointsText.text = "+ " + FormatNumber(points);

        if (points >= 100000)
        {
            pointsText.fontSize *= 0.8f;
        }

        transform.DOMoveY(transform.position.y + 100, duration);
        pointsText.DOFade(0, duration);
        Destroy(gameObject, duration);
    }
    #endregion

}


