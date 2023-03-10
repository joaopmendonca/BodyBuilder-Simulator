using UnityEngine;

public class StatManager : MonoBehaviour
{
    public int feedDecayRate = 1;
    public int suplementationDecayRate = 1;
    public int energyDecayRate = 1;
    public int musculatureDecayRate = 1;
    public int bodyFatDecayRate = 1;

    public MainMenuController mainMenuController;

    void Start()
    {
        // Inicia a redu��o das vari�veis de bem-estar em intervalos regulares
        InvokeRepeating("ReduceStats", 1f, 1f);
    }

    void ReduceStats()
    {
        // Reduz os valores das vari�veis de bem-estar com base nas informa��es do MainMenuController
        mainMenuController.currentFeed -= feedDecayRate;
        mainMenuController.currentSuplementation -= suplementationDecayRate;
        mainMenuController.currentEnergy -= energyDecayRate;
        mainMenuController.currentMusculature -= musculatureDecayRate;
        mainMenuController.currentBodyFat -= bodyFatDecayRate;

        // Faz algo com as vari�veis de bem-estar atualizadas, por exemplo:
        Debug.Log("Current Feed: " + mainMenuController.currentFeed);
        Debug.Log("Current Suplementation: " + mainMenuController.currentSuplementation);
        Debug.Log("Current Energy: " + mainMenuController.currentEnergy);
        Debug.Log("Current Musculature: " + mainMenuController.currentMusculature);
        Debug.Log("Current Body Fat: " + mainMenuController.currentBodyFat);
    }
}