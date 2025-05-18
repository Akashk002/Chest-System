using System;
public class EventService
{
    public EventController<SlotController> OnSlotSelect { get; private set; }

    public EventService()
    {
        OnSlotSelect = new EventController<SlotController>();
    }
}
public class EventController<T, K>
{
    public event Action<T, K> baseEvent;
    public void InvokeEvent(T type, K type2) => baseEvent?.Invoke(type, type2);
    public void AddListener(Action<T, K> listener) => baseEvent += listener;
    public void RemoveListener(Action<T, K> listener) => baseEvent -= listener;
}

public class EventController<T>
{
    public event Action<T> baseEvent;
    public void InvokeEvent(T type) => baseEvent?.Invoke(type);
    public void AddListener(Action<T> listener) => baseEvent += listener;
    public void RemoveListener(Action<T> listener) => baseEvent -= listener;
}


