using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject ObjectPrefab;

    [SerializeField] private float minimumSecondsUntilCreate;
    [SerializeField] private float maximumSecondsUntilCreate;

    [SerializeField] private float spawnDistanceFromPlayer;
    [SerializeField] private float maximumEnemyCount;

    private Coroutine CountdownCoroutine;
    private Player player;
    private PauseMenuUI pauseMenuUI;
    private bool isWaitingToCreate = false;

    private void Awake()
    {
        pauseMenuUI = FindObjectOfType<PauseMenuUI>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Update()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (!pauseMenuUI.isPaused)
            if (enemies < maximumEnemyCount)
                if (!isWaitingToCreate)
                    CountdownCoroutine = StartCoroutine(routine: CountdownUntilCreation());
    }

    IEnumerator CountdownUntilCreation()
    {
        isWaitingToCreate = true;
        float secondsToWait = Random.Range(minimumSecondsUntilCreate, maximumSecondsUntilCreate);
        yield return new WaitForSeconds(secondsToWait);
        Place();
        isWaitingToCreate = false;
    }

    public virtual void Place()
    {
        var position = player.transform.position;
        while ((player.transform.position - position).magnitude < spawnDistanceFromPlayer)
            position = new Vector3(Random.Range(-23, 23), transform.position.y, Random.Range(-23, 23));

        Instantiate(ObjectPrefab, position, Quaternion.identity);
    }
}
