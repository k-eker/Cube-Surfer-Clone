using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private GameObject touchToPlayText;

    [Range(0.0001f, 0.001f)] [SerializeField]
    private float m_DragSpeed = 1f;

    private const float MIN_POS_X = -0.17f;
    private const float MAX_POS_X = 0.14f;

    Vector3 m_LastPosition;



    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float targetX = transform.position.x + touch.deltaPosition.x * m_DragSpeed;
                transform.position = new Vector3(Mathf.Clamp(targetX, MIN_POS_X, MAX_POS_X), transform.position.y,
                    transform.position.z);
            }
            else if (!touchToPlayText.activeSelf && touch.phase == TouchPhase.Began)
            {
                touchToPlayText.SetActive(false);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            m_LastPosition = Input.mousePosition;
            if (touchToPlayText.activeSelf)
            {

                touchToPlayText.SetActive(false);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 deltaPositon = Input.mousePosition - m_LastPosition;

            float targetX = transform.position.x + deltaPositon.x * m_DragSpeed;
            transform.position = new Vector3(Mathf.Clamp(targetX, MIN_POS_X, MAX_POS_X), transform.position.y,
                transform.position.z);

            m_LastPosition = Input.mousePosition;
        }
    }

}
