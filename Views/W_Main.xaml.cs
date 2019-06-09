using System.Windows;
using System.Windows.Input;

namespace Accounting.Views
{
    /// <summary>
    /// Interaction logic for W_Main.xaml
    /// </summary>
    public partial class W_Main : Window
    {
        public W_Main()
        {
            InitializeComponent();
        }

      

        private void group_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            W_Group groupInstance = new W_Group();
            groupInstance.ShowDialog();
        }

        private void kol_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            W_KolAccount kolInstance = new W_KolAccount();
            kolInstance.ShowDialog();
        }

        private void moin_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            W_MoinAccount moinInstance = new W_MoinAccount();
            moinInstance.ShowDialog();
        }

        private void tafsiliGroup_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            W_TafsiliGroupAccount tafsiliGroupInstance = new W_TafsiliGroupAccount();
            tafsiliGroupInstance.ShowDialog();
        }

        private void peopleGroup_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            W_PeopleGroup peopleInstance = new W_PeopleGroup();
            peopleInstance.ShowDialog();
        }

        private void costCenter_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            W_CostCenter costCenterInstance = new W_CostCenter();
            costCenterInstance.ShowDialog();
        }
    }
}
