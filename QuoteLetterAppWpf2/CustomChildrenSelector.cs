using ClassLibrary.Models;
using DevExpress.Xpf.Grid;
using System.Collections;

namespace QuoteLetterAppWpf2
{
    public class CustomChildrenSelector : IChildNodesSelector
    {
        //public class RFQObject : BaseObject
        //{
        //    public ObservableCollection<PartModel> Parts { get; set; }
        //}

        //public class PartObject : BaseObject
        //{
        //    State state;
        //}
        public IEnumerable SelectChildren(object item)
        {
            if (item is PartModel)
                return null;
            else if (item is RFQModel)
                return (item as RFQModel).Parts;
            return null;
        }
    }
}
