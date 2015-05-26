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
using Blaze.SoundForge.Diagram;
using Blaze.SoundForge.Model;
using Blaze.SoundPlayer.Waves;
using Blaze.SoundForge.ViewModels;
using SoundFurnace.Diagram;
namespace Blaze.SoundForge
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
        
        void MainWindow_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            
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

            mDesigner.Document.Action = Dalssoft.DiagramNet.DesignerAction.Connect;
            // Add the interop host control to the Grid 
            // control's collection of child controls. 
            this.DiagramHost.Children.Add(host);

            //Drag target setup
            mDesigner.AllowDrop = true;
            mDesigner.DragOver += mDesigner_DragOver;
            mDesigner.DragDrop += mDesigner_DragDrop;

            //Drag Source setup
            ToolBox.MouseMove += ToolBox_MouseMove;

            
            //Binding hBind = new Binding("Height"){Source = mDesigner};
            //BindingOperations.SetBinding(host, WindowsFormsHost.HeightProperty, hBind);
            //Binding wBind = new Binding("Width") { Source = mDesigner };
            //BindingOperations.SetBinding(host, WindowsFormsHost.WidthProperty, hBind);

            //mDesigner.Height = (int)(DiagramHost.Parent as Grid).ActualHeight;
            //mDesigner.Width = (int)(DiagramHost.Parent as Grid).ActualWidth;
        }

        #region Drag/Drop
        void mDesigner_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = System.Windows.Forms.DragDropEffects.None;
            var obj = e.Data.GetData("PersistentObject");//this is absolutely ugly. Why not just pass me the damn object I wanted to pass.
            //object GetData() {return m_Object;} //or some shit.
            if (obj != null)
            {
                e.Effect = System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.Move;
            }
        }

        void mDesigner_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            var obj = e.Data.GetData("PersistentObject") as Type;
            if (obj != null)
            {
                
                SoundComponent comp;

                object[] parameters = null;
                var getParamsMethod = obj.GetMethod("GetParameters");
                if(getParamsMethod != null)
                    parameters = getParamsMethod.Invoke(null, null) as object[];
                if (parameters == null)
                {
                    try
                    {
                        comp = Activator.CreateInstance(obj) as SoundComponent;
                    }
                    catch (MissingMemberException)
                    {
                        var str = "No default constructor exists for this type!";
                        if (getParamsMethod == null)
                            str += " No method to get parameters from user has been defined!";
                        MessageBox.Show(str, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    try
                    {
                        comp = Activator.CreateInstance(obj, parameters) as SoundComponent;
                    }
                    catch (MissingMemberException)
                    {
                        var str = string.Format("No constructor with {0} exists for this type. Parameter types are: ", parameters.Length);
                        var ii = 0;
                        for(ii = 0; ii < parameters.Length - 1; ++ii)
                            str += parameters[ii].GetType() + ", ";
                        str += parameters[ii].GetType();
                        MessageBox.Show(str , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;                    
                    }
                }
                
                var element = new CircuitElement(comp);

                var p = ScreenToDiagramCoordinates(e.X, e.Y);
                element.Location = new System.Drawing.Point((int)p.X, (int)p.Y); ;
                var cmd = new AddNodeCommand(mDesigner, element);
                cmd.Do();
            }
        }

        public Point ScreenToDiagramCoordinates(int x, int y)
        {
            return DiagramHost.PointFromScreen(new Point(x, y));
        }

        void ToolBox_MouseMove(object sender, MouseEventArgs e)
        {
            ListView toolBox = sender as ListView;
            if (toolBox != null && e.LeftButton == MouseButtonState.Pressed)
            {
                try
                {
                    DragDrop.DoDragDrop(toolBox, toolBox.SelectedItem, DragDropEffects.Copy);
                }
                catch (Exception x)
                {
                    MessageBox.Show(string.Format("Error {0}.\n Details: {1}", x.Message, x), "Woops", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion //Drag/Drop

        private MainWindowViewModel VM { get { return DataContext as MainWindowViewModel; } }
    }
}
