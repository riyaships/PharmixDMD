using System.Collections.Generic;
using X.PagedList;

namespace Pharmix.Web.Models
{
    public class GridBoxViewModel
    {
        public string GridName { get; set; } = "GridBoxResult";
        public IPagedList<GridBoxRow> Rows { get; set; }
        public string PagingRoute { get; set; }
        public string PagingAction { get; set; }
        public string DefaultSortColum { get; set; }
        public bool HideFooter { get; set; }
        public string BackgroundIconCss { get; set; }
        public bool IsBoxDraggable { get; set; }
        public bool ShowBodyContentOnHover { get; set; }
    }
    
    public class GridBoxRow
    {
        public GridBoxRow()
        {
            BoxBodyDictionary = new Dictionary<string, string>();
            DataAttriDictionary = new Dictionary<string, string>();
        }
        public int IdentityValue { get; set; }

        public string BoxHeading { get; set; }

        public IDictionary<string, string> BoxBodyDictionary { get; set; }

        public IDictionary<string, string> DataAttriDictionary { get; set; }

        public List<ActionLinkCell> ActionLinks { get; set; } = new List<ActionLinkCell>();

        public void AddActionLink(string linkText, string linkUrl = "#")
        {
            ActionLinks.Add(new ActionLinkCell { LinkText = linkText, LinkUrl = linkUrl });
        }

        public void AddActionIcon(string iconCss, string titleText = "", string linkUrl = "#")
        {
            ActionLinks.Add(new ActionLinkCell { IconCss = iconCss, LinkUrl = linkUrl, LinkTitleText = titleText });
        }
    }
}
