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

    [Title("������")]
    public Camera_movement cameraBounds;

    [Title("���������")]
    public ObjectCollection objectCollection;

    [Title("���� ���������")]
    public SlotManager slotManager;
    public TaskAdapter taskAdapter;

    [Title("������� ������")]
    public GameConditions gameConditions;
    public TimerToLose timerToLose;
}
