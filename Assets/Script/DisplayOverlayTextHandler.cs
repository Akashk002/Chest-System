using UnityEngine;
using TMPro;

public class DisplayOverlayTextHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text OverlayText;
    [SerializeField] private string generateChestFailedString;
    [SerializeField] private string unlockedChestFailedString;
    [SerializeField] private string unlockedChestByGemFailedString;

    public void SubscribeEvent()
    {
        GameService.Instance.EventService.OnFailedString.AddListener(UpdateTextOnFailed);
    }
    private void OnDisable()
    {
        GameService.Instance.EventService.OnFailedString.RemoveListener(UpdateTextOnFailed);
    }

    private void UpdateTextOnFailed(FailedStringType FailedStringType)
    {
        string failedText = "";

        switch (FailedStringType)
        {
            case FailedStringType.GenerateChestFailed:
                failedText = generateChestFailedString;
                break;
            case FailedStringType.UnlockedChestFailed:
                failedText = unlockedChestFailedString;
                break;
            case FailedStringType.UnlockedChestByGemFailed:
                failedText = unlockedChestByGemFailedString;
                break;
            default:
                break;
        }

        OverlayText.enabled = true;
        OverlayText.text = failedText;
        Invoke(nameof(DisableOverLayText), 3f);
    }

    private void DisableOverLayText()
    {
        OverlayText.enabled = false;
    }
}

[System.Serializable]
public enum FailedStringType
{
    GenerateChestFailed,
    UnlockedChestFailed,
    UnlockedChestByGemFailed
}