using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class PythonSyntaxChecker : MonoBehaviour {

    class ErrorReporter : ErrorListener
    {
        public List<string> Errors = new List<string>();

        public ErrorReporter() {
        }

        public override void ErrorReported(ScriptSource source, string message, SourceSpan span, int errorCode, Severity severity)
        {
            Errors.Add(message);
        }

        public int Count
        {
            get { return Errors.Count; }
        }
    }

    public GameObject errorPanel;
    public GameObject codeCorrectPanel;

    private ScriptEngine engine = Python.CreateEngine();

    public void VerifyCode(InputField inputField)
    {
        var scope = engine.CreateScope();

        string code = inputField.text;
        code = Regex.Replace(code, "<.*?>", string.Empty);

        var source = engine.CreateScriptSourceFromString(code);

        var errors = new ErrorReporter();
        CompiledCode result = source.Compile(errors);

        Debug.Log(errors.Count);

        if (errors.Count > 0)
            errorPanel.SetActive(true);
        else
            codeCorrectPanel.SetActive(true);
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
