using UnityEngine;

public class Compass : MonoBehaviour
{
    public GameObject Goal;

    RectTransform rt;

    private Camera camera;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        camera = Camera.main;
    }

    void Update()
    {
        Vector3 objScreenPos = camera.WorldToScreenPoint(Goal.transform.position);
        
        Vector3 dir = (objScreenPos - rt.position).normalized;
        
        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(dir, Vector3.up));
        
        Vector3 cross = Vector3.Cross(dir, Vector3.up);
        angle = -Mathf.Sign(cross.z) * angle;
        
        rt.localEulerAngles = new Vector3(rt.localEulerAngles.x, rt.localEulerAngles.y, angle);
    }

    void SwitchQuest(GameObject currentQuest)
    {
        Goal = currentQuest;
    }
}
