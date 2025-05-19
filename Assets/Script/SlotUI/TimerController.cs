using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;
    private float remainingTimeInSeconds; // Time remaining in the countdown in seconds
    private SlotController slotController; // Time remaining in the countdown in seconds

    public void SetTime(float seconds)
    {
        remainingTimeInSeconds = seconds; // Convert minutes to seconds
    }

    public void SetSlotController(SlotController slotController)
    {
        this.slotController = slotController;
    }

    public float GetTime()
    {
        return remainingTimeInSeconds / 60; // Convert seconds to minutes
    }

    private void Update()
    {
        remainingTimeInSeconds -= Time.deltaTime;
        UpdateTimer(remainingTimeInSeconds);
    }

    public void UpdateTimer(float timeInSeconds)
    {
        if (timeInSeconds <= 0)
        {
            slotController.UnlockChest();
        }

        // Convert time in seconds to hours, minutes, and seconds
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // Format the time as HH:MM:SS
        string timeFormatted = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);

        timeText.text = timeFormatted;
    }
}
