using System.Diagnostics;
using System.Windows.Documents;

namespace BaseBall;

public class Game
{
    private Player[] Team { get; }

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

    public void PlayGame()
    {
        // 試合ループ
        int nextBatter = 0;
        int score = 0;
        for (int inning = 1; inning <= 9; inning++)
        {
            // イニングループ
            int diamond = 0;
            Debug.WriteLine($@"{inning}回の攻撃：");
            for (int outCnt = 0; outCnt < 3; )
            {
                var nowPlayer = Team[nextBatter];
                var hittingResult =  Player.Batting(nowPlayer);

                Debug.WriteLine($@" バッター：{nowPlayer.Name}");
                Debug.WriteLine($@" 結果：{hittingResult.ToResultString()}");


                if (hittingResult == HittingResult.Out)
                {
                    outCnt++;
                }
                else
                {
                    var run = CalcScore(hittingResult, ref diamond);
                    if (run > 0)
                    {
                        Debug.WriteLine($@" 得点：{run}");
                    }
                }

                // TODO 得点計算
                // TODO 選手記録の加算
                Debug.WriteLine($@" ランナー:{DiamondToString(diamond)}");


                nextBatter = (nextBatter + 1) % 9;
            }
        }
    }

    private  const int firstR = 0b0001;
    private const int secondR = 0b0010;
    private const int thirdR = 0b0100;
    private const int homeR1 = 0b0001000;
    private const int homeR2 = 0b0010000;
    private const int homeR3 = 0b0100000;
    private const int homeR4 = 0b1000000;

    private static int CalcScore(HittingResult result, ref int diamond)
    {
        // TODO テストクラスにしたい
        var score = 0;

        // 四球のとき
        if (result == HittingResult.FourBall)
        {
            // 1塁が空いている + 1
            if ((diamond & firstR) != firstR)
            {
                diamond += 1;
            }
            // 1塁が埋まっている
            else
            {
                //   2塁が空いている 1塁ランナー進んで出塁 1+1
                if ((diamond & secondR) != secondR)
                {
                    diamond += 1;
                    diamond += 1;

                }
                //   2塁が埋まっている 全進塁して出塁 *2 +1
                else
                {
                    diamond *= 2;
                    diamond += 1;
                    
                }
            }
        }
        // 単打のとき
        if (result == HittingResult.SingleHit)
        {
            // 全進塁して出塁 *2 +1
            diamond *= 2;
            diamond += 1;
            // TODO 確率で二つ進む
        }

        // 2塁打
        if (result == HittingResult.TwoBase)
        {
            // ２進塁して２塁へ
            diamond *= 4;
            diamond += 2;
            // TODO 確率で3つ進む
        }

        // 3塁打
        if (result == HittingResult.ThreeBase)
        {
            // 3進塁して3塁へ
            diamond *= 8;
            diamond += 4;
        }

        // ホームラン
        if (result == HittingResult.Homerun)
        {
            // 4進塁して4塁へ
            diamond *= 16;
            diamond += 8;
        }
        // 得点計算
        score += CountScore(ref diamond);

        return score;

    }

    /// <summary>
    /// ホームを踏んだ人数をカウントし、ホームを踏んだ分ダイヤモンドをリセットする。
    /// (満塁でホームランであれば、最大4人踏むことになる)
    /// </summary>
    /// <param name="diamond"></param>
    /// <returns></returns>
    private static int CountScore(ref int diamond)
    {
        int result = 0;
        if ((diamond & homeR1) == homeR1)
        {
            result++;
            diamond -= homeR1;
        }
        if ((diamond & homeR2) == homeR2)
        {
            result++;
            diamond -= homeR2;
        }
        if ((diamond & homeR3) == homeR3)
        {
            result++;
            diamond -= homeR3;
        }
        if ((diamond & homeR4) == homeR4)
        {
            result++;
            diamond -= homeR4;
        }
        return result;
    }

    /// <summary>
    /// 指定された塁上の状況を文字列で返す
    /// </summary>
    /// <param name="diamond"></param>
    /// <returns></returns>
    private static string DiamondToString(int diamond)
    {
        var str = $"";
        if ((diamond & firstR) == firstR) str += " 1塁";
        if ((diamond & secondR) == secondR) str += " 2塁";
        if ((diamond & thirdR) == thirdR) str += " 3塁";
        if (diamond == 0) str += " なし";
        return str;
    }

}