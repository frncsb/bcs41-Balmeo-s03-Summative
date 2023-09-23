using Godot;
using System;

public class MyPlayer : KinematicBody2D
{

	float speed = 500f; //----------------------> Player Paddle Speed

	public override void _Ready()
	{
		
	}

	public override void _Process(float delta)
	{
		//Key Input Movements
		Vector2 velocity = Vector2.Zero;
		if (Input.IsKeyPressed((int)KeyList.W)){
			velocity.y -= 1;
		}
		 if (Input.IsKeyPressed((int)KeyList.S)){
			velocity.y += 1;
		}
		MoveAndSlide(velocity * speed); //Paddle Physics
	}

	public void disableMove(){ //----------------------> Disables movement when there exists a winner.
		speed = 0;
	}

}
