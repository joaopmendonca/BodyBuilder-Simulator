using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // O objeto a ser seguido pela c�mera
    public float rotateSpeed = 5.0f; // Velocidade de rota��o da c�mera
    private Vector3 offset; // Dist�ncia entre a c�mera e o objeto seguido
    private float previousTouchPosition; // Posi��o do toque anterior
    private float currentTouchPosition; // Posi��o do toque atual
    private bool isDragging; // Sinaliza se o toque est� sendo arrastado

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