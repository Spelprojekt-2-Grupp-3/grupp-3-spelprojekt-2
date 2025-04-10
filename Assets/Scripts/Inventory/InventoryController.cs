
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class InventoryController : MonoBehaviour
{
   [SerializeField] private bool devving;
    
   [HideInInspector] public ItemGrid selectedGrid;
   [HideInInspector] public ItemGrid otherGrid;
   private ItemGrid prevGrid;
   [SerializeField, Tooltip("Reference to the main inventory, the one that contains the player's things")] public ItemGrid mainGrid;
   
   private InventoryItem selectedPackage;
   private InventoryItem overlapItem;
   private RectTransform packageRectTransform;

   private PlayerInputActions playerControls;
   private InputAction lClick;
   private InputAction rClick;
   private InputAction cancel;
   private InputAction confirm;
   private InputAction shift;
   private InputAction pointerPosition;
   private InputAction middleClick;
   private InputAction markerMovement;
   //private InputAction openInventory;

   private bool isOpen;

   //[HideInInspector]public List<InventoryItem> inventoryItems;
   
   [SerializeField, Tooltip("For randomly generated packages, currently only dev functions")] private List<PackageData> packageTypes;
   [SerializeField, Tooltip("The object to instantiate")] private GameObject packagePrefab;
   [SerializeField, Tooltip("Reference to the canvas that holds the grid")] private Transform canvasTransform;
   //[SerializeField, Tooltip("Reference to the submenu")]private GameObject subMenuObject;
   private SubMenu subMenu;

   private Vector2Int markerPosition;
   [SerializeField] private float markerMoveCooldown;
   private float actualCooldown;
   [SerializeField] private GameObject slotHighlightObject;

   //[SerializeField] private DialogueManager dialogueManager;

   private void Awake()
   {
       playerControls = new PlayerInputActions();
       //subMenu = subMenuObject.GetComponent<SubMenu>();
   }

   private void Start()
   {
       markerPosition = mainGrid.FirstSlot();
       mainGrid.gameObject.SetActive(false);
       DisableControls();
   }

   void CreateControls()
   {
       lClick = playerControls.UI.Click;
       rClick = playerControls.UI.RightClick;
       confirm = playerControls.UI.Submit;
       cancel = playerControls.UI.Cancel;
       shift = playerControls.UI.ModifierButton;
       pointerPosition = playerControls.UI.Point;
       middleClick = playerControls.UI.MiddleClick;
       markerMovement = playerControls.UI.Navigate;
   }
   
   private void OnEnable()
   {
       CreateControls();
       EnableControls();
   }

   private void OnDisable()
   {
       DisableControls();
   }

   public void EnableControls()
   {
       lClick.Enable();
       rClick.Enable();
       cancel.Enable();
       confirm.Enable();
       shift.Enable();
       middleClick.Enable();
       pointerPosition.Enable();
       markerMovement.Enable();
   }

   public void DisableControls()
   {
       lClick.Disable();
       rClick.Disable();
       cancel.Disable();
       shift.Disable();
       confirm.Disable();
       middleClick.Disable();
       pointerPosition.Disable();
       markerMovement.Disable();
   }

   public void SetEnableState(bool state)
   {
       if(state)
           EnableControls();
       else 
           DisableControls();
       mainGrid.gameObject.SetActive(state);
   }
   
   
   private void Update()
   {
       if(!markerMovement.enabled){return;}
       
       //item drag
       if (selectedPackage != null)
       {
           Vector2 temp = markerPosition;
           packageRectTransform.position = temp;
       }
           
       if ((cancel.WasPressedThisFrame()|| rClick.WasPressedThisFrame()) && selectedPackage!=null && prevGrid!=null)
           CancelPickup();
       
       GuiMovement();

       if (mainGrid != null)
       {
           GuiClicking();
       }

       if (selectedGrid != null)
       {
           MouseGuiClicking();
       }
   }
   
  
   /// <summary>
   /// Creates a Package based on packages List. It becomes the selectedPackage.
   /// </summary>
   /// <param name="recipient">A list of recipients</param>
   public void CreateRandomItem(List<string> recipients)
   {
       if(selectedPackage!=null)
           return;
       //Could maybe move this to another script
       InventoryItem item = Instantiate(packagePrefab).GetComponent<InventoryItem>();
       selectedPackage = item;
       packageRectTransform = item.GetComponent<RectTransform>();
       packageRectTransform.SetParent(canvasTransform);
       packageRectTransform.SetAsLastSibling();
       
       item.Set(packageTypes[Random.Range(0,packageTypes.Count)]);
       item.packageData.recipient = recipients[Random.Range(0,recipients.Count)];
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
       if (confirm.WasPressedThisFrame())
       {
           ControllerInteractClick();
       }

       if (cancel.WasPressedThisFrame())
       {
           ControllerAltInteract();
       }
       
       //Dev thingies
       if (devving && middleClick.WasPressedThisFrame())
       {
           if (mainGrid == null) { return;}

           CreateRandomItem(new List<string>());  
           if (shift.inProgress)
           {
               InventoryItem toInsert = selectedPackage;
               InsertItem(toInsert);
           }
       }
   }

   void MouseGuiClicking()
   {
       if (lClick.WasPressedThisFrame())
       {
           InteractClick();
       }
       
       if (rClick.WasPressedThisFrame())
       {
           AltInteractClick();
       }
   }
   
   void GuiMovement()
   {
       if (actualCooldown<markerMoveCooldown)
       {
           actualCooldown += Time.deltaTime;
       }
       Vector2 direction = markerMovement.ReadValue<Vector2>();
       if (direction != Vector2.zero && actualCooldown >= markerMoveCooldown)
       {
           markerPosition = mainGrid.NextSlot(markerPosition, new Vector2Int((int)direction.x, (int)direction.y));
           actualCooldown = 0;
       }
       

       Vector2 vector = markerPosition;
       slotHighlightObject.GetComponent<RectTransform>().position = vector;
   }
   
   void InteractClick()
   {
       //TODO: change mousePosition to markerPosition
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
               //if (shift.inProgress)
               //{ 
               //    FindOtherGrid(); //maybe move this
               //    if (otherGrid != null)
               //    {
               //        Vector2Int? otherPos = otherGrid.FindSpace(selectedPackage);
               //        if (otherPos == null) return;
               //        otherGrid.PlaceItem(selectedPackage, otherPos.Value.x, otherPos.Value.y);
               //        Debug.Log("moved to other grid");
               //        selectedPackage = null;
               //        prevGrid = null;
               //    }
               //}
               //else
               {
                   prevGrid = selectedGrid;
                   packageRectTransform = selectedPackage.GetComponent<RectTransform>();
                   packageRectTransform.SetParent(canvasTransform);
               }
               
           }
       }
       //Placing item
       else
       {   
           bool success = selectedGrid.PlaceItemWithChecks(selectedPackage, posOnGrid.x, posOnGrid.y);
           if (success)
           {
               selectedPackage = null;
               prevGrid = null;
               //packageRectTransform.SetAsLastSibling();
           }
       }
   }
   void AltInteractClick()
   {
       Vector2 mousePosition = Mouse.current.position.ReadValue();
       Vector2Int posOnGrid = selectedGrid.GetTileGridPosition(mousePosition);
       
       if (selectedPackage == null)
       {
           InventoryItem hoveredItem = selectedGrid.GetItem(posOnGrid.x, posOnGrid.y);
           //open alternatives gui, like package info and drop package or smt
           //ubMenu.ShowInformation(hoveredItem.packageData);
           Debug.Log("'Opened' sub-menu");
       }
   }

   void ControllerAltInteract()
   {
       Vector2Int posOnGrid = mainGrid.GetTileGridPosition(markerPosition);
       if (selectedPackage == null)
       {
           InventoryItem hoveredItem = mainGrid.GetItem(posOnGrid.x, posOnGrid.y);
           //open alternatives gui, like package info and drop package or smt
           //subMenu.ShowInformation(hoveredItem.packageData);
           Debug.Log("'Opened' sub-menu");
       }
   }

   void ControllerInteractClick()
   {
       Vector2Int posOnGrid = mainGrid.GetTileGridPosition(markerPosition);

       //Picking up item
       if (selectedPackage == null)
       {
           selectedPackage = mainGrid.PickUpItem(posOnGrid.x, posOnGrid.y);

           if (selectedPackage != null)
           {
               prevGrid = mainGrid;
               packageRectTransform = selectedPackage.GetComponent<RectTransform>();
               packageRectTransform.SetParent(canvasTransform);
           }
       }
       //Placing item
       else
       {   
           bool success = mainGrid.PlaceItemWithChecks(selectedPackage, posOnGrid.x, posOnGrid.y);
           if (success)
           {
               selectedPackage = null;
               prevGrid = null;
               //packageRectTransform.SetAsLastSibling();
           }
       }
   }
   
   void CancelPickup()
   {
       prevGrid.PlaceItem(selectedPackage, selectedPackage.onGridPosition.x, selectedPackage.onGridPosition.y);
       selectedPackage = null;
       prevGrid = null;
   }
   
   /// <summary>
   /// Inserts item into grid
   /// </summary>
   /// <param name="itemToInsert">what item is attempted to be inserted</param>
   /// <returns>true if successful, false if not</returns>
   public bool InsertItem(InventoryItem itemToInsert)
   {
       Vector2Int? gridPos = mainGrid.FindSpace(itemToInsert);
       mainGrid.SetAsParent(itemToInsert);

       if (gridPos == null)
       {
           return false;
       }

       mainGrid.PlaceItem(itemToInsert, gridPos.Value.x, gridPos.Value.y);
       selectedPackage = null;
       return true;
   }
   
   /// <summary>
   /// Creates a new item and attempts to inserts it into the main inventory
   /// </summary>
   /// <param name="data">what data the created package should have</param>
   /// <returns>true if successfully inserted, false if not</returns>
   public bool InsertNewItem(PackageData data)
   {
       CreateItem(data);
       
       Vector2Int? gridPos = mainGrid.FindSpace(selectedPackage);

       if (gridPos == null)
       {
           Destroy(selectedPackage.gameObject);
           selectedPackage = null;
           return false;
       }
       mainGrid.PlaceItem(selectedPackage, gridPos.Value.x, gridPos.Value.y);
       selectedPackage = null;
       return true;
   }

   /// <summary>
   /// finds candidates for and sets otherGrid
   /// </summary>
   void FindOtherGrid()
   {
       ItemGrid[] grids = FindObjectsOfType<ItemGrid>();
       for (int i = 0; i < grids.Length; i++)
       {
           if (grids[i] != selectedGrid)
           {
               otherGrid = grids[i];
               Debug.Log("Found other grid");
               return;
           }
       }
   }
}
