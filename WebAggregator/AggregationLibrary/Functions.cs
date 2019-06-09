using System;
using System.Collections.Generic;
using System.IO;

namespace AggregationLibrary
{
    public class Functions
    {
        public Functions(string path)
        {
            fullFilePath = path + @"\FullData.csv";
        }
        string fullFilePath;
        public List<string> GetHeaders(string filepath)
        {
            using (StreamReader sr = new StreamReader(filepath, System.Text.Encoding.Default))
            {
                string line = sr.ReadLine();
                var array = line.Split(';');
                List<string> headers = new List<string>();
                for (int i = 0; i < array.Length; ++i)
                {
                    headers.Add(array[i]);
                }
                return headers;
            }
        }

        public List<string> GetCurrentHeaders()
        {
            return GetHeaders(fullFilePath);
        }
        public bool IsResultFileEmpty()
        {
            using (StreamReader sr = new StreamReader(fullFilePath, System.Text.Encoding.Default))
            {
                string headers = sr.ReadLine();
                if (headers == null)
                    return true;
                else
                    return false;
            }
        }

        public void WriteToEmptyFile(string filepath, int year, List<int> indexs)
        {
            using (StreamReader sr = new StreamReader(filepath, System.Text.Encoding.Default))
            {
                string headers = sr.ReadLine();
                using (StreamWriter sw = new StreamWriter(fullFilePath, false, System.Text.Encoding.Default))
                {
                    var arrayHeaders = headers.Split(';');
                    string headersLine = "years";
                    for (int i = 0; i < indexs.Count; ++i)
                    {
                        headersLine += ";" + arrayHeaders[indexs[i]];
                    }
                    sw.WriteLine(headersLine);
                    string line;

                    while((line = sr.ReadLine()) != null)
                    {
                        var array = line.Split(';');
                        line = year.ToString();
                        for (int i = 0; i < indexs.Count; ++i)
                        {
                            line += ";" + array[indexs[i]];
                        }
                        sw.WriteLine(line);
                    }
                }
            }
        }

        public void WriteToFile(string filepath,int year, List<int> indexs)
        {
            int length = GetCurrentHeaders().Count;
            using (StreamReader sr = new StreamReader(filepath, System.Text.Encoding.Default))
            {
                using (StreamWriter sw = new StreamWriter(fullFilePath, true, System.Text.Encoding.Default))
                {
                   
                    string line;
                    line = sr.ReadLine();
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] array = new string[length];
                        var curline = line.Split(';');
                        for (int i = 0; i < indexs.Count; ++i)
                        {
                            if (indexs[i] > 0)
                            {
                                array[indexs[i]] = curline[i];
                            }
                        }
                        line = year.ToString();
                        for (int i = 1; i < array.Length; ++i)
                        {
                            line += ";" + array[i];
                        }
                        sw.WriteLine(line);
                    }
                }
            }
        }
    }
}
