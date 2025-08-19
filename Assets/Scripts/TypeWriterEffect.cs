using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public string fullText;
    public float typingSpeed = 0.05f;
    public bool IsFinished { get; private set; }

    private TextMeshProUGUI textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void Play(string message)
    {
        fullText = message;
        StopAllCoroutines();
        textMesh.text = "";
        IsFinished = false;
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in fullText)
        {
            textMesh.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        IsFinished = true;
    }
}
