using Godot;
using System;

/// <summary>
/// -base scene
/// --horizontal rotation scene
/// ---vertical rotation scene
/// ----camera
/// </summary>

public partial class FPSController : CharacterBody3D{
	[Export()] public float mouseSensitivity = 100.0f;
	[Export()] public float moveSpeed = 20.0f;

	[Export()] public NodePath vRotPath;
	private Node3D vRot;

	[Export()] public NodePath hRotPath;
	private Node3D hRot;

	[Export()] public NodePath cameraPath;
	private Camera3D camera;
	private float delta;
	private Vector3 movement;
	private Vector3 rotationDegrees;
	
	public override void _Ready(){
		if(vRotPath != null) vRot = GetNode(vRotPath) as Node3D;
		if(hRotPath != null) hRot = GetNode(hRotPath) as Node3D;
		if(cameraPath != null) camera = GetNode(cameraPath) as Camera3D;
	}

	public override void _Process(double delta){
		this.delta = (float)delta;

		movement.x = 0;
		movement.y = 0;
		movement.z = 0;
		if (Input.IsPhysicalKeyPressed(Key.W)){
			movement -= hRot.GlobalTransform.basis.z;
		}
		if (Input.IsPhysicalKeyPressed(Key.S)){
			movement += hRot.GlobalTransform.basis.z;
		}
		if (Input.IsPhysicalKeyPressed(Key.A)){
			movement -= hRot.GlobalTransform.basis.x;
		}
		if (Input.IsPhysicalKeyPressed(Key.D)){
			movement += hRot.GlobalTransform.basis.x;
		}

		Velocity = movement * (float)delta * moveSpeed;
		MoveAndSlide();
	}

	public override void _Input(InputEvent @event){
		if(@event is InputEventMouseMotion){
			InputEventMouseMotion motion = @event as InputEventMouseMotion;
			
			hRot.RotateY( Mathf.DegToRad(-motion.Relative.x * mouseSensitivity * delta));
			vRot.RotateX(Mathf.DegToRad(-motion.Relative.y * mouseSensitivity * delta));
			rotationDegrees = vRot.RotationDegrees;
			rotationDegrees.x = Mathf.Clamp(rotationDegrees.x, -70, 70);
			vRot.RotationDegrees = rotationDegrees;
		}
	}
}
