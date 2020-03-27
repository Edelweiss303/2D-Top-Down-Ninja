using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Image container;
    public Image joystick;
    public Vector3 inputDirection;


    // Start is called before the first frame update
    void Start()
    {
        inputDirection = Vector3.zero;
    }

    public void OnDrag(PointerEventData data)
    {
        Vector2 position = Vector2.zero;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            container.rectTransform,
            data.position,
            data.pressEventCamera,
            out position
            );

        position.x = (position.x + 150)/ container.rectTransform.sizeDelta.x;
        position.y = (position.y + 150)/ container.rectTransform.sizeDelta.y;

        float x = (container.rectTransform.pivot.x == 1.0f) ? position.x * 2 + 1 : position.x * 2 - 1;
        float y = (container.rectTransform.pivot.y == 1.0f) ? position.y * 2 + 1 : position.y * 2 - 1;

        inputDirection = new Vector3(x, y, 0);
        inputDirection = (inputDirection.magnitude > 1) ? inputDirection.normalized : inputDirection;

        joystick.rectTransform.anchoredPosition = new Vector3 (
            inputDirection.x * (container.rectTransform.sizeDelta.x/3),
            inputDirection.y * (container.rectTransform.sizeDelta.y/3) 
            );
    }

    public void OnPointerDown(PointerEventData data)
    {
        OnDrag(data);
    }

    public void OnPointerUp(PointerEventData data)
    {
        inputDirection = Vector3.zero;
        joystick.rectTransform.anchoredPosition = Vector3.zero;
    }
}
