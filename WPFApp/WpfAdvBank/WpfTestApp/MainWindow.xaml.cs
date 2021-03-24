using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTestApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            /*// 동적 바인딩 (이벤트에서 생성)
            PersonList people = new PersonList();
            this.DataContext = people;*/
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // converter 작업으로 대체 가능 (NameConverter.cs)
            // convert : 값으로 자동으로 변환, 알아서 formatting
            try
            {
                var temp = LsbPerson.SelectedItem as Person;
                TxtNormal.Text = $"{temp.FirstName} {temp.LastName}";
                TxtLastFirst.Text = $"{temp.LastName}, {temp.FirstName}";
            }
            catch (Exception)
            {
            }
        }
    }
}
