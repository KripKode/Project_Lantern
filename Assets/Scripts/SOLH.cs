using System.Collections.Generic;
using UnityEngine;

public class SOLH : MonoBehaviour
{
    public static readonly HashSet<SOLH> Entities = new HashSet<SOLH>();

    void Awake()
    {
        Entities.Add(this);
    }

    void OnDestroy()
    {
        Entities.Remove(this);
    }
}
