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
    class WindowSupplyViewModel : ViewModelBase
    {
        public DelegateCommand OpenPanAndZoomCommand { get; private set; }

        public DelegateCommand ChangeSupplyFromSelectedSupplyNodeCommand { get; private set; }

        public DelegateCommand ChangeSupplyFromSelectedSupplyNodeGroupCommand { get; private set; }

        public IEnumerable<SUPPLY> Supplies
        {
            get { return GetValue<IEnumerable<SUPPLY>>(nameof(Supplies)); }
            set { SetValue(value, nameof(Supplies)); }
        }
        public IEnumerable<SUPPLY> Supply
        {
            get { return GetValue<IEnumerable<SUPPLY>>(nameof(Supply)); }
            set { SetValue(value, nameof(Supply)); }
        }
        public SUPPLY SelectedSupplyNode
        {
            get { return GetValue<SUPPLY>(nameof(SelectedSupplyNode)); }
            set { SetValue(value, nameof(SelectedSupplyNode)); }
        }
        public SUPPLY SelectedSupplyNodeGroup
        {
            get { return GetValue<SUPPLY>(nameof(SelectedSupplyNodeGroup)); }
            set { SetValue(value, nameof(SelectedSupplyNodeGroup)); }
        }
        public bool PanAndZoom
        {
            get { return GetValue<bool>(nameof(PanAndZoom)); }
            set { SetValue(value, nameof(PanAndZoom)); }
        }
        // PanAndZoom
        void OpenPanAndZoomCommandExecute()
        {
            OpenPanAndZoom();
        }
        void ChangeSupplyFromSelectedSupplyNodeCommandExecute()
        {
            SetSupplyFromSelectedSupplyNode();
        }

        void ChangeSupplyFromSelectedSupplyNodeGroupCommandExecute()
        {
            SetSupplyFromSelectedSupplyNodeGroup();
        }
        public WindowSupplyViewModel()
        {

        }

        protected override void OnInitializeInDesignMode()
        {
           
            base.OnInitializeInDesignMode();
           /* 
            Supplies = new List<SUPPLY>()
            {
                new SUPPLY()
                {
                    Demand = "2019-10-01B^P1^027060-114102^P1^0^1",
                    ParentDemand = "-",
                    RootDemand = "2019-10-01B^P1^027060-114102^P1^0^1",
                    RootLocation = "P1",
                    RootMaterial = "027060-114102",
                    DemandDate = "2019-10-01",
                    SplitNo = 0,
                    RoutingKind = "PROD393",
                    RoutingLeadtime = "0",
                    QPA = "1",
                    RoutingFrom = "P1",
                    Location = "P1",
                    LLC = 0,
                    OriginQTY = "12000",
                    PartialQTY = "12000",
                    ExtraQTY = "0",
                    AvailQTY = "6300",
                    AvailDate = "2019-10-01",
                    RoutingAction = "STOCK[100]",
                    Material = "027060-114102",
                    RootDemandCode = "1",
                    AltGroup = "",
                    PrimeMaterial = "027060-114102",
                    InputDate = "2019-10-01",
                    Resource = "",
                    IActualRoutingFrom = "P1",
                    IRootDemandDate = "2019-10-01",
                    IInventory = "0",
                    ISafetyLT = "0",
                    OAltOrder = "",
                    ORootPlant = "P1",
                    DemandID = "O0000378",
                    DemandType = "Order",
                    DemandBucket = "1",
                    AvailBucket = "1",
                    QpaMultiple = "1",
                    QpaUnit = ""

                },
                new SUPPLY()
                {
                    Demand = "2019-10-01B^P1^027060-114102^P1^0^1",
                    ParentDemand = "-",
                    RootDemand = "2019-10-01B^P1^027060-114102^P1^0^1",
                    RootLocation = "P1",
                    RootMaterial = "027060-114102",
                    DemandDate = "2019-10-01",
                    SplitNo = 1,
                    RoutingKind = "PROD393",
                    RoutingLeadtime = "1",
                    QPA = "1",
                    RoutingFrom = "P1",
                    Location = "P1",
                    LLC = 0,
                    OriginQTY = "12000",
                    PartialQTY = "5700",
                    ExtraQTY = "5070",
                    AvailQTY = "5700",
                    AvailDate = "2019-10-02",
                    RoutingAction = "PROD393",
                    Material = "027060-114102",
                    RootDemandCode = "1",
                    AltGroup = "",
                    PrimeMaterial = "027060-114102",
                    InputDate = "2019-10-01",
                    Resource = "393",
                    IActualRoutingFrom = "P1",
                    IRootDemandDate = "2019-10-01",
                    IInventory = "5070",
                    ISafetyLT = "0",
                    OAltOrder = "",
                    ORootPlant = "P1",
                    DemandID = "O0000378",
                    DemandType = "Order",
                    DemandBucket = "1",
                    AvailBucket = "2",
                    QpaMultiple = "1",
                    QpaUnit = ""
                }
            };
            */
        }
        protected override void OnInitializeInRuntime()
        {
            base.OnInitializeInRuntime();
            OpenPanAndZoomCommand = new DelegateCommand(OpenPanAndZoomCommandExecute);
            ChangeSupplyFromSelectedSupplyNodeCommand = new DelegateCommand(ChangeSupplyFromSelectedSupplyNodeCommandExecute);
            ChangeSupplyFromSelectedSupplyNodeGroupCommand = new DelegateCommand(ChangeSupplyFromSelectedSupplyNodeGroupCommandExecute);
            GenSupply();
        }
        private void GenSupply()
        {
            List<SUPPLY> supplyList = new List<SUPPLY>();
            DataTable dt = MpInputCSVUtil.ConvertCSVtoDataTable(@"C:\PPLAN\0.OUTPUT\DEMAND_OUT.csv");
            dt.DefaultView.Sort = "LLC,ROOT_MATERIAL,DEMAND,DEMAND_DATE,SPLIT_NO";
            int cnt = 0;
            
            foreach (DataRow dr in dt.DefaultView.ToTable().Rows)
            {
                 SUPPLY sp = new SUPPLY()
                {
                    RowNo = cnt,
                    Demand = dr["DEMAND"].ToString(),
                    ParentDemand = dr["PARENT_DEMAND"].ToString(),
                    RootDemand = dr["ROOT_DEMAND"].ToString(),
                    RootLocation = dr["ROOT_LOCATION"].ToString(),
                    RootMaterial = dr["ROOT_MATERIAL"].ToString(),
                    DemandDate = dr["DEMAND_DATE"].ToString().Substring(0, 10),
                    SplitNo = Convert.ToInt32(dr["SPLIT_NO"]),
                    RoutingKind = dr["ROUTING_KIND"].ToString(),
                    RoutingLeadtime = dr["ROUTING_LEADTIME"].ToString(),
                    QPA = dr["QPA"].ToString(),
                    RoutingFrom = dr["ROUTING_FROM"].ToString(),
                    Location = dr["LOCATION"].ToString(),
                    LLC = Convert.ToInt32(dr["LLC"]),
                    OriginQTY = dr["ORIGIN_QTY"].ToString(),
                    PartialQTY = dr["PARTIAL_QTY"].ToString(),
                    ExtraQTY = dr["EXTRA_QTY"].ToString(),
                    AvailQTY = dr["AVAIL_QTY"].ToString(),
                    AvailDate = dr["AVAIL_DATE"].ToString().Substring(0, 10),
                    RoutingAction = dr["ROUTING_ACTION"].ToString(),
                    Material = dr["MATERIAL"].ToString(),
                    RootDemandCode = dr["ROOT_DEMAND_CODE"].ToString(),
                    AltGroup = dr["ALT_GROUP"].ToString(),
                    PrimeMaterial = dr["PRIME_MATERIAL"].ToString(),
                    InputDate = dr["INPUT_DATE"].ToString().Substring(0, 10),
                    Resource = dr["RESOURCE"].ToString(),
                    IActualRoutingFrom = dr["I_ACTUAL_ROUTING_FROM"].ToString(),
                    IRootDemandDate = dr["I_ROOT_DEMAND_DATE"].ToString().Substring(0,10),
                    IInventory = dr["I_INVENTORY"].ToString(),
                    ISafetyLT = dr["I_SAFETY_LT"].ToString(),
                    OAltOrder = dr["O_ALT_ORDER"].ToString(),
                    ORootPlant = dr["O_ROOT_PLANT"].ToString(),
                    DemandID = dr["DEMAND_ID"].ToString(),
                    DemandType = dr["DEMAND_TYPE"].ToString(),
                    DemandBucket = dr["DEMAND_BUCKET"].ToString(),
                    AvailBucket = dr["AVAIL_BUCKET"].ToString(),
                    QpaMultiple = dr["QPA_MULTIPLE"].ToString(),
                    QpaUnit = dr["QPA_UNIT"].ToString()
                };
                //sp.RowPNo = FindParentRowID(sp, supplyList);
                supplyList.Add(sp);

                cnt++;
            }

            Supplies = supplyList;
            Supply = supplyList.Where(d => d.RootDemand.Equals("2021-05-17B^MX1^67672^SW1^0^4052") && d.RootMaterial.Equals("67672"));
        }
        private int FindParentRowID(SUPPLY sp, List<SUPPLY> supplyList)
        {
            for (int i = supplyList.Count - 1; i >= 0; i--)
            {
                if (supplyList[i].Demand.Equals(sp.ParentDemand))
                {
                    return supplyList[i].RowNo;
                }
            }
            return -1;
        }

        public void SetSupplyFromSelectedSupplyNode()
        {
            Supply = Supplies.Where(d => d.RootDemand.Equals(SelectedSupplyNode.RootDemand) && d.RootMaterial.Equals(SelectedSupplyNode.RootMaterial));
        }
        public void SetSupplyFromSelectedSupplyNodeGroup()
        {
            Supply = Supplies.Where(d => d.RootDemand.Equals(SelectedSupplyNodeGroup.RootDemand) && d.RootMaterial.Equals(SelectedSupplyNodeGroup.RootMaterial));
        }
        public void OpenPanAndZoom()
        {
            PanAndZoom = true;
        }

    }

}
