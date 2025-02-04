
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(PlayerInput))]
public class InventoryController : MonoBehaviour
{ 
   [HideInInspector]public ItemGrid selectedItemGrid;

   private Shipment selectedPackage;
   private RectTransform packageRectTransform;
   
   private void Update()
   {
       //item drag
       if (selectedPackage != null)
           packageRectTransform.position = Input.mousePosition;
       
       if(selectedItemGrid==null){return;}
      
       //TODO:Implement input manager here
       if (Mouse.current.leftButton.wasPressedThisFrame)
       {
           Vector2Int posOnGrid = selectedItemGrid.GetTileGridPosition(Input.mousePosition);

           if (selectedPackage == null)
           {
               selectedPackage = selectedItemGrid.PickUpItem(posOnGrid.x, posOnGrid.y);
               if (selectedPackage != null)
                   packageRectTransform = selectedPackage.GetComponent<RectTransform>();
           }
           else
           {
               selectedItemGrid.PlaceItem(selectedPackage, posOnGrid.x, posOnGrid.y);
               selectedPackage = null;
           }
       }
   }
   
}
