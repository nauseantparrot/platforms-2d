using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] private int initialHealth;
    [SerializeField] private GameObject[] healthIndicators;
    [SerializeField] private bool shouldDropReward = false;
    [SerializeField] private GameObject reward;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private GameObject victoryVolume;

    private int health;
    private float healthByIndicator;

    private void Awake()
    {
        health = initialHealth;
        if (healthIndicators.Length > 0)
        {
            healthByIndicator = initialHealth / healthIndicators.Length;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(DamageFeedback());
        if (healthIndicators.Length > 0)
        {
            UpdateHealthIndicators();
        }
        if (health <= 0)
        {
            if (shouldDropReward && reward != null)
            {
                Instantiate(reward, transform.position, Quaternion.identity);
            }
            if (gameObject.CompareTag(playerTag))
            {
                victoryVolume.GetComponent<VictoryVolumeBehaviour>().GameOver();
            }
            Destroy(gameObject);
        }
    }

    private void UpdateHealthIndicators()
    {
        Color normalColor = new Color(1f, 1f, 1f, 1f);
        Color disabledColor = new Color(1f, 1f, 1f, 0.07f);
        int firstDisabledIndex = (int)Mathf.Ceil(health / healthByIndicator);
        for (int i = 0; i < healthIndicators.Length; i++)
        {
            Color currentColor = i < firstDisabledIndex ? normalColor : disabledColor;
            healthIndicators[i].TryGetComponent(out SpriteRenderer spriteRenderer);
            if (spriteRenderer != null)
            {
                spriteRenderer.color = currentColor;
            }
            else
            {
                healthIndicators[i].GetComponent<Image>().color = currentColor;
            }
        }
    }

    public void FillHealth()
    {
        health = initialHealth;
        UpdateHealthIndicators();
    }

    private IEnumerator DamageFeedback()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.45f, 0.45f);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }
}
