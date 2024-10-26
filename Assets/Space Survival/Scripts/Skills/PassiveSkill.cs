using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : MonoBehaviour
{
	public float damage;
	public float projSpeed;
	public float projScale;

	private void Awake()
	{
		GameManager.Instance.player.damage += damage;
		GameManager.Instance.player.projSpeed += projSpeed;
		GameManager.Instance.player.projScale += projScale;
	}
}
