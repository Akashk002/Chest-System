using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestView : MonoBehaviour
{
    [SerializeField] private Animator chestOpenAnim;
    private ChestController chestController;

    private void Start()
    {
        chestOpenAnim.enabled = false;
    }

    public void SetChestController(ChestController chestController)
    {
        this.chestController = chestController;
    }

    public Animator GetChestAnimator()
    {
        return chestOpenAnim;
    }

    private void OnDestroy()
    {
        chestController.GetChestModel().GetSlotController().ResetSlot();
    }
}
