using System.Diagnostics;

namespace BaseBall;

public class Game
{
    public Game(Player player1, Player player2, Player player3, Player player4, Player player5, Player player6, Player player7, Player player8, Player player9)
    {
        Team = new Player[]
        {
            player1,
            player2,
            player3,
            player4,
            player5,
            player6,
            player7,
            player8,
            player9
        };
    }

    private Player[] Team { get; }

    public void PlayGame()
    {
        int nextBatter = 0;

        for (int inning = 1; inning <= 9; inning++)
        {
            Debug.WriteLine($@"{inning}回の攻撃：");
            for (int outCnt = 0; outCnt < 3; )
            {
                var nowPlayer = Team[nextBatter];
                var result =  Player.Batting(nowPlayer);

                if (result == HittingResult.Out)
                {
                    outCnt++;
                }

                // TODO 得点計算
                // TODO 選手記録の加算

                Debug.WriteLine($@"  {nextBatter + 1}番 {nowPlayer.Name}：{result.ToString()}");
                nextBatter = (nextBatter + 1) % 9;
            }
        }
    }
}