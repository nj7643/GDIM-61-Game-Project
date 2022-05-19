using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableWorld_Script : MonoBehaviour
{
    [SerializeField] private string collectableName;

    public string GetCollectableName()
    {
        return collectableName;
    }
}
