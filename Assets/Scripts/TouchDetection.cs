using UnityEngine;
using UnityEngine.EventSystems;

public class TouchDetection : MonoBehaviour
{
    [SerializeField] private LayerMask touchableLayer; // Camada que contém os objetos interativos
    [SerializeField] private GameObject interactableObjectMenu; // Menu de interação que será exibido quando um objeto interativo for tocado

    private void Start()
    {
        interactableObjectMenu.SetActive(false); // Desativa o menu de interação no início do jogo
    }

    private void Update()
    {
        // Verifica se há pelo menos um toque na tela e se o toque acabou de começar
        if (Input.touchCount == 0 || Input.GetTouch(0).phase != TouchPhase.Began) return;

        // Verifica se o toque ocorreu em um objeto da UI
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            Debug.Log("Tocou na UI");
            return;
        }

        // Cria um raio a partir da posição do toque na tela e verifica se colide com um objeto interativo
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, touchableLayer))
        {
            // Cria um segundo raio para verificar se há algum objeto da UI na frente do objeto interativo
            Ray uiRay = new Ray(hit.point, -ray.direction);
            if (Physics.Raycast(uiRay, out RaycastHit uiHit, Mathf.Infinity, LayerMask.GetMask("UI")))
            {
                Debug.Log("Tocou na UI");
                return;
            }

            // Se não houver objetos da UI na frente do objeto interativo, ativa o menu de interação e define sua posição na tela
            interactableObjectMenu.SetActive(true);
            interactableObjectMenu.transform.position = Camera.main.WorldToScreenPoint(hit.transform.position);
        }
    }
}