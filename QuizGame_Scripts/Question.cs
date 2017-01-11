
[System.Serializable]

public class Question 
{
	public string fact;
	public bool isTrue;


	public void setFact (string question)
	{
		fact = question;
	}
	public void setAnswer (string answer)
	{
		if(answer.Equals ("true"))
			{
				isTrue = true;
			}else{
				isTrue = false;
			}
	}
}


