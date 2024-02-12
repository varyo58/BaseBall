using System.Diagnostics;
using System.Text;
using System.Windows.Documents;
using BaseBall.Model;

namespace BaseBall;

public class Game
{
    private Team Team { get; }
    private Diamond Diamond { get; }

    public Game(Team team)
    {
        Team = team;
        Diamond = new Diamond();
    }

    public void PlayGame()
    {
        // 試合ループ
        int gameScore = 0;
        for (var inning = 1; inning <= 9; inning++)
        {
            // イニングループ
            Diamond.Reset();
            Debug.WriteLine($@"{inning}回の攻撃：");
            for (var outCnt = 0; outCnt < 3; )
            {
                var daten = 0;
                var nowPlayer = Team.NextBatter();
                var hittingResult =  Player.Batting(nowPlayer);

                Debug.WriteLine($@" バッター：{nowPlayer.Name}");
                Debug.WriteLine($@" 結果：{hittingResult.ToResultString()}");


                if (hittingResult == HittingResult.Out)
                {
                    outCnt++;
                }
                else
                {
                    daten = Diamond.CalcScore(hittingResult);
                    if (daten > 0)
                    {
                        Debug.WriteLine($@" 得点：{daten}");
                    }
                }

                // 選手記録の加算
                nowPlayer.WriteRecord(hittingResult, daten);
                Debug.WriteLine($@" ランナー:{Diamond.DiamondToString()}");

                gameScore += daten;
            }
        }
    }

}