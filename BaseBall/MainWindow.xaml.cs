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


            var sankan = new Player("三冠王", 85, 85, 83);
            var homerunKing = new Player("主砲", 88, 65, 88);
            var ahetan = new Player("アヘ単", 50, 85, 50);
            var freeSwinger = new Player("パワー系助っ人", 67, 58, 88);
            var syubisen = new Player("守備の人", 60, 63, 53);
            var nopower = new Player("非力", 45, 75, 50);
            var romam = new Player("ロマン砲", 60, 55, 82);
            var ibushi = new Player("いぶし銀", 70, 62, 60);
            var chukyori = new Player("巧打者", 72, 75, 72);

            var game = new Game(
                sankan,
                homerunKing,
                ahetan,
                freeSwinger,
                syubisen,
                nopower,
                romam,
                ibushi,
                chukyori);

            //game.PlayGame();

            Debug.WriteLine($@"------{sankan.Name}-----");
            Player.Simulate(sankan, 5000);

            Debug.WriteLine($@"------{homerunKing.Name}-----");
            Player.Simulate(homerunKing, 5000);

            Debug.WriteLine($@"------{ahetan.Name}-----");
            Player.Simulate(ahetan, 5000);

            Debug.WriteLine($@"------{freeSwinger.Name}-----");
            Player.Simulate(freeSwinger, 5000);

            Debug.WriteLine($@"------{syubisen.Name}-----");
            Player.Simulate(syubisen, 5000);

            Debug.WriteLine($@"------{nopower.Name}-----");
            Player.Simulate(nopower, 5000);

            Debug.WriteLine($@"------{romam.Name}-----");
            Player.Simulate(romam, 5000);

            Debug.WriteLine($@"------{ibushi.Name}-----");
            Player.Simulate(ibushi, 5000);

            Debug.WriteLine($@"------{chukyori.Name}-----");
            Player.Simulate(chukyori, 5000);

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
        public string Name { get; }
        public int Senkyu { get; }
        public int Meet { get; }
        public int Power { get; }
        public PlayerRecord Record { get; set; }

        public Player(string name, int senkyu, int meet, int power)
        {
            Senkyu = senkyu;
            Meet = meet;
            Power = power;
            Name = name;
            Record = new PlayerRecord();
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
            // もうちょっと差をつけたい。
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
        }

        public int OneBaseCnt { get; set; }
        public int TwoBaseCnt { get; set; }
        public int ThreeBaseCnt { get; set; }
        public int HomerunCnt { get; set; }
        public int FourBallCnt { get; set; }
        public int DasekiCnt { get; set; }


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