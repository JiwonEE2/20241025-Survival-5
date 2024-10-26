using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
	public Text statsText;
	public Text skillText;

	private void Update()
	{
		// pauseText �ֱ�
		statsText.text = $"�÷��̾� ���� : {GameManager.Instance.player.level + 1}\n" +
			$"�÷��̾� ����ġ : {GameManager.Instance.player.exp}\n" +
			$"�÷��̾� ü�� : {GameManager.Instance.player.hp}\n" +
			$"�÷��̾� ���ݷ� : {GameManager.Instance.player.damage}\n" +
			$"�÷��̾� �̵��ӵ� : {GameManager.Instance.player.moveSpeed}\n\n";

		string str = "==== ���� ��ų ====\n";
		int skillCount = GameManager.Instance.player.skills.Count;
		foreach (Skill skill in GameManager.Instance.player.skills)
		{
			if (skill.skillName == "LaserGun" || skill.skillLevel > 0)
			{
				if (skill.skillName == "LaserGun")
				{
					str += $"{skill.skillName} : {skill.skillLevel + 1}����\n";
				}
				else
				{
					str += $"{skill.skillName} : {skill.skillLevel}����\n";
				}
			}
		}
		skillText.text = str;
	}
}
