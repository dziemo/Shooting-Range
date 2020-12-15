using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeAttackMode : MonoBehaviour
{
    public Transform targetsParent;
    public FloatVariable timeLeft;

    public TextMeshPro scoreDisplay;
    public TextMeshPro timeDisplay;

    List<GameObject> allTargets = new List<GameObject>();
    GameObject activeTarget;

    bool inProgress = false;

    int score = 0;

    private void Start()
    {
        foreach (Transform target in targetsParent)
        {
            target.gameObject.SetActive(false);
            allTargets.Add(target.gameObject);
        }

        StartMode();
    }

    private void Update()
    {
        if (inProgress && timeLeft.Value > 0)
        {
            timeLeft.Value -= Time.deltaTime;
            timeDisplay.text = timeLeft.Value.ToString("F2");
        } else
        {
            EndMode();
        }
    }

    public void StartMode ()
    {
        inProgress = true;
        score = 0;
        timeLeft.Value = 30f;
        timeDisplay.text = timeLeft.Value.ToString();
        activeTarget = allTargets[Random.Range(0, allTargets.Count)];
        allTargets.Remove(activeTarget);
        activeTarget.SetActive(true);
    }

    public void EndMode ()
    {
        timeDisplay.text = 0.ToString();
        inProgress = false;
        if (activeTarget)
        {
            activeTarget.SetActive(false);
            allTargets.Add(activeTarget);
            activeTarget = null;
        }
    }

    public void OnTargetShot (int scoreToAdd)
    {
        activeTarget.SetActive(false);
        var newTarget = allTargets[Random.Range(0, allTargets.Count)];
        score += scoreToAdd;
        scoreDisplay.text = score.ToString();
        allTargets.Add(activeTarget);
        activeTarget = newTarget;
        activeTarget.SetActive(true);
    }
}
