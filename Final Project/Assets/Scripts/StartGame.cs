using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartGame : MonoBehaviour
{
    public Button planetMode;
    public Button Instructions;
    public Button Back;
    public GameObject show;
    public GameObject Retro;
    public TextMeshProUGUI Future;
    public RawImage astronaut;
    public Button restart;
    public bool isWin = false;
    public bool isLose = false;
    public int level = 1;
    public GameObject winText;
    public GameObject Boss;
    public TextMeshProUGUI loseLevel;
    public GameObject SpiderLevel;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI Scores;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        //am = asteroid.GetComponent<SpidersManager>();
        planetMode.onClick.AddListener(startGame);
        Instructions.onClick.AddListener(showIns);
    }
    void showIns()
    {
        planetMode.gameObject.SetActive(false);
        Instructions.gameObject.SetActive(false);
        Retro.gameObject.SetActive(false);
        Future.gameObject.SetActive(false);
        show.gameObject.SetActive(true);
        Back.gameObject.SetActive(true);
    }
    void startGame()
    {   
        Scores.gameObject.SetActive(false);
        SpiderLevel.gameObject.SetActive(true);
        planetMode.gameObject.SetActive(false);
        Instructions.gameObject.SetActive(false);
        Retro.gameObject.SetActive(false);
        Future.gameObject.SetActive(false);
        astronaut.gameObject.SetActive(false);
        //gameObject.SetActive(false);
    }
    void restartGame()
    {
        Scores.gameObject.SetActive(false);
        planetMode.gameObject.SetActive(false);
        Instructions.gameObject.SetActive(false);
        Retro.gameObject.SetActive(false);
        Future.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // Update is called once per frame
    void Update()
    {
        if (isLose == true && level==1)
        {
            Scores.gameObject.SetActive(true);
            Scores.text = "SCORE: " + score;
            Boss.gameObject.SetActive(false);
            loseLevel.gameObject.SetActive(true);
            restart.gameObject.SetActive(true);
            SpiderLevel.gameObject.SetActive(false);
            restart.onClick.AddListener(restartGame);
            //StartCoroutine(ResetAll());
        }

        if (isWin == true && level == 1)
        {
            Scores.gameObject.SetActive(true);
            Scores.text = "SCORE: " + score;
            Boss.gameObject.SetActive(false);
            SpiderLevel.gameObject.SetActive(false);
            winText.gameObject.SetActive(true);
            restart.gameObject.SetActive(true);
            restart.onClick.AddListener(restartGame);
        }
       
        if(show.gameObject.activeSelf)
        {
            Back.onClick.AddListener(disable);
        }
    }
    private void disable()
    {
        show.gameObject.SetActive(false);
        Back.gameObject.SetActive(false);
        planetMode.gameObject.SetActive(true);
        Instructions.gameObject.SetActive(true);
        Retro.gameObject.SetActive(true);
        Future.gameObject.SetActive(true);
        astronaut.gameObject.SetActive(true);
    }
    

    IEnumerator ResetAll()
    {
        yield return new WaitForSeconds(2);
        winText.gameObject.SetActive(false);
        restart.gameObject.SetActive(true);
        SpiderLevel.gameObject.SetActive(false);

        restart.onClick.AddListener(restartGame);
        yield break;
    }
}
