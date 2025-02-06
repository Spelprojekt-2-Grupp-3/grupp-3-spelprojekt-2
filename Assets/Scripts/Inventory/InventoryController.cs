
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(PlayerInput))]
public class InventoryController : MonoBehaviour
{ 
   [HideInInspector] public ItemGrid selectedItemGrid;
   [HideInInspector]public ItemGrid otherGrid;
   
   private InventoryItem selectedPackage;
   private InventoryItem overlapItem;
   private RectTransform packageRectTransform;

   private PlayerInputActions playerControls;
   private InputAction lClick;
   private InputAction rClick;
   private InputAction shift;
   private InputAction mousePosition;
   
   [SerializeField] private List<PackageData> packages;
   [SerializeField] private GameObject packagePrefab;
   [SerializeField] private Transform canvasTransform;


   private void Awake()
   {
       playerControls = new PlayerInputActions();
   }

   private void OnEnable()
   {
       lClick = playerControls.UI.Click;
       rClick = playerControls.UI.RightClick;
       shift = playerControls.UI.ModifierButton;
       mousePosition = playerControls.UI.Point;
       EnableControls();
   }

   private void OnDisable()
   {
       DisableControls();
   }

   void EnableControls()
   {
       lClick.Enable();
       rClick.Enable();
       shift.Enable();
       mousePosition.Enable();
   }

   void DisableControls()
   {
       lClick.Disable();
       rClick.Disable();
       shift.Disable();
       mousePosition.Disable();
   }

   private void Start()
   {
       CreateItem();
   }

   private void Update()
   {
       //item drag
       if (selectedPackage != null)
           packageRectTransform.position = Mouse.current.position.ReadValue();
       if(selectedItemGrid==null){return;}
       GuiClicking();

       if (Mouse.current.rightButton.wasPressedThisFrame)
       {
           if (selectedItemGrid == null) { return;}
           CreateItem();

           InventoryItem toInsert = selectedPackage;
           selectedPackage = null;
           InsertItem(toInsert);
       }
   }
   
   /// <summary>
   /// Creates a Package based on packages List. It becomes the selectedPackage.
   /// </summary>
   void CreateItem()
   {
       //Could maybe move this to another script
       InventoryItem item = Instantiate(packagePrefab).GetComponent<InventoryItem>();
       selectedPackage = item;
       packageRectTransform = item.GetComponent<RectTransform>();
       packageRectTransform.SetParent(canvasTransform);
       packageRectTransform.SetAsLastSibling();
       
       item.Set(packages[0]);
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
       {   
           //Picking up item
           selectedPackage = selectedItemGrid.PickUpItem(posOnGrid.x, posOnGrid.y);
           
           if (selectedPackage != null)
           {
               if (shift.inProgress&& otherGrid!=null)
               {
                   Vector2Int? otherPos = otherGrid.FindSpace(selectedPackage);
                   if(otherPos!=null)
                       otherGrid.PlaceItem(selectedPackage,otherPos.Value.x,otherPos.Value.y);
               }
               packageRectTransform = selectedPackage.GetComponent<RectTransform>();
               packageRectTransform.SetParent(canvasTransform);
           }
       }
       else
       {   //Placing item
           bool success = selectedItemGrid.PlaceItemWithChecks(selectedPackage, posOnGrid.x, posOnGrid.y);
           if (success)
           {
               selectedPackage = null;
               //packageRectTransform.SetAsLastSibling();
           }
       }
   }
   bool InsertItem(InventoryItem itemToInsert)
   {
       Vector2Int? gridPos = selectedItemGrid.FindSpace(itemToInsert);

       if (gridPos == null)
       {
           Destroy(itemToInsert.gameObject);
           return false;
       }

       selectedItemGrid.PlaceItem(itemToInsert, gridPos.Value.x, gridPos.Value.y);
       return true;
   }
   
   
}
