//==============================================================
// HealthSystem
// HealthSystem.Instance.TakeDamage (float Damage);
// HealthSystem.Instance.HealDamage (float Heal);
// HealthSystem.Instance.UseMana (float Mana);
// HealthSystem.Instance.RestoreMana (float Mana);
// Attach to the Hero.
//==============================================================

using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
	public static HealthSystem Instance;
	public Image currentHealthGlobe;
	public Image currentManaGlobe;


	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
        UpdateHealthGlobe();
        UpdateManaGlobe();
		ScoreView();
    }

	void Update()
	{
		UpdateHealthGlobe();
		UpdateManaGlobe();
		ScoreView();
	}

	

	private void UpdateHealthGlobe()
	{
		float ratio = GameManager.instance.player.hitpoint / GameManager.instance.player.maxHitpoint;
		currentHealthGlobe.rectTransform.localPosition = new Vector3(0, currentHealthGlobe.rectTransform.rect.height * ratio - currentHealthGlobe.rectTransform.rect.height, 0);

	}


	private void UpdateManaGlobe()
	{
		float ratio = GameManager.instance.player.currMana / GameManager.instance.player.maxMana;
		currentManaGlobe.rectTransform.localPosition = new Vector3(0, currentManaGlobe.rectTransform.rect.height * ratio - currentManaGlobe.rectTransform.rect.height, 0);
	}

	private void ScoreView()
	{
        GameObject.Find("Score").GetComponent<Text>().text ="Score: " + GameManager.instance.score.ToString();

    }
}

