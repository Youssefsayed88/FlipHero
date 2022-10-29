using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject gamePanel;

    public float playerHp;
    public Slider hpSlider;

    public float playerEndurance;
    public Slider enduranceSlider;

    public int score;
    public float distance;
    float gainedPoints;

    public float playerSpeed;
    [SerializeField] float maxSpeed;

    public Text scoreText;
    public Text distanceText;

    public AudioSource coinAudio;


    #region GameOverUI
    public GameObject gameoverPanel;
    public Text finalScoreText;
    public Text finalDistanceText;
    bool gameOver = false;
    #endregion
    public static event Action<float> onSpeedChange;

    //Setting up this object to be a singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        PlayerBehaviour.onPlayerDeath += GameOver;
    }

    void Start()
    {
        CoinBehaviour.onCoinCollected += CoinCollected;

        #region PlayerHp
        PlayerBehaviour.playerTookDamage += UpdateHealthSlider;
        hpSlider.maxValue = playerHp;
        hpSlider.value = playerHp;
        #endregion

        #region PlayerEndurance
        PlayerBehaviour.onEnduranceChange += UpdateEnduranceSlider;
        enduranceSlider.maxValue = playerEndurance;
        enduranceSlider.value = playerEndurance;
        #endregion

        InvokeRepeating("InvokeUpdate", 5f, 5f);
    }

    private void Update()
    {
        if (!gameOver) { UpdateScores(); } 
    }

    void UpdateScores()
    {
        distance += playerSpeed * Time.deltaTime;
        distanceText.text = "Distance:" + ((int)distance).ToString();


        gainedPoints += playerSpeed * Time.deltaTime;
        if(gainedPoints > 500)
        {
            score += 100;
            scoreText.text = "Score:" + score.ToString();
            gainedPoints = 0;
        }
    }

    void CoinCollected()
    {
        coinAudio.Play();
        score += 50;
        scoreText.text = "Score:" + score.ToString();
    }

    void UpdateHealthSlider(float damage)
    {
        hpSlider.value -= damage;
    }

    void UpdateEnduranceSlider(float currenctEndurance)
    {
        enduranceSlider.value = currenctEndurance;
    }

    void UpgradingDifficulty(float speed)
    {
        onSpeedChange.Invoke(speed);
        playerSpeed += speed;
    }

    void InvokeUpdate()
    {
        if(playerSpeed < maxSpeed)
        {
            UpgradingDifficulty(10f);
        }
    }

    void GameOver()
    {
        CancelInvoke("InvokeUpdate");
        gameOver = true;
        finalScoreText.text = "Score:" + score.ToString();
        finalDistanceText.text = "Distance:" + distance.ToString();
        gamePanel.SetActive(false);
        gameoverPanel.SetActive(true);
    }

    public void Retry()
    {
        Instance = null;
        SceneManager.LoadScene(0);
    }

}
