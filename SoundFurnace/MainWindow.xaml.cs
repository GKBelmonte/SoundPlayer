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
using System.Windows.Forms.Integration;
using SoundFurnace.Diagram;
using SoundFurnace.Model;
using Blaze.SoundPlayer.Waves;
namespace SoundFurnace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dalssoft.DiagramNet.Designer mDesigner;
        public MainWindow()
        {
            InitializeComponent();
            
        }
        bool placed = false;
        void MainWindow_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (placed)
                return;
            var pos = e.Location;
            CircuitElement element = new CircuitElement(new WaveComponent(new Sinusoid(1024)));
            element.Location = pos;
            mDesigner.Document.AddElement(element);
            placed = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the interop host control.
            WindowsFormsHost host = new WindowsFormsHost();

            // Create the MaskedTextBox control.
            mDesigner = new Dalssoft.DiagramNet.Designer();
            mDesigner.UndoStackEnabled = false;
            // Assign the MaskedTextBox control as the host control's child.
            host.Child = mDesigner;
            mDesigner.MouseClick += MainWindow_MouseDown;
            // Add the interop host control to the Grid 
            // control's collection of child controls. 
            this.DiagramHost.Children.Add(host);

            //Binding hBind = new Binding("Height"){Source = mDesigner};
            //BindingOperations.SetBinding(host, WindowsFormsHost.HeightProperty, hBind);
            //Binding wBind = new Binding("Width") { Source = mDesigner };
            //BindingOperations.SetBinding(host, WindowsFormsHost.WidthProperty, hBind);

            //mDesigner.Height = (int)(DiagramHost.Parent as Grid).ActualHeight;
            //mDesigner.Width = (int)(DiagramHost.Parent as Grid).ActualWidth;
        }
    }
}
