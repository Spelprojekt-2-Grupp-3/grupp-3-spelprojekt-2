
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

//[RequireComponent(typeof(PlayerInput))]
public class InventoryController : MonoBehaviour
{ 
   [HideInInspector] public ItemGrid selectedGrid;
   [HideInInspector] public ItemGrid otherGrid;
   
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

   [SerializeField]private ItemGrid mainGrid;

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
       CreateRandomItem();
   }

   private void Update()
   {
       //item drag
       if (selectedPackage != null)
           packageRectTransform.position = Mouse.current.position.ReadValue();
       if(selectedGrid==null){return;}
       GuiClicking();
   }
   
   /// <summary>
   /// Creates a Package based on packages List. It becomes the selectedPackage.
   /// </summary>
   /// <param name="package">which package in the list should be generated</param>
   public void CreateRandomItem()
   {
       if(selectedPackage!=null)
           return;
       //Could maybe move this to another script
       InventoryItem item = Instantiate(packagePrefab).GetComponent<InventoryItem>();
       selectedPackage = item;
       packageRectTransform = item.GetComponent<RectTransform>();
       packageRectTransform.SetParent(canvasTransform);
       packageRectTransform.SetAsLastSibling();
       
       item.Set(packages[Random.Range(0,packages.Count)]);
   }

   /// <summary>
   /// Creates a package from outside of the packages List. It becomes the selectedPackage.
   /// </summary>
   /// <param name="packageData">Data for the package</param>
   public void CreateItem(PackageData packageData)
   {
       if(selectedPackage!=null)
           return;
       //Could maybe move this to another script
       InventoryItem item = Instantiate(packagePrefab).GetComponent<InventoryItem>();
       selectedPackage = item;
       packageRectTransform = item.GetComponent<RectTransform>();
       packageRectTransform.SetParent(canvasTransform);
       packageRectTransform.SetAsLastSibling();
       
       item.Set(packageData);
   }
   void GuiClicking()
   {
       //TODO: Controller support
       if (Mouse.current.leftButton.wasPressedThisFrame)
       {
           InteractClick();
       }
       
       //Quick-Insert Item
       if (Mouse.current.rightButton.wasPressedThisFrame)
       {
           if (selectedGrid == null) { return;}
           CreateRandomItem();

           if (shift.inProgress)
           {
               InventoryItem toInsert = selectedPackage;
               selectedPackage = null;
               InsertItem(toInsert);
           }
       }
   }
   void InteractClick()
   {
       Vector2 mousePosition = Mouse.current.position.ReadValue();
       
       //Corrects position of package
       if (selectedPackage != null)
       {
           mousePosition.x -= (selectedPackage.packageData.gridSize.x - 1) * ItemGrid.tileSizeWidth / 2;
           mousePosition.y += (selectedPackage.packageData.gridSize.y - 1) * ItemGrid.tileSizeHeight / 2;
       }
           
       Vector2Int posOnGrid = selectedGrid.GetTileGridPosition(mousePosition);

       //Picking up item
       if (selectedPackage == null)
       {
           selectedPackage = selectedGrid.PickUpItem(posOnGrid.x, posOnGrid.y);
           
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
       //Placing item
       else
       {   
           bool success = selectedGrid.PlaceItemWithChecks(selectedPackage, posOnGrid.x, posOnGrid.y);
           if (success)
           {
               selectedPackage = null;
               //packageRectTransform.SetAsLastSibling();
           }
       }
   }
   
   /// <summary>
   /// old InsertItem, remains for dev purposes
   /// </summary>
   /// <param name="itemToInsert">what item is attempted to be inserted</param>
   /// <returns>true if successful, false if not</returns>
   bool InsertItem(InventoryItem itemToInsert)
   {
       Vector2Int? gridPos = selectedGrid.FindSpace(itemToInsert);

       if (gridPos == null)
       {
           Destroy(itemToInsert.gameObject);
           return false;
       }

       selectedGrid.PlaceItem(itemToInsert, gridPos.Value.x, gridPos.Value.y);
       return true;
   }

   
   /// <summary>
   /// Creates and inserts an item into the main inventory
   /// </summary>
   /// <param name="data"></param>
   /// <returns></returns>
   public bool InsertItem(PackageData data)
   {
       CreateItem(data);
       
       Vector2Int? gridPos = mainGrid.FindSpace(selectedPackage);

       if (gridPos == null)
       {
           Destroy(selectedPackage.gameObject);
           return false;
       }
       
       mainGrid.PlaceItem(selectedPackage, gridPos.Value.x, gridPos.Value.y);
       return true;

   }
}
