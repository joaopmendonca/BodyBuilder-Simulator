using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // O objeto a ser seguido pela câmera
    public float rotateSpeed = 5.0f; // Velocidade de rotação da câmera
    private Vector3 offset; // Distância entre a câmera e o objeto seguido
    private float previousTouchPosition; // Posição do toque anterior
    private float currentTouchPosition; // Posição do toque atual
    private bool isDragging; // Sinaliza se o toque está sendo arrastado

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousTouchPosition = touch.position.x;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                currentTouchPosition = touch.position.x;
                float delta = currentTouchPosition - previousTouchPosition;
                transform.RotateAround(target.position, Vector3.up, delta * rotateSpeed * Time.deltaTime);
                previousTouchPosition = currentTouchPosition;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
    }
}