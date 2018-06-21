using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadCode : MonoBehaviour
{

    private CodeSnippets codeSnippets;

    [Serializable]
    private class Snippet
    {
        public string story;
        public string code;
    }

    [Serializable]
    private class CodeSnippets
    {
        public Snippet[] snippets;
    }

    public InputField codeInput;
    public Text storyText;

    void Awake()
    {
        codeSnippets = JsonUtility.FromJson<CodeSnippets>(Resources.Load<TextAsset>("JSON/python-snippets").text);
    }

    // Use this for initialization
    void Start()
    {
        int rand = (int)Math.Round(UnityEngine.Random.value * (codeSnippets.snippets.Length - 1));

        Snippet snip = codeSnippets.snippets[rand];

        storyText.text = snip.story;
        codeInput.text = snip.code;
    }

    // Update is called once per frame
    void Update()
    {

    }
}