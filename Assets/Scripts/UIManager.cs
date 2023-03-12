using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private LayerMask touchableLayer;
    [SerializeField] private GameObject interactableObjectMenu;
    [SerializeField] private AudioController audioController;

    [Header("UI Elements")]
    public Sprite[] welfareStatsImg;
    public Image currentWelfareImg;
    public GameObject[] allFloatingMenus;
    public GameObject floatingPlayMenu;
    public GameObject floatingEdit;
    public GameObject detailsHealthLabel;
    public GameObject detailsMusculatureLabel;
    public GameObject configurePanel;

    [Header("Other Variables")]
    private EventSystem eventSystem;
    public bool mouseIsOverUI;
    private bool contextMenuOpen = false;
    private bool touchOnInteractiveObject = false;
    private bool touchMovedCamera = false;

    private void Start()
    {
        interactableObjectMenu.SetActive(false);
        eventSystem = FindObjectOfType<EventSystem>();
    }

    private void Update()
    {
        if (TouchOutsideUI())
        {
            DeactivateAllFloatingMenus();
        }

        if (Input.touchCount == 0)
        {
            ResetTouchVariables();
            return;
        }

        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            touchOnInteractiveObject = false;
            touchMovedCamera = false;

            if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId)) // check if touch happens outside of UI elements
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, touchableLayer))
                {
                    touchOnInteractiveObject = true;
                    Invoke("CheckTouchMovedCamera", 0.25f);
                }
            }
        }
        else if (touch.phase == TouchPhase.Moved && touchOnInteractiveObject)
        {
            touchMovedCamera = true;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            if (!touchMovedCamera && touchOnInteractiveObject)
            {
                OpenInteractiveMenu();
            }

            ResetTouchVariables();
        }
    }

    private void CheckTouchMovedCamera()
    {
        if (!touchMovedCamera && touchOnInteractiveObject)
        {
            OpenInteractiveMenu();
        }

        ResetTouchVariables();
    }

    private void ResetTouchVariables()
    {
        touchOnInteractiveObject = false;
        touchMovedCamera = false;
        CancelInvoke();
    }

    public void ActivateUIElement(GameObject menuElement)
    {
        menuElement.SetActive(true);
    }

    public void DeactivateUIElement(GameObject menuElement)
    {
        menuElement.SetActive(false);
    }

    public void DeactivateAllFloatingMenus()
    {
        for (int i = allFloatingMenus.Length - 1; i >= 0; i--)
        {
            allFloatingMenus[i].SetActive(false);
        }
        contextMenuOpen = false;
    }

    public bool TouchOutsideUI()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public void OpenInteractiveMenu()
    {
        audioController.PlaySound(audioController.menuConfirm);
        DeactivateAllFloatingMenus();
        interactableObjectMenu.SetActive(true);
        interactableObjectMenu.transform.position = Input.GetTouch(0).position;
    } 
}