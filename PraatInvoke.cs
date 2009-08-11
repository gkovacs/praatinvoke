//  
//  Copyright (C) 2009 Geza Kovacs
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace praatinvoke
{
	public class PraatInvoke
	{
		OutputPraatDelegate outputpraat;
		
		public string praatexe;
		public string praatscript;
		
		public static Pair<string, float>[] ParsePraatOutput(string s)
		{
			try
			{
				if (s == null)
					return null;
				if (s == string.Empty)
					return null;
				string[] sl = s.Split('\n');
				List<Pair<string, float>> o = new List<Pair<string, float>>();
				foreach (string x in sl)
				{
					string c = x.Trim();
					if (c.Count("=") != 1)
					{
						continue;
					}
					string[] parts = c.Split('=');
					if (parts.Length < 2)
						continue;
					string pname = parts[0].Trim();
					string psval = parts[1].Trim();
					if (pname == null || pname == string.Empty)
						continue;
					if (psval == null || psval == string.Empty)
						continue;
					bool convsuccess;
					float pval = psval.ToFloat(out convsuccess);
					if (!convsuccess)
						continue;
					o.Add(new Pair<string, float>(pname, pval));
				}
				return o.ToArray();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
		
		public string[] ListPraatVariables()
		{
			try
			{
				return ListPraatVariables(praatscript);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
		
		public static string[] ListPraatVariables(string filename)
		{
			try
			{
				FileStream r = new FileStream(filename, FileMode.Open);
				string[] ret = ListPraatVariables(r);
				r.Close();
				return ret;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
		
		public static string[] ListPraatVariables(FileStream stream)
		{
			try
			{
				StreamReader r = new StreamReader(stream);
				string[] ret = ListPraatVariables(r);
				r.Close();
				return ret;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
		
		public static string[] ListPraatVariables(StreamReader s)
		{
			try
			{
				List<string> paramlist = new List<string>();
				while (!s.EndOfStream)
				{
					string l = s.ReadLine().Trim();
					if (l.StartsWith("printline"))
					{
						l = l.Replace("printline", "").Trim();
						if (l.Count('=') == 1)
						{
							paramlist.Add(l.Split('=')[0].Trim());
						}
					}
				}
				return paramlist.ToArray();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
		
		public void StdoutHandler(object sender, DataReceivedEventArgs s)
		{
			try
			{
				if (s == null || s.Data == null)
					return;
				outputpraat(ParsePraatOutput(s.Data));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		
		public void StderrHandler(object sender, DataReceivedEventArgs s)
		{
			try
			{
				if (s == null || s.Data == null)
					return;
				outputpraat(ParsePraatOutput(s.Data));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		
		public void CallPraat(string wavfile)
		{
			try
			{
				Process p = new Process();
				p.StartInfo.RedirectStandardOutput = true;
	//			p.StartInfo.RedirectStandardError = true;
				p.StartInfo.UseShellExecute = false;
				p.StartInfo.CreateNoWindow = true;
				p.StartInfo.FileName = praatexe;
				p.StartInfo.Arguments += "-a "+praatscript+" "+wavfile;
				p.OutputDataReceived += new DataReceivedEventHandler(StdoutHandler);
	//			p.ErrorDataReceived += new DataReceivedEventHandler(StderrHandler);
				p.Start();
				p.BeginOutputReadLine();
	//			p.BeginErrorReadLine();
	//			p.WaitForExit();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		
		public CallPraatDelegate GetPraatDelegate()
		{
			try
			{
				return new CallPraatDelegate(CallPraat);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
		
		public void SetOutputPraatDelegate(OutputPraatDelegate d)
		{
			try
			{
				outputpraat = d;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		
		public PraatInvoke(string exef, string scriptf)
		{
			try
			{
				praatexe = exef;
				praatscript = scriptf;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
