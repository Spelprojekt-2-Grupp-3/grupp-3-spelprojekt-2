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
        deliveryObj.gameObject.GetComponentInChildren<TMP_Text>().SetText(textToShow);
    }

    private void RemoveUIElement()
    {
        Destroy(deliveryObj); 
    }

    private void Start()
    {
        DeliveryCheck("Ingrid Ysterman\n\nRaspberry Island", 0);
        Invoke("RemoveUIElement", 2);
    }
}
