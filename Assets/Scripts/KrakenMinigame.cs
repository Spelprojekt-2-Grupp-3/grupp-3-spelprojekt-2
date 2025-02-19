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
    [SerializeField]
    private GameObject kraken;

    [SerializeField]
    private KrakenDifficulty krakenDifficulty;

    [SerializeField]
    private GameObject canvas;

    [SerializeField]
    private GameObject hpBar;

    [SerializeField]
    private GameObject actionButton;
    private List<Tentacle> tentacles = new List<Tentacle>();
    private Camera camera;
    private PlayerInputActions playerControls;
    private InputAction minigame;
    private GameObject krakenInstance;
    private BoatMovement boatMovement;

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
        boatMovement = FindObjectOfType<BoatMovement>();
        StartMinigame();
    }

    public override void StartMinigame()
    {
        Events.stopBoat?.Invoke();
        var canvasInst = Instantiate(canvas);
        canvasInst.GetComponent<Canvas>().worldCamera = camera;

        Vector3 krakenPos = boatMovement.transform.position;

        krakenInstance = Instantiate(kraken, krakenPos, kraken.transform.rotation);

        if (krakenInstance.transform.childCount != 2)
        {
            Debug.Log("There need to be exactly 2 children in the kraken object.");
            return;
        }

        for (int i = 0; i < krakenInstance.transform.childCount; i++)
        {
            var child = krakenInstance.transform.GetChild(i);
            //child.transform.LookAt(krakenPos);
            Vector3 barPos = child.transform.position;
            barPos.y += 15;
            Vector3 actionButtonPos = child.transform.position;
            barPos.y += 30;
            var hpBarInst = Instantiate(
                hpBar,
                barPos,
                hpBar.transform.rotation,
                canvasInst.transform
            );
            var actionButtonInst = Instantiate(
                actionButton,
                actionButtonPos,
                actionButton.transform.rotation,
                canvasInst.transform
            );
            var tentacle = child.AddComponent<Tentacle>();
            tentacle.hpBar = hpBarInst;
            tentacle.actionButton = actionButtonInst;
            hpBarInst.GetComponent<Image>().fillAmount = tentacle.hp / tentacle.maxHp;
            tentacles.Add(tentacle);
        }

        tentacles[1].actionButton.GetComponent<GamepadIconsExampleNew>();
    }

    public override void StopMinigame()
    {
        Events.startBoat.Invoke();
    }

    private void Update()
    {
        krakenInstance.transform.position = boatMovement.transform.position;
        krakenInstance.transform.rotation = boatMovement.transform.rotation;

        for (int i = 0; i < tentacles.Count; i++)
        {
            if (tentacles[i] == null)
                return;
            tentacles[i].hpBar.transform.position = new Vector3(
                tentacles[i].transform.position.x,
                tentacles[i].transform.position.y + 15,
                tentacles[i].transform.position.z
            );
            Debug.Log(camera);
            tentacles[i].hpBar.transform.LookAt(camera.transform.position);
            tentacles[i].actionButton.transform.position = new Vector3(
                tentacles[i].transform.position.x,
                tentacles[i].transform.position.y + 20,
                tentacles[i].transform.position.z
            );
            tentacles[i].actionButton.transform.LookAt(camera.transform.position);
        }

        if (minigame.WasPerformedThisFrame())
        {
            switch (minigame.ReadValue<Vector2>())
            {
                case Vector2 v when (v.x < 0):
                    tentacles[0].hp -= 1;
                    tentacles[0].UpdateHealthBar(tentacles[0]);
                    break;
                case Vector2 v when (v.x > 0):
                    tentacles[1].hp -= 1;
                    tentacles[1].UpdateHealthBar(tentacles[1]);
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
    public GameObject actionButton;

    private void Awake()
    {
        hp = maxHp;
    }

    public void UpdateHealthBar(Tentacle tentacle)
    {
        hpBar.GetComponent<Image>().fillAmount = (float)hp / maxHp;
        if (hp <= 0)
        {
            Destroy(tentacle.hpBar);
            Destroy(tentacle.actionButton);
            Destroy(tentacle.gameObject);
        }
    }
}
