using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AreaClickAgent : MonoBehaviour
{
    #region Fields

    [field: SerializeField] private Image _timerImage;
    private GameController _game;
    private List<SlotButtonUI> affectedSlots = new List<SlotButtonUI>();
    private float _duration = 30;
    #endregion

    #region UnityCallbacks
    private void Start()
    {
        _game = FindAnyObjectByType<GameController>();

        // Guardamos los slots y cambiamos su comportamiento
        foreach (var slot in _game._slots)
        {
            affectedSlots.Add(slot);
            slot._clickButton.onClick.AddListener(() => ClickNearbySlots(slot));
        }

        StartCoroutine(EliminateAfterDelay());
    }
    #endregion

    #region Private Methods
    private void ClickNearbySlots(SlotButtonUI slot)
    {
        int slotIndex = _game._slots.IndexOf(slot);
        int clickRatio = Mathf.RoundToInt(_game.ClickRatio);

        // Afecta al slot tocado y los dos siguientes (si existen)
        for (int i = slotIndex; i < slotIndex + 3 && i < _game._slots.Count; i++)
        {
            _game._slots[i].Click(clickRatio);
        }
    }

    private IEnumerator EliminateAfterDelay()
    {
        yield return new WaitForSeconds(_duration);

        // Restauramos el comportamiento original de los botones
        foreach (var slot in affectedSlots)
        {
            slot._clickButton.onClick.RemoveAllListeners();
            slot._clickButton.onClick.AddListener(() => slot.Click(Mathf.RoundToInt(_game.ClickRatio)));
        }

        Destroy(gameObject);
    }
    #endregion
}




