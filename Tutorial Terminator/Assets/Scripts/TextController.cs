using System.Collections;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    public TextMeshProUGUI textbox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushText(string text) {
        Debug.Log(text);
        StartCoroutine(AnimateText(text));
    }

    IEnumerator AnimateText(string text)
    {
        int i = 0;
        textbox.text = "";
        while (textbox.text.Length < text.Length)
        {
            Debug.Log(i);
            textbox.text = text.Substring(0,i);
            i++;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
