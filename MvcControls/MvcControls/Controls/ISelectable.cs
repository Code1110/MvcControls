using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcControls.Controls
{
    public interface ISelectable
    {
        int Id { get; set; }
        string Caption { get; set; }
        bool IsSelected { get; set; }
    }
}
