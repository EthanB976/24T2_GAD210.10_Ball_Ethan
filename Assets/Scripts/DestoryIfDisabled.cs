using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryIfDisabled : MonoBehaviour
{
    public bool SelfDestructionEnabled { get; set; } = false;

    private void OnDisable()
    {
        if (SelfDestructionEnabled)
        {
            Destroy(gameObject);
        }
    }
}
