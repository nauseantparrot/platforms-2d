using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryVolumeBehaviour : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Button retryButton;

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
    }

    public void GameCompleted()
    {
        gameOverText.text = "Thanks for playing!";
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            GameCompleted();
        }
    }
}
