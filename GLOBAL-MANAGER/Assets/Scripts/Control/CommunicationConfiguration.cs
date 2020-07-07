using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Control
{
    public enum CommunicationTool
    {
        PHONE,
        SKYPE,
        MICROSOFT_TEAMS,
        WHATSAPP,
        FORUM,
        E_MAIL,
        NO_TOOL
    }
    public class CommunicationConfiguration
    {
        public int NumSite1 { get; set; }
        public int NumSite2 { get; set; }
        public int CodSite1 { get; set; }
        public int CodSite2 { get; set; }
        public List<CommunicationTool> Communication { get; set; }

        public CommunicationConfiguration(int numSite1, int numSite2, List<string> communicationList)
        {
            NumSite1 = numSite1;
            NumSite2 = numSite2;

            Communication = new List<CommunicationTool>();

            foreach(string tool in communicationList)
            {
                Communication.Add(GetCommunicationTool(tool));
            }
        }

        private CommunicationTool GetCommunicationTool(string tool)
        {
            switch(tool)
            {
                case "Telephone":
                    return CommunicationTool.PHONE;
                case "Whatsapp":
                    return CommunicationTool.WHATSAPP;
                case "Skype":
                    return CommunicationTool.SKYPE;
                case "Forum":
                    return CommunicationTool.FORUM;
                case "Teams":
                    return CommunicationTool.MICROSOFT_TEAMS;
                case "Email":
                    return CommunicationTool.E_MAIL;
            }

            return CommunicationTool.NO_TOOL;
        }
    }
}
