using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UIManager : MonoBehaviour

{
    [Header("Elementos do Menu")]
    public Sprite[] welfareStatsImg;
    public Image currentWelfareImg;
    public GameObject[] allFloatingMenus;
    public GameObject floatingPlayMenu;
    public GameObject floatingEdit;
    public GameObject detailsHealthLabel;
    public GameObject detailsMusculatureLabel;
    public GameObject configurePanel;

    [Header("Outras")]
    private EventSystem eventSystem;
    public bool mouseIsOverUI;


    void Update()
    {
        if (TouchOutsideUI() == true)
        {
            DeactiveAllFloatingMenus();
        }
    }

    public void ActiveUIElement(GameObject menuElement)
    {
        menuElement.SetActive(true);
    }


    public void DesactiveUIElement(GameObject menuElement)
    {
        menuElement.SetActive(false);
    }

    public void DeactiveAllFloatingMenus()
    {
        for (int i = allFloatingMenus.Length - 1; i >= 0; i--)
        {
            allFloatingMenus[i].SetActive(false);
        }
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

}
