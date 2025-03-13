using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    private InputAction minigameButtonWest, minigameButtonEast;
    private GameObject krakenInstance;
    [SerializeField] private CurrentInputIcons inputIcons;
    private BoatMovement boatMovement;
    [SerializeField] private float maxTime;
    private float timer;

    private void OnEnable()
    {
        minigameButtonWest = playerControls.Boat.MinigameButtonWest;
        minigameButtonWest.Enable();
        minigameButtonEast = playerControls.Boat.MinigameButtonEast;
        minigameButtonEast.Enable();
    }

    private void OnDisable()
    {
        minigameButtonWest.Disable();
        minigameButtonEast.Disable();
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        boatMovement = FindObjectOfType<BoatMovement>();
    }

    public override void StartMinigame()
    {
        camera = Camera.main;
        var canvasInst = Instantiate(canvas);
        canvasInst.GetComponent<Canvas>().worldCamera = camera;
        Vector3 krakenPos = boatMovement.transform.position;

        krakenInstance = Instantiate(kraken, krakenPos, boatMovement.transform.rotation);

        if (krakenInstance.transform.childCount != 2)
        {
            Debug.Log("There need to be exactly 2 children in the kraken object.");
            return;
        }

        for (int i = 0; i < krakenInstance.transform.childCount; i++)
        {
            var child = krakenInstance.transform.GetChild(i);
            Vector3 barPos = child.transform.position;
            barPos.y += 5;
            Vector3 actionButtonPos = barPos;
            actionButtonPos.y += 5;
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
            tentacle.maxHp = krakenDifficulty.hp;
            tentacle.regenPerSecond = krakenDifficulty.regenPerSecond;
            tentacle.hpBar = hpBarInst;
            tentacle.actionButton = actionButtonInst;
            hpBarInst.GetComponent<Image>().fillAmount = tentacle.hp / tentacle.maxHp;
            tentacles.Add(tentacle);
        }
        UpdateIcons();
    }

    public override void StopMinigame()
    {
        Events.startBoat.Invoke();
    }

    private void Update()
    {
        if (krakenInstance is null)
        {
            return;
        }
        krakenInstance.transform.position = boatMovement.transform.position;
        var camPos = camera.transform.position;
        
        if (minigameButtonWest.WasPressedThisFrame())
        {
            tentacles[0].hp -= 1;
        }

        if (minigameButtonEast.WasPerformedThisFrame())
        {
            tentacles[1].hp -= 1;
        }

        for (int i = 0; i < tentacles.Count; i++)
        {
            var tentaclePos = tentacles[i].transform.position;
            Vector3 barPos = tentaclePos;
            Vector3 actionBtnPos = tentaclePos;
            barPos.y += 5;
            actionBtnPos.y += 12;
            tentacles[i].hpBar.transform.position = barPos;
            tentacles[i].hpBar.transform.LookAt(camPos);
            tentacles[i].actionButton.transform.position = actionBtnPos;
            tentacles[i].actionButton.transform.LookAt(camPos);
            var euler = tentacles[i].actionButton.transform.localEulerAngles;
            euler.x += 45;
            euler.y -= 180;
            tentacles[i].actionButton.transform.rotation = Quaternion.Euler(euler);
            tentacles[i].hp += tentacles[i].regenPerSecond * Time.deltaTime;
            tentacles[i].UpdateHealthBar(tentacles[i]);
        }

        if (!tentacles[0].gameObject.activeSelf && !tentacles[1].gameObject.activeSelf)
        {
            StopMinigame();
            DestroyImmediate(krakenInstance);
            DestroyImmediate(gameObject);
        }

        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            Debug.Log("you lost");
        }
    }

    private void UpdateIcons()
    {
        for (int i = 0; i < tentacles.Count; i++)
        {
            if (i == 0)
            {
                tentacles[i].actionButton.GetComponent<Image>().sprite = inputIcons.currentInputDevice.buttonWest;
            }
            else
            {
                tentacles[i].actionButton.GetComponent<Image>().sprite = inputIcons.currentInputDevice.buttonEast;
            }
        }
    }
}

public class Tentacle : MonoBehaviour
{
    public float maxHp;
    public float hp;
    public float regenPerSecond;
    [HideInInspector] public GameObject hpBar;
    [HideInInspector] public GameObject actionButton;

    private void Start()
    {
        hp = maxHp;
    }
    
    public void UpdateHealthBar(Tentacle tentacle)
    {
        hpBar.GetComponent<Image>().fillAmount = hp / maxHp;
        if (hp <= 0 && gameObject.activeSelf)
        {
            tentacle.hpBar.SetActive(false);
            tentacle.actionButton.SetActive(false);
            Timer t = new Timer();
            GetComponent<Animator>().SetTrigger("Death");
            StartCoroutine(t.ExecuteAfterTime(1.9f, SetInactive));
        }
        else if (hp > maxHp)
        {
            hp = maxHp;
        }
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
