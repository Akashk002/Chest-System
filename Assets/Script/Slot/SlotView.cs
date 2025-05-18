using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler ,IPointerClickHandler
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


    private void OnEnable()
    {
       // slotController.OnEnable();
    }
    private void OnDisable()
    {
        //slotController.OnDisable();
    }

    public void SetSlotCountroller(SlotController slotController)
    {
        this.slotController = slotController;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        slotController.OnPointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotController.OnPointerExit();
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
        Destroy(slotController.GetSlotModel().chestController.GetChestView().gameObject,2);
    }
}
