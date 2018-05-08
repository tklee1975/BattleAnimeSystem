using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class CharAnimeTest : BaseTest {
	public BattleModel testModel;
	public Vector2 targetPos = Vector2.zero;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		ShowScreenLog();
	}

	[Test]
	public void testHit()
	{
		UpdateLog("Hit Test");
		testModel.Hit(() => {
			AppendLog("Hit Done!");
		});
	}

	[Test]
	public void testAttack()
	{
		UpdateLog("Attack Test");
		testModel.Attack(0, 
						() => { AppendLog("Attack Done!"); },
						() => { AppendLog("Hit Triggered!"); }						
				);
	}

	[Test]
	public void testSkill()
	{
		UpdateLog("Skill Test");
		testModel.Attack(1, 
						() => { AppendLog("Skill Done!"); },
						() => { AppendLog("Hit Triggered!"); }						
				);
	}

	[Test]
	public void testDie()
	{
		UpdateLog("Test Die");

		testModel.Die(() => {
			AppendLog("Die Done");
		});
	}

	[Test]
	public void testMove()
	{
		UpdateLog("Move Test");

		AppendLog("Move Forward");
		testModel.MoveForward(targetPos, () => {
			AppendLog("Move Back");
			testModel.MoveBack(() => {
				AppendLog("Move Done!");
			});
		});
		// UpdateLog("Skill Test");
		// testModel.Attack(1, 
		// 				() => { AppendLog("Hit Triggered!"); }, 
		// 				() => { AppendLog("Skill Done!"); }
		// 		);
	}
}
