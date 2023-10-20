using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Model;

public class MgDatabaseSettings : IMgDatabaseSettings
{

    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }

}

public interface IMgDatabaseSettings
{

    string ConnectionString { get; set; }
    string DatabaseName { get; set; }

}