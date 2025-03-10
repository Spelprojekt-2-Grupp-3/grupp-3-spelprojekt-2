using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ScreenshotScript : MonoBehaviour
{
    bool screnshotInProgress;
    [SerializeField] bool screenshotEnabled;
    private int index = 1;

    [SerializeField] int secondsBetweenShots;

    private void Start()
    {
        if (!Directory.Exists(Application.dataPath + "/screenshots"))
        {
            Directory.CreateDirectory(Application.dataPath + "/screenshots");
        }
    }

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
        ScreenCapture.CaptureScreenshot(Application.dataPath +"/screenshots/" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".png");
        Debug.Log("Screenshot saved in " + Application.dataPath + "/screenshots/" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png");
        index++;
        screnshotInProgress = false;
    }
}
