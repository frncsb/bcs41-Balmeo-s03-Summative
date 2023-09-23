using Godot;
using System;

public class Computer : KinematicBody2D
{

	public float Speed { get; set; } = 250f; //----------------------> Initial Computer AI speed
	private Node2D ball;

	public override void _Ready()
	{ 
	   ball = GetParent().GetNode<Node2D>("Ball"); //Instance of Object from Parent Node
	}

	public override void _Process(float delta){
		MoveAndSlide(new Vector2(0, get_opponent_direction()) * Speed); //Computer AI Paddle Physics
	}

	public int get_opponent_direction(){ //----------------------> Computer Follows Ball Position
	 if (Math.Abs(ball.Position.y - Position.y) > 25)
	{
		if (ball.Position.y > Position.y)
		{
			return 1;
		}
		else
		{
			return -1;
		}
	}
	return 0; 

	}

	public void changeSpeed(float newSpeed){
		Speed = newSpeed;
		GD.Print("Current: " + Speed);
	}

}
