using System.Collections.Generic;
using X.PagedList;

namespace Pharmix.Web.Models
{
    public class GridViewModel
    {
        public string GridName { get; set; } = "GridResult";
        public List<GridColumn> Columns { get; set; } = new List<GridColumn>();
        public IPagedList<GridRow> Rows { get; set; }
        public string PagingRoute { get; set; }
        public string PagingAction { get; set; }
        public string DefaultSortColum { get; set; }
        public bool HideFooter { get; set; }
        
        public void AddColumn(string columnName, bool allowSort = false, string sortCol = "")
        {
            Columns.Add(new GridColumn { AllowSort = allowSort, ColumnName = columnName, SortBy = sortCol });
        }
    }

    public class GridColumn
    {
        public string SortBy { get; set; }
        public string ColumnName { get; set; }
        public bool AllowSort { get; set; }
    }

    public class GridRow
    {
        public int IdentityValue { get; set; }

        public List<GridCell> Cells { get; set; } = new List<GridCell>();

        public List<ActionLinkCell> ActionLinks { get; set; } = new List<ActionLinkCell>();

        public void AddCell(string cellValue, bool isLinkCell = false, string linkUrl = "#", bool isImage = false, string cellCss = "")
        {
            Cells.Add(new GridCell { CellValue = cellValue, IsLinkCell = isLinkCell, LinkUrl = linkUrl, IsImage = isImage, CellCss = cellCss});
        }

        public void AddActionLink(string linkText, string linkUrl = "#")
        {
            ActionLinks.Add(new ActionLinkCell { LinkText = linkText, LinkUrl = linkUrl });
        }

        public void AddActionIcon(string iconCss, string titleText = "", string linkUrl = "#")
        {
            ActionLinks.Add(new ActionLinkCell { IconCss = iconCss, LinkUrl = linkUrl, LinkTitleText = titleText });
        }
    }

    public class GridCell
    {
        public string CellValue { get; set; }
        public bool IsLinkCell { get; set; }
        public string LinkUrl { get; set; } = "#";
        public bool IsImage { get; set; }
        public string CellCss { get; set; }
    }

    public class ActionLinkCell
    {
        public string LinkText { get; set; }
        public string IconCss { get; set; }
        public string LinkTitleText { get; set; }
        public string LinkUrl { get; set; } = "#";
    }
}