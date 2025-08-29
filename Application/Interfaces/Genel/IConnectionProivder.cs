using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Genel
{
    public interface IConnectionProivder
    {
        IDbConnection CreateConnection();
    }

    public interface ISQLXConneciton
    {
      IDbConnection CreateConnection();
    }
    public interface IXConnectionProvider
    {
        IDbConnection CreateConnection();
    }
}
