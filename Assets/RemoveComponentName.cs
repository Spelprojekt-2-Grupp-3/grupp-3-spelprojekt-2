using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveComponentName : MonoBehaviour
{
    [ExecuteInEditMode]
    public void RemoveComponents()
    {
        Component[] components = GetComponentsInChildren(typeof(MeshCollider), true);

        foreach (Transform child in this.gameObject.transform)
        {
            Component[] c = GetComponents<MeshCollider>();
            Debug.Log(c.Length);
            if (c.Length > 1)
            {
                for (int i = 1; i < c.Length - 1; i++)
                {
                    DestroyImmediate(c[i]);
                }
            }
        }
        /* foreach (var c in components)
         {
             DestroyImmediate(c);
         }*/
    }
}
