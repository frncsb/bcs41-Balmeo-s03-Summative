using Godot;
using System;
using System.Diagnostics;

public class Scenes : Node2D
{
    private Sprite _icon;
    private Vector2 _startPosition = new Vector2(-50, 150);
    public override void _Ready()
    {
        //GD.Print("Hello");
        //_icon = GetNode<Sprite>("Sprite");
    }


 // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        /*_icon.Position += new Vector2(100,2) * (float)delta;
        //if(_icon.Position.x > 1000){
           // _icon.Position = _startPosition;
        }*/

        Godot.Sprite child = this.GetNode<Godot.Sprite>("Sprite");
        float amount = 5;
        
        if(Input.IsKeyPressed((int)KeyList.W)){
            child.GlobalPosition += new Vector2(0, -amount);
        }

        if(Input.IsKeyPressed((int)KeyList.S)){
            child.GlobalPosition += new Vector2(0, +amount);
        }

         if(Input.IsKeyPressed((int)KeyList.A)){
            child.GlobalPosition += new Vector2(-amount,0);
        }

         if(Input.IsKeyPressed((int)KeyList.D)){
            child.GlobalPosition += new Vector2(+amount,0);
        }
        //A left,D right,S down
        /*Mini-Activity
        WASD Movement
        */ 
    }
}
