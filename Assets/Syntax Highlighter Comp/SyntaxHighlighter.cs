using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SyntaxHighlighter : MonoBehaviour {
    public GameObject errorPanel;
    public GameObject codeCorrectPanel;

    private ScriptEngine engine = Python.CreateEngine();
    private static string SCRIPT = File.ReadAllText("./Assets/Syntax Highlighter Comp/run.py");

    delegate void Listener();
    ScriptSource source;

    private string input = "";
    private int actualCaret = 0;

    private List<Listener> listeners = new List<Listener>();
    public void Highlight()
    {
        
        source = engine.CreateScriptSourceFromString(SCRIPT);

        //code = Regex.Replace(code, "<.*?>", string.Empty);

        
    }

    // Use this for initialization
    void Start () {
        var paths = engine.GetSearchPaths();
        paths.Add("C:/Users/Jacob/Documents/Unity/Server Tycoon/Assets/Scripts/unity-python/Lib");
        engine.SetSearchPaths(paths);
        
        Highlight();
	}
	
	// Update is called once per frame
	void Update () {
        bool b = false;
        if (Input.GetKey(KeyCode.Tab))
        {
            input += "\t";
            b = true;
        }
        else if (Input.inputString != "" || Input.inputString == "\n" || Input.inputString == "\r" || Input.inputString == "\b")
        {
            InputField inputField = this.GetComponent<InputField>();
            int caretPos = inputField.caretPosition;

            b = true;
            if (Input.inputString == "\b")
            {
                if (input.Length > 0 && inputField.caretPosition > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    inputField.caretPosition -= 1;
                }
            }
            else
            {
                if (input.Length > 0)
                {
                    int diff = inputField.text.Length - input.Length;
                    Debug.Log(diff);
                    int actPos = caretPos - diff;
                    Debug.Log("Act: ");
                    Debug.Log((caretPos - inputField.text.Length));
                    input += Input.inputString;// input.Substring(0, actPos + 1) + Input.inputString + input.Substring(actPos, input.Length - actPos);
                }
                else
                {
                    input += Input.inputString;
                }
                //input += Input.inputString;
                inputField.caretPosition += 1;
            }
        }
        if (b)
        {
            //Debug.Log(input);
            InputField inputField = this.GetComponent<InputField>();
                var scope = engine.CreateScope();

            string code = (string)input.Clone();
               // code = Regex.Replace(code, "<.*?>", string.Empty);

            scope.SetVariable("code", "s" + code); //adding any old random character to the start as iron python has issues with strings

            source.Execute(scope);

            var highlighted = scope.GetVariable<string>("highlighted");

            if (code.Length > 0)
            {
                input = input.Replace("\r", "\n");
                int i = 0;

                while (code.StartsWith("\n") || code.StartsWith("\t"))
                {
                    Debug.Log("starting");
                    code = code.Substring(1, code.Length - 1);
                    i++;
                }

                string leadingWhitespace = input.Substring(0, i);

                if (i > 0)
                {
                    highlighted = leadingWhitespace + highlighted;
                }

                code = (string)input.Clone();
                i = 0;
                while (code.EndsWith("\n") || code.EndsWith("\t"))
                {
                    code = code.Substring(0, code.Length - 1);
                    i++;
                }

                
                string endWhitespace = input.Substring(input.Length - i, i);

                if (i > 0)
                {
                    highlighted += endWhitespace;
                }
            }

            inputField.text = highlighted;
            if (input.Length == 1)
                inputField.caretPosition = highlighted.Length - 1;
        }
	}
}
