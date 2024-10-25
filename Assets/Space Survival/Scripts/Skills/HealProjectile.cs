using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealProjectile : Projectile
{
	public float healAmount = 5;

	protected override void Update()
	{
		Move(Vector2.up);
		Collider2D contactedColl = Physics2D.OverlapCircle(transform.position, coll.radius);
		if (contactedColl != null)
		{
			if (contactedColl.CompareTag("Enemy"))
			{
				if (false == contactedColls.Contains(contactedColl))
				{
					// 유효한 타격이 발생
					//print($"Contacted Collider : {contactedColl.name}");
					contactedColl.SendMessage("TakeDamage", damage);

					// 이때 힐
					GameManager.Instance.player.TakeHeal(healAmount);
					contactedColls.Add(contactedColl);
					pierceCount--;
					if (pierceCount == 0)
					{
						// 관통 횟수가 모두 소모되면 Destroy
						//Destroy(gameObject);
						//ProjectilePool.pool.Push(this);
						LeanPool.Despawn(this);
					}
				}
			}
		}
	}
}
