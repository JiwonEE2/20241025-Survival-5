using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class HealGun : LaserGun
{
	[Tooltip("힐량입니다")]
	public float healAmount;

	public HealProjectile healProjectilePrefab; // 투사체 프리팹

	protected override void Fire()
	{
		HealProjectile proj = LeanPool.Spawn(healProjectilePrefab, transform.position, Quaternion.identity);

		proj.damage = damage;
		proj.moveSpeed = projectileSpeed;
		proj.transform.localScale *= projectileScale;
		proj.pierceCount = pierceCount;

		// HealGun 고유 능력
		proj.healAmount = healAmount;

		LeanPool.Despawn(proj, proj.duration);
	}
}
