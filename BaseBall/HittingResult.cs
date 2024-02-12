namespace BaseBall;

public enum HittingResult
{
    Out = 0,
    SingleHit = 1,
    TwoBase = 2,
    ThreeBase = 3,
    Homerun = 4,
    FourBall = 6,
}

public static class HittingResultExtension
{
    private static Dictionary<HittingResult, string> viewStrDictionary = new Dictionary<HittingResult, string>()
    {
        {HittingResult.Out,"アウト" },
        {HittingResult.SingleHit,"ヒット" },
        {HittingResult.TwoBase,"二塁打" },
        {HittingResult.ThreeBase,"三塁打" },
        {HittingResult.Homerun,"ホームラン" },
        {HittingResult.FourBall,"四球" },

    };
    public static string ToResultString(this HittingResult result)       //<-　拡張メソッド
    {
        return viewStrDictionary[result];
    }

}