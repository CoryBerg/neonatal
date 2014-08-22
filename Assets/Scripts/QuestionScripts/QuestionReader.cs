using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionReader : MonoBehaviour {
	
	
	public GameObject questionGUI;
	public GameObject aAGUI;
	public GameObject aBGUI;
	public GameObject aCGUI;
	public GameObject aDGUI;
	
	public bool aPressed = false;
	public bool bPressed = false;
	public bool cPressed = false;
	public bool dPressed = false;
	
	public int qNumber = 0;
	
	public Question[] arrayOfQuestions;
	
	// a question class with constructors
	public class Question{
		public string questionText;
		public string answerAText;
		public string answerBText;
		public string answerCText;
		public string answerDText;
		
		public Question(string qText, string aAText, string aBText, string aCText, string aDText){
			questionText = qText;
			answerAText = aAText;
			answerBText = aBText;
			answerCText = aCText;
			answerDText = aDText;
		}
		
		public Question(string qText, string aAText, string aBText, string aCText){
			questionText = qText;
			answerAText = aAText;
			answerBText = aBText;
			answerCText = aCText;
		}
		
		public Question(){
			questionText = "qText";
			answerAText = "aAText";
			answerBText = "aBText";
			answerCText = "aCText";
			answerDText = "aDText";
		}
	}
	
	// Initialize question class array
	void Start(){
		arrayOfQuestions = new Question[7];
		
		arrayOfQuestions [0] = new Question("Number of post graduate training years", "One", "Two", "Three", "Four and beyond");
		arrayOfQuestions [1] = new Question("Current Career goals", "Gen Peds", "Outpatient subspecialty care", "Inpatient subspecialty care", "Other");
		arrayOfQuestions [2] = new Question("Current comfort level in managing  a patient with a tension pneumothorax?", "Very comfortable", "Somewhat comfortable", "Somewhat uncomfortable", "Very uncomfortable");
		arrayOfQuestions [3] = new Question ("Current comfort level with Neonatal Emergencies", "Very comfortable", "Somewhat comfortable", "Somewhat uncomfortable", "Very uncomfortable");
		arrayOfQuestions [4] = new Question ("Current level of medical knowledge pertaining to NICU", "Above average", "Average", "Below average");
		arrayOfQuestions [5] = new Question ("Level of interest in pursuing a career in neonatology", "Very interested", "Somewhat interested", "Somewhat disinterested", "Very disinterested");
		arrayOfQuestions [6] = new Question ("Number of previous participation in simulation experiences", "One", "Two", "Three or more", "None");
		
		// Initialize descriptions of the first question
		questionGUI.GetComponent<dfLabel> ().Text = arrayOfQuestions [qNumber].questionText;
		aAGUI.GetComponent<dfButton> ().Text = arrayOfQuestions [qNumber].answerAText;
		aBGUI.GetComponent<dfButton> ().Text = arrayOfQuestions [qNumber].answerBText;
		aCGUI.GetComponent<dfButton> ().Text = arrayOfQuestions [qNumber].answerCText;
		aDGUI.GetComponent<dfButton> ().Text = arrayOfQuestions [qNumber].answerDText;
	}
	
	void Update(){
		// Check if a button is pressed
		if (aPressed) {
			if (qNumber >= 6) {
				LoadLevel();
			}
			else {
				NextQuestion();
				aPressed = false;
			}
		}
		
		if (bPressed) {
			if (qNumber >= 6) {
				LoadLevel();
			}
			else {
				NextQuestion();
				bPressed = false;
			}
		}
		
		if (cPressed) {
			if (qNumber >= 6) {
				LoadLevel();
			}
			else {
				NextQuestion();
				cPressed = false;
			}
		}
		
		if (dPressed) {
			if (qNumber >= 6) {
				LoadLevel();
			}
			else {
				NextQuestion();
				dPressed = false;
			}
		}
	}
	
	void NextQuestion(){
		qNumber++;

		questionGUI.GetComponent<dfLabel> ().Text = arrayOfQuestions [qNumber].questionText;
		aAGUI.GetComponent<dfButton> ().Text = arrayOfQuestions [qNumber].answerAText;
		aBGUI.GetComponent<dfButton> ().Text = arrayOfQuestions [qNumber].answerBText;
		aCGUI.GetComponent<dfButton> ().Text = arrayOfQuestions [qNumber].answerCText;
		aDGUI.GetComponent<dfButton> ().Text = arrayOfQuestions [qNumber].answerDText;
	}
	
	void LoadLevel(){
		Application.LoadLevel ("IntroRespCase");
	}
}