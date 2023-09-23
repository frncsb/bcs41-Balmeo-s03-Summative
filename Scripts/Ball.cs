using Godot;
using System;

public class Ball : KinematicBody2D
{
	float speed =  600f; //Ball Speed
	Vector2 velocity = Vector2.Zero; //Ball Velocity

	 public override void _Ready()
	{
		GD.Randomize();//----------------------> Takes a random value from -1 & 1, from -0.8 & 0.8, determines trajectory
		velocity.x = new float[] { -1f, 1f }[GD.Randi() % 2];
		velocity.y = new float[] { -0.8f, 0.8f }[GD.Randi() % 2];

	}

	 public override void _Process(float delta)
	{
	  var collision_obj = MoveAndCollide(velocity * speed * delta);//----------------------> Ball Collision Physics

	  if (collision_obj != null){ //----------------------> Bounce Physics
		velocity = velocity.Bounce(collision_obj.Normal);
		
		//Collision Sound Effect
		AudioStreamPlayer hit = GetNode<AudioStreamPlayer>("BallHit");
		hit.Play();
	  }
	}
	
	public void stopBall(){ //----------------------> Stops Ball from Moving
		speed = 0;
	}
	public void restartBall(){//----------------------> Re-initialize values
		speed = 600f;
		velocity.x = new float[] { -1f, 1f }[GD.Randi() % 2];
		velocity.y = new float[] { -0.8f, 0.8f }[GD.Randi() % 2];
	}

}
