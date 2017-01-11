﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Question[] questions;
	private static List<Question> unansweredQuestions;
	private Question currentQuestion;
	[SerializeField]
	private Text factText;

	[SerializeField]
	private float timeBetweenQuestions = 3f;

	[SerializeField]
	private Text trueAnswerText;
	[SerializeField]
	private Text falseAnswerText;

	[SerializeField]
	private Animator animator;


	void Start()
	{
		if (unansweredQuestions == null || unansweredQuestions.Count == 0) 
		{
			unansweredQuestions = questions.ToList<Question>();
		}

		SetCurrentQuestion ();


	}

	void SetCurrentQuestion()
	{
		int randomQuestionIndex = Random.Range (0, unansweredQuestions.Count);
		currentQuestion = unansweredQuestions [randomQuestionIndex];

		factText.text = currentQuestion.fact;

		if (currentQuestion.isTrue) {
			trueAnswerText.text = "CORRECT!";
			falseAnswerText.text = "WRONG!";
		} else {
			trueAnswerText.text = "WRONG!";
			falseAnswerText.text = "CORRECT!";
		}


	}

	IEnumerator TransistionToNextQuestion ()
	{
		unansweredQuestions.Remove(currentQuestion);

		yield return new WaitForSeconds (timeBetweenQuestions);

		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void UserSelectTrue()
	{
		animator.SetTrigger ("True");
		if (currentQuestion.isTrue) {
			Debug.Log ("CORRECT!");
		} else {
			Debug.Log ("WRONG!");
		}
		StartCoroutine (TransistionToNextQuestion ());

	}

	public void UserSelectFalse()
	{
		animator.SetTrigger ("False");
		if (!currentQuestion.isTrue) {
			Debug.Log ("CORRECT!");
		} else {
			Debug.Log ("WRONG!");
		}
		StartCoroutine (TransistionToNextQuestion ());
	}


	public void LoadQuestionList()
	{
		using (StreamReader reader = new StreamReader("yourfile.txt")) {
			string line = null;
			while (null != (line = reader.ReadLine ())) {
				string[] values = line.Split (',');
				Question temp;
				temp.setFact (values [0]);
				temp.setAnswer (values[1]);


			}
	}
		
}
