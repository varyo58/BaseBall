using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BaseBall.Model;

namespace BaseBall
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Start.");

            var player1 = new Player(70, 70, 70);
            Debug.WriteLine("------Player1------");
            Player.Simulate(player1, 500);

            var player2 = new Player(60, 85, 60);
            Debug.WriteLine("------Player2------");
            Player.Simulate(player2, 500);

            var player3 = new Player(85, 60, 85);
            Debug.WriteLine("------Player3------");
            Player.Simulate(player3, 500);

            var sankan = new Player(75, 85, 85);
            Debug.WriteLine("------sankan------");
            Player.Simulate(sankan, 500);

            var homerunKing = new Player(85, 65, 90);
            Debug.WriteLine("------homerunKing------");
            Player.Simulate(homerunKing, 500);

            var ahetan = new Player(50, 85, 50);
            Debug.WriteLine("------ahetan------");
            Player.Simulate(ahetan, 500);

            var freeSwinger = new Player(67, 55, 88);
            Debug.WriteLine("------freeSwinger------");
            Player.Simulate(freeSwinger, 500);

            var syubisen = new Player(60, 60, 55);
            Debug.WriteLine("------syubisen------");
            Player.Simulate(syubisen, 500);

            var nopower = new Player(45, 80, 45);
            Debug.WriteLine("------nopower------");
            Player.Simulate(nopower, 500);
            //int hitCnt = 0;
            //for (int i = 0; i < 1000; i++)
            //{
            //    var result = Player.Batting(player);
            //    if (result == HittingResult.Out)
            //    {
            //        //Debug.WriteLine($"第{i}打席：OUT");
            //    }
            //    else
            //    {
            //        hitCnt++;
            //        //Debug.WriteLine($"第{i}打席：HIT");

            //    }


            //}

            //Debug.WriteLine($"ヒット数：{hitCnt}");

            // TODO
            // Teamクラス、Gameクラス
            // ダイヤモンドクラスで状態を管理する？
            // 2進数表現がいいかも
            // 000 ランナーなし
            // 001 シングルヒット(1)
            // 010 進塁(かける2 = 2)
            // 011 シングルヒット(+1=3)
            // 110 進塁(*2 = 6)
            // 2塁打は3進める、ヒットは50%で2進めるがいいか？


        }
    }

    public class Player
    {
        public int Senkyu { get; }
        public int Meet { get; }
        public int Power { get; }


        public Player(int senkyu, int meet, int power)
        {
            Senkyu = senkyu;
            Meet = meet;
            Power = power;
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
                decimal homerunRitsu = (decimal)(tempPower * (decimal)15.0 / 1000);
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
            decimal shikyuRitsu = (decimal)(player.Senkyu * 2.0 / 1000);
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

    public enum HittingResult
    {
        Out = 0,
        SingleHit = 1,
        TwoBase = 2,
        ThreeBase = 3,
        Homerun = 4,
        FourBall = 6,
    }
}