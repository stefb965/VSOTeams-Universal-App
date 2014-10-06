using System;
using System.Collections.Generic;
using System.Text;

namespace VSOTeams.Interfaces
{
    public interface INavigable
    {
        void Activate(object parameter);
        void Deactivate(object parameter);

    }
}
