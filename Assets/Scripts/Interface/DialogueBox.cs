using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    static DialogueBox Instance;
    public Text Text;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Hide();
    }

    public static void Show(string displayText)
    {
        Instance.DoShow(displayText);
    }

    void DoShow(string displayText)
    {
        GetComponent<Image>().enabled = true;
        GetComponentInChildren<Text>().enabled = true;
        Text.text = displayText;
    }

    public static void Hide()
    {
        Instance.DoHide();
    }

    void DoHide()
    {
        GetComponent<Image>().enabled = false;
        GetComponentInChildren<Text>().enabled = false;
        Text.text = "";
    }

}
