using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
/// <summary>
/// 合并GridView列
/// </summary>
public class MergGridRow
{
    public MergGridRow()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 合并单列的行
    /// </summary>
    /// <param name="gv">GridView</param>
    /// <param name="currentCol">当前列</param>
    /// <param name="startRow">开始合并的行索引</param>
    /// <param name="endRow">结束合并的行索引</param>
    private static void MergeRow(GridView gv, int currentCol, int startRow, int endRow)
    {
        for (int rowIndex = endRow; rowIndex >= startRow; rowIndex--)
        {
            GridViewRow currentRow = gv.Rows[rowIndex];
            GridViewRow prevRow = gv.Rows[rowIndex + 1];
            //if (currentRow.Cells[currentCol].Text != "" && currentRow.Cells[currentCol].Text != " ")
            {
                if (currentRow.Cells[currentCol].Text == prevRow.Cells[currentCol].Text)
                {
                    currentRow.Cells[currentCol].RowSpan = prevRow.Cells[currentCol].RowSpan < 1 ? 2 : prevRow.Cells[currentCol].RowSpan + 1;
                    prevRow.Cells[currentCol].Visible = false;
                }
            }
        }
    }
    /// <summary>
    /// 遍历前一列
    /// </summary>
    /// <param name="gv">GridView</param>
    /// <param name="prevCol">当前列的前一列</param>
    /// <param name="list"></param>
    private static void TraversesPrevCol(GridView gv, int prevCol, List<RowArg> list)
    {
        if (list == null)
        {
            list = new List<RowArg>();
        }
        RowArg ra = null;
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            if (!gv.Rows[i].Cells[prevCol].Visible)
            {
                continue;
            }
            ra = new RowArg();
            ra.StartRowIndex = gv.Rows[i].RowIndex;
            ra.EndRowIndex = ra.StartRowIndex + gv.Rows[i].Cells[prevCol].RowSpan - 2;
            list.Add(ra);
        }
    }
    /// <summary>
    /// GridView合并行，
    /// </summary>
    /// <param name="gv">GridView</param>
    /// <param name="startCol">开始列</param>
    /// <param name="endCol">结束列</param>
    public static void MergeRow(GridView gv, int startCol, int endCol)
    {
        RowArg init = new RowArg()
        {
            StartRowIndex = 0,
            EndRowIndex = gv.Rows.Count - 2
        };
        for (int i = startCol; i < endCol + 1; i++)
        {
            if (i > 0)
            {
                List<RowArg> list = new List<RowArg>();
                //从第二列开始就要遍历前一列
                TraversesPrevCol(gv, i - 1, list);
                foreach (var item in list)
                {
                    MergeRow(gv, i, item.StartRowIndex, item.EndRowIndex);
                }
            }
            //合并开始列的行
            else
            {
                MergeRow(gv, i, init.StartRowIndex, init.EndRowIndex);
            }
        }
    }

    /// <summary>
    /// 合并GridView单元格
    /// </summary>
    /// <param name="gv">要合并的GridView</param>
    /// <param name="cols">指定的列，要按照顺序传入</param>
    public static void MergeRow(GridView gv, params int[] cols)
    {
        RowArg init = new RowArg()
        {
            StartRowIndex = 0,
            EndRowIndex = gv.Rows.Count - 2
        };
        for (int i = 0; i < cols.Length; i++)
        {
            if (i > 0)
            {
                List<RowArg> list = new List<RowArg>();
                //从第二列开始就要遍历前一列
                TraversesPrevCol(gv, cols[i - 1], list);
                foreach (var item in list)
                {
                    MergeRow(gv, cols[i], item.StartRowIndex, item.EndRowIndex);
                }
            }
            //合并开始列的行
            else
            {
                MergeRow(gv, i, init.StartRowIndex, init.EndRowIndex);
            }
        }
    }
    //private static void TraversesPrevCol(GridView gv, int prevCol, List<RowArg> list)
    // {
    //     if (list == null)
    //    {
    //       list = new List<RowArg>();
    //    }
    //    RowArg ra = null;
    //    for (int i = 0; i<gv.Rows.Count; i++)
    //    {
    //        if (!gv.Rows[i].Cells[prevCol].Visible)
    //        {
    //            continue;
    //        }
    //        ra = new RowArg();
    //        ra.StartRowIndex = gv.Rows[i].RowIndex;
    //        ra.EndRowIndex = ra.StartRowIndex + gv.Rows[i].Cells[prevCol].RowSpan - 2;
    //        list.Add(ra);
    //    }
    //}
    //  /// <summary>
    // /// 合并单列的行
    // /// </summary>
    // /// <param name="gv">GridView</param>
    // /// <param name="currentCol">当前列</param>
    // /// <param name="startRow">开始合并的行索引</param>
    // /// <param name="endRow">结束合并的行索引</param>
    //    private static void MergeRow(GridView gv, int currentCol, int startRow, int endRow)
    // {
    //    for (int rowIndex = endRow; rowIndex >= startRow; rowIndex--)
    //    {
    //        GridViewRow currentRow = gv.Rows[rowIndex];
    //        GridViewRow prevRow = gv.Rows[rowIndex + 1];
    //        if (currentRow.Cells[currentCol].Text != "" && currentRow.Cells[currentCol].Text != " ")
    //        {
    //            if (currentRow.Cells[currentCol].Text == prevRow.Cells[currentCol].Text)
    //           {
    //                currentRow.Cells[currentCol].RowSpan = prevRow.Cells[currentCol].RowSpan< 1 ? 2 : prevRow.Cells[currentCol].RowSpan + 1;
    //                prevRow.Cells[currentCol].Visible = false;
    //            }
    //        }
    //    }
    //}

}
public class RowArg
{
    public int StartRowIndex { get; set; }
    public int EndRowIndex { get; set; }
}
