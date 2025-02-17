using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KrakenMinigame : Minigames
{
    [SerializeField] private GameObject kraken;
    [SerializeField] private KrakenDifficulty krakenDifficulty;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject hpBar;
    [SerializeField] private int tentacleCount;
    private List<GameObject> hpBarInstances = new List<GameObject>();
    private Camera camera;
    private PlayerInputActions playerControls;
    private InputAction minigame;

    private void OnEnable()
    {
        minigame = playerControls.Boat.Minigame;
        minigame.Enable();
    }

    private void OnDisable()
    {
        minigame.Disable();
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void Start()
    {
        camera = Camera.main;
        StartMinigame();
    }

    public override void StartMinigame()
    {
        Events.stopBoat?.Invoke();
        var canvasInst = Instantiate(canvas);
        canvasInst.GetComponent<Canvas>().worldCamera = camera;

        for (int i = 0; i < tentacleCount; i++)
        {
            var krakenInstance = Instantiate(kraken, new Vector3(10*i, 0, 0), kraken.transform.rotation);
            Vector3 barPos = new Vector3(krakenInstance.transform.position.x, krakenInstance.transform.position.y + 15, krakenInstance.transform.position.z);
            var hpBarInst = Instantiate(hpBar, barPos, hpBar.transform.rotation, canvasInst.transform);
            var tentacle = krakenInstance.AddComponent<Tentacle>();
            hpBarInst.GetComponent<Image>().fillAmount = tentacle.hp / tentacle.maxHp;
            hpBarInstances.Add(hpBarInst);
        }
    }

    public override void StopMinigame()
    {
        Events.startBoat.Invoke();
    }

    private void Update()
    {
        foreach (var hpBarInst in hpBarInstances)
        {
            hpBarInst.transform.LookAt(camera.transform.position);
        }

        if (minigame.WasPerformedThisFrame())
        { 
            switch (minigame.ReadValue<Vector2>())
            {
                case Vector2 v when(v.x > 0):
                    Debug.Log("right");
                    break;
                case Vector2 v when(v.x < 0):
                    Debug.Log("left");
                    break;
                case Vector2 v when(v.y > 0):
                    Debug.Log("up");
                    break;
                case Vector2 v when(v.y < 0):
                    Debug.Log("down");
                    break;
            }
        }
    }
}

public class Tentacle : MonoBehaviour
{
    public int maxHp = 15;
    public int hp;

    private void Awake()
    {
        hp = maxHp;
    }
}
