using UnityEngine;

/* MainCamera
 * определяет положение маски стрелок
 */


public class MovingAimArrow : MonoBehaviour
{
    Vector3 posAim;
    [SerializeField] private Transform ArrowAim;
    [SerializeField] Transform ArrowAimBooster;
    [SerializeField] private Transform PlayerPos;

    public void Moving()
    {
        posAim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ArrowAim.position = posAim;
        ArrowAim.localPosition = Vector2.ClampMagnitude(new Vector2(ArrowAim.localPosition.x, 0), 22f);
    }
}
