using DevExpress.Mvvm;
using WpfApp3.Model;
using WpfApp3.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Diagram;
using System.Windows;
using System.Windows.Input;

namespace WpfApp3
{
    class WindowSBOMViewModel : ViewModelBase
    {

        public String version;

        public DelegateCommand ChangeSbomFromSelectedSbomNodeCommand { get; private set; }

        public DelegateCommand OpenPanAndZoomCommand { get; private set; }

        public DelegateCommand ChangeSbomFromSelectedSbomNodeGroupCommand { get; private set; }

        public DelegateCommand GridRightClickCommand { get; private set; }

        public bool PanAndZoom
        {
            get { return GetValue<bool>(nameof(PanAndZoom)); }
            set { SetValue(value,nameof(PanAndZoom)); }
        }
        //SelectedSbomNode
        void ChangeSbomFromSelectedSbomNodeCommandExecute()
        {
            SetSbomFromSelectedSbomNode();
        }
        // PanAndZoom
        void OpenPanAndZoomCommandExecute()
        {
            OpenPanAndZoom();
        }
        //SbomNodeGroup
        void ChangeSbomFromSelectedSbomNodeGroupCommandExecute()
        {
            SetSbomFromSelectedSbomNodeGroup();
        }
        //GridContext 
        void GridRightClickCommandExecute()
        {
            
        }

        //전체 SBOM List
        public IEnumerable<SBOM> Sboms
        {
            get { return GetValue<IEnumerable<SBOM>>(nameof(Sboms));  }
            set { SetValue(value, nameof(Sboms)); }
        }

        //SBOM 한 셋트
        public IEnumerable<SBOM> Sbom
        {
            get { return GetValue<IEnumerable<SBOM>>(nameof(Sbom)); }
            set { SetValue(value, nameof(Sbom)); }
        }

        //선택된 Node
        public SBOM SelectedSbomNode
        { 
            get { return GetValue<SBOM>(nameof(SelectedSbomNode)); }
            set { SetValue(value, nameof(SelectedSbomNode)); }
        }

        //선택된 NodeGroup
        public SBOM SelectedSbomNodeGroup
        {
            get { return GetValue<SBOM>(nameof(SelectedSbomNodeGroup)); }
            set { SetValue(value, nameof(SelectedSbomNodeGroup)); }
        }


        public WindowSBOMViewModel()
        {
            
        }
       
        protected override void OnInitializeInDesignMode()
        {
            base.OnInitializeInDesignMode();
            Sboms = new List<SBOM>()
            {
                new SBOM(){
                    RootPlant = "SW1",
                    RootLocation = "SW1",
                    RootMaterial = "29824",
                    ParentLocation = "-",
                    ParentMaterial = "-",
                    Location = "SW1",
                    Material = "29824",
                    Level = 0,
                    RoutingFrom = "SW1",
                    RoutingKind = "PROD1000",
                    RoutingLeadTime = 0,
                    Qpa = 1.035,
                    AltGroup = "",
                    AltOrder = "",
                    AIsMTS = ""
                },
                new SBOM(){
                    RootPlant = "SW1",
                    RootLocation = "SW1",
                    RootMaterial = "29824",
                    ParentLocation = "SW1",
                    ParentMaterial = "29824",
                    Location = "SW1",
                    Material = "29825",
                    Level = 1,
                    RoutingFrom = "SW1",
                    RoutingKind = "PROD1200",
                    RoutingLeadTime = 0,
                    Qpa = 1,
                    AltGroup = "",
                    AltOrder = "",
                    AIsMTS = ""
                },
            };
        }

        protected override void OnInitializeInRuntime()
        {
            base.OnInitializeInRuntime();
            ChangeSbomFromSelectedSbomNodeCommand = new DelegateCommand(ChangeSbomFromSelectedSbomNodeCommandExecute);
            OpenPanAndZoomCommand = new DelegateCommand(OpenPanAndZoomCommandExecute);
            ChangeSbomFromSelectedSbomNodeGroupCommand = new DelegateCommand(ChangeSbomFromSelectedSbomNodeGroupCommandExecute);
            GridRightClickCommand = new DelegateCommand(GridRightClickCommandExecute);
            GenSBOM();
        }
        

        private void GenSBOM()
        {

            List<SBOM>  sbomList = new List<SBOM>();
            DataTable dt = MpInputCSVUtil.ConvertCSVtoDataTable(@"C:\PPLAN\0.INPUT\sbom.csv"); ;
            dt.DefaultView.Sort = "ROOT_LOCATION,ROOT_MATERIAL,ROOT_PLANT,LEVEL,PARENT_MATERIAL,MATERIAL";
            Console.WriteLine(version);
            int cnt = 0;
            foreach (DataRow dr in dt.DefaultView.ToTable().Rows)
            {

                SBOM sbom = new SBOM()
                {
                    RowNo = cnt,
                    RootPlant = dr["ROOT_PLANT"].ToString(),
                    RootLocation = dr["ROOT_LOCATION"].ToString(),
                    RootMaterial = dr["ROOT_MATERIAL"].ToString(),
                    ParentLocation = dr["PARENT_LOCATION"].ToString(),
                    ParentMaterial = dr["PARENT_MATERIAL"].ToString(),
                    Location = dr["LOCATION"].ToString(),
                    Material = dr["MATERIAL"].ToString(),
                    Level = Convert.ToInt32(dr["LEVEL"]),
                    RoutingFrom = dr["ROUTING_FROM"].ToString(),
                    RoutingKind = dr["ROUTING_KIND"].ToString(),
                    RoutingLeadTime = Convert.ToInt32(dr["ROUTING_LEADTIME"]),
                    Qpa = Convert.ToDouble(dr["QPA"]),
                    AltGroup = dr["ALT_GROUP"].ToString(),
                    AltOrder = dr["ALT_ORDER"].ToString(),
                    AIsMTS = dr["A_IS_MTS"].ToString(),
                };
                sbom.RowPNo = FindParentRowID(sbom, sbomList);
                sbomList.Add(sbom);

                cnt++;
            }

            Sboms = sbomList;
            Sbom = sbomList.Where(d => d.RootLocation.Equals("MX1") && d.RootMaterial.Equals("59733"));

        }

        private int FindParentRowID(SBOM sbom, List<SBOM> sbomList)
        {
            for(int i = sbomList.Count - 1; i >= 0; i--)
            {
                if(sbomList[i].SBomID.Equals(sbom.SBomPID))
                {
                    return sbomList[i].RowNo;
                }
            }
            return -1;
        }


        public void SetSbomFromSelectedSbomNode()
        {
            Sbom = Sboms.Where(d => d.RootPlant.Equals(SelectedSbomNode.RootPlant) && d.RootLocation.Equals(SelectedSbomNode.RootLocation) && d.RootMaterial.Equals(SelectedSbomNode.RootMaterial));
        }
        public void SetSbomFromSelectedSbomNodeGroup()
        {
            Sbom = Sboms.Where(d => d.RootPlant.Equals(SelectedSbomNodeGroup.RootPlant) && d.RootLocation.Equals(SelectedSbomNodeGroup.RootLocation) && d.RootMaterial.Equals(SelectedSbomNodeGroup.RootMaterial));
        }

        public void OpenPanAndZoom()
        {
            PanAndZoom = true;
        }


    }

    public class DiagramSelectionBehavior : Behavior<DiagramControl>
    {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(DiagramSelectionBehavior), new PropertyMetadata(null, OnMySelectedItemChanged));

        bool isSelectionLocked = false;

        //(d, e) => ((DiagramSelectionBehavior)d).OnSelectedItemChanged()

        private static void OnMySelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DiagramSelectionBehavior)d).OnSelectedItemChanged();
        }

        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
        }

        void AssociatedObject_SelectionChanged(object sender, DiagramSelectionChangedEventArgs e)
        {
            DoLockedActionIfNotLocked(() =>
            {
                if (AssociatedObject.PrimarySelection != null)
                    SelectedItem = AssociatedObject.PrimarySelection.DataContext;
                else
                    SelectedItem = null;
            });
        }

        void OnSelectedItemChanged()
        {
            DoLockedActionIfNotLocked(() =>
            {
                if (SelectedItem != null)
                {
                    var diagramItem = AssociatedObject.Items.FirstOrDefault(x => x.DataContext == SelectedItem);
                    if (diagramItem != null)
                    {
                        AssociatedObject.SelectItem(diagramItem);
                        AssociatedObject.BringItemsIntoView(new[] { diagramItem });
                    }
                }
                else
                {
                    AssociatedObject.ClearSelection();
                }
            });
        }

        void DoLockedActionIfNotLocked(Action action)
        {
            if (isSelectionLocked)
                return;
            isSelectionLocked = true;
            action();
            isSelectionLocked = false;
        }

    }
}
