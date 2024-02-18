using System.Diagnostics;

namespace BaseBall;

public class Player
{
    private readonly string _name;
    private readonly int _senkyu;
    private readonly int _meet;
    private readonly int _power;
    private readonly PlayerRecord _record;
    private readonly PlayerRecord _totalRecord;

    public string Name => _name;
    public int Senkyu => _senkyu;
    public int Meet => _meet;
    public int Power => _power;
    public PlayerRecord Record => _record;
    public PlayerRecord TotalRecord => _totalRecord;

    public Player(string name, int senkyu, int meet, int power)
    {
        _senkyu = senkyu;
        _meet = meet;
        _power = power;
        _name = name;
        _record = new PlayerRecord();
        _totalRecord = new PlayerRecord();

    }

    /// <summary>
    /// 試合記録を通算記録に移してリセット
    /// </summary>
    public void SummaryRecord()
    {
        _totalRecord.AddRecord(_record);
        _record.ResetRecord();
    }

    /// <summary>
    /// 打席の結果を記録
    /// </summary>
    /// <param name="result"></param>
    /// <param name="daten"></param>
    public void WriteRecord(HittingResult result, int daten)
    {
        _record.DasekiCnt++;
        _record.Daten += daten;
        
        switch (result)
        {
            case HittingResult.SingleHit:
                _record.OneBaseCnt++;
                break;
            case HittingResult.TwoBase:
                _record.TwoBaseCnt++;
                break;

            case HittingResult.ThreeBase:
                _record.ThreeBaseCnt++;
                break;

            case HittingResult.Homerun:
                _record.HomerunCnt++;
                break;

            case HittingResult.FourBall:
                _record.FourBallCnt++;
                break;
        }
    }

    public static HittingResult Batting(Player player)
    {
        // ヒット判定
        var random = (decimal)new Random().Next(0, 999) / 1000;

        // meet→打率変換
        decimal daritsu = (decimal)(player.Meet * 3.4 / 1000);

        if (random < daritsu)
        {
            // ホームラン判定
            decimal tempPower = player.Power - 40;
            if (tempPower < 0) tempPower = 1;
            tempPower = (int)Math.Pow((double)tempPower, 2) / (decimal)100;
            //tempPower = player.Power - 50;
            //if (tempPower < 0) tempPower = 1;
            decimal homerunRitsu = (decimal)(tempPower * (decimal)14.0 / 1000);
            random = (decimal)new Random().Next(0, 999) / 1000;
            if (random < homerunRitsu)
            {
                return HittingResult.Homerun;
            }

            // 三塁打判定
            decimal threeBaseRitsu = (decimal)((100 - player.Power) * 0.5 / 1000);
            random = (decimal)new Random().Next(0, 999) / 1000;
            if (random < threeBaseRitsu)
            {
                return HittingResult.ThreeBase;
            }

            // 二塁打判定
            tempPower = player.Power - 30;
            if (tempPower < 0) tempPower = 1;
            decimal twoBaseRitsu = tempPower / (decimal)(player.Meet * 2 + 100);
            random = (decimal)new Random().Next(0, 999) / 1000;
            if (random < twoBaseRitsu)
            {
                return HittingResult.TwoBase;
            }

            return HittingResult.SingleHit;
        }

        // 四球判定
        // TODO もうちょっと差をつけたい。
        var tempSenkyu = player.Senkyu - 30;
        if (tempSenkyu < 0) tempSenkyu = 1;
        decimal shikyuRitsu = (decimal)(tempSenkyu * 2.3 / 1000);
        random = (decimal)new Random().Next(0, 999) / 1000;

        if (random < shikyuRitsu)
        {
            return HittingResult.FourBall;
        }
        else
        {
            return HittingResult.Out;
        }
    }

    public static void Simulate(Player player, int simCnt)
    {

        int oneBaseCnt = 0;
        int twoBaseCnt = 0;
        int threeBaseCnt = 0;
        int homerunCnt = 0;
        int fourBallCnt = 0;
        int hitCnt;
        int dasu;
        decimal daritsu;
        decimal syutsuruiRitsu;
        decimal chodaRitsu;

        int i = 0;
        while (i < simCnt)
        {
            var hittingResult = Batting(player);
            switch (hittingResult)
            {
                case HittingResult.SingleHit:
                    oneBaseCnt++;
                    break;
                case HittingResult.TwoBase:
                    twoBaseCnt++;
                    break;

                case HittingResult.ThreeBase:
                    threeBaseCnt++;
                    break;

                case HittingResult.Homerun:
                    homerunCnt++;
                    break;

                case HittingResult.FourBall:
                    fourBallCnt++;
                    break;

            }

            i++;
        }

        // 結果表示
        hitCnt = oneBaseCnt + twoBaseCnt + threeBaseCnt + homerunCnt;
        dasu = simCnt - fourBallCnt;
        daritsu = hitCnt / (decimal)dasu;
        syutsuruiRitsu = (hitCnt + fourBallCnt) / (decimal)simCnt;
        chodaRitsu = (oneBaseCnt + twoBaseCnt * 2 + threeBaseCnt * 3 + homerunCnt * 4) / (decimal)dasu;

        Debug.WriteLine($@"打席　：{simCnt}");
        Debug.WriteLine($@"打数　：{dasu}");
        Debug.WriteLine($@"安打数：{hitCnt}");
        Debug.WriteLine($@"本塁打：{homerunCnt}");
        Debug.WriteLine($@"三塁打：{threeBaseCnt}");
        Debug.WriteLine($@"二塁打：{twoBaseCnt}");
        Debug.WriteLine($@"単打　：{oneBaseCnt}");
        Debug.WriteLine($@"四球　：{fourBallCnt}");
        Debug.WriteLine($@"打率　：{daritsu:0.000}");
        Debug.WriteLine($@"出塁率：{syutsuruiRitsu:0.000}");
        Debug.WriteLine($@"長打率：{chodaRitsu:0.000}");
        Debug.WriteLine($@"ＯＰＳ：{syutsuruiRitsu + chodaRitsu:0.000}");

    }

}