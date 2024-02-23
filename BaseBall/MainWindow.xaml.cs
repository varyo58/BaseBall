using System.ComponentModel;
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

    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string PropertyName)
        {
            var e = new PropertyChangedEventArgs(PropertyName);
            PropertyChanged?.Invoke(this, e);
        }

        public Player Player1 { get => _player1;  }
        private Player _player1;

        public Player Player2 { get => _player2; }
        private Player _player2;

        public MainWindowVM()
        {
            _player1 = new Player("三冠王", 85, 85, 83);
            _player2 = new Player("主砲", 88, 65, 88);

            _player1.Simulate(500);
            _player2.Simulate(500);

        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static BaseBallLogger log = BaseBallLogger.GetInstance("baseball.log", false);
        private MainWindowVM vm = new MainWindowVM();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
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

            //var team = new Team(
            //    ahetan,
            //    ibushi,
            //    sankan,
            //    homerunKing,
            //    freeSwinger,
            //    syubisen,
            //    nopower,
            //    romam,
            //    chukyori);

            var team = new Team(
                sankan,
                homerunKing,
                ahetan,
                freeSwinger,
                syubisen,
                nopower,
                romam,
                ibushi,
                chukyori);

            var game = new Game(team, 144);
            //game.PlayGame();
            game.PlaySeason();

            // TODO 出力をロガークラスにまとめたい

            //Debug.WriteLine($@"------{sankan.Name}-----");
            //Player.Simulate(sankan, 5000);

            //Debug.WriteLine($@"------{homerunKing.Name}-----");
            //Player.Simulate(homerunKing, 5000);

            //Debug.WriteLine($@"------{ahetan.Name}-----");
            //Player.Simulate(ahetan, 5000);

            //Debug.WriteLine($@"------{freeSwinger.Name}-----");
            //Player.Simulate(freeSwinger, 5000);

            //Debug.WriteLine($@"------{syubisen.Name}-----");
            //Player.Simulate(syubisen, 5000);

            //Debug.WriteLine($@"------{nopower.Name}-----");
            //Player.Simulate(nopower, 5000);

            //Debug.WriteLine($@"------{romam.Name}-----");
            //Player.Simulate(romam, 5000);

            //Debug.WriteLine($@"------{ibushi.Name}-----");
            //Player.Simulate(ibushi, 5000);

            //Debug.WriteLine($@"------{chukyori.Name}-----");
            //Player.Simulate(chukyori, 5000);




        }
    }
}