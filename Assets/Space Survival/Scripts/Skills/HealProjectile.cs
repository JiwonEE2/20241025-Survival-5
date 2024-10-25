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
					// ��ȿ�� Ÿ���� �߻�
					//print($"Contacted Collider : {contactedColl.name}");
					contactedColl.SendMessage("TakeDamage", damage);

					// �̶� ��
					GameManager.Instance.player.TakeHeal(healAmount);
					contactedColls.Add(contactedColl);
					pierceCount--;
					if (pierceCount == 0)
					{
						// ���� Ƚ���� ��� �Ҹ�Ǹ� Destroy
						//Destroy(gameObject);
						//ProjectilePool.pool.Push(this);
						LeanPool.Despawn(this);
					}
				}
			}
		}
	}
}
