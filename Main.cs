// 
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

namespace praatinvoke
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			try
			{
				if (args.Length < 2)
				{
					Console.WriteLine("not enough arguments");
					return;
				}
				
				WekaInvoke wki = new WekaInvoke("iris.arff");
				DataFilter dtf = new DataFilter(wki.attributes);
				WekaOutput wko = new WekaOutput(wki.classifications);
				
				Console.WriteLine(wki.attributes.mkstring());
				Console.WriteLine(wki.classifications.mkstring());
				
				Pair<string, double>[] encinstance = new Pair<string, double>[wki.attributes.Length+1];
				encinstance[0] = new Pair<string, double>("sepallength", 6.3);
				encinstance[1] = new Pair<string, double>("sepalwidth", 2.4);
				encinstance[2] = new Pair<string, double>("petallength", 4.8);
				encinstance[3] = new Pair<string, double>("petalwidth", 1.6);
				
				wki.SetWekaOutputDelegate(wko.GetWekaOutputDelegate());
				dtf.SetFilterOutputDelegate(wki.GetWekaInputDelegate());
				dtf.FilterData(encinstance);
/*
				weka.core.Instance inst = new weka.core.Instance(wki.attributes.Length+1);
				inst.setDataset(wki.trainset);
				inst.setValue(wki.FindAttribute("sepallength"), 6.3);
				inst.setValue(wki.FindAttribute("sepalwidth"), 2.4);
				inst.setValue(wki.FindAttribute("petallength"), 4.8);
				inst.setValue(wki.FindAttribute("petalwidth"), 1.6);
				double[] results = wki.ClassifyInstance(inst);
				Console.WriteLine(results.mkstring());
				Console.WriteLine(wki.classifications[results.greatest()]);
*/
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
//				while (true)
//				{
//					
//				}
				//rec.Sleep(-1);
				//FaceAPI fca = new FaceAPI();
				//fca.Run();
                /*
				FaceAPIOutput fao = new FaceAPIOutput();
				FaceAPIWrapper.FaceInvoke fci = new FaceAPIWrapper.FaceInvoke();
				fci.SetHeadPoseDelegate(fao.GetHeadPoseDelegate());
				fci.SetLandmarksDelegate(fao.GetLandmarksDelegate());
				fci.Run();
                 */
				
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