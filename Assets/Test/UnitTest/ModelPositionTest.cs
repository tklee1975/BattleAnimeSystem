using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;
using BattleAnimeSystem;

public class ModelPositionTest : BaseTest {
	public GameObject marker;

	public Model attacker;
	public Model target;
	public int attackStyle;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		ShowScreenLog();	
	}

	void MoveMarker(Vector2 pos) {
		marker.gameObject.SetActive(true);
		marker.transform.position = (Vector3) pos + new Vector3(0, 0, -5);
	}

	[Test]
	public void MoveMarker()
	{
		//Debug.Log("###### TEST 1 ######");
		MoveMarker(Vector2.one);
	}

	[Test]
	public void CloseAttackPosition()
	{
		Vector2 pos = attacker.GetCloseAttackPosition(target.GetPosition(), attackStyle);
		UpdateLog("AttackPosition=" + pos);
		AppendLog("TargetPosition=" + target.GetPosition());
		MoveMarker(pos);
	}
}
