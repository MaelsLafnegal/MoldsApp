using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoldsApp.Infrastructure.Interfaces
{
    public interface IMessenger
    {
        void Notify(object sender, object DataContextViewModel);
    }
}
