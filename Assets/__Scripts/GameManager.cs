using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // ������ �� ��������� GameManager
    public GameObject levelUpMenu;
    public LevelUpMenu lvlmenu;
    public Player player; // ������ �� ������
    public int score; // ������� ����
    public FireMagic fire;
    public Enemy enemy;
    
    private void Awake()
    {
        fire.fireballDamage = 5;
        fire.percentOfExpload = 15f;
        fire.explosionRadius = 8f;
        fire.manacost = 5;
        fire.fireballSpeed = 15;
        if (instance == null) // ���������, ��� ��������� GameManager ��� �� ������
        {
            instance = this; // ���� ���, �� ������� ���
        }
        else
        {
            Destroy(gameObject); // ���� ��������� ��� ����, �� ������� ������ GameManager, ����� �� ���� ����������
        }
    }

    public void AddScore(int amount)
    {
        score += amount; // ��������� ���� � �����
    }

    public void GameOver()
    {
        // ���, ������� ������ ����������� ��� ���������� ����, ��������, ����� ������ ��������� � �.�.
    }
}