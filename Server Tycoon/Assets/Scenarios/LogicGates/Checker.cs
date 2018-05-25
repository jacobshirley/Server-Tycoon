using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

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
			load();
			answer1 = answers.answer1;
			answer2 = answers.answer2;
			answer3 = answers.answer3;
			answer4 = answers.answer4;
		}

		public void load(){
			answers = JsonUtility.FromJson<Answers>(File.ReadAllText("./Assets/LogicGates/Answers.json"));
		}

		public void check(){

			button1 = script.GetComponent<buttonControl>().out1;
			button2 = script.GetComponent<buttonControl>().out2;
			button3 = script.GetComponent<buttonControl>().out3;
			button4 = script.GetComponent<buttonControl>().out4;

			if(answer1 == button1 && answer2 == button2 && answer3 == button3 && answer4 == button4){
				Debug.Log(true);
			}
			else{
				Debug.Log(false);
			}
		}
}
