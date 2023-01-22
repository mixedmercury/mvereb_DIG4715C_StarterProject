using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    private float nextSpawn = 0.0f;
    public float spawnTime = 0.1f;
    private float timeRemaining = 10;
    private int seconds;
    private Vector3 spawnCoordinate;

    public GameObject enemyCar;
    public GameObject player;
    public GameObject startText;
    public GameObject winText;
    public Text Timer;

    void Start()
    {
        player.SetActive(false);
        startText.SetActive(true);
        nextSpawn = 2;
        StartCoroutine(StartScreen());
    }

    IEnumerator StartScreen()
    {
        yield return new WaitForSeconds (2);
        startText.SetActive(false);
        player.SetActive(true);
        yield return new WaitForSeconds (10);
        if(player.activeSelf ==true)
        {
            winText.SetActive(true);
            player.SetActive(false);
        }
    }

    void Update()
    {
        if (player.activeSelf == true)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                seconds = (int)(timeRemaining % 60);
                Timer.text = "Time Left " + seconds.ToString();
            }
            if (Time.timeSinceLevelLoad > nextSpawn)
            {
                var spawnCount = Random.Range(1, 4);
                for (var i = 0; i < spawnCount; i++)
                {
                    var spawnDirection = Random.Range(1,5);
                    var spawnPosition = Random.Range(-5, 5);
                    
                    if (spawnDirection == 1)
                    {
                        spawnCoordinate = new Vector3(spawnPosition, 5, 0);
                    }
                    else if(spawnDirection == 2)
                    {
                        spawnCoordinate = new Vector3(5, spawnPosition, 0); 
                    }
                    else if(spawnDirection == 3)
                    {
                        spawnCoordinate = new Vector3(spawnPosition, -5, 0);
                    }
                    else if(spawnDirection == 4)
                    {
                        spawnCoordinate = new Vector3(-5, spawnPosition, 0);
                    }
                    Instantiate(enemyCar, spawnCoordinate, Quaternion.identity);
                }
                nextSpawn += spawnTime;
            }
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
