using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemeysKilled : MonoBehaviour
{

    [SerializeField] private int totalEnemies;

    private void Start()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void EnemiesDefeated()
    {
        totalEnemies--;

        if (totalEnemies >= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}
