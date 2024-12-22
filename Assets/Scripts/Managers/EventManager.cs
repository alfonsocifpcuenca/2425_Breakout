using UnityEngine.Events;

public class EventManager
{
    // Evento cuando se a�ade una vida
    public UnityEvent OnLiveAdded = new UnityEvent();
    // Evento cuando se pierde una vida
    public UnityEvent OnLiveLost = new UnityEvent();
    // Evento cuando se establecen las vidas
    public UnityEvent OnLivesChanged = new UnityEvent();
    // Evento cuando se a�aden puntos
    public UnityEvent OnPointsAdded = new UnityEvent();
    // Evento cuando se cambia de nivel
    public UnityEvent OnLevelChanged = new UnityEvent();
}
