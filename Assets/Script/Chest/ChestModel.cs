using Random = UnityEngine.Random;

public class ChestModel
{
    private ChestController chestController;
    public SlotController slotController;
    public ChestScriptable chestScriptable;
    public ChestState chestState = ChestState.Locked;

    public ChestModel(ChestScriptable chestScriptable, SlotController slotController, ChestState chestState = ChestState.Locked)
    {
        this.chestScriptable = chestScriptable;
        this.slotController = slotController;
        this.chestState = chestState;
    }

    public void SetChestController(ChestController chestController)
    {
        this.chestController = chestController;
    }

    public ChestController GetChestController()
    {
        return chestController;
    }
    public SlotController GetSlotController()
    {
        return slotController;
    }

    public float GetUnlockingTimeInSec()
    {
        return chestScriptable.timeInMin * 60;
    }

    public ChestScriptable GetChestInfo()
    {
        return chestScriptable;
    }

    public ChestState GetChestState()
    {
        return chestState;
    }

    public void SetChestState(ChestState chestState)
    {
        this.chestState = chestState;
    }

    public void GetChestReward()
    {
        int coins = Random.Range(chestScriptable.minimumCoin, chestScriptable.maximumCoin + 1);
        int gems = Random.Range(chestScriptable.minimumGem, chestScriptable.maximumgem + 1);

        GameService.Instance.CurrencyHandler.AddCoin(coins);
        GameService.Instance.CurrencyHandler.AddGems(gems);
    }
}
