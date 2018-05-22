using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class EffectDemo : BaseTest {
	public short attackStyle = 0;

	[Header("Team Formation")]
	public Model[] leftTeam;
	public Model[] rightTeam;

	[Header("Projectile & Effect")]
	public GameObject[] effectPrefab;


	[Header("Effects")]
	public AnimationClip[] skillEffect;
	public AnimationClip[] slashEffect;

	[Header("Other Setting")]
	public GameObject damageTextPrefab;

	const float zOrderVfx = -3;

	public AnimeAction CreateEffectAttack(Model actor, Model target,
						 bool isMoving, GameObject effectObj) 
	{
		
		return CreateAttackAction(actor, target, attackStyle, 
							isMoving, CreateHitDamageAction(target, effectObj));
		
	}

	public ModelAttackAction CreateAttackAction(Model actor, Model target, 
							short style, bool isMoving, AnimeAction onHitAction) {
		ModelAttackAction attackAction = new ModelAttackAction();
		attackAction.actor = actor;
		attackAction.style = style;
		attackAction.isMoving = isMoving;
		attackAction.postionType = ModelAttackAction.PositionType.UseModel;
		attackAction.targetModel = target;
		attackAction.onHitAction = onHitAction;
		
		return attackAction;
	}


	public AnimeAction CreateHitDamageAction(Model targetModel, 
					GameObject hitEffect=null) {
		ParallelAction hitDamagePack = new ParallelAction();

		hitDamagePack.name = "HitDamage";


		ModelHitAction hitAction = new ModelHitAction();
		hitAction.name = "enemyHit";
		hitAction.actor = targetModel;
		hitDamagePack.AddAction(hitAction);

		GameTextAction damageAction = new GameTextAction();
		damageAction.textPrefab = damageTextPrefab;
		damageAction.text = 1000.ToString();
		damageAction.spawnPostion = targetModel.transform.position + new Vector3(0, 2, -2);

		hitDamagePack.AddAction(damageAction);

		if(hitEffect == null) {
			return hitDamagePack;
		}
		// 


		// 
		Effect effect = hitEffect.GetComponent<Effect>();
		PositionType posType = effect == null ? PositionType.Ground : effect.positionType;

		EffectAction effectAction = EffectAction.CreatePointEffect(hitEffect,
											 targetModel.GetPositionByType(posType) );
											 
		effectAction.onHitAction = hitDamagePack;									 
		return effectAction;									 
	}


	[Test]
	public void MeleeEffect()
	{
		SequenceAction sequence = new SequenceAction();

		bool isMoving = true;
		foreach(GameObject effect in effectPrefab) {
			AnimeAction action = CreateEffectAttack(rightTeam[1], leftTeam[0], isMoving, effect);
			sequence.AddAction(action);
		}

		AnimeActionManager.Instance.RunAction(sequence);
	}

	[Test]
	public void RangeEffect()
	{
		SequenceAction sequence = new SequenceAction();

		bool isMoving = false;
		foreach(GameObject effect in effectPrefab) {
			AnimeAction action = CreateEffectAttack(rightTeam[0], leftTeam[1], isMoving, effect);
			sequence.AddAction(action);
		}

		AnimeActionManager.Instance.RunAction(sequence);
	}
}
