using UnityEngine;

public class SlotController
{
    private SlotView slotView;
    private SlotModel slotModel;
    public SlotController(SlotView slotPrefab, SlotModel slotModel, Transform slotTransform)
    {
        slotView = Object.Instantiate(slotPrefab, slotTransform);
        slotView.transform.SetParent(slotTransform);
        slotView.SetSlotCountroller(this);
        this.slotModel = slotModel;
        slotModel.SetSlotCountroller(this);
    }

    public SlotView GetSlotView()
    {
        return slotView;
    }

    public SlotModel GetSlotModel()
    {
        return slotModel;
    }

    public void OnPointerEnter()
    {
        if (slotModel.IsSlotEmpty()) return;

        if (GetChestState() == ChestState.Unlocking)
        {
            GetSlotView().UnlockChestByGem.UpdateGemCount(slotModel.GetGemCountByTime());
            slotView.UnlockChestByGem.gameObject.SetActive(true);
            slotView.displayChestData.gameObject.SetActive(true);
        }
        else
        {
            slotView.displayChestData.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit()
    {
        if (slotModel.IsSlotEmpty()) return;

        if (GetChestState() == ChestState.Unlocking)
        {
            slotView.UnlockChestByGem.gameObject.SetActive(false);
            slotView.displayChestData.gameObject.SetActive(false);
        }
        else
        {
            slotView.displayChestData.gameObject.SetActive(false);
        }
    }

    public void OnPointerClick()
    {
        if (slotModel.IsSlotEmpty()) return;

        if (GetChestState() == ChestState.Opened)
        {
            OpenChest();
        }
        else
        if (GetChestState() == ChestState.Unlocking)
        {
            UnlockChestByGem();
        }
        else
        if (GetChestState() == ChestState.Locked)
        {
            if (GetUnlockingSlot() == null)
            {
                OpenChestUnlockPopup();
            }
            else
            {
                GameService.Instance.EventService.OnFailedString.InvokeEvent(FailedStringType.UnlockedChestFailed);
            }
        }
    }

    private void OpenChestUnlockPopup()
    {
        GameService.Instance.OpenUnlockChestPopup();
        GameService.Instance.EventService.OnSlotSelect.InvokeEvent(this);
    }

    private void OpenChest()
    {
        slotModel.chestController.OpenChest();
        slotView.OpenChestText.enabled = false;
        slotView.displayChestData.gameObject.SetActive(false);
        slotView.undoButton.gameObject.SetActive(false);
        slotView.OpenChestText.enabled = false;
        slotView.DestroyChest();
    }

    public SlotController GetUnlockingSlot()
    {
        return GameService.Instance.SlotService.GetUnlockingSlot();
    }

    public void SetUnlockingSlot()
    {
        GameService.Instance.SlotService.SetUnlockingSlot(this);
    }

    public ChestState GetChestState()
    {
        return slotModel.chestController.GetChestModel().GetChestState();
    }

    public void SetChestState(ChestState chestState)
    {
        slotModel.chestController.GetChestModel().SetChestState(chestState);
    }

    public void StartTimerForUnlockChest()
    {
        slotView.timeText.enabled = false;
        slotView.timerController.gameObject.SetActive(true);
        slotView.timerController.SetTime(slotModel.timeNeededToUnlock);
        slotView.timerController.SetSlotController(this);
        SetChestState(ChestState.Unlocking);
        SetUnlockingSlot();
    }

    public void UnlockChestByGem()
    {
        int gemNeededToUnlockChest = slotModel.GetGemCountByTime();

        if (gemNeededToUnlockChest <= GameService.Instance.CurrencyHandler.GetGem())
        {
            GameService.Instance.CurrencyHandler.SpendGems(gemNeededToUnlockChest);
            slotView.timerController.gameObject.SetActive(false);
            slotView.UnlockChestByGem.gameObject.SetActive(false);
            slotView.timeText.enabled = false;
            slotView.lockedChestText.enabled = false;
            slotView.OpenChestText.enabled = true;
            SetChestState(ChestState.Opened);
            slotView.undoButton.gameObject.SetActive(true);
            GameService.Instance.SlotService.SetUnlockingSlot(null);
            int slotIndex = GameService.Instance.SlotService.GetSlotIndex(this);
            GameService.Instance.ChestService.SetChestSavedData(slotIndex, ChestState.Opened);
        }
        else
        {
            GameService.Instance.EventService.OnFailedString.InvokeEvent(FailedStringType.UnlockedChestByGemFailed);
        }
    }

    public void UnlockChest()
    {
        SetChestState(ChestState.Opened);
        slotView.timerController.gameObject.SetActive(false);
        slotView.UnlockChestByGem.gameObject.SetActive(false);
        slotView.timeText.enabled = false;
        slotView.lockedChestText.enabled = false;
        slotView.OpenChestText.enabled = true;
        GameService.Instance.SlotService.SetUnlockingSlot(null);
    }

    public void UndoUnlockingChest()
    {
        GameService.Instance.CurrencyHandler.AddGems(slotModel.GetGemCountByTime());
        SetChestState(ChestState.Locked);
        slotView.timeText.enabled = true;
        slotView.lockedChestText.enabled = true;
        slotView.OpenChestText.enabled = false;
        slotView.undoButton.gameObject.SetActive(false);
    }

    public void ResetSlot()
    {
        slotModel.chestController = null;
        slotModel.timeNeededToUnlock = 0;
    }
}
