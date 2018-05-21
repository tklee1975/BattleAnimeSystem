using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class EffectAnimeTest : BaseTest {

	public IEffectAnimePlayer testPlayer;
	public IEffectAnimePlayer particlePlayer;
	public GameObject testObject;
	public GameObject particleObject;
	public EffectParticle effectParticle;
	[Range(1, 7)] public int effectID;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		testPlayer = new EffectAnimePlayerAnimator(testObject);

		ShowScreenLog();
	} 

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		testPlayer.Update(Time.deltaTime);
	}

	[Test]
	public void testEffectParticle() {
		effectParticle.Play(
			() => { AppendLog("End"); }, 
			() => { AppendLog("Hit"); }
		);
	}

	[Test]
	public void testParticle() {
		testPlayer = new EffectAnimePlayerParticleSystem(particleObject);
		testPlayer.PlayOnce(
			() => { AppendLog("End"); }, 
			() => { AppendLog("Hit"); }
		);
	}

	[Test]
	public void test1()
	{
		testPlayer.setEffectID(1);
		testPlayer.PlayForever();
	}

	[Test]
	public void test2()
	{
		testPlayer.setEffectID(2);
		testPlayer.PlayForever();
	}

	[Test]
	public void PlayOnce()
	{
		UpdateLog("Testing PlayOnce");
		testPlayer.setEffectID(2);
		testPlayer.PlayOnce(
			() => { AppendLog("End"); }, 
			() => { AppendLog("Hit"); }
		);
	}

	[Test]
	public void PlayRepeat()
	{
		UpdateLog("Testing Three Times");
		testPlayer.setEffectID(2);
		testPlayer.PlayRepeat(3, 
			() => { AppendLog("End"); }, 
			() => { AppendLog("Hit"); }
		);
	}

	[Test]
	public void MoveRight()
	{
		UpdateLog("Testing Move");
		testPlayer.setEffectID(4);
		testPlayer.Move(Vector3.zero, Vector3.one * 3, 5.0f, 
			() => { AppendLog("Move End"); }
		);
	}

	[Test]
	public void MoveLeft()
	{
		UpdateLog("Testing Move");
		testPlayer.setEffectID(4);
		testPlayer.Move(Vector3.zero, Vector3.zero + new Vector3(-5, 1, 0), 0.5f, 
			() => { AppendLog("Move End"); }
		);
	}

	[Test]
	public void TestEffect()
	{
		UpdateLog("Testing Effect " + effectID);
		testPlayer.setEffectID(effectID);
		testPlayer.PlayForever();
	}
}
