using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [Header("Welfare Stats")]
    public Image generaWelfareBar;
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

        audioController.playMusic(audioController.mainMenuMusic);
    }

    // Update is called once per frame
    private void Update()
    {
        // Atualizar valores das barras
        generaWelfareBar.fillAmount = (float)currentGeneralWelfare / maxGeneralWelfare;
        feedBar.fillAmount = (float)currentFeed / maxFeed;
        suplementationBar.fillAmount = (float)currentSuplementation / maxSuplementation;
        energyBar.fillAmount = (float)currentEnergy / maxEnergy;
        generalMuscleBar.fillAmount = (float)currentGeneralMuscle / maxGeneralMuscle;
        musculatureBar.fillAmount = (float)currentMusculature / maxMusculature;
        bodyFatBar.fillAmount = (float)currentBodyFat / maxBodyFat;

        // Atualizar valor do texto de ouro
        goldTxt.text = goldCount.ToString();
    }

    private void OnDisable()
    {
        // Define um array com os nomes das chaves dos PlayerPrefs
        string[] keys = new string[] { CurrentGeneralWelfareKey, CurrentFeedKey, CurrentSuplementationKey, CurrentEnergyKey, CurrentGeneralMuscleKey, CurrentMusculatureKey, CurrentBodyFatKey, GoldCountKey };

        // Define um array com as variáveis das barras e do contador de ouro
        object[] values = new object[] { currentGeneralWelfare, currentFeed, currentSuplementation, currentEnergy, currentGeneralMuscle, currentMusculature, currentBodyFat, goldCount };

        // Salva os valores das variáveis nos PlayerPrefs usando um laço de repetição
        for (int i = 0; i < keys.Length; i++)
        {
            PlayerPrefs.SetInt(keys[i], (int)values[i]);
        }

        // Salva os PlayerPrefs imediatamente para garantir que os dados sejam gravados
        PlayerPrefs.Save();
    }
}