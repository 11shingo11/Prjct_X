using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // ссылка на экземпл€р GameManager
    public GameObject levelUpMenu;
    public LevelUpMenu lvlmenu;
    public Player player; // ссылка на игрока
    public int score; // текущий счет
    public FireMagic fire;
    public Enemy enemy;
    
    private void Awake()
    {
        fire.fireballDamage = 5;
        fire.percentOfExpload = 15f;
        fire.explosionRadius = 8f;
        fire.manacost = 5;
        fire.fireballSpeed = 15;
        if (instance == null) // провер€ем, что экземпл€р GameManager еще не создан
        {
            instance = this; // если нет, то создаем его
        }
        else
        {
            Destroy(gameObject); // если экземпл€р уже есть, то удал€ем объект GameManager, чтобы не было дубликатов
        }
    }

    public void AddScore(int amount)
    {
        score += amount; // добавл€ем очки к счету
    }

    public void GameOver()
    {
        // код, который должен выполнитьс€ при завершении игры, например, показ экрана поражени€ и т.д.
    }
}