using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject deadScreen;
    public GameObject victoryScreen;
    void Start()
    {
        PlayerHealth.instance.OnDeath += Dead;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Win();
        }
    }


    void Win()
    {
        Time.timeScale = 0;
        victoryScreen.SetActive(true);
    }
    void Dead()
    {
        Time.timeScale = 0;
        deadScreen.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
