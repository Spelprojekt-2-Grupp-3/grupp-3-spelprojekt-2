using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveComponentName : MonoBehaviour
{
    public void RemoveComponents()
    {
        Component[] components = GetComponentsInChildren(typeof(MeshCollider), true);

        foreach (var c in components)
        {
            DestroyImmediate(c);
        }
    }
}
