using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    private List<Tentacle> tentacles = new List<Tentacle>();
    private Camera camera;
    private PlayerInputActions playerControls;
    private InputAction minigame;
    private GameObject krakenInstance;

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
        
        Vector3 krakenPos = FindObjectOfType<BoatMovement>().transform.position;
        
        krakenInstance = Instantiate(kraken, krakenPos, kraken.transform.rotation);

        if (krakenInstance.transform.childCount != 4)
        {
            Debug.Log("There need to be exactly 4 children in the kraken object.");
            return;
        }


        for (int i = 0; i < krakenInstance.transform.childCount; i++)
        {
            var child = krakenInstance.transform.GetChild(i);
            child.transform.LookAt(krakenPos);
            Vector3 barPos = child.transform.position;
            barPos.y += 15;
            var hpBarInst = Instantiate(hpBar, barPos, hpBar.transform.rotation, canvasInst.transform);
            var tentacle = child.AddComponent<Tentacle>();
            tentacle.hpBar = hpBarInst;
            hpBarInst.GetComponent<Image>().fillAmount = tentacle.hp / tentacle.maxHp;
            tentacles.Add(tentacle);
        }
        
    }

    public override void StopMinigame()
    {
        Events.startBoat.Invoke();
    }

    private void Update()
    {
        foreach (var tentacleHealthBar in tentacles)
        {
            tentacleHealthBar.hpBar.transform.LookAt(camera.transform.position);
        }

        if (minigame.WasPerformedThisFrame())
        {
            switch (minigame.ReadValue<Vector2>())
            {
                case Vector2 v when(v.x < 0):
                    tentacles[0].hp -= 1;
                    tentacles[0].UpdateHealthBar();
                    break;
                case Vector2 v when(v.x > 0):
                    tentacles[1].hp -= 1;
                    tentacles[1].UpdateHealthBar();
                    break;
                case Vector2 v when(v.y > 0):
                    tentacles[2].hp -= 1;
                    tentacles[2].UpdateHealthBar();
                    break;
                case Vector2 v when(v.y < 0):
                    tentacles[3].hp -= 1;
                    tentacles[3].UpdateHealthBar();
                    break;
            }
        }
    }
}

public class Tentacle : MonoBehaviour
{
    public int maxHp = 15;
    public int hp;
    public GameObject hpBar;

    private void Awake()
    {
        hp = maxHp;
    }

    public void UpdateHealthBar()
    {
        hpBar.GetComponent<Image>().fillAmount = (float) hp / maxHp;
        if (hp < 0)
        {
            hp = 0;
        }
    }
}
