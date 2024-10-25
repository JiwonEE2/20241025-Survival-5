using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class HealGun : LaserGun
{
	[Tooltip("�����Դϴ�")]
	public float healAmount;

	public HealProjectile healProjectilePrefab; // ����ü ������

	protected override void Fire()
	{
		HealProjectile proj = LeanPool.Spawn(healProjectilePrefab, transform.position, Quaternion.identity);

		proj.damage = damage;
		proj.moveSpeed = projectileSpeed;
		proj.transform.localScale *= projectileScale;
		proj.pierceCount = pierceCount;

		// HealGun ���� �ɷ�
		proj.healAmount = healAmount;

		LeanPool.Despawn(proj, proj.duration);
	}
}
