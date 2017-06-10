using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngkorMoon.Desktop.Services;
using AngkorMoon.Desktop.Utils;

namespace AngkorMoon.Desktop.ViewModules.Parts
{
    class PartListViewModel : BindableBase
    {
        public PartListViewModel(ICommandHandler commandHandler)
            : base(commandHandler)
        {
        }
    }
}
