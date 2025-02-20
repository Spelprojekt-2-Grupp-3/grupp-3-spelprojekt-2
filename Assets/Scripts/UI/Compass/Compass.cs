using UnityEngine;

public class Compass : MonoBehaviour
{
    public GameObject goal;

    RectTransform rt;

    private Camera camera;
    
    private RectTransform canvasRect;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        camera = Camera.main;
        
        Canvas canvas = transform.GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            canvasRect = canvas.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogError("Compass arrow is not a child of a Canvas.");
        }
    }

    void Update()
    {
        if (canvasRect == null)
            return;
        
        Vector3 targetScreenPos = camera.WorldToScreenPoint(goal.transform.position);
        
        if (targetScreenPos.z < 0)
        {
            targetScreenPos *= -1f;
        }
        
        Camera conversionCamera = (transform.GetComponentInParent<Canvas>().renderMode == RenderMode.ScreenSpaceOverlay)
            ? null : camera;
        
        Vector2 targetLocalPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, targetScreenPos, conversionCamera, out targetLocalPos);
        Vector2 compassLocalPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, transform.position, conversionCamera, out compassLocalPos);
        
        Vector2 direction = (targetLocalPos - compassLocalPos).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        rt.localEulerAngles = new Vector3(0, 0, angle - 90);
    }

    void SwitchQuest(GameObject currentQuest)
    {
        goal = currentQuest;
    }
}
