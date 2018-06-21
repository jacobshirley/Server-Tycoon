using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Checker : MonoBehaviour {

		public Answers answers;
		public GameObject submit;

		public GameObject script;

		private bool answer1;
		private bool answer2;
		private bool answer3;
		private bool answer4;

		private bool button1;
		private bool button2;
		private bool button3;
		private bool button4;

		void Start(){
			Debug.Log("Started");
			load();
			answer1 = answers.answer1;
			answer2 = answers.answer2;
			answer3 = answers.answer3;
			answer4 = answers.answer4;
			Debug.Log("Answers loaded");
		}

		public void load(){
			answers = JsonUtility.FromJson<Answers>(Resources.Load<TextAsset>("JSON/logic-gates-answers").text);
		}

		public void check(){

			button1 = script.GetComponent<buttonControl>().out1;
			button2 = script.GetComponent<buttonControl>().out2;
			button3 = script.GetComponent<buttonControl>().out3;
			button4 = script.GetComponent<buttonControl>().out4;

			if(answer1 == button1 && answer2 == button2 && answer3 == button3 && answer4 == button4){
				SceneManager.LoadScene("Passed");
			}
			else{
				GameObject.Find("Attempts").GetComponent<attempts>().Attempted();
			}
		}
}
