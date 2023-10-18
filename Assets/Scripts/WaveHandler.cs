using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveHandler : MonoBehaviour
{
    public int waveNumber = 1;
    public int startCurrency = 5;
    public int currencyIteration = 1;
    public GameObject[] enemyPrefabs;
    public Transform[] spawners;
    public List<GameObject> spawnedEnemies;
    public int[] enemiesCost;
    public int state = 0; //0 = wave over, 1 = wave is currently happening
    public Animator waveNumberAnimator;
    public TMP_Text waveNumberText;
    public AudioSource audioSource;

    public IEnumerator StartWave()
    {
        waveNumberText.text = "Wave " + waveNumber;
        waveNumberAnimator.SetTrigger("ShowText");
        audioSource.Play();
        for (int i = startCurrency; i > 0;)
        {
            yield return new WaitForSeconds(1f);

            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController.cost < i)
            {
                enemy = Instantiate(enemy, spawners[Random.Range(0, spawners.Length)].position, Quaternion.identity);
                spawnedEnemies.Add(enemy);
                enemy.GetComponent<EnemyController>().waveHandler = this;
                i -= enemiesCost[0];
            }
        }
        state = 1;
    }

    public IEnumerator CheckForEnd()
    {
        if (spawnedEnemies.Count == 0)
        {
            state = 2;
            yield return new WaitForSeconds(3f);

            startCurrency += currencyIteration;
            waveNumber += 1;
            state = 0;
        }
    }

    private void Update()
    {
        switch (state)
        {
            case 0:
                StartCoroutine(StartWave());
                state = 2;
                break;
            case 1:
                StartCoroutine(CheckForEnd());
                break;
            default:
                break;
        }
    }
}
