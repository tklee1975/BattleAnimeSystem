using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class GameTextActionTest : BaseTest {
	public AnimeActionManager actionManager;
	public GameObject hitPrefab;
	public GameObject buffPrefab;

	[Test]
	public void DamageText()
	{
		GameTextAction action = new GameTextAction();
		action.text = "123456";
		action.textPrefab = hitPrefab;
		action.textStyle = GameText.Style.Damage;

		actionManager.RunAction(action);
	}

	[Test]
	public void ParallelDamage()
	{
		ParallelAction parallel = new ParallelAction();

		GameTextAction action = new GameTextAction();
		action.text = "123456";
		action.textPrefab = hitPrefab;
		action.textStyle = GameText.Style.Damage;
		parallel.AddAction(action);

		action = new GameTextAction();
		action.text = "123456";
		action.textPrefab = hitPrefab;
		action.spawnPostion = new Vector3(2, 2, -3);
		action.textStyle = GameText.Style.Damage;
		parallel.AddAction(action);

		actionManager.RunAction(parallel);
	}


}
