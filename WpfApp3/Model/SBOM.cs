using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.Model
{
    class SBOM : BindableBase
    {
        // 시스템 내부 Node 번호
        public int RowNo
        {
            get { return GetValue<int>(nameof(RowNo)); }
            set { SetValue(value, nameof(RowNo)); }
        }

        // 시스템 내부 부모 Node 번호
        public int RowPNo
        {
            get { return GetValue<int>(nameof(RowPNo)); }
            set { SetValue(value, nameof(RowPNo)); }
        }

        //Root Plant
        public string RootPlant
        {
            get { return GetValue<string>(nameof(RootPlant)); }
            set { SetValue(value, nameof(RootPlant)); }
        }

        //Root Locaton
        public string RootLocation
        {
            get { return GetValue<string>(nameof(RootLocation)); }
            set { SetValue(value, nameof(RootLocation)); }
        }

        //Root Material
        public string RootMaterial
        {
            get { return GetValue<string>(nameof(RootMaterial)); }
            set { SetValue(value, nameof(RootMaterial)); }
        }

        //Parent Location
        public string ParentLocation
        {
            get { return GetValue<string>(nameof(ParentLocation)); }
            set { SetValue(value, nameof(ParentLocation)); }
        }

        //Parent Material
        public string ParentMaterial
        {
            get { return GetValue<string>(nameof(ParentMaterial)); }
            set { SetValue(value, nameof(ParentMaterial)); }
        }

        //LEVEL
        public int Level
        {
            get { return GetValue<int>(nameof(Level)); }
            set { SetValue(value, nameof(Level)); }
        }

        //Location
        public string Location
        {
            get { return GetValue<string>(nameof(Location)); }
            set { SetValue(value, nameof(Location)); }
        }

        //Material
        public string Material
        {
            get { return GetValue<string>(nameof(Material)); }
            set { SetValue(value, nameof(Material)); }
        }

        //Routing From
        public string RoutingFrom
        {
            get { return GetValue<string>(nameof(RoutingFrom)); }
            set { SetValue(value, nameof(RoutingFrom)); }
        }

        //Routing Kind
        public string RoutingKind
        {
            get { return GetValue<string>(nameof(RoutingKind)); }
            set { SetValue(value, nameof(RoutingKind)); }
        }

        //Routing LeadTime
        public int RoutingLeadTime
        {
            get { return GetValue<int>(nameof(RoutingLeadTime)); }
            set { SetValue(value, nameof(RoutingLeadTime)); }
        }

        //QPA
        public double Qpa
        {
            get { return GetValue<double>(nameof(Qpa)); }
            set { SetValue(value, nameof(Qpa)); }
        }

        //ALT Group
        public string AltGroup
        {
            get { return GetValue<string>(nameof(AltGroup)); }
            set { SetValue(value, nameof(AltGroup)); }
        }

        //ALT Order
        public string AltOrder
        {
            get { return GetValue<string>(nameof(AltOrder)); }
            set { SetValue(value, nameof(AltOrder)); }
        }

        //A IS MTS
        public string AIsMTS
        {
            get { return GetValue<string>(nameof(AIsMTS)); }
            set { SetValue(value, nameof(AIsMTS)); }
        }

        //SBomID Key
        public string SBomID
        {
            get { return string.Format("{0}_{1}_{2}", Level.ToString(), Location, Material); }
        }

        //SBomID Key
        public string SBomPID
        {
            get
            {
                if (Level == 0)
                {
                    return "";
                }
                return string.Format("{0}_{1}_{2}", (Level - 1).ToString(), ParentLocation, ParentMaterial);
            }
        }

        //Location, Qpa 표현
        public string LocationQpa
        {
            get
            {
                return string.Format("{0}({1})", Location, Qpa.ToString());
            }
        }

        public override string  ToString()
        {
            return string.Format("{0},{1},{2}", Level, Location, Material);
        }
    }

}
