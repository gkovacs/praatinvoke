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
				wki.attributes = wki.ListAttributes();
				wki.classifications = wki.ListClassifications();
				Console.WriteLine(wki.attributes.mkstring());
				Console.WriteLine(wki.classifications.mkstring());
				
				weka.core.Instance inst = new weka.core.Instance(wki.attributes.Length+1);
				inst.setDataset(wki.trainset);
				inst.setValue(wki.trainset.attribute(0), 5.1);
				inst.setValue(wki.trainset.attribute(1), 3.5);
				inst.setValue(wki.trainset.attribute(2), 1.4);
				inst.setValue(wki.trainset.attribute(3), 0.2);
				Console.WriteLine(wki.ClassifyInstance(inst).mkstring());
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