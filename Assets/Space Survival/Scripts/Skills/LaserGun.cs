using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

// 기본 공격. 투사체를 발사하는 스킬
public class LaserGun : MonoBehaviour
{
	public Transform target;            // 투사체가 향해야 할 방향에 있는 대상
	public Projectile projectilePrefab; // 투사체 프리팹
	public ProjectilePool projPool; // Projectile Prefab으로 만들어진 게임 오브젝트를 관리하는 오브젝트풀

	public float damage = 0;                // 데미지
	public float projectileSpeed = 0;       // 투사체 속도
	public float projectileScale = 0;       // 투사체 크기
	public float shotInterval = 0;          // 공격 간격
	public int projectileCount = 0;         // 투사체 개수 1~5
	public float innerInterval = 0;
	[Tooltip("관통 횟수입니다\n무제한 관통을 원할 경우 0")]
	public int pierceCount = 0;             // 관통 횟수

	protected virtual void Awake()
	{
		damage += GameManager.Instance.player.damage;
		projectileSpeed += GameManager.Instance.player.projSpeed;
		projectileScale += GameManager.Instance.player.projSize;
		shotInterval -= GameManager.Instance.player.projShotInterval;
		projectileCount += GameManager.Instance.player.projCount;
		innerInterval -= GameManager.Instance.player.projInnerInterval;
	}

	protected virtual void Start()
	{
		StartCoroutine(FireCoroutine());
	}

	protected virtual void Update()
	{
		if (target == null)
		{
			return;
		}
		transform.up = target.position - transform.position;
	}

	// 공격 코루틴
	protected virtual IEnumerator FireCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(shotInterval);
			// 1. 투사체 개수가 올라가면 0.05초 간격으로 투사체 개수만큼 발사 반복
			for (int i = 0; i < projectileCount; i++)
			{
				Fire();
				yield return new WaitForSeconds(innerInterval);
			}
		}
	}

	protected virtual void Fire()
	{
		Projectile proj =
			// 일반적으로 유니티에서 객체를 생성할 때
			//Instantiate(projectilePrefab, transform.position, transform.rotation);

			// 커스텀 오브젝트 풀을 사용할 때
			//projPool.Pop();
			//proj.transform.SetPositionAndRotation(transform.position, transform.rotation);

			// 오브젝트 풀 라이브러리(LeanPool) 활용
			LeanPool.Spawn(projectilePrefab, transform.position, transform.rotation);

		proj.damage = damage;
		proj.moveSpeed = projectileSpeed;
		proj.transform.localScale *= projectileScale;
		proj.pierceCount = pierceCount;

		LeanPool.Despawn(proj, proj.duration);
	}
}
