using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MvcControls.Controls;

namespace MvcControls.Models
{
    public class ListViewTestModel
    {
        [DisplayName("Die Items")]
        public List<MeinItem> MeineItems { get; set; }

        [DisplayName("Mitarbeiter")]
        public List<MeinItem> DieChicks { get; set; }

        public List<string> SelectedChicks { get; set; }

        public string Test { get; set; }

        public List<string> SelectedItems { get; set; }

        public List<string> Ruedi { get; set; }

        public string SelectionResult { get; set; }

        public ListViewTestModel()
        {
            MeineItems = new List<MeinItem>
            {
                new MeinItem(1, "Hans"),
                new MeinItem(2, "Pesche"),
                new MeinItem(3, "Ueli"),
                new MeinItem(4, "Ruedi"),
                new MeinItem(5, "Rudolf")
            };

            DieChicks = new List<MeinItem>
            {
                new MeinItem(770, "Saskia"),
                new MeinItem(501, "Janine"),
                new MeinItem(8804, "Sarah"),
                new MeinItem(5152, "Elvira"),
                new MeinItem(2, "Petra"),
                new MeinItem(99, "Julia"),
                new MeinItem(69, "Judith"),
                new MeinItem(70, "Miranda"),
                new MeinItem(71, "Melanie"),
                new MeinItem(72, "Mirella"),
                new MeinItem(73, "Marlene"),
                new MeinItem(74, "Leila")
            };
        }
    }

    [DebuggerDisplay("MeinItem: Id={Id}, IsSelected={IsSelected}")]
    public class MeinItem : ISelectable
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public bool IsSelected { get; set; }

        public MeinItem()
        {

        }

        public MeinItem(int id, string caption)
        {
            Id = id;
            Caption = caption;
        }
    }
}
