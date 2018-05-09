using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class ModelActionTest : BaseTest {
	[Range(0, 3)] public int actorIndex = 0;
	[Range(0, 3)] public int targetIndex = 1;

	public Model[] modelList; 

	[Header("Hit Setting")]
	public bool dieWhenHit = false;

	[Header("Attack Setting")]
	public short attackStyle = 0;
	public Vector2 targetPosition = Vector2.zero;
	public bool isMoving = false;

	Model GetModel(int index) {
		if(modelList == null || index >= modelList.Length) {
			return null;
		}

		return modelList[index];
	}

	Model GetActor() {
		return GetModel(actorIndex);
	}

	Model GetTarget() {
		return GetModel(targetIndex);
	}

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		ShowScreenLog();
		AnimeActionManager.Instance.Reset();
	}

	AnimeAction GetOnHitAction() {
		if(dieWhenHit) {
			return ModelDieAction.Create(GetTarget());
		} else {
			return ModelHitAction.Create(GetTarget());
		}
	}

	[Test]
	public void AttackToPosition()
	{
		UpdateLog("Attack to a position");

		AnimeAction onHitAction = GetOnHitAction();

		ModelAttackAction action = ModelAttackAction.CreateAttackToPos(
							GetActor(), attackStyle, isMoving, targetPosition, onHitAction);

		AnimeActionManager.Instance.StartAction(action);
	}

	[Test]
	public void AttackToModel()
	{
		UpdateLog("Attack to a model");

		AnimeAction onHitAction = GetOnHitAction();

		ModelAttackAction action = ModelAttackAction.CreateAttackToModel(
							GetActor(), attackStyle, isMoving, GetTarget(), onHitAction);

		AnimeActionManager.Instance.StartAction(action);
	}

	[Test]
	public void KillAModel()
	{
		UpdateLog("Kill to a model");

		AnimeAction onHitAction = ModelDieAction.Create(GetTarget());

		ModelAttackAction action = ModelAttackAction.CreateAttackToModel(
							GetActor(), attackStyle, isMoving, GetTarget(), onHitAction);

		AnimeActionManager.Instance.StartAction(action);
	}

	[Test]
	public void Resurrect()
	{
		foreach(Model model in modelList) {
			ModelResurrectAction action = ModelResurrectAction.Create(model);
			AnimeActionManager.Instance.StartAction(action);
		}
	}
}
