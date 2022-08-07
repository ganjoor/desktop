using System;
using System.Collections.Generic;
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
                var Lines = File.ReadAllLines(filePath);
                var curSection = "";
                foreach (var Line in Lines)
                {
                    var trimedLine = Line.Trim();
                    var bracketStart = trimedLine.IndexOf('[');
                    var bracketEnd = trimedLine.IndexOf(']');
                    if (bracketStart != -1 && bracketEnd > bracketStart)
                    {
                        //section
                        curSection = trimedLine.Substring(bracketStart + 1, bracketEnd - (bracketStart + 1));
                    }
                    else
                    {
                        var keyValue = trimedLine.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                        if (keyValue.Length == 2)
                        {
                            var newKeyValue = new Dictionary<string, string>();
                            newKeyValue.Add(keyValue[0].Trim(), keyValue[1].Trim());
                            _Values.Add(curSection, newKeyValue);
                        }
                    }
                }
            }
        }

        private Dictionary<string, Dictionary<string, string>> _Values;

        public Dictionary<string, Dictionary<string, string>> Values => _Values;
    }
}
