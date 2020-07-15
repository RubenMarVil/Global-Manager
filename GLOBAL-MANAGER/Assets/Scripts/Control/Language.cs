using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Language
{
    public string Name { get; set; }
    public bool Official { get; set; }

    public Language(string name, int official)
    {
        Name = name;
        Official = (official == 1) ? true : false;
    }
}