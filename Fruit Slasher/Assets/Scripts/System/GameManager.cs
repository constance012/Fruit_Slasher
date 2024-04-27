using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("UI References"), Space]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image gameOverOverlay;

    [Header("GameObject References"), Space]
    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject playerBlade;

    // Private fields.
    private int _score;

    public void UpdateScore(int amount)
    {
        _score += amount;
        scoreText.text = _score.ToString();
    }

    public void GameOver(float duration)
    {
        Debug.LogWarning("Game Over!!");
        spawner.SetActive(false);
        playerBlade.SetActive(false);

        StartCoroutine(ExplosionSequence(duration));
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("Game");
    }

    private IEnumerator ExplosionSequence(float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            gameOverOverlay.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(.5f);

        RestartGame();
    }
}