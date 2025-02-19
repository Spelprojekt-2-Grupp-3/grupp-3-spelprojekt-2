using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public IEnumerator ExecuteAfterTime(float t, Action exec) // so we can wait a bit for hte bastard to move before we spawn actual tentacle
    {
        yield return new WaitForSeconds(t);
        //transform.LookAt(player.transform); We do not want here
        //Debug.Log("Waited");
        exec();
    }
}
