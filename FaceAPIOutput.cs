// 
// FaceAPIOutput.cs
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
using FaceAPIDatatypes;

namespace praatinvoke
{
	public class FaceAPIOutput
	{
		public DataPairDelegate dataoutput;
        public int curidx;
		
		public static void PrintHeadpose(smEngineHeadPoseData_cli headpose)
		{
			//Console.WriteLine(headpose.mkstring());
		}
		
		public static void PrintLandmarks(smFaceLandmark_cli[] landmarks)
		{
			for (int i = 0; i < landmarks.Length; ++i)
			{
				Console.WriteLine(landmarks[i].mkstring());
			}
		}
		
		public void OutputRawDataPairs(smFaceLandmark_cli[] landmarks)
		{
			curidx = (curidx + 1) % 20;
			if (curidx != 0)
				return;
			Pair<string, double>[] valpairs = new Pair<string, double>[landmarks.Length * 3];
			for (int i = 0; i < landmarks.Length; ++i)
			{
				valpairs[i*3] = new Pair<string, double>("landmark_"+landmarks[i].id+"_fc_x", landmarks[i].fc.x);
				valpairs[i*3 + 1] = new Pair<string, double>("landmark_"+landmarks[i].id+"_fc_y", landmarks[i].fc.y);
				valpairs[i*3 + 2] = new Pair<string, double>("landmark_"+landmarks[i].id+"_fc_z", landmarks[i].fc.z);
			}
			dataoutput(valpairs);
		}
		
		public HeadPoseDelegate GetHeadPoseDelegate()
		{
			return new HeadPoseDelegate(PrintHeadpose);
		}
		
		public LandmarksDelegate GetLandmarksDelegate()
		{
			return new LandmarksDelegate(OutputRawDataPairs);
		}
		
		public void SetDataFilterDelegate(DataPairDelegate d)
		{
			dataoutput = d;
		}
	}
}
