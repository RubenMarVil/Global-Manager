using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Control
{
    public enum ProjectCharacteristicLevels
    {
        NULL,
        VERY_HIGH,
        HIGH,
        NORMAL,
        LOW,
        VERY_LOW
    }

    public class ProjectCharacteristic
    {
        public string Name;
        public ProjectCharacteristicLevels Level;

        public ProjectCharacteristic(string name, string value)
        {
            Name = name;
            
            switch(value)
            {
                case "VERY HIGH":
                    Level = ProjectCharacteristicLevels.VERY_HIGH;
                    break;
                case "HIGH":
                    Level = ProjectCharacteristicLevels.HIGH;
                    break;
                case "NORMAL":
                    Level = ProjectCharacteristicLevels.NORMAL;
                    break;
                case "LOW":
                    Level = ProjectCharacteristicLevels.LOW;
                    break;
                case "VERY LOW":
                    Level = ProjectCharacteristicLevels.VERY_LOW;
                    break;
            }
        }

        public ProjectCharacteristic(string name, ProjectCharacteristicLevels level)
        {
            Name = name;
            Level = level;
        }
    }
}
