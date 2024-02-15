using System;
using System.IO;
using System.Text;

class Test
{
  public static void Main()
    {
      string pathInput = @"./InputText.txt";
      string pathOutput = @"./OutputText.txt";

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
      
      while ((line = streamReader.ReadLine()) != null)
        {
          for (int i = 0; i < line.Length; i++)
            {
              line = line.ToLower().
                Replace("!", "").
                Replace("?", "").
                Replace(",", "").
                Replace(".", "").
                Replace(";", "").
                Replace("\\", "").
                Replace("\'", "").
                Replace("\"", "").
                Replace("[", "").
                Replace("]", "").
                Replace("*", "").
                Replace("/", "").
                Replace(":", "").
                Replace("(", "").
                Replace(")", "").
                Replace("-", "");
            }
          lineString = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
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
            // Console.WriteLine("{0}   {1}", lineString[v], wordCounter[lineString[v]]);
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