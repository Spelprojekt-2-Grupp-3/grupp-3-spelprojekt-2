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
    private InputAction minigame;
    private GameObject krakenInstance;
    [SerializeField] private CurrentInputIcons inputIcons;

    private BoatMovement boatMovement;

    private void OnEnable()
    {
        minigame = playerControls.Boat.Minigame;
        minigame.Enable();
        Events.updateIcons.AddListener(UpdateIcons);
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
        //StartMinigame();
    }

    public override void StartMinigame()
    {
        camera = Camera.main;
        //  Events.stopBoat?.Invoke();
        var canvasInst = Instantiate(canvas);
        canvasInst.GetComponent<Canvas>().worldCamera = camera;

        boatMovement = FindObjectOfType<BoatMovement>();
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
            //child.transform.LookAt(krakenPos);
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
            tentacle.hpBar = hpBarInst;
            tentacle.actionButton = actionButtonInst;
            hpBarInst.GetComponent<Image>().fillAmount = tentacle.hp / tentacle.maxHp;
            tentacles.Add(tentacle);
        }
        
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

    public override void StopMinigame()
    {
        Events.startBoat.Invoke();
    }

    private void Update()
    {
        if (krakenInstance == null)
        {
            return;
        }
        krakenInstance.transform.position = boatMovement.transform.position;
        krakenInstance.transform.rotation = boatMovement.transform.rotation;

        for (int i = 0; i < tentacles.Count; i++)
        {
            tentacles[i].hpBar.transform.position = new Vector3(
                tentacles[i].transform.position.x,
                tentacles[i].transform.position.y + 5,
                tentacles[i].transform.position.z
            );
            tentacles[i].hpBar.transform.LookAt(camera.transform.position);
            tentacles[i].actionButton.transform.position = new Vector3(
                tentacles[i].transform.position.x,
                tentacles[i].transform.position.y + 12,
                tentacles[i].transform.position.z
            );
            tentacles[i].actionButton.transform.LookAt(camera.transform.position);
            var euler = tentacles[i].actionButton.transform.localEulerAngles;
            euler.x += 45;
            euler.y -= 180;
            tentacles[i].actionButton.transform.rotation = Quaternion.Euler(euler);
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

        if (!tentacles[0].gameObject.activeSelf && !tentacles[1].gameObject.activeSelf)
        {
            StopMinigame();
            Destroy(krakenInstance);
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
    public int maxHp = 15;
    public int hp;
    public GameObject hpBar;
    public GameObject actionButton;

    private void Awake()
    {
        hp = maxHp;
    }

    private Tentacle tentacoloses;

    public void UpdateHealthBar(Tentacle tentacle)
    {
        tentacoloses = tentacle;

        hpBar.GetComponent<Image>().fillAmount = (float)hp / maxHp;
        if (hp <= 0)
        {
            tentacoloses.hpBar.gameObject.SetActive(false);
            tentacoloses.actionButton.gameObject.SetActive(false);
            Timer t = new Timer();
            GetComponent<Animator>().SetTrigger("Death");
            StartCoroutine(t.ExecuteAfterTime(1.9f, cunt));
        }
    }

    private void cunt()
    {
        tentacoloses.gameObject.SetActive(false);
    }
}
