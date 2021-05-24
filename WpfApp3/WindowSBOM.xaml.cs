using DevExpress.Xpf.Core;
using DevExpress.Xpf.Diagram;
using System;
using System.Linq;
using System.Windows;
using WpfApp3.Model;

using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Diagram.Themes;

namespace WpfApp3
{
    /// <summary>
    /// WindowSBOM.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WindowSBOM : BaseWindow
    {

        public WindowSBOM(string versionNm)
        {
            SetVersionName(versionNm);

            var theme = new Theme("NewTheme2");
            theme.AssemblyName = "DevExpress.Xpf.Themes.NewTheme2.v20.2";
            try
            {
                Theme.RegisterTheme(theme);
            }
            catch { }
            ApplicationThemeHelper.ApplicationThemeName = "NewTheme2";

            WindowSBOMViewModel vm = new WindowSBOMViewModel();
            vm.version = versionNm;

            InitializeComponent();

            diagram.ShowPanAndZoomPanel = true;


        }
        void GenerateSbomChartItem(object sender, DiagramGenerateItemEventArgs e)
        {
            SBOM s = (SBOM)e.DataObject;
            
            string templateName = (s.RoutingKind.Equals("BUY_")) ? "BUY" : "PROD";
            e.Item = e.CreateItemFromTemplate(templateName);
        }

        private void OnPanelLoaded(object sender, RoutedEventArgs e)
        {
            var offset = GetPanelBottomRightOffset();
            var location = diagramPanel.GetBounds(diagramPanel).BottomRight;
            location.Offset(-panAndZoomGroup.FloatSize.Width - offset, -panAndZoomGroup.FloatSize.Height - offset);
            panAndZoomGroup.FloatLocation = location;
        }

        double GetPanelBottomRightOffset()
        {
            var key = new DiagramDesignerControlThemeKeysExtension() { ResourceKey = DiagramDesignerControlThemeKeys.PanZoomBottomRightOffset };
            return (double)diagram.FindResource(key);
        }

        private void grid1_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Type type = e.MouseDevice.Target.GetType();
            int targetLevel = 0;
            
            if (type.Name.Equals("DXBorder"))
            {
                DevExpress.Xpf.Core.DXBorder target = (DevExpress.Xpf.Core.DXBorder)e.MouseDevice.Target;
                if (target.DataContext.GetType().Name.Equals("GroupRowData"))
                {
                    DevExpress.Xpf.Grid.GroupRowData dataContext = (DevExpress.Xpf.Grid.GroupRowData)target.DataContext;
                    targetLevel = dataContext.DataRowsContainer.GroupLevel;
                }
            }

            if (type.Name.Equals("TextBlock"))
            {
                System.Windows.Controls.TextBlock target = (System.Windows.Controls.TextBlock)e.MouseDevice.Target;
                if (target.DataContext.GetType().Name.Equals("GroupRowData"))
                {
                    DevExpress.Xpf.Grid.GroupRowData dataContext = (DevExpress.Xpf.Grid.GroupRowData)target.DataContext;
                    targetLevel = dataContext.DataRowsContainer.GroupLevel;
                }
                
            }
            // 3레벨 그룹만
            if (targetLevel.Equals(3))
            {
                showSbomTest.IsVisible = true;
                DevExpress.Xpf.Grid.TableView tableView = (DevExpress.Xpf.Grid.TableView)e.Source;
                sbomvm.SelectedSbomNodeGroup = (SBOM)tableView.FocusedRowData.Row;
            } 
            else
            {
                showSbomTest.IsVisible = false;
            }
        }

    }

   
}
