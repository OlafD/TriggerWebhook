using System;
using System.IO;
using System.Reflection;

class ConsoleHelper
{
	private string m_strProgramName = "";
	private string m_strCopyright = "";

	public string ProgramName
	{
		get
		{
			return m_strProgramName;
		}

		set
		{
			m_strProgramName = value;
		}
	}

	public string Copyright
	{
		get
		{
			return m_strCopyright;
		}

		set
		{
			m_strCopyright = value;
		}
	}

	/// <summary>
	/// Create a ConsoleHeper-object
	/// </summary>
	public ConsoleHelper() {}

	/// <summary>
	/// Create a ConsoleHelper-object and set member ProgramName
	/// </summary>
	/// <param name="strProgramName">Name of the program for the welcome text</param>
	public ConsoleHelper(string strProgramName)
	{
		m_strProgramName = strProgramName;
	}

	/// <summary>
	/// Create a ConsoleHelper-object and set members ProgramName and Copyright
	/// </summary>
	/// <param name="strProgramName">Name of the program for the welcome text</param>
	/// <param name="strCopyright">Copyright for the program for the welcome text</param>
	public ConsoleHelper(string strProgramName, string strCopyright)
	{
		m_strProgramName = strProgramName;
		m_strCopyright = strCopyright;
	}

	/// <summary>
	/// Show name, version and copyright
	/// </summary>
	public  void welcome()
	{
		string strMajor = "";
		string strMinor = "";
		string strBuild = "";
		string strRevision = "";
		string strVersion = "";

		strMajor = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
		strMinor = Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString();
		strBuild = Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();
		strRevision = Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();
		
		strVersion = strMajor + "." + strMinor + "." + strBuild;

		Console.WriteLine("");
		Console.WriteLine(m_strProgramName + " " + strVersion + " - " + m_strCopyright);
		// Console.WriteLine("WssBackupSites {0} - (c) SPO/odi, 2006", strVersion);
		Console.WriteLine("");
	}

	public  bool getYesNoValue(string strValue)
	{
		return getYesNoValue(strValue, false);
	}

	public  bool getYesNoValue(string strValue, bool bDefault)
	{
		bool bResult = bDefault;

		switch (strValue.ToUpper())
		{
			case "1":
			case "YES":
			case "ON":
			case "TRUE":
				bResult = true;
				break;
			case "0":
			case "NO":
			case "OFF":
			case "FALSE":
				bResult = false;
				break;
		}

		return bResult;
	}

	public  bool hasNamedArgument(string[] args, string argName)
	{
		bool bResult = false;
		string[] strArg = null;
		string strActArg = "";

		for (int i=0 ; i<args.Length ; i++)
		{
			strArg = args[i].Split(':');

			if (strArg.Length >= 1)  // 16-10-2006, ODI, changed from 2 to 1
			{
				strActArg = strArg[0].Remove(0, 1);

				if (strActArg.ToUpper() == argName.ToUpper())
				{
					bResult = true;
				}
			}
		}

		return bResult;
	}

	public  string getNamedArgument(string[] args, string argName, string strDefault)
	{
		string strResult = "";
		string[] strArg;
		string strActArg;

		for (int i=0 ; i<args.Length ; i++)
		{
			strArg = args[i].Split(':');

			if (strArg.Length >= 2)
			{
				strActArg = strArg[0].Remove(0, 1);

				if (strActArg.ToUpper() == argName.ToUpper())
				{
					strResult = strArg[1];

					if (strArg.Length > 2)
					{
						for (int j=2; j<strArg.Length; j++)
						{
							strResult = strResult + ":" + strArg[j];
						}
					}
				}
			}
		}

		if (strResult == "")
			strResult = strDefault;

		return strResult;
	}

	public  int getNamedArgumentInt(string[] arguments, string strParameter, int iDefault)
	{
		string strValue = getNamedArgument(arguments, strParameter, Convert.ToString(iDefault));

		int iResult = Convert.ToInt32(strValue);

		return iResult;
	}

	public double getNamedArgumentDouble(string[] arguments, string strParameter, double iDefault)
	{
		string strValue = getNamedArgument(arguments, strParameter, Convert.ToString(iDefault));

		double dblResult = Convert.ToDouble(strValue);

		return dblResult;
	}

	public  bool getNamedArgumentBool(string[] arguments, string strParameter, bool bDefault)
	{
		string strValue = getNamedArgument(arguments, strParameter, "");

		bool bResult = getYesNoValue(strValue, bDefault);

		return bResult;
	}

	public  void error(Exception ex)
	{
		Console.WriteLine("ERROR: {0}", ex.Message);
	}

	public  void error(string strMessage)
	{
		Console.WriteLine("Error: {0}", strMessage);
	}
}
