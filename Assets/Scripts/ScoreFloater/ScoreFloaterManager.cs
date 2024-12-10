using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFloaterManager : MonoBehaviour
{
    public static ScoreFloaterManager Instance { get; private set; }

    [SerializeField] private ScoreFloater scoreFloaterPrefab;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("There can not be more than one ScoreFloaterManager instance!");
            Destroy(gameObject);
        }
    }

    public void SpawnScoreFloater(Transform floaterParent, int textValue) {
        ScoreFloater newScoreFloater = Instantiate(scoreFloaterPrefab, floaterParent);
        newScoreFloater.SetValue(textValue);
    }
}
