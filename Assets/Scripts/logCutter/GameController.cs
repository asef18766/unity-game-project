using UnityEngine;
using System.Collections;
namespace LogCutter
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]int minigameID;
        [SerializeField]int roundTime;
        public int score = 0;
        public static GameController instance = null;
        private PlayerData player;
        private int AHuaEquation()
        {
            return score*2+50;
        }
        private IEnumerator gameEnded()
        {
            while(roundTime > 0)
            {
                yield return new WaitForSecondsRealtime(1);
                roundTime--;
            }
            PlayerData.LoadPlayerData();
            var rec = PlayerData.record[minigameID];
            rec.times++;
            if(score > rec.highestRecord)
                rec.highestRecord = score;
            PlayerData.money += AHuaEquation();
        }
        private IEnumerator addScore()
        {
            score ++;
            yield return null;
        }
        private IEnumerator minusScore()
        {
            score -= 2;
            yield return null;
        }
        private void Start()
        {
            instance = this;
            StartCoroutine(gameEnded());
        }
    }
}