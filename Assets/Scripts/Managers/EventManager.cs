using UnityEngine.Events;

public class EventManager
{
    public UnityEvent OnLiveAdded = new UnityEvent();
    public UnityEvent OnLiveLost = new UnityEvent();
    public UnityEvent OnLivesChanged = new UnityEvent();

    public UnityEvent OnPointsAdded = new UnityEvent();

    public UnityEvent OnLevelChanged = new UnityEvent();
}
