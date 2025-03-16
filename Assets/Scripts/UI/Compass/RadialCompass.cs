using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialCompass : MonoBehaviour
{
    public GameObject iconPrefab;
    [SerializeField] private RawImage compassImage;
    [SerializeField] private Transform boat;
    private Camera camera;
    [SerializeField] private List<IslandMarker> markers = new List<IslandMarker>();
    
    private List<IslandMarker> islandMarkers = new List<IslandMarker>();

    private float compassUnit;

    private void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360f;
        foreach (var marker in markers)
        {
            AddMarker(marker);
        }

        camera = Camera.main;
    }

    void Update()
    {
        compassImage.uvRect = new Rect(camera.transform.localEulerAngles.y/360f, compassImage.uvRect.y, compassImage.uvRect.width, compassImage.uvRect.height);

        foreach (var marker in islandMarkers)
        {
            marker.image.rectTransform.anchoredPosition = GetPos(marker);
        }
    }

    public void AddMarker(IslandMarker marker)
    {
        GameObject newMarker = Instantiate(iconPrefab, compassImage.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;
        
        islandMarkers.Add(marker);
    }

    Vector2 GetPos(IslandMarker marker)
    {
        Vector2 boatPos = new Vector2(boat.position.x, boat.position.z);
        Vector2 boatForward = new Vector2(camera.transform.forward.x, camera.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - boatPos, boatForward);

        return new Vector2(compassUnit * angle, 0f);
    }
}
