using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    //Config params
    [Range(0.1f, 10.0f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 83;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;

    //State variables
    [SerializeField] int currentScore;

    private void Awake()
    {
        var gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            GameObject o;
            (o = gameObject).SetActive(false);
            Destroy(o);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
