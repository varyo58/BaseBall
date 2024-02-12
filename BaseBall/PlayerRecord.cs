﻿namespace BaseBall;

public class PlayerRecord
{
    public PlayerRecord()
    {
        OneBaseCnt = 0;
        TwoBaseCnt = 0;
        ThreeBaseCnt = 0;
        HomerunCnt = 0;
        FourBallCnt = 0;
        DasekiCnt = 0;
        Daten = 0;
    }

    public int OneBaseCnt { get; set; }
    public int TwoBaseCnt { get; set; }
    public int ThreeBaseCnt { get; set; }
    public int HomerunCnt { get; set; }
    public int FourBallCnt { get; set; }
    public int DasekiCnt { get; set; }
    public int Daten { get; set; }

    public int HitCnt => OneBaseCnt + TwoBaseCnt + ThreeBaseCnt + HomerunCnt;
    public int DasuCnt => DasekiCnt - FourBallCnt;
    public decimal SyutsuRuiRitsu => (HitCnt + FourBallCnt) / (decimal)DasekiCnt;
    public decimal ChodaRitsu => (OneBaseCnt + TwoBaseCnt * 2 + ThreeBaseCnt * 3 + HomerunCnt * 4) / (decimal)DasuCnt;

    public string RecordString => @$"{DasekiCnt}打席{DasuCnt}打数{HitCnt}安打{Daten}打点";
}