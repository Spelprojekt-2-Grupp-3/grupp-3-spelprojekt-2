using UnityEngine;

public class Compass : MonoBehaviour
{
    public GameObject goal;

    RectTransform rt;

    private Camera camera;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        camera = Camera.main;
    }

    void Update()
    {
        Vector3 objScreenPos = camera.WorldToScreenPoint(goal.transform.position);
        
        Vector3 dir = (objScreenPos - rt.position).normalized;

        if (objScreenPos.z < 0)
        {
            dir = -dir;
        }
        
        float angle = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
        
        rt.localEulerAngles = new Vector3(0, 0, angle - 90);
    }

    void SwitchQuest(GameObject currentQuest)
    {
        goal = currentQuest;
    }
}
