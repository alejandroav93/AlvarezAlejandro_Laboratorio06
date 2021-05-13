using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int score = 0;
    public Text scoreText;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        scoreText.text = "PowerUp: " + score.ToString();

    }


    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGames()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }



}
