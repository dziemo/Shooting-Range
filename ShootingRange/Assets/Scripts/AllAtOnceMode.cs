using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class AllAtOnceMode : MonoBehaviour
{
    public List<Transform> targetPositions;
    public GameObject targetPrefab;
    public GameEvent onShotEvent;

    int allTargets = 0;
    float time = 0;
    bool inProgress = false;

    [ContextMenu("Start")]
    public void OnStart ()
    {
        inProgress = true;
        time = 0;
        foreach (var pos in targetPositions)
        {
            var newTarget = Instantiate(targetPrefab, pos.position, pos.rotation);
            newTarget.GetComponent<TargetController>().onShotEvent = onShotEvent;
        }
        allTargets = targetPositions.Count;
    }

    public void OnTargetShot ()
    {
        allTargets--;
        if (allTargets <= 0)
        {
            inProgress = false;
            Debug.Log("Time " + time);
        }
    }

    private void Update()
    {
        if (inProgress)
        {
            time += Time.deltaTime;
        }
    }
}
