// 
// PraatInvoke.cs
//  
// Author:
//       Geza Kovacs <gkovacs@mit.edu>
// 
// Copyright (c) 2009 Geza Kovacs
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

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
