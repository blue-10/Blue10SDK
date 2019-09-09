using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Blue10SDKExampleConsole
{
    public class csvHelper
    {
        public static DataTable ConvertCSVtoDataTable(string pFile, char pSeparator)
        {
            var fRes = new DataTable();
            using (var fStreamReader = new StreamReader(pFile))
            {
                var headers = fStreamReader.ReadLine().Split(pSeparator);
                foreach (string header in headers)
                {
                    fRes.Columns.Add(header);
                }
                while (!fStreamReader.EndOfStream)
                {
                    string[] rows = fStreamReader.ReadLine().Split(pSeparator);
                    DataRow dr = fRes.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    fRes.Rows.Add(dr);
                }
            }
            return fRes;
        }
    }
}
