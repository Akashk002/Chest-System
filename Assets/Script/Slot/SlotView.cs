using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TMP_Text emptyText;
    public TMP_Text lockedChestText;
    public TMP_Text OpenChestText;
    public TMP_Text timeText;
    public DisplayChestData displayChestData;
    public TimerController timerController;
    public UnlockChestByGem UnlockChestByGem;
    public Button undoButton;
    private SlotController slotController;

    public void SetSlotCountroller(SlotController slotController)
    {
        this.slotController = slotController;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (slotController.IsSlotEmpty()) return;
        if (slotController.GetChestState() == ChestState.Collected) return;

        if (slotController.GetChestState() == ChestState.Unlocking)
        {
            UnlockChestByGem.UpdateGemCount(slotController.GetGemCountByTime());
            UnlockChestByGem.gameObject.SetActive(true);
            displayChestData.gameObject.SetActive(true);
        }
        else
        {
            displayChestData.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (slotController.IsSlotEmpty()) return;

        if (slotController.GetChestState() == ChestState.Unlocking)
        {
            UnlockChestByGem.gameObject.SetActive(false);
            displayChestData.gameObject.SetActive(false);
        }
        else
        {
            displayChestData.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        slotController.OnPointerClick();
    }

    public void UndoUnlockingChest()
    {
        slotController.UndoUnlockingChest();
    }

    public void DestroyChest()
    {
        Destroy(slotController.chestController.GetChestView().gameObject, 2);
        int slotIndex = GameService.Instance.SlotService.GetSlotIndex(slotController);
        GameService.Instance.ChestService.DeleteChestSavedData(slotIndex);
    }

    public void OpenChest()
    {
        OpenChestText.enabled = false;
        displayChestData.gameObject.SetActive(false);
        undoButton.gameObject.SetActive(false);
        OpenChestText.enabled = false;
        DestroyChest();
    }

    public void StartTimer(float time)
    {
        timeText.enabled = false;
        timerController.gameObject.SetActive(true);
        timerController.SetTime(time);
        timerController.SetSlotController(slotController);
    }

    public void UnlockChestByGems()
    {
        timerController.gameObject.SetActive(false);
        UnlockChestByGem.gameObject.SetActive(false);
        timeText.enabled = false;
        lockedChestText.enabled = false;
        OpenChestText.enabled = true;
        undoButton.gameObject.SetActive(true);
    }
    public void UnlockChest()
    {
        timerController.gameObject.SetActive(false);
        UnlockChestByGem.gameObject.SetActive(false);
        timeText.enabled = false;
        lockedChestText.enabled = false;
        OpenChestText.enabled = true;
    }

    public void UndoUnlocking()
    {
        timeText.enabled = true;
        lockedChestText.enabled = true;
        OpenChestText.enabled = false;
        undoButton.gameObject.SetActive(false);
    }


    public void UpdateSlotTimeText()
    {
        timeText.enabled = true;
        float timeInHours = (slotController.timeNeededToUnlock / 60) / 60f;
        timeText.SetText(timeInHours + "H");
    }

    public void SetSlotInfo(ChestController chestController)
    {
        emptyText.transform.SetAsFirstSibling();
        lockedChestText.enabled = true;
        displayChestData.SetChestData(chestController.GetChestModel().GetChestInfo());
        UpdateSlotTimeText();
    }
}
