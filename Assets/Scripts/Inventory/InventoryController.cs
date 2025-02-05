
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(PlayerInput))]
public class InventoryController : MonoBehaviour
{ 
   [HideInInspector]public ItemGrid selectedItemGrid;

   private InventoryItem selectedPackage;
   private InventoryItem overlapItem;
   private RectTransform packageRectTransform;

   [SerializeField] private List<PackageData> packages;
   [SerializeField] private GameObject packagePrefab;
   [SerializeField] private Transform canvasTransform;
   
   private void Start()
   {
       CreateItem();
   }

   /// <summary>
   /// Creates a Package based on packages List. It becomes the selectedPackage. MAKE SURE THERE IS NO ALREADY SELECTED PACKAGE BEFORE USE
   /// </summary>
   void CreateItem()
   {
       InventoryItem item = Instantiate(packagePrefab).GetComponent<InventoryItem>();
       selectedPackage = item;
       packageRectTransform = item.GetComponent<RectTransform>();
       packageRectTransform.SetParent(canvasTransform);
       
       item.Set(packages[0]);
   }
   
   private void Update()
   {
       //item drag
       if (selectedPackage != null)
           packageRectTransform.position = Mouse.current.position.ReadValue();
       if(selectedItemGrid==null){return;}
       GuiClicking();
   }

   void GuiClicking()
   {
       //TODO: Controller support
       if (Mouse.current.leftButton.wasPressedThisFrame)
       {
           LeftClick();
       }
   }

   void LeftClick()
   {
       Vector2 mousePosition = Mouse.current.position.ReadValue();

       if (selectedPackage != null)
       {
           mousePosition.x -= (selectedPackage.packageData.gridSize.x - 1) * ItemGrid.tileSizeWidth / 2;
           mousePosition.y += (selectedPackage.packageData.gridSize.y - 1) * ItemGrid.tileSizeHeight / 2;
       }
           
       Vector2Int posOnGrid = selectedItemGrid.GetTileGridPosition(mousePosition);

       if (selectedPackage == null)
       {   //Picking up item
           selectedPackage = selectedItemGrid.PickUpItem(posOnGrid.x, posOnGrid.y);
           if (selectedPackage != null)
               packageRectTransform = selectedPackage.GetComponent<RectTransform>();
       }
       else
       {   //Placing item
           bool success = selectedItemGrid.PlaceItem(selectedPackage, posOnGrid.x, posOnGrid.y);
           if (success)
           {
               selectedPackage = null;
           }
       }
   }
}
