using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController 
{
    private ChestView chestView;
    private ChestModel chestModel;

    public ChestController(ChestView chestPrefab , ChestModel chestModel, Transform transform)
    {
        chestView = GameObject.Instantiate(chestPrefab, transform);
        chestView.transform.SetAsFirstSibling();
        chestView.SetChestController(this);
        this.chestModel = chestModel;
        chestModel.SetChestController(this);
    }

    public ChestView GetChestView()
    {
        return chestView;
    }
    public ChestModel GetChestModel()
    {
        return chestModel;
    }

    public void OpenChest()
    {
        chestView.GetChestAnimator().enabled = true;
        chestModel.GetChestReward();
    }
}
