using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOOL;

// 风筝的上升力只跟与风速的相对速度有关系
// 风的速度跟当前所处的高度有正关系
// 风速大体上方向是不会改变的，但是根据时间强弱有所变化
// 风筝线盘影响收放线的效率，好的风筝线盘比较容易放起大的风筝。
// 同的风筝应为质量，受风的面不同，放飞的难度也有所不同
// 风筝一旦放到一定的高度，就基本不会掉下来了

// 大的风筝会非常消耗体力，很难放起来。
// 多数风筝可以同时放起来，比较有视觉冲击力
// 夜晚的时候风筝会发光，这样也可以看得清
// 不同风筝需要不同的地形高度，需要不用的风力大小才能放起来
// 偶尔会出现不同的天气，会造成某些风筝会落下来

// 场景不同的地形会有不同方向上的风，多个风向标，风筝会自动获得周围的风向标，并进行差值融合
// 风筝最大高度为2000m，普通风筝最大高度为200-500m。
// 人一般的奔跑速度为5-10m，风速
// 当风力为2-3级时适合放0.1-0.5平方米的风筝，3-4级为1平方米，5-6级为2平方米以上

/// <summary>
/// Wind.
/// </summary>
public class Wind  : U3DSingleton<Wind> {

	public Vector3 WindDir{
		get{ return transform.forward;}
	}

	public float mSpeed = 1, mOffset = 1; 

	[SerializeField]
	AnimationCurve mWindHeightCurve;

	public Vector3 WindForce(Rigidbody rig){
		float height = rig.transform.position.y;
		Vector3 wind = WindDir * mSpeed * mWindHeightCurve.Evaluate(height);
		Vector3 velocity = rig.velocity;
		Vector3 force = wind + Vector3.up * (velocity - wind).magnitude * mOffset;
		rig.AddForce (force);
		return force;
	}
}
