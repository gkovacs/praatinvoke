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
using System.Diagnostics;

namespace praatinvoke
{
	public class PraatInvoke
	{
		public string praatexe;
		public string praatscript;
		
		public void StdoutHandler(object sender, DataReceivedEventArgs s)
		{
			Console.WriteLine(s.ToString());
		}
		
		public void StderrHandler(object sender, DataReceivedEventArgs s)
		{
			Console.WriteLine(s.ToString());
		}
		
		public void CallPraat(string wavfile)
		{
			Process p = new Process();
			p.OutputDataReceived += new DataReceivedEventHandler(StdoutHandler);
			p.ErrorDataReceived += new DataReceivedEventHandler(StderrHandler);
			p.StartInfo.FileName = praatexe;
			p.StartInfo.Arguments += "-a "+praatscript+" "+wavfile;
			p.Start();
//			p.WaitForExit();
		}
		
		public CallPraatDelegate GetPraatDelegate()
		{
			return new CallPraatDelegate(CallPraat);
		}
		
		public PraatInvoke(string exef, string scriptf)
		{
			praatexe = exef;
			praatscript = scriptf;
		}
	}
}
