using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

// �⺻ ����. ����ü�� �߻��ϴ� ��ų
public class LaserGun : MonoBehaviour
{
	public Transform target;            // ����ü�� ���ؾ� �� ���⿡ �ִ� ���
	public Projectile projectilePrefab; // ����ü ������
	public ProjectilePool projPool; // Projectile Prefab���� ������� ���� ������Ʈ�� �����ϴ� ������ƮǮ

	public float damage = 0;                // ������
	public float projectileSpeed = 0;       // ����ü �ӵ�
	public float projectileScale = 0;       // ����ü ũ��
	public float shotInterval = 0;          // ���� ����
	public int projectileCount = 0;         // ����ü ���� 1~5
	public float innerInterval = 0;
	[Tooltip("���� Ƚ���Դϴ�\n������ ������ ���� ��� 0")]
	public int pierceCount = 0;             // ���� Ƚ��

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

	// ���� �ڷ�ƾ
	protected virtual IEnumerator FireCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(shotInterval);
			// 1. ����ü ������ �ö󰡸� 0.05�� �������� ����ü ������ŭ �߻� �ݺ�
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
			// �Ϲ������� ����Ƽ���� ��ü�� ������ ��
			//Instantiate(projectilePrefab, transform.position, transform.rotation);

			// Ŀ���� ������Ʈ Ǯ�� ����� ��
			//projPool.Pop();
			//proj.transform.SetPositionAndRotation(transform.position, transform.rotation);

			// ������Ʈ Ǯ ���̺귯��(LeanPool) Ȱ��
			LeanPool.Spawn(projectilePrefab, transform.position, transform.rotation);

		proj.damage = damage;
		proj.moveSpeed = projectileSpeed;
		proj.transform.localScale *= projectileScale;
		proj.pierceCount = pierceCount;

		LeanPool.Despawn(proj, proj.duration);
	}
}
