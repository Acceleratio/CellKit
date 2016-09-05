﻿using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Acceleratio.XCellKit
{
    public class SpredsheetRow
    {
        public SpredsheetRow()
        {
            RowCells = new List<SpredsheetCell>();
        }

        public void AddCell(SpredsheetCell cell)
        {
            RowCells.Add(cell);
        }

        public void AddCellRange(List<SpredsheetCell> cells)
        {
            RowCells.AddRange(cells);
        }

        private bool _isVisible = true;

        public void SetRowVisibleState(bool isVisible)
        {
            _isVisible = isVisible;
        }

        public List<SpredsheetCell> RowCells { get; set; }

        public void WriteRow(OpenXmlWriter writer, int columnIndex, int rowIndex, SpredsheetStylesManager stylesManager, SpredsheetHyperlinkManager hyperlinkManager)
        {
            var span = string.Format("{0}:{1}", columnIndex, RowCells.Count + columnIndex);
            var attributeList = new List<OpenXmlAttribute>();
            var rowIndexAtt = new OpenXmlAttribute("r", null, rowIndex.ToString());
            var spanAtt = new OpenXmlAttribute("spans", null, span);
            attributeList.Add(rowIndexAtt);
            attributeList.Add(spanAtt);
            if (!_isVisible)
            {
                var hiddenAttribute = new OpenXmlAttribute("hidden", null, 1.ToString());
                attributeList.Add(hiddenAttribute);
            }

            writer.WriteStartElement(new Row(), attributeList);
            foreach (var cell in RowCells)
            {
                cell.WriteCell(writer, columnIndex, rowIndex, stylesManager, hyperlinkManager);
                columnIndex++;
            }
            writer.WriteEndElement();
        }
    }
}
