using System.Diagnostics;
using System.Text;
using System.Windows.Documents;
using BaseBall.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BaseBall;

public class Game
{
    private Team Team { get; }
    private Diamond Diamond { get; }
    private int GameCount { get; }
    private int TotalScore { get; set; }
    private static BaseBallLogger log = BaseBallLogger.GetInstance("baseball.log");

    public Game(Team team, int gameCount)
    {
        Team = team;
        Diamond = new Diamond();
        GameCount = gameCount;
        TotalScore = 0;
    }

    public string RecordString => $"{GameCount}試合 {TotalScore}得点 (平均 {(double)TotalScore / GameCount}得点)";

    public void PlaySeason()
    {
        for (var i = 0; i < GameCount; i++)
        {
            PlayGame();
        }
    }

    public void PlayGame()
    {
        // 9イニングループ
        var gameScore = 0;
        for (var inning = 1; inning <= 9; inning++)
        {
           // 3アウトループ
            Diamond.Reset();
            log.write($@"{inning}回の攻撃：");
            for (var outCnt = 0; outCnt < 3; )
            {
                var daten = 0;
                var nowPlayer = Team.NextBatter();
                var hittingResult =  Player.Batting(nowPlayer);

                log.write($@" バッター：{nowPlayer.Name}");
                log.write($@" 結果：{hittingResult.ToResultString()}");

                if (hittingResult == HittingResult.Out)
                {
                    outCnt++;
                }
                else
                {
                    daten = Diamond.CalcScore(hittingResult);
                    if (daten > 0)
                    {
                        log.write($@" 得点：{daten}");
                    }
                }

                // 選手記録の加算
                nowPlayer.WriteRecord(hittingResult, daten);
                log.write($@" ランナー:{Diamond.DiamondToString()}");

                gameScore += daten;
            }
        }

        // 選手記録の通算
        Team.SummaryRecord();

        TotalScore += gameScore;
    }

}