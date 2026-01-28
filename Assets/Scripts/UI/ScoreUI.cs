using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text maxScoreText;  
    [SerializeField] private GameObject retry; 
    [SerializeField] private PlayerHealth playerHealth; 

    private const string MAX_SCORE_KEY = "MAX_SCORE";

    private float score = 0;
    private float maxScore = 0;

    private void OnEnable()
    {
        if(maxScoreText != null)
        {
            maxScore = PlayerPrefs.GetFloat(MAX_SCORE_KEY, 0f);
            UpdateMaxScore();
        }

        if (playerHealth != null)
        {
            PlayerHealth.OnDie += RecordScore;
            PlayerHealth.OnDie += Retry;
        }
    }

    private void OnDisable()
    {
        if (playerHealth != null)
        {
            PlayerHealth.OnDie -= RecordScore;
            PlayerHealth.OnDie -= Retry;
        }
    }

    private void Update()
    {
        if (playerHealth == null) return;
        if (playerHealth.IsDead) return;

        UpdateScore();
    }

    private void UpdateScore()
    {
        score += Time.deltaTime;

        if (scoreText != null)
            scoreText.text = $"{score:0} M";
    }

    private void RecordScore()
    {
        if (score > maxScore)
        {
            maxScore = score;
            PlayerPrefs.SetFloat(MAX_SCORE_KEY, maxScore);
        }
        UpdateMaxScore();
    }

    private void UpdateMaxScore()
    {
        if (maxScoreText != null)
            maxScoreText.text = $"{maxScore:0} M";
    }

    private void Retry()
    {
        if (retry != null)
            retry.SetActive(true);
    }
}
