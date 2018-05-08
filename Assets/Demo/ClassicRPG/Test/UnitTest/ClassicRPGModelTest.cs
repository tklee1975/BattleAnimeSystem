using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class ClassicRPGModelTest : BaseTest {
	public Model testModel1;
	public Model testModel2;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		ShowScreenLog();
	}

	[Test]
	public void testAttack()
	{
		UpdateLog("Test Attack");
		testModel1.Attack(1, 			
			() => { AppendLog("Attack End"); },
			() => { AppendLog("On Hit"); } 			
		);
	}

	[Test]
	public void testMove()
	{
		testModel1.MoveForward(testModel2.GetPosition(), 
			() => { 
				AppendLog("Move Fwd FEnd"); 
				testModel1.MoveBack(
					() => {
						 AppendLog("Move Back End"); 
					});			
			}
		);
	}

	[Test]
	public void testMoveAttack()
	{
		AnimeCallback callbackBack = () => {
			AppendLog("Move Back End"); 
		};
		
		AnimeCallback callbackAttack = () => {
			testModel1.MoveBack(callbackBack);
		};

		AnimeCallback callbackHit = () => {
			testModel2.Hit();
		};


		testModel1.MoveForward(testModel2.GetPosition() + new Vector2(1, 0), 
			() => { 
				AppendLog("Move Fwd FEnd"); 
				testModel1.Attack(1, callbackHit, callbackAttack);					
			}
		);
	}
}
