using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

class Test
{
  public static void Main()
    {
      string pathInput = @"./inputText.txt";
      string pathOutput = @"./outputText.txt";

      if (!File.Exists(pathInput))
        {
          string createText = "The file was empty" + Environment.NewLine;
          File.WriteAllText(pathOutput, createText);
        }

      const Int32 BufferSize = 128;
      using var fileStream = File.OpenRead(pathInput);
      using var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize);
      
      Dictionary<string, int> wordCounter = [];
      string[] lineString;
      string line;
      string pattern = 
        "(-- -- -|[:]|[/]+|\\*|\\.+|\\,+|\\!+|\\;|\\?+|\\[|\\]|\\(+|\\)+|\"+|([0-9]+)|([\\-]{2}))";
      
      while ((line = streamReader.ReadLine()) != null)
        {
          var lineClean = Regex.Replace(line, pattern, " ");
          lineString = lineClean.ToLower().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
          for (int v = 0; v < lineString.Length; v++)
            {
              if (wordCounter.ContainsKey(lineString[v])) 
                {
                wordCounter[lineString[v]] += 1;
                }
              else 
                {
                wordCounter[lineString[v]] = 1; 
                }
            }
       
        }
      var sortedDict = from entry in wordCounter orderby entry.Value descending select entry;
      foreach(var kvp in sortedDict)
        {
            var appendText = kvp.Key + " " + kvp.Value + Environment.NewLine;
            File.AppendAllText(pathOutput, appendText);
        }
    }
}
