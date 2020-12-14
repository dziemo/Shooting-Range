using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public GameEvent onShotEvent;

    public void OnShot (Vector2 point)
    {
        Debug.Log("Distance to center: " + Vector2.Distance(transform.position, point));
        onShotEvent.Raise();
        Destroy(gameObject);
    }
}
