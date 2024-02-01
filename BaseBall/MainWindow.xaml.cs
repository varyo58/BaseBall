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

            var player = new Player(350);

            int hitCnt = 0;
            for (int i = 0; i < 1000; i++)
            {
                var result = Player.Batting(player);
                if (result == HittingResult.Out)
                {
                    //Debug.WriteLine($"第{i}打席：OUT");
                }
                else
                {
                    hitCnt++;
                    //Debug.WriteLine($"第{i}打席：HIT");

                }


            }

            Debug.WriteLine($"ヒット数：{hitCnt}");

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

            new Game();

        }
    }

    public class Player
    {
        private readonly int _daritsu;

        public int Daritsu => _daritsu;

        public Player(int daritsu)
        {
            _daritsu = daritsu;
        }

        public static HittingResult Batting(Player player)
        {
            var random = new Random().Next(0,999);
            if (random < player.Daritsu)
            {
                return HittingResult.SingleHit;
            }
            else
            {
                return HittingResult.Out;
            }

        }

    }

    public enum HittingResult
    {
        Out = 0,
        SingleHit = 1,
    }
}