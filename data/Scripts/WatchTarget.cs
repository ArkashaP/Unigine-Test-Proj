using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using Unigine;
// ало ало Виктория на связи
[Component(PropertyGuid = "475fce5a9a1456239d694c3cdc5c1b4e8d6485c2")]
public class WatchTarget : Component
{
	public Node target;

	public bool activated = false;
	float activation_timer = MathLib.RandFloat(10f, 50f);
	BodyRigid br;
	dvec3 look_point;
	dvec3 look_target;
	void Init()
	{
		Console.Onscreen = true;
		Visualizer.Enabled = true;
		br = node.ObjectBodyRigid;
	}
	// Comment
	void activate(){
		activated = true;
	}

	void LockAt(dvec3 pos){

	}

	void Update()
	{
		if(!activated && Game.Time > activation_timer) activated = true; 
		Visualizer.RenderLine3D(
			node.WorldBoundSphere.Center,
			look_point,
			vec4.RED/2
		);

		double distance = MathLib.Distance(node.WorldBoundSphere.Center, target.WorldBoundBox.Center);
		look_point = node.WorldBoundSphere.Center + node.GetWorldDirection()*MathLib.Clamp(distance, 10.0d, 100.0d);
		look_target = target.WorldBoundBox.Center;
	}
	void UpdatePhysics(){
		if(!activated) return;
		
		br.AddWorldImpulse(br.Position, new vec3(0,0,0.8f));

		vec3 look_delta = new vec3(look_target - look_point);
		
		br.AddWorldForce(
			look_point,
			look_delta*4
		);
	}
}