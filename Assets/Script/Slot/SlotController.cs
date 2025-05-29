using UnityEngine;

public class SlotController
{
    private SlotView slotView;
    public ChestController chestController;
    public float timeNeededToUnlock;
    public SlotController(SlotView slotPrefab, Transform slotTransform)
    {
        slotView = Object.Instantiate(slotPrefab, slotTransform);
        slotView.transform.SetParent(slotTransform);
        slotView.SetSlotCountroller(this);
    }

    public SlotView GetSlotView()
    {
        return slotView;
    }

    public void SetChestInfo(ChestController chestController)
    {
        this.chestController = chestController;
        timeNeededToUnlock = chestController.GetChestModel().GetUnlockingTimeInSec();
        slotView.SetSlotInfo(chestController);
    }

    public void OnPointerClick()
    {
        if (IsSlotEmpty()) return;

        if (GetChestState() == ChestState.Unlocked)
        {
            OpenChest();
        }
        else if (GetChestState() == ChestState.Unlocking)
        {
            UnlockChestByGem();
        }
        else if (GetChestState() == ChestState.Locked)
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
        SetChestState(ChestState.Collected);
        chestController.OpenChest();
        slotView.OpenChest();
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
        return chestController.GetChestModel().GetChestState();
    }

    public void SetChestState(ChestState chestState)
    {
        chestController.GetChestModel().SetChestState(chestState);
    }

    public void StartTimerForUnlockChest()
    {
        slotView.StartTimer(timeNeededToUnlock);
        SetChestState(ChestState.Unlocking);
        SetUnlockingSlot();
    }

    public void UnlockChestByGem()
    {
        int gemNeededToUnlockChest = GetGemCountByTime();

        if (gemNeededToUnlockChest <= GameService.Instance.CurrencyHandler.GetGem())
        {
            GameService.Instance.CurrencyHandler.SpendGems(gemNeededToUnlockChest);
            SetChestState(ChestState.Unlocked);
            slotView.UnlockChestByGems();
            GameService.Instance.SlotService.SetUnlockingSlot(null);
            int slotIndex = GameService.Instance.SlotService.GetSlotIndex(this);
            GameService.Instance.ChestService.SetChestSavedData(slotIndex, ChestState.Unlocked);
        }
        else
        {
            GameService.Instance.EventService.OnFailedString.InvokeEvent(FailedStringType.UnlockedChestByGemFailed);
        }
    }

    public void UnlockChest()
    {
        SetChestState(ChestState.Unlocked);
        slotView.UnlockChest();
        GameService.Instance.SlotService.SetUnlockingSlot(null);
    }

    public void UndoUnlockingChest()
    {
        GameService.Instance.CurrencyHandler.AddGems(GetGemCountByTime());
        SetChestState(ChestState.Locked);
        slotView.UndoUnlocking();
    }

    public bool IsSlotEmpty()
    {
        return chestController == null;
    }

    public void ResetSlot()
    {
        chestController = null;
        timeNeededToUnlock = 0;
    }


    public int GetGemCountByTime()
    {
        if (GetChestState() == ChestState.Unlocking)
        {
            float time = slotView.timerController.GetTime();
            return Mathf.CeilToInt(time / 10);
        }

        return Mathf.CeilToInt((timeNeededToUnlock / 60) / 10); ;
    }

    public void SetRemainingTime(float remainingTimeInSec)
    {

        timeNeededToUnlock = remainingTimeInSec;
    }
}
