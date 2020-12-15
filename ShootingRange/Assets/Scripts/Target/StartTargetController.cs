using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectArchitecture;

public class StartTargetController : MonoBehaviour
{
    public GameEvent onShotEvent;

    public void OnShot()
    {
        onShotEvent.Raise();
    }
}
