using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Model
{
    class SUPPLY : BindableBase
    {
        public int RowNo
        {
            get { return GetValue<int>(nameof(RowNo)); }
            set { SetValue(value, nameof(RowNo)); }
        }

        public int RowPNo
        {
            get { return GetValue<int>(nameof(RowPNo)); }
            set { SetValue(value, nameof(RowPNo)); }
        }
        public String Demand
        {
            get { return GetValue<String>(nameof(Demand)); }
            set { SetValue(value, nameof(Demand)); }
        }
        public String ParentDemand
        {
            get { return GetValue<String>(nameof(ParentDemand)); }
            set { SetValue(value, nameof(ParentDemand)); }
        }
        public String RootDemand
        {
            get { return GetValue<String>(nameof(RootDemand)); }
            set { SetValue(value, nameof(RootDemand)); }
        }
        public String RootLocation
        {
            get { return GetValue<String>(nameof(RootLocation)); }
            set { SetValue(value, nameof(RootLocation)); }
        }
        public String RootMaterial
        {
            get { return GetValue<String>(nameof(RootMaterial)); }
            set { SetValue(value, nameof(RootMaterial)); }
        }
        public String DemandDate
        {
            get { return GetValue<String>(nameof(DemandDate)); }
            set { SetValue(value, nameof(DemandDate)); }
        }
        public int SplitNo
        {
            get { return GetValue<int>(nameof(SplitNo)); }
            set { SetValue(value, nameof(SplitNo)); }
        }
        public String RoutingKind
        {
            get { return GetValue<String>(nameof(RoutingKind)); }
            set { SetValue(value, nameof(RoutingKind)); }
        }
        public String RoutingLeadtime
        {
            get { return GetValue<String>(nameof(RoutingLeadtime)); }
            set { SetValue(value, nameof(RoutingLeadtime)); }
        }
        public String QPA
        {
            get { return GetValue<String>(nameof(QPA)); }
            set { SetValue(value, nameof(QPA)); }
        }
        public String RoutingFrom
        {
            get { return GetValue<String>(nameof(RoutingFrom)); }
            set { SetValue(value, nameof(RoutingFrom)); }
        }
        public String Location
        {
            get { return GetValue<String>(nameof(Location)); }
            set { SetValue(value, nameof(Location)); }
        }
        public int LLC
        {
            get { return GetValue<int>(nameof(LLC)); }
            set { SetValue(value, nameof(LLC)); }
        }
        public String OriginQTY
        {
            get { return GetValue<String>(nameof(OriginQTY)); }
            set { SetValue(value, nameof(OriginQTY)); }
        }
        public String PartialQTY
        {
            get { return GetValue<String>(nameof(PartialQTY)); }
            set { SetValue(value, nameof(PartialQTY)); }
        }
        public String ExtraQTY
        {
            get { return GetValue<String>(nameof(ExtraQTY)); }
            set { SetValue(value, nameof(ExtraQTY)); }
        }
        public String AvailQTY
        {
            get { return GetValue<String>(nameof(AvailQTY)); }
            set { SetValue(value, nameof(AvailQTY)); }
        }
        public String AvailDate
        {
            get { return GetValue<String>(nameof(AvailDate)); }
            set { SetValue(value, nameof(AvailDate)); }
        }
        public String RoutingAction
        {
            get { return GetValue<String>(nameof(RoutingAction)); }
            set { SetValue(value, nameof(RoutingAction)); }
        }
        public String Material
        {
            get { return GetValue<String>(nameof(Material)); }
            set { SetValue(value, nameof(Material)); }
        }
        public String RootDemandCode
        {
            get { return GetValue<String>(nameof(RootDemandCode)); }
            set { SetValue(value, nameof(RootDemandCode)); }
        }
        public String AltGroup
        {
            get { return GetValue<String>(nameof(AltGroup)); }
            set { SetValue(value, nameof(AltGroup)); }
        }
        public String PrimeMaterial
        {
            get { return GetValue<String>(nameof(PrimeMaterial)); }
            set { SetValue(value, nameof(PrimeMaterial)); }
        }
        public String InputDate
        {
            get { return GetValue<String>(nameof(InputDate)); }
            set { SetValue(value, nameof(InputDate)); }
        }
        public String Resource
        {
            get { return GetValue<String>(nameof(Resource)); }
            set { SetValue(value, nameof(Resource)); }
        }
        public String IActualRoutingFrom
        {
            get { return GetValue<String>(nameof(IActualRoutingFrom)); }
            set { SetValue(value, nameof(IActualRoutingFrom)); }
        }
        public String IRootDemandDate
        {
            get { return GetValue<String>(nameof(IRootDemandDate)); }
            set { SetValue(value, nameof(IRootDemandDate)); }
        }
        public String IInventory
        {
            get { return GetValue<String>(nameof(IInventory)); }
            set { SetValue(value, nameof(IInventory)); }
        }
        public String ISafetyLT
        {
            get { return GetValue<String>(nameof(ISafetyLT)); }
            set { SetValue(value, nameof(ISafetyLT)); }
        }
        public String OAltOrder
        {
            get { return GetValue<String>(nameof(OAltOrder)); }
            set { SetValue(value, nameof(OAltOrder)); }
        }
        public String ORootPlant
        {
            get { return GetValue<String>(nameof(ORootPlant)); }
            set { SetValue(value, nameof(ORootPlant)); }
        }
        public String DemandID
        {
            get { return GetValue<String>(nameof(DemandID)); }
            set { SetValue(value, nameof(DemandID)); }
        }
        public String DemandType
        {
            get { return GetValue<String>(nameof(DemandType)); }
            set { SetValue(value, nameof(DemandType)); }
        }
        public String DemandBucket
        {
            get { return GetValue<String>(nameof(DemandBucket)); }
            set { SetValue(value, nameof(DemandBucket)); }
        }
        public String AvailBucket
        {
            get { return GetValue<String>(nameof(AvailBucket)); }
            set { SetValue(value, nameof(AvailBucket)); }
        }
        public String QpaMultiple
        {
            get { return GetValue<String>(nameof(QpaMultiple)); }
            set { SetValue(value, nameof(QpaMultiple)); }
        }
        public String QpaUnit
        {
            get { return GetValue<String>(nameof(QpaUnit)); }
            set { SetValue(value, nameof(QpaUnit)); }
        }

        public String SupplyID
        {
            get { return string.Format("{0}_{1}_{2}", Demand, LLC.ToString(), SplitNo.ToString()); }
        }

        public String SupplyPID
        {
            get
            {
                if (SplitNo == 0 && LLC == 0)
                {
                    return "";
                }
                if (SplitNo > 0 && LLC == 0)
                {
                    return string.Format("{0}_{1}_{2}", Demand, LLC.ToString(), "0");
                }
                return string.Format("{0}_{1}_{2}", ParentDemand, (LLC - 1).ToString(), "0");
            }
        }

    }

}
