using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DeliveryCheckUI : MonoBehaviour
{
    [SerializeField] private GameObject deliveryObj;
    [SerializeField] private GameObject canvas;
    [SerializeField] private List<GameObject> packages = new List<GameObject>();
    
    private void DeliveryCheck(string textToShow, int index)
    {
        deliveryObj = packages[index];
        deliveryObj = Instantiate(deliveryObj, canvas.transform);
        deliveryObj.transform.SetSiblingIndex(1);
        deliveryObj.gameObject.GetComponentInChildren<TMP_Text>().SetText(textToShow);
    }

    private void Start()
    {
        DeliveryCheck("Du är så rolig\npuss puss", 1);
    }
}
