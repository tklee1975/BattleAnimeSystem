using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class EffectActionTest : BaseTest {
	public AnimeActionManager actionManager;
	public GameObject effectPrefab;

	[Test]
	public void testSimple()
	{
		EffectAction effectAction = new EffectAction();
		effectAction.effectPrefab = effectPrefab;
		effectAction.targetPostion = Vector3.zero;

		actionManager.RunAction(effectAction);
	}

	[Test]
	public void EffectCloud()
	{
		ParallelAction parallel = new ParallelAction();

		int effectCount = 30;

		for(int i=0; i<effectCount; i++) {
			float x = Random.Range(-4f, 4f);
			float y = Random.Range(-2f, 2f);

			EffectAction effectAction = new EffectAction();
			effectAction.effectPrefab = effectPrefab;
			effectAction.repeat = 10;
			effectAction.targetPostion = new Vector3(x, y, -5);
			parallel.AddAction(effectAction);
		}

		actionManager.RunAction(parallel);
	}
}
