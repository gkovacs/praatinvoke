﻿// 
// Main.cs
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
using praatinvoke;

namespace praatinvoke_cs
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			try
			{
				//if (args.Length < 2)
				//{
				//	Console.WriteLine("not enough arguments");
				//	return;
				//}

				WekaInvoke<weka.classifiers.lazy.IBk> wki = new WekaInvoke<weka.classifiers.lazy.IBk>("train.arff");
				DataFilter dtf = new DataFilter(wki.attributes);
				//WekaOutput wko = new WekaOutput(wki.classifications);
				ValueDisplayBars vds = new ValueDisplayBars(wki.classifications);

				Console.WriteLine(wki.attributes.mkstring());
				Console.WriteLine(wki.classifications.mkstring());

				//wki.SetWekaOutputDelegate(wko.GetWekaOutputDelegate());
				wki.SetWekaOutputDelegate(vds.GetWekaOutputDelegate());
				dtf.SetFilterOutputDelegate(wki.GetWekaInputDelegate());

				/*
				PraatInvoke pri = new PraatInvoke(args[0], args[1]);
				PraatOutput pao = new PraatOutput();
				WaveWriter wwr = new WaveWriter();
				PortAudioRecord rec = new PortAudioRecord();
				rec.SetSamplesDelegate(wwr.GetSamplesDelegate());
				wwr.SetPraatDelegate(pri.GetPraatDelegate());
				pri.SetOutputPraatDelegate(pao.GetPraatOutputDelegate());
				rec.audio.Start();
				*/

				FaceAPIOutput fao = new FaceAPIOutput();
				FaceAPIWrapper.FaceInvoke fci = new FaceAPIWrapper.FaceInvoke();
				fci.SetHeadPoseDelegate(fao.GetHeadPoseDelegate());
				fci.SetLandmarksDelegate(fao.GetLandmarksDelegate());
				fao.SetDataFilterDelegate(dtf.GetFilterInputDelegate());
				fci.RunThread();

				System.Windows.Forms.Application.Run(vds);
				fci.thread.Abort();
				//System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
				//rec.Stop();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}