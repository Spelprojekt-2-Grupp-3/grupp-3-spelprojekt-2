using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotScript : MonoBehaviour
{
    bool screnshotInProgress = false;
    [SerializeField] bool screenshotEnabled;
    private int index = 1;

    [SerializeField] int secondsBetweenShots;
    
    // Update is called once per frame
    void Update()
    {
        if (!screnshotInProgress && screenshotEnabled)
        {
            screnshotInProgress = true;
            Invoke("Screenshot", secondsBetweenShots);
        }
    }

    void Screenshot()
    {
        ScreenCapture.CaptureScreenshot(Application.dataPath +"/screenshots/" + "Test"+ index + ".png");
        Debug.Log("Screenshot saved in " + Application.dataPath + "/screenshots/" + "Test" + + index + ".png");
        index++;
        screnshotInProgress = false;
    }
}
