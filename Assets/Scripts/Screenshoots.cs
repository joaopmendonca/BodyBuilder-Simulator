using UnityEngine;
using System.Collections;
using System.IO;

public class Screenshot : MonoBehaviour
{
    public AudioClip screenshotSound; // Som que será tocado ao tirar o print
    public AudioSource soundFxSource;
    // Define a resolução da imagem de saída
    public int resolutionWidth = 512;
    public int resolutionHeight = 512;

    // Define se o fundo da imagem será transparente
    public bool transparentBackground = true;

    // Define a cor de fundo da imagem
    public Color backgroundColor = Color.white;

    // Define o caminho para salvar as screenshots
    public string screenshotPath = "/Screenshots/";

    // Captura a imagem da cena e salva em um arquivo PNG
    public void Capture()
    {
        // Configura a renderização da cena com o fundo transparente, com a cor de fundo escolhida ou com o fundo original
        if (transparentBackground)
        {
            Camera.main.clearFlags = CameraClearFlags.SolidColor;
            Camera.main.backgroundColor = new Color(0, 0, 0, 0);
        }
        else if (backgroundColor != Color.white)
        {
            Camera.main.clearFlags = CameraClearFlags.SolidColor;
            Camera.main.backgroundColor = backgroundColor;
        }
        else
        {
            Camera.main.clearFlags = CameraClearFlags.Skybox;
            Camera.main.backgroundColor = Color.blue;
        }

        // Captura o nome do objeto apontado pelo raycast
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            string objectName = hit.collider.gameObject.name;

            // Captura a imagem renderizada da cena
            RenderTexture rt = new RenderTexture(resolutionWidth, resolutionHeight, 24);
            Camera.main.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resolutionWidth, resolutionHeight, TextureFormat.ARGB32, false);
            Camera.main.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resolutionWidth, resolutionHeight), 0, 0);
            Camera.main.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);

            // Salva a imagem em um arquivo PNG com o nome do objeto + "Sprite"
            string filename = objectName + "Sprite.png";
            string fullPath = Application.dataPath + screenshotPath + filename;
            byte[] bytes = screenShot.EncodeToPNG();
            File.WriteAllBytes(fullPath, bytes);

            // Toca o som ao tirar o print
            soundFxSource.PlayOneShot(screenshotSound);

            // Exibe uma mensagem no console
            StartCoroutine(WaitForFileToSave(fullPath));
        }

        // Restaura a configuração da câmera
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        Camera.main.backgroundColor = Color.blue;
    }

    // Chama o método Capture() quando a tecla "P" é pressionada
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Capture();
        }
    }

    // Aguarda o arquivo ser salvo e exibe mensagem no console
    IEnumerator WaitForFileToSave(string fullPath)
    {
        while (!File.Exists(fullPath))
        {
            yield return null;
        }
        Debug.Log("Screenshot capturada e salva em " + fullPath);
    }
}