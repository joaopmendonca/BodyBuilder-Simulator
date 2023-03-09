using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("Welfare Stats")]
    public Image generalWelfareBar;
    public int currentGeneralWelfare;
    public int maxGeneralWelfare;
    public Image feedBar;
    public int currentFeed;
    public int maxFeed;
    public Image suplementationBar;
    public int currentSuplementation;
    public int maxSuplementation;
    public Image energyBar;
    public int currentEnergy;
    public int maxEnergy;

    [Header("Musculature Stats")]
    public Image generalMuscleBar;
    public int currentGeneralMuscle;
    public int maxGeneralMuscle;
    public Image musculatureBar;
    public int currentMusculature;
    public int maxMusculature;
    public Image bodyFatBar;
    public int currentBodyFat;
    public int maxBodyFat;

    [Header("Outros")]
    public AudioController audioController;
    public TextMeshProUGUI goldTxt;
    public int goldCount;

    // Identificadores dos valores salvos com PlayerPrefs
    private const string CurrentGeneralWelfareKey = "currentGeneralWelfare";
    private const string CurrentFeedKey = "currentFeed";
    private const string CurrentSuplementationKey = "currentSuplementation";
    private const string CurrentEnergyKey = "currentEnergy";
    private const string CurrentGeneralMuscleKey = "currentGeneralMuscle";
    private const string CurrentMusculatureKey = "currentMusculature";
    private const string CurrentBodyFatKey = "currentBodyFat";
    private const string GoldCountKey = "goldCount";

    // Start is called before the first frame update    
    void Start()
    {
        // Carregar valores das variáveis current dos PlayerPrefs
        currentGeneralWelfare = PlayerPrefs.GetInt(CurrentGeneralWelfareKey);
        currentFeed = PlayerPrefs.GetInt(CurrentFeedKey);
        currentSuplementation = PlayerPrefs.GetInt(CurrentSuplementationKey);
        currentEnergy = PlayerPrefs.GetInt(CurrentEnergyKey);
        currentGeneralMuscle = PlayerPrefs.GetInt(CurrentGeneralMuscleKey);
        currentMusculature = PlayerPrefs.GetInt(CurrentMusculatureKey);
        currentBodyFat = PlayerPrefs.GetInt(CurrentBodyFatKey);
        goldCount = PlayerPrefs.GetInt(GoldCountKey);

        // Definir valor de maxGeneralMuscle e currentGeneralMuscle conforme especificado
        maxGeneralMuscle = (maxMusculature + maxBodyFat) / 2;
        currentGeneralMuscle = (currentMusculature + currentBodyFat) / 2;

        // Definir valor de maxGeneralWelfare conforme especificado
        maxGeneralWelfare = (maxFeed + maxSuplementation + maxEnergy) / 3;

        audioController.playMusic(audioController.mainMenuMusic);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateUI();
    }
    private void OnDisable()
    {
        // Atualiza os valores das variáveis de musculatura
        maxGeneralMuscle = (maxMusculature + maxBodyFat) / 2;
        currentGeneralMuscle = (currentMusculature + currentBodyFat) / 2;

        // Atualiza os valores das variáveis de bem-estar geral
        maxGeneralWelfare = (maxFeed + maxSuplementation + maxEnergy) / 3;
        currentGeneralWelfare = (currentFeed + currentSuplementation + currentEnergy) / 3;

        // Salva os valores das variáveis nos PlayerPrefs
        PlayerPrefs.SetInt(CurrentGeneralWelfareKey, currentGeneralWelfare);
        PlayerPrefs.SetInt(CurrentFeedKey, currentFeed);
        PlayerPrefs.SetInt(CurrentSuplementationKey, currentSuplementation);
        PlayerPrefs.SetInt(CurrentEnergyKey, currentEnergy);
        PlayerPrefs.SetInt(CurrentGeneralMuscleKey, currentGeneralMuscle);
        PlayerPrefs.SetInt(CurrentMusculatureKey, currentMusculature);
        PlayerPrefs.SetInt(CurrentBodyFatKey, currentBodyFat);
        PlayerPrefs.SetInt(GoldCountKey, goldCount);

        // Salva os PlayerPrefs imediatamente para garantir que os dados sejam gravados
        PlayerPrefs.Save();
    }

    private void UpdateUI()
    {
        // Limita os valores das estatísticas musculares para seus máximos definidos em max
        currentGeneralWelfare = Mathf.Clamp(currentGeneralWelfare, 0, maxGeneralWelfare);
        currentFeed = Mathf.Clamp(currentFeed, 0, maxFeed);
        currentSuplementation = Mathf.Clamp(currentSuplementation, 0, maxSuplementation);
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        currentGeneralMuscle = Mathf.Clamp(currentGeneralMuscle, 0, maxGeneralMuscle);
        currentMusculature = Mathf.Clamp(currentMusculature, 0, maxMusculature);
        currentBodyFat = Mathf.Clamp(currentBodyFat, 0, maxBodyFat);

        // Atualiza os valores das barras
        generalWelfareBar.fillAmount = (float)currentGeneralWelfare / maxGeneralWelfare;
        feedBar.fillAmount = (float)currentFeed / maxFeed;
        suplementationBar.fillAmount = (float)currentSuplementation / maxSuplementation;
        energyBar.fillAmount = (float)currentEnergy / maxEnergy;

        // Atualiza os valores das estatísticas de bem-estar geral
        int generalWelfareSum = maxGeneralWelfare - maxFeed - maxSuplementation - maxEnergy;
        maxGeneralWelfare = generalWelfareSum + maxFeed + maxSuplementation + maxEnergy;
        currentGeneralWelfare = Mathf.RoundToInt((float)(currentFeed + currentSuplementation + currentEnergy) / 3);
        currentGeneralWelfare = Mathf.Clamp(currentGeneralWelfare, 0, maxGeneralWelfare);

        // Atualiza os valores das estatísticas musculares
        int muscleSum = maxGeneralMuscle - maxMusculature - maxBodyFat;
        maxGeneralMuscle = muscleSum + maxMusculature + maxBodyFat;
        currentGeneralMuscle = Mathf.RoundToInt((float)(currentMusculature + currentBodyFat) / 2);
        currentGeneralMuscle = Mathf.Clamp(currentGeneralMuscle, 0, maxGeneralMuscle);

        // Atualiza os valores das barras de estatísticas musculares
        generalMuscleBar.fillAmount = (float)currentGeneralMuscle / maxGeneralMuscle;
        musculatureBar.fillAmount = (float)currentMusculature / maxMusculature;
        bodyFatBar.fillAmount = (float)currentBodyFat / maxBodyFat;

        // Atualiza o texto do contador de ouro
        goldTxt.SetText(goldCount.ToString());
    }
}