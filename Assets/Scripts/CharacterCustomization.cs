using UnityEngine;
using TMPro;
using System.Collections;

public class CharacterCustomization : MonoBehaviour
{
    // Estilos de cabelo e barba
    public GameObject[] hairStyle;
    public GameObject[] facialHair;

    public GameObject feedBackMessage;

    // Tempo em segundos para o feedback message desaparecer gradativamente
    public float feedbackFadeTime = 5f;

    // Opacidade inicial do feedback message
    private float feedbackOpacity = 1f;

    // ID do estilo de cabelo atual
    private int hairStyleId;

    // ID do estilo de barba atual
    private int facialHairId;

    // Nome da chave de preferência para o estilo de cabelo
    private const string HAIRSTYLE_PREF = "hairstyle_pref";

    // Nome da chave de preferência para o estilo de barba
    private const string FACIALHAIR_PREF = "facialhair_pref";

    // Variáveis de TextMeshProUGUI para mostrar o valor atual da ID da barba e do cabelo
    public TextMeshProUGUI hairStyleIdText;
    public TextMeshProUGUI facialHairIdText;

    private void Start()
    {
        // Carrega as preferências salvas, ou usa o valor padrão 0
        hairStyleId = PlayerPrefs.GetInt(HAIRSTYLE_PREF, 0);
        facialHairId = PlayerPrefs.GetInt(FACIALHAIR_PREF, 0);

        // Atualiza a visualização dos estilos
        UpdateHairStyleView();
        UpdateFacialHairView();

        // Atualiza o valor atual da ID do cabelo e da barba
        UpdateHairStyleIdText();
        UpdateFacialHairIdText();

        // Ativa ou desativa os estilos de acordo com o ID
        ActivateHairStyle();
        ActivateFacialHair();
    }

    // Atualiza a visualização do estilo de cabelo
    private void UpdateHairStyleView()
    {
        // Desativa todos os estilos de cabelo
        foreach (GameObject hair in hairStyle)
        {
            hair.SetActive(false);
        }

        // Ativa o estilo de cabelo correspondente ao ID
        if (hairStyleId >= 0 && hairStyleId < hairStyle.Length)
        {
            hairStyle[hairStyleId].SetActive(true);
        }
    }

    // Atualiza a visualização do estilo de barba
    private void UpdateFacialHairView()
    {
        // Desativa todos os estilos de barba
        foreach (GameObject facial in facialHair)
        {
            facial.SetActive(false);
        }

        // Ativa o estilo de barba correspondente ao ID
        if (facialHairId >= 0 && facialHairId < facialHair.Length)
        {
            facialHair[facialHairId].SetActive(true);
        }
    }

    // Ativa o estilo de cabelo correspondente ao ID
    private void ActivateHairStyle()
    {
        if (hairStyleId >= 0 && hairStyleId < hairStyle.Length)
        {
            hairStyle[hairStyleId].SetActive(true);
        }
    }

    // Ativa o estilo de barba correspondente ao ID
    private void ActivateFacialHair()
    {
        if (facialHairId >= 0 && facialHairId < facialHair.Length)
        {
            facialHair[facialHairId].SetActive(true);
        }
    }

    // Muda o estilo de cabelo atual
    public void ChangeHairStyle(int value)
    {
        // Incrementa o ID do estilo de cabelo atual
        hairStyleId += value;
        // Se o ID ultrapassar o tamanho do vetor, volta para o último estilo
        if (hairStyleId >= hairStyle.Length)
        {
            hairStyleId = hairStyle.Length - 1;
        }
        // Se o ID for menor que zero, volta para o primeiro estilo
        else if (hairStyleId < 0)
        {
            hairStyleId = 0;
        }

        // Atualiza a visualização do estilo de cabelo
        UpdateHairStyleView();

        // Atualiza o valor atual da ID do cabelo
        UpdateHairStyleIdText();       
    }

    // Muda o estilo de barba atual
    public void ChangeFacialHair(int value)
    {
        // Incrementa o ID do estilo de barba atual
        facialHairId += value;

        // Se o ID ultrapassar o tamanho do vetor, volta para o último estilo
        if (facialHairId >= facialHair.Length)
        {
            facialHairId = facialHair.Length - 1;
        }
        // Se o ID for menor que zero, volta para o primeiro estilo
        else if (facialHairId < 0)
        {
            facialHairId = 0;
        }

        // Atualiza a visualização do estilo de barba
        UpdateFacialHairView();

        // Atualiza o valor atual da ID da barba
        UpdateFacialHairIdText();     
    }

    // Atualiza o valor atual da ID do cabelo na variável TextMeshProUGUI correspondente
    private void UpdateHairStyleIdText()
    {
        hairStyleIdText.text = hairStyleId.ToString("00");
    }

    // Atualiza o valor atual da ID da barba na variável TextMeshProUGUI correspondente
    private void UpdateFacialHairIdText()
    {
        facialHairIdText.text = facialHairId.ToString("00");
    }

    public void SavePreferences()
    {
        PlayerPrefs.SetInt(HAIRSTYLE_PREF, hairStyleId);
        PlayerPrefs.SetInt(FACIALHAIR_PREF, facialHairId);
        PlayerPrefs.Save();
        feedBackMessage.SetActive(true);
        StartCoroutine(FadeOutFeedback());
    }

    // Coroutine para diminuir gradativamente a opacidade do feedback message
    private IEnumerator FadeOutFeedback()
    {
        // Espera um segundo antes de começar a diminuir a opacidade
        yield return new WaitForSeconds(1f);

        // Diminui a opacidade gradativamente ao longo do tempo
        float elapsedTime = 0f;
        while (elapsedTime < feedbackFadeTime)
        {
            feedbackOpacity = Mathf.Lerp(1f, 0f, elapsedTime / feedbackFadeTime);
            feedBackMessage.GetComponent<CanvasGroup>().alpha = feedbackOpacity;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Desativa o feedback message depois que a opacidade atingir zero
        feedBackMessage.SetActive(false);

        // Reseta a opacidade para 1f
        feedbackOpacity = 1f;
    }

    public void CancelChanges()
    {
        // Obtém os valores salvos do PlayerPrefs
        hairStyleId = PlayerPrefs.GetInt(HAIRSTYLE_PREF, 0);
        facialHairId = PlayerPrefs.GetInt(FACIALHAIR_PREF, 0);

        // Atualiza a visualização dos estilos
        UpdateHairStyleView();
        UpdateFacialHairView();

        // Ativa ou desativa os estilos de acordo com o ID
        ActivateHairStyle();
        ActivateFacialHair();
    }


}