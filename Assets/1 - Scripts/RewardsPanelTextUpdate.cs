using DG.Tweening;
using TMPro;
using UnityEngine;

public class RewardsPanelTextUpdate : MonoBehaviour
{
    [field: SerializeField] private TextMeshProUGUI _rewardText;
    [field: SerializeField] private GameObject _rewardPanel;

    public void ShowTextReward(Reward reward)
    {
        ActivePanel();
        UpdateTextReward(reward);
        SequenceCreation();
    }

    public void ShowTextAgent(Agent agent)
    {
        ActivePanel();
        UpdateTextAgent(agent);
        SequenceCreation();
    }

    private void UpdateTextReward(Reward reward)
    {
        //Update Text
        string _rewardTypeText = null;

        if (reward.RewardType == RewardType.Plus)
        {
            _rewardTypeText = "+ " + reward.Value + " Clicks";
        }
        else if (reward.RewardType == RewardType.Multi)
        {
            _rewardTypeText = "Clicks multiplicados por " + reward.Value;
        }

        _rewardText.text = ("REWARD!!!\n" + _rewardTypeText);
    }

    private void UpdateTextAgent(Agent agent)
    {
        string _rewardTypeText = null;

        if (agent.AgentType == AgentType.AutoClicker)
        {
            _rewardTypeText = "+1 AUTOCLICKER";
        }

        else if (agent.AgentType == AgentType.SpeedBoost)
        {
            _rewardTypeText = "X2 CLICK SPEED";
        }
        _rewardText.text = ("REWARD!!!\n" + _rewardTypeText);
    }

    private void ActivePanel ()
    {
        //Active Panel
        if (!_rewardPanel.gameObject.activeSelf)
        {
            _rewardPanel.gameObject.SetActive(true);
            _rewardPanel.transform.localScale = Vector3.zero;
        }
    }
    private void SequenceCreation()
    {
        //Sequence Creation
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(_rewardPanel.transform.DOScale(1, 1));
        mySequence.Append(_rewardPanel.transform.DOShakeRotation(1, new Vector3(0, 0, 15)));
        mySequence.Append(_rewardPanel.transform.DOScale(0, 1));
        mySequence.Play();
    }
}
