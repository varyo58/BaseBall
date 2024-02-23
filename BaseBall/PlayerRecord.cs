namespace BaseBall;

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
    public decimal Daritsu => (HitCnt) / (decimal)DasuCnt;
    public decimal SyutsuRuiRitsu => (HitCnt + FourBallCnt) / (decimal)DasekiCnt;
    public decimal ChodaRitsu => (OneBaseCnt + TwoBaseCnt * 2 + ThreeBaseCnt * 3 + HomerunCnt * 4) / (decimal)DasuCnt;

    public string RecordString => @$"{DasekiCnt}打席{DasuCnt}打数{HitCnt}安打{Daten}打点";
    public string RecordStringWithOutDaten => @$"{DasekiCnt}打席{DasuCnt}打数{HitCnt}安打 / 出塁率{SyutsuRuiRitsu:0.000)} 長打率{ChodaRitsu:0.000}";

    public void AddRecord(PlayerRecord record)
    {
        OneBaseCnt += record.OneBaseCnt;
        TwoBaseCnt += record.TwoBaseCnt;
        ThreeBaseCnt += record.ThreeBaseCnt;
        HomerunCnt += record.HomerunCnt;
        FourBallCnt += record.FourBallCnt;
        DasekiCnt += record.DasekiCnt;
        Daten += record.Daten;
    }

    public void ResetRecord()
    {
        OneBaseCnt = 0;
        TwoBaseCnt = 0;
        ThreeBaseCnt = 0;
        HomerunCnt = 0;
        FourBallCnt = 0;
        DasekiCnt = 0;
        Daten = 0;
    }

}