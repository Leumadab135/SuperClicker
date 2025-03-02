using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;

public class PointsElementUI : MonoBehaviour
{
    [SerializeField] float _duration = 1.5f;
    [SerializeField] private TextMeshProUGUI _pointsText;

    void Start()
    {
        //Set Click
        _pointsText.text ="+ " + FindFirstObjectByType<GameController>().ClickRatio.ToString();

        //Movement
        transform.DOMoveY(transform.position.y + 60, _duration);
        
        //Fade Color
        _pointsText.DOColor(new Color(0,0,0,0), _duration);
        Destroy(gameObject, _duration);
    }
}
