using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseBall
{
    /// <summary>
    /// 塁上の状況を2進数で表現するクラス
    /// </summary>
    public class Diamond
    {
        private int _diamond;

        private const int firstR  = 0b0000001;
        private const int secondR = 0b0000010;
        private const int thirdR  = 0b0000100;
        private const int homeR1  = 0b0001000;
        private const int homeR2  = 0b0010000;
        private const int homeR3  = 0b0100000;
        private const int homeR4  = 0b1000000;

        public Diamond()
        {
            _diamond = 0;
        }

        public void Reset()
        {
            _diamond = 0;
        }

        public int CalcScore(HittingResult result)
        {
            // TODO テストクラスにしたい
            var score = 0;

            // 四球のとき
            if (result == HittingResult.FourBall)
            {
                // 1塁が空いている + 1
                if ((_diamond & firstR) != firstR)
                {
                    _diamond += 1;
                }
                // 1塁が埋まっている
                else
                {
                    //   2塁が空いている 1塁ランナー進んで出塁 1+1
                    if ((_diamond & secondR) != secondR)
                    {
                        _diamond += 1;
                        _diamond += 1;

                    }
                    //   2塁が埋まっている 全進塁して出塁 *2 +1
                    else
                    {
                        _diamond *= 2;
                        _diamond += 1;

                    }
                }
            }
            // 単打のとき
            if (result == HittingResult.SingleHit)
            {
                // 全進塁して出塁 *2 +1
                _diamond *= 2;
                _diamond += 1;
                // TODO 確率で二つ進む
            }

            // 2塁打
            if (result == HittingResult.TwoBase)
            {
                // ２進塁して２塁へ
                _diamond *= 4;
                _diamond += 2;
                // TODO 確率で3つ進む
            }

            // 3塁打
            if (result == HittingResult.ThreeBase)
            {
                // 3進塁して3塁へ
                _diamond *= 8;
                _diamond += 4;
            }

            // ホームラン
            if (result == HittingResult.Homerun)
            {
                // 4進塁して4塁へ
                _diamond *= 16;
                _diamond += 8;
            }
            // 得点計算
            score += CountScore();

            return score;

        }

        /// <summary>
        /// ホームを踏んだ人数をカウントし、ホームを踏んだ分ダイヤモンドをリセットする。
        /// ex)満塁でホームランであれば、最大4人踏むことになる
        /// </summary>
        /// <returns></returns>
        private int CountScore()
        {
            var result = 0;
            if ((_diamond & homeR1) == homeR1)
            {
                result++;
                _diamond -= homeR1;
            }
            if ((_diamond & homeR2) == homeR2)
            {
                result++;
                _diamond -= homeR2;
            }
            if ((_diamond & homeR3) == homeR3)
            {
                result++;
                _diamond -= homeR3;
            }
            if ((_diamond & homeR4) == homeR4)
            {
                result++;
                _diamond -= homeR4;
            }
            return result;
        }

        /// <summary>
        /// 塁上の状況を文字列で返す
        /// </summary>
        /// <returns></returns>
        public string DiamondToString()
        {
            var str = $"";
            if ((_diamond & firstR) == firstR) str += "1塁";
            if ((_diamond & secondR) == secondR) str += "2塁";
            if ((_diamond & thirdR) == thirdR) str += "3塁";
            if (_diamond == 0) str += "なし";
            return str;
        }

    }
}
