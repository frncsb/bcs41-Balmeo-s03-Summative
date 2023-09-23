using Godot;
using System;
using System.Security.Cryptography;
using System.Xml.Serialization;

public class Level : Node
{

	private int PlayerScore = 0, OpponenetScore = 0;
	private string User = "", Result = "";


	private void _on_Left_body_entered(object body){ //----------------------> on left side Computer Scores
		osMechanics();
	}

	private void _on_Right_body_entered(object body){ //----------------------> on right side Player Scores
		psMechanics();
	}

		private void _on_CountdownTimer_timeout() //----------------------> Timer
	{
		GetTree().CallGroup("BallGroup","restartBall"); //---------------------->References group, calls restart Function
		Label cdLabel = GetNode<Label>("cdLabel");
		cdLabel.Visible = false;
	}
	
	private void _on_Username_text_changed(object body){
		//
	}
    public override void _Input(InputEvent @event) //---------------------->Inputs User/Player Data
    {
        if (Input.IsKeyPressed((int)KeyList.Enter)){
			LineEdit player = GetNode<LineEdit>("Username");
			User = player.Text;
			player.Visible = false;
			GD.Print(User);
			SaveDataToFile(User.Replace("USER: ", ""),PlayerScore,Result);
		}
    }

    public override void _Process(float delta)
	{
		Label playerScore = GetNode<Label>("PlayerScore");
		Label opponentScore = GetNode<Label>("ComputerScore");

		//Set Score
		playerScore.Text = PlayerScore.ToString(); 
		opponentScore.Text = OpponenetScore.ToString();

		//Reset
		Timer count = GetNode<Timer>("CountdownTimer");
		Label cdLabel = GetNode<Label>("cdLabel");
		
		int newValue = (int)count.TimeLeft + 1;
		cdLabel.Text = newValue.ToString();
	}


	//Player Score Mechanics
	private void psMechanics(){
		Node2D ball = GetNode<Node2D>("Ball");
		ball.Position = new Vector2(640,360);//---------------------->Ball is position at the center
		PlayerScore++; //---------------------->add 1 point to Score

		GetTree().CallGroup("BallGroup","stopBall"); //----------------------> References from Group, calls stop Function

		//----------------------> A Countdown Timer every time ball is socred
		Timer count = GetNode<Timer>("CountdownTimer");
		count.Start();
		Label cdLabel = GetNode<Label>("cdLabel");
		cdLabel.Visible = true;

		//----------------------> Play Sound Effect
		AudioStreamPlayer score = GetNode<AudioStreamPlayer>("ScoreSound");
		score.Play();

		//----------------------> To Adjust Player Movement, Computer Difficulty, & Win Condition
		Computer computer = GetNode<Computer>("Computer");
		MyPlayer movement = GetNode<MyPlayer>("Player");

		if (PlayerScore == 3 ){
			Label level = GetNode<Label>("levelLabel");
			level.Text = "LEVEL: AVERAGE";
			computer.changeSpeed(400f); 
		}
		if (PlayerScore == 8 ){
			Label level = GetNode<Label>("levelLabel");
			level.Text = "LEVEL: PRO";
			computer.changeSpeed(420f); 
		}
		if (PlayerScore == 11){
			WinCondition("Player");
			GetTree().CallGroup("BallGroup","stopBall");
			count.Stop();
			cdLabel.Visible = false;
			LineEdit player = GetNode<LineEdit>("Username");
			player.Visible = true;
			movement.disableMove();
			Result = "Win";
		}
	}

	//Opponent Score Mechanics
	private void osMechanics(){
		Node2D ball = GetNode<Node2D>("Ball");
		ball.Position = new Vector2(640,360); //---------------------->Ball is position at the center
		OpponenetScore++; //---------------------->add 1 point to Score
		
		GetTree().CallGroup("BallGroup","stopBall"); //----------------------> References from Group, calls stop Function

		//----------------------> A Countdown Timer every time ball is socred
		Timer count = GetNode<Timer>("CountdownTimer");
		count.Start();
		Label cdLabel = GetNode<Label>("cdLabel");
		cdLabel.Visible = true;
		
		//----------------------> Play Sound Effect
		AudioStreamPlayer score = GetNode<AudioStreamPlayer>("ScoreSound");
		score.Play();

		//----------------------> To Adjust Player Movement & Win Condition
		MyPlayer movement = GetNode<MyPlayer>("Player");
		if (OpponenetScore == 11){
			WinCondition("Computer");
			GetTree().CallGroup("BallGroup","stopBall");
			count.Stop();
			cdLabel.Visible = false;
			LineEdit player = GetNode<LineEdit>("Username");
			player.Visible = true;
			Result = "Lose";
			movement.disableMove();
		}
	}

	//----------------------> Announces the Winner
	private string WinCondition(string winner){
		Label level = GetNode<Label>("levelLabel");
		return level.Text = winner + " wins!";
	}

	//Database
	private void SaveDataToFile(string username, int score, string result){
    var filePath = "C://Users//Balmeo//Documents//bcs41-Balmeo//s03//database.txt"; //----------------------> path of file

    //Open the file in "read" mode to check if it exists
    var fileExists = System.IO.File.Exists(filePath);

    using (var writer = new System.IO.StreamWriter(filePath, true))
    {
        if (!fileExists)
        {
            //If the file doesn't exist, write a header or any initial content
            writer.WriteLine("name, score, result");
        }

        var dataLine = "name="+username + ", score=" + score + ", result=" + result;
        writer.WriteLine(dataLine);
    }
}

}



