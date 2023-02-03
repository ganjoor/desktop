using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ganjoor
{
    /// <summary>
    /// This is not a serious parser, I just wanted to not use DLLImport,
    /// </summary>
    public class GINIParser
    {
        public GINIParser(string filePath)
        {
            _Values = new Dictionary<string, Dictionary<string, string>>();
            if (File.Exists(filePath))
            {
                string[] Lines = File.ReadAllLines(filePath);
                string curSection = "";
                foreach (string Line in Lines)
                {
                    string trimedLine = Line.Trim();
                    int bracketStart = trimedLine.IndexOf('[');
                    int bracketEnd = trimedLine.IndexOf(']');
                    if (bracketStart != -1 && bracketEnd > bracketStart)
                    {
                        //section
                        curSection = trimedLine.Substring(bracketStart + 1, bracketEnd - (bracketStart + 1));
                    }
                    else
                    {
                        string[] keyValue = trimedLine.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        if (keyValue.Length == 2)
                        {
                            Dictionary<string, string> newKeyValue = new Dictionary<string,string>();
                            newKeyValue.Add(keyValue[0].Trim(), keyValue[1].Trim());
                            _Values.Add(curSection, newKeyValue);
                        }
                    }
                }
            }
        }

        private Dictionary<string, Dictionary<string, string>> _Values;

        public Dictionary<string, Dictionary<string, string>> Values
        {
            get
            {
                return _Values;
            }
        }
    }
}
