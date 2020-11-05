using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Control
{
    public class CommunicationConfiguration
    {
        public List<string> Communication { get; set; }

        public CommunicationConfiguration(List<ToggleButton> communicationList)
        {
            Communication = new List<string>();

            foreach(ToggleButton tool in communicationList)
            {
                Communication.Add(tool.name);
            }
        }
    }
}