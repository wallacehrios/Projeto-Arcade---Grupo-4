using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    int score = 0;
    int boss = 0;
    int bossGoal = 1;
    public int scoreGoal = 5;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        scoreText.text = score.ToString() + " PONTOS";
    }
    void Update()
    {
        if (score >= scoreGoal){
            score = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
           }
        if (boss >= bossGoal)
           {
            boss = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene("VictoryScreen");
           }
    }
    // Update is called once per frame
    public void AddPoint()
    {
        score += 1;
        scoreText.text = score.ToString() + " PONTOS";
    }
    public void BossMorto()
    {
        boss += 1;
    }
    public void Lose(){
        UnityEngine.SceneManagement.SceneManager.LoadScene ("LoseScreen");
    }
}
