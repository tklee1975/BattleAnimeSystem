using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class CharMoveTest : BaseTest {
	public BattleModel hero;
	public Animator target;
	public GameObject hitValuePrefab;

	public Vector2 targetPos;
	public AnimeActionManager actionManager;


	public AnimeAction GetTargetHitDamageAction() {
		SequenceAction sequence = new SequenceAction();
		sequence.name = "HitSequence";

		AnimatorAction hitAction = new AnimatorAction();
		hitAction.name = "enemyHit";
		hitAction.animator = target;
		hitAction.triggerState = "Hit";
		sequence.AddAction(hitAction);


		HitValueAction damageAction = new HitValueAction();
		damageAction.valueTextPrefab = hitValuePrefab;
		damageAction.hitValue = 1000;
		damageAction.position = target.transform.position + new Vector3(0, 1, -2);
		sequence.AddAction(damageAction);

		return sequence;
	}

	[Test]
	public void AttackStyle0() 
	{		
		ModelAttackAction attackAction = new ModelAttackAction();
		attackAction.actor = hero;
		attackAction.style = 0;
		attackAction.isMoving = true;
		attackAction.targetPostion = target.transform.position + new Vector3(1, 0, -2);
		attackAction.onHitAction = GetTargetHitDamageAction();

		actionManager.RunAction(attackAction);
	}

	[Test]
	public void AttackStyle1() 
	{		
		ModelAttackAction attackAction = new ModelAttackAction();
		attackAction.actor = hero;
		attackAction.style = 1;
		attackAction.isMoving = false;
		attackAction.onHitAction = GetTargetHitDamageAction();

		actionManager.RunAction(attackAction);
	}

	[Test]
	public void AttackAction() 
	{
		ModelAttackAction action = new ModelAttackAction();
		action.actor = hero;
		action.targetPostion = target.transform.position;

		actionManager.RunAction(action);
		// action. = animeClip;
		// action.spawnPosition = new Vector3(0, 0, zOrderVfx);
		// action.repeat = 3;
		// action.destroySelf = false;

		// actionManager.RunAction(action);
	}

	[Test]
	public void AttackChain()
	{
		Vector2 endPos = new Vector2(-3, 1);

		AnimeCallback hitCallback = () => {
			Debug.Log("Attack hit!");
		};


		AnimeCallback endCallback = () => {
			Debug.Log("Attack Chain End!");
		};

		hero.MoveForward(endPos, () => {
			hero.Attack(1, ()=> {
				hero.MoveBack(endCallback);
			}, hitCallback);
		});
	}

	[Test]
	public void Hit()
	{
		AnimeCallback endCallback = () => {
			Debug.Log("Hit End!");
		};
		hero.Hit(endCallback);
	}

	[Test]
	public void Attack()
	{
		AnimeCallback hitCallback = () => {
			Debug.Log("Attack hit!");
		};

		AnimeCallback endCallback = () => {
			Debug.Log("Attack End!");
		};


		hero.Attack(1, endCallback, hitCallback);
	}


	[Test]
	public void MoveTo()
	{
		hero.MoveTo(targetPos, 0.4f, null);
	}

	[Test]
	public void TestMove()
	{
		Vector2 endPos = hero.transform.position.x >= 0 ? new Vector2(-3, 1) : new Vector2(0, 0);
		hero.Move(endPos);
	}

}
