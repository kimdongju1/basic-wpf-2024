using System.Windows;

namespace ex12_AnimalHospital_Find
{
    /// <summary>
    /// MapWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MapWindow : Window
    {
        public MapWindow()
        {
            InitializeComponent();
        }

        public MapWindow(string LC, string lon) : this()
        {
            BrsLoc.Address = $"https://map.naver.com/p/search/{LC}";
        }
    }
}
