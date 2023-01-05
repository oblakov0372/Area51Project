using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastProjectVersion2.Models
{
    public enum EFloor
    {
        G,
        S,
        T1,
        T2
    }
    public class Floor
    {
        private EFloor name;
        public EFloor Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                if(ButtonToCallElevator != null)
                    ButtonToCallElevator.Floor = value;
            }
        }
        public Button ButtonToCallElevator { get; set; }
        public Floor(EFloor name)
        {
            Name = name;
            ButtonToCallElevator = new Button(name);
        }
    }
}
