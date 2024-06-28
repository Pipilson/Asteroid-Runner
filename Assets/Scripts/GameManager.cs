using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameObject player, ast;
    public TMP_Text gOver, gRestart, gQuit;
    public AudioSource astEx;
    bool gameOver = false;
    float spawnCD = 5f, seg;
    int min, hor;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("o manager nao esta na cena");
            }

            return instance;
        }
    }

    [SerializeField]
    int score = 0, life = 3, shield = 100;
    [SerializeField]
    Text scoreNumber, lifeNumber, shieldNumber, time;

    void Start()
    {
        if (instance != null)
        {
            Debug.LogError("existe mais de um manager na cena");
        }

        instance = this;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        seg += Time.deltaTime * 1;

        if (seg >= 60)
        {
            seg = 0;
            min++;
        }

        if (min >= 60)
        {
            min = 0;
            hor++;
        }

        scoreNumber.text = "Pontos: " + score.ToString("0000");
        lifeNumber.text = "Vidas: " + life.ToString("0") + "/3";
        shieldNumber.text = "Escudo: " + shield.ToString("000") + "%";
        time.text = hor.ToString("00") + ":" + min.ToString("00") + ":" + seg.ToString("00");

        if (shield > 60)
        {
            shieldNumber.color = Color.green;
        }

        else if (shield == 40)
        {
            shieldNumber.color = Color.yellow;
        }

        else if (shield <= 20)
        {
            shieldNumber.color = Color.red;
        }

        if (life == 3)
        {
            lifeNumber.color = Color.green;
        }

        if (life == 2)
        {
            lifeNumber.color = Color.yellow;
        }

        if (life <= 1)
        {
            lifeNumber.color = Color.red;
        }

        spawnCD -= 1 * Time.deltaTime;

        if (spawnCD <= 0)
        {
            System.Random rndx, rndy;
            rndx = new System.Random();
            rndy = new System.Random();

            float x, y;

            x = rndx.Next(-40, 40);
            y = rndx.Next(-18, 18);

            Instantiate(ast, new Vector3(x, y, 0), Quaternion.identity);
            spawnCD = 5;
        }

        if (gameOver)
        {
            life = 0;
            shield = 0;
            Time.timeScale = 0;
            gOver.text = "game over";
            gRestart.text = "'r' einiciar";
            gQuit.text = "'s' air";

            if (Input.GetKey(KeyCode.R))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Level1");
            }

            if (Input.GetKey(KeyCode.S))
            {
                Application.Quit();
            }
        }
    }

    public void SetScore()
    {
        score++;
    }

    public void SetLife()
    {
        if (life < 3)
        {
            life++;
        }
    }

    public void SetShield()
    {
        if (shield < 100)
        {
            shield += 20;
        }
    }

    public void Spawn()
    {
        if (shield >= -20)
        {
            shield -= 20;
        }

        if (shield == -20)
        {
            if (life >= 1)
            {
                life--;
                shield = 100;

                player.transform.position = new Vector3(-0.64f, 11.88f, 0);
                player.transform.rotation = new Quaternion(0, 0, 0, 0);
            }

            if (life <= 0)
            {
                gameOver = true;
                
            }
        }
    }

    public void AsteroidSom()
    {
        astEx.Play();
    }
}