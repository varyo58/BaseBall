using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BaseBall;

public class Player
{
    private  string _name;
    private  int _senkyu;
    private  int _meet;
    private  int _power;
    private PlayerRecord _record;
    private PlayerRecord _simRecord;
    private PlayerRecord _totalRecord;

    public string Name { get => _name; set => _name = value; }
    public int Senkyu { get => _senkyu; set => _senkyu = value; }
    public int Meet { get => _meet; set => _meet = value; }
    public int Power { get => _power; set => _power = value; }
    public PlayerRecord Record => _record;
    public PlayerRecord SimRecord => _simRecord;
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
    /// 自身の打席の結果を記録
    /// </summary>
    /// <param name="result"></param>
    /// <param name="daten"></param>
    public void WriteRecord(HittingResult result, int daten)
    {
        WriteRecord(result, daten, _record);
    }

    /// <summary>
    /// 打席の結果を記録
    /// </summary>
    /// <param name="result"></param>
    /// <param name="daten"></param>
    /// <param name="playerRecord"></param>
    public static void WriteRecord(HittingResult result, int daten, PlayerRecord playerRecord)
    {
        playerRecord.DasekiCnt++;
        playerRecord.Daten += daten;
        
        switch (result)
        {
            case HittingResult.SingleHit:
                playerRecord.OneBaseCnt++;
                break;
            case HittingResult.TwoBase:
                playerRecord.TwoBaseCnt++;
                break;

            case HittingResult.ThreeBase:
                playerRecord.ThreeBaseCnt++;
                break;

            case HittingResult.Homerun:
                playerRecord.HomerunCnt++;
                break;

            case HittingResult.FourBall:
                playerRecord.FourBallCnt++;
                break;
        }
    }

    public HittingResult Batting()
    {
        // ヒット判定
        var random = (decimal)new Random().Next(0, 999) / 1000;

        // meet→打率変換
        decimal daritsu = (decimal)(_meet * 3.4 / 1000);

        if (random < daritsu)
        {
            // ホームラン判定
            decimal tempPower = _power - 40;
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
            decimal threeBaseRitsu = (decimal)((100 - _power) * 0.5 / 1000);
            random = (decimal)new Random().Next(0, 999) / 1000;
            if (random < threeBaseRitsu)
            {
                return HittingResult.ThreeBase;
            }

            // 二塁打判定
            tempPower = _power - 30;
            if (tempPower < 0) tempPower = 1;
            decimal twoBaseRitsu = tempPower / (decimal)(_meet * 2 + 100);
            random = (decimal)new Random().Next(0, 999) / 1000;
            if (random < twoBaseRitsu)
            {
                return HittingResult.TwoBase;
            }

            return HittingResult.SingleHit;
        }

        // 四球判定
        // TODO もうちょっと差をつけたい。
        var tempSenkyu = _senkyu - 30;
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

    public void Simulate(int simCnt)
    {
        _simRecord = new PlayerRecord();
        int i = 0;

        while (i < simCnt)
        {
            var hittingResult = Batting();
            WriteRecord(hittingResult, 0, _simRecord);

            i++;
        }


        //int oneBaseCnt = 0;
        //int twoBaseCnt = 0;
        //int threeBaseCnt = 0;
        //int homerunCnt = 0;
        //int fourBallCnt = 0;
        //int hitCnt;
        //int dasu;
        //decimal daritsu;
        //decimal syutsuruiRitsu;
        //decimal chodaRitsu;

        //int i = 0;
        //while (i < simCnt)
        //{
        //    var hittingResult = Batting(player);
        //    switch (hittingResult)
        //    {
        //        case HittingResult.SingleHit:
        //            oneBaseCnt++;
        //            break;
        //        case HittingResult.TwoBase:
        //            twoBaseCnt++;
        //            break;

        //        case HittingResult.ThreeBase:
        //            threeBaseCnt++;
        //            break;

        //        case HittingResult.Homerun:
        //            homerunCnt++;
        //            break;

        //        case HittingResult.FourBall:
        //            fourBallCnt++;
        //            break;

        //    }

        //    i++;
        //}

        //// 結果表示
        //hitCnt = oneBaseCnt + twoBaseCnt + threeBaseCnt + homerunCnt;
        //dasu = simCnt - fourBallCnt;
        //daritsu = hitCnt / (decimal)dasu;
        //syutsuruiRitsu = (hitCnt + fourBallCnt) / (decimal)simCnt;
        //chodaRitsu = (oneBaseCnt + twoBaseCnt * 2 + threeBaseCnt * 3 + homerunCnt * 4) / (decimal)dasu;

        //Debug.WriteLine($@"打席　：{simCnt}");
        //Debug.WriteLine($@"打数　：{dasu}");
        //Debug.WriteLine($@"安打数：{hitCnt}");
        //Debug.WriteLine($@"本塁打：{homerunCnt}");
        //Debug.WriteLine($@"三塁打：{threeBaseCnt}");
        //Debug.WriteLine($@"二塁打：{twoBaseCnt}");
        //Debug.WriteLine($@"単打　：{oneBaseCnt}");
        //Debug.WriteLine($@"四球　：{fourBallCnt}");
        //Debug.WriteLine($@"打率　：{daritsu:0.000}");
        //Debug.WriteLine($@"出塁率：{syutsuruiRitsu:0.000}");
        //Debug.WriteLine($@"長打率：{chodaRitsu:0.000}");
        //Debug.WriteLine($@"ＯＰＳ：{syutsuruiRitsu + chodaRitsu:0.000}");

    }

}