using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DiscountSystem.Data.Common
{
  public class ExceptionManager
  {
    private readonly IConfiguration _config;
    public void LogFile(string Exception, string logType, int nErrorLineNo)
    {
      string today = DateTime.Now.ToString("M-d-yyyy");

      string folderPath;

      if (logType == "Error")
      {
        CreateFolder(_config["LogFile"] + "/Error/" + today);
        folderPath = _config["LogFile"] + "/Error/" + today;

      }
      else
      {
        CreateFolder(_config["LogFile"] + "/Info/" + today);
        folderPath = _config["LogFile"] + "/Info/" + today;

      }

      StreamWriter log;



      string logPath = folderPath + "/logfile.txt";

      if (!File.Exists(logPath))
      {

        log = new StreamWriter(logPath);

      }

      else
      {

        log = File.AppendText(logPath);

      }

      // Write to the file:

      log.WriteLine(DateTime.Now + "|" + Exception + "|" + nErrorLineNo);

      //log.WriteLine("Error Line No.:" + nErrorLineNo);

      // Close the stream:

      log.Close();
    }

    private void CreateFolder(string path)
    {
      bool folderExists = Directory.Exists(path);
      if (!folderExists)
        Directory.CreateDirectory(path);
    }
  }
  public static class ExceptionHelper
  {
    public static int LineNumber(this Exception e)
    {
      int linenum = 0;
      try
      {
        linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(":line") + 5));
      }
      catch
      {
        //Stack trace is not available!
      }
      return linenum;
    }

  }
}
