using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheekyCunt : MonoBehaviour
{
    [SerializeField]
    private DialogueManager dM;

    // Start is called before the first frame update
    public void FadeBrother(int i)
    {
        if (dM != null)
        {
            if (i == 0)
            {
               // Debug.Log("Starting Fade");
                dM.SetFadeState(true);
            }
            else if (i == 1)
            {
          //      Debug.Log("Ending Fade");
                dM.SetFadeState(false);
            }
            else
            {
                Debug.LogWarning("Fade Animation event passes unexpected integer");
            }
        }
    }
}
