using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class MonsterController : MonoBehaviour
    {

        [Header("Random Values")]
        public float Attack = 5f / 6f;
        public float Miss = 1f / 8f;

        public float HealValue = 10f;

        [Header("Game Objects")]
        public GameController Controller;
        public PlayByPlayController PlayByPlay;

        public enum MonsterActions
        {
            Hit,
            Miss,
            Heal
        }

        public IEnumerator MonsterTurn()
        {
            yield return new WaitForSeconds(1);
            PlayByPlay.Clear();
            yield return new WaitForSeconds(0.5f);

            float randVal = Random.Range(0f, 1f);

            if (randVal < Attack)
            {
                if (randVal < Attack * Miss)
                {
                    Controller.Miss();
                    yield return PlayByPlay.DisplayMonsterAction(MonsterActions.Miss);
                }
                else
                {
                    float damage = Random.Range(15, 20);
                    Controller.DamagePlayer(damage);
                    yield return PlayByPlay.DisplayMonsterAction(MonsterActions.Hit);
                }
            }
            else
            {
                Controller.HealMonster(HealValue);
                yield return PlayByPlay.DisplayMonsterAction(MonsterActions.Heal);
            }

            yield return new WaitForSeconds(1);
        }
    }
}