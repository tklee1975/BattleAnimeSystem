using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleTDD;

public class BMFontTest : BaseTest {
	public SimpleAnimation textAnime;

	[Test]
	public void DamageText()
	{
		textAnime.Stop();
		textAnime.Play();
	}

	[Test]
	public void test2()
	{
		Debug.Log("###### TEST 2 ######");
	}
}
