using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SigridMinigame : Minigames
{
    private GameObject pickedObject;
    private EmptyFuse[] emptyFuses = new EmptyFuse[15];
    private Fuse[] fuses = new Fuse[9];
    private List<int> fusesPos = new List<int>(){0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14};

    private void Awake()
    {
        pickedObject = null;
    }

    private void Start()
    {
        StartMinigame();
    }

    public override void StartMinigame()
    {
        base.StartMinigame();
        EventSystem.current.firstSelectedGameObject = transform.Find("Background").transform.Find("Fuse").gameObject;
        emptyFuses = gameObject.transform.Find("EmptyFuses").GetComponentsInChildren<EmptyFuse>();
        fuses = gameObject.transform.Find("Background").GetComponentsInChildren<Fuse>();
        for (int i = 0; i < 6; i++)
        {
            var random = fusesPos[Random.Range(0, fusesPos.Count)];
            var chosenFuse = emptyFuses[random];
            chosenFuse.GetComponent<Image>().color = Color.grey;
            Destroy(chosenFuse.GetComponent<Selectable>());
            fusesPos.Remove(random);
            chosenFuse.emptyFuse = false;
        }

        foreach (int pos in fusesPos)
        {
            emptyFuses[pos].emptyFuse = true;
            emptyFuses[pos].voltage = Random.Range(0, 80f);
        }
    }

    public override void StopMinigame()
    {
        base.StopMinigame();
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject is null) return;
        if (pickedObject != null)
        {
            Debug.Log(pickedObject);
            var selectedObjPos = EventSystem.current.currentSelectedGameObject.transform.position;
            selectedObjPos.z = -0.1f;
            pickedObject.transform.position = selectedObjPos;
        }
    }

    public void PickUp(GameObject obj)
    {
        Debug.Log(obj);
        pickedObject = obj;
    }
}
