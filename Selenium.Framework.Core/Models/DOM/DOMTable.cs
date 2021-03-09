using OpenQA.Selenium;
using System.Collections.Generic;

namespace Selenium.Framework.Core.Models
{
    public class DOMTable
    {

        public IWebDriver driver { get; set; }
        private IWebElement baseTable;
        private IWebElement baseTableHead;
        private IWebElement baseTableBody;

        public DOMTable(IWebDriver driver, IWebElement baseTable)
        {
            this.driver = driver;
            this.baseTable = baseTable;
            baseTableHead = baseTable.FindElements(By.TagName("thead"))[0];
            baseTableBody = baseTable.FindElements(By.TagName("tbody"))[0];


            TableHeaders = new List<TableHeader>();
            int _colIndex = 0;
            foreach (IWebElement th in baseTableHead.FindElements(By.TagName("th")))
            {
                TableHeaders.Add(new TableHeader(th, _colIndex));
                _colIndex++;
            }


            TableRows = new List<TableRow>();
            int _rowIndex = 0;
            foreach (IWebElement row in baseTableBody.FindElements(By.TagName("tr")))
            {
                TableRows.Add(new TableRow(row, _rowIndex));
                _rowIndex++;
            }

        }

        #region Headers
        public List<TableHeader> TableHeaders { get; set; }
        public class TableHeader
        {
            public IWebElement Content { get; set; }
            public int ColumnIndex { get; set; }


            public TableHeader(IWebElement th, int colIndex)
            {
                Content = th;
                ColumnIndex = colIndex;
            }
        }
        #endregion

        #region Rows

        public List<TableRow> TableRows { get; set; }

        public class TableRow
        {
            public List<TableCell> TableCells { get; set; }
            public int RowIndex { get; set; }

            public TableRow(IWebElement row, int rowIndex)
            {
                TableCells = new List<TableCell>();
                foreach (IWebElement cell in row.FindElements(By.TagName("td")))
                {
                    TableCells.Add(new TableCell(cell));
                }

                RowIndex = rowIndex;
            }
        }

        #endregion

        public class TableCell
        {
            public IWebElement Content { get; set; }
            public TableCell(IWebElement webElement)
            {
                Content = webElement;
            }
        }


    }


}
