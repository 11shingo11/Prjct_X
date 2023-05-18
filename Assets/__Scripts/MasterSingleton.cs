using Sirenix.OdinInspector;
using UnityEngine;

public class MasterSingleton : MonoBehaviour
{
    #region Singleton
    public static MasterSingleton Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    #endregion

    [Title("Камера")]
    public Camera_movement cameraBounds;

    [Title("Коллекции")]
    public ObjectCollection objectCollection;

    [Title("Слот менеджеры")]
    public SlotManager slotManager;
    public TaskAdapter taskAdapter;

    [Title("Условия победы")]
    public GameConditions gameConditions;
    public TimerToLose timerToLose;
}
