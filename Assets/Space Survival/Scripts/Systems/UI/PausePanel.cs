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
		// pauseText 넣기
		statsText.text = $"플레이어 레벨 : {GameManager.Instance.player.level + 1}\n" +
			$"플레이어 경험치 : {GameManager.Instance.player.exp}\n" +
			$"플레이어 체력 : {GameManager.Instance.player.hp}\n" +
			$"플레이어 공격력 : {GameManager.Instance.player.damage}\n" +
			$"플레이어 이동속도 : {GameManager.Instance.player.moveSpeed}\n\n";

		string str = "==== 가진 스킬 ====\n";
		int skillCount = GameManager.Instance.player.skills.Count;
		foreach (Skill skill in GameManager.Instance.player.skills)
		{
			if (skill.skillName == "LaserGun" || skill.skillLevel > 0)
			{
				if (skill.skillName == "LaserGun")
				{
					str += $"{skill.skillName} : {skill.skillLevel + 1}레벨\n";
				}
				else
				{
					str += $"{skill.skillName} : {skill.skillLevel}레벨\n";
				}
			}
		}
		skillText.text = str;
	}
}
