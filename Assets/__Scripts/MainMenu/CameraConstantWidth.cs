using UnityEngine;


public class CameraConstantWidth : MonoBehaviour
{
    public Vector2 DefaultResolution = new Vector2(1440, 720);
    [Range(0f, 1f)] public float WidthOrHeight = 0;

    public GameObject field;

    private Camera componentCamera;
    
    private float initialSize;
    private float targetAspect;
    private Vector3 scaleField;



    private void Start()
    {
        componentCamera = GetComponent<Camera>();
        initialSize = componentCamera.orthographicSize;
        scaleField = field.transform.localScale;
        targetAspect = DefaultResolution.x / DefaultResolution.y;
    }

    private void Update()
    {
        if (componentCamera.orthographic)
        {
            float constantWidthSize = initialSize * (targetAspect / componentCamera.aspect);
            componentCamera.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, WidthOrHeight);
            AspectField();
        }
    }

    // размер поля боя
    void AspectField()
    {
        float ratio;
        if (initialSize > componentCamera.orthographicSize)
        {
            ratio = initialSize / componentCamera.orthographicSize;
        } else
        {
            ratio = componentCamera.orthographicSize/initialSize;
        }

        field.transform.localScale = new Vector3(scaleField.x , scaleField.y*ratio, scaleField.z);
    }

}