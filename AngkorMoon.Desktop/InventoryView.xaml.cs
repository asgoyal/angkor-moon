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
using AngkorMoon.Desktop.Utils.Helpers;
using Microsoft.Practices.Unity;

namespace AngkorMoon.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class InventoryView : Window
    {
        public InventoryView()
        {
            InitializeComponent();
            this.DataContext = ContainerHelper.Container.Resolve<InventoryViewModel>();
        }
    }
}
