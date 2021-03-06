// 
// DataFilter.cs
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
	public class DataFilter
	{
		public DataPairDelegate dataoutput;
		public string[] attributes;
		public OpGroup[] featureIdx;
		
		public DataFilter(string[] a)
		{
			attributes = a;
			featureIdx = new OpGroup[attributes.Length];
			for (int i = 0; i < attributes.Length; ++i)
			{
				featureIdx[i] = new OpGroup(attributes[i]);
			}
		}
		
		public void FilterData(Pair<string, double>[] rawdatapairs)
		{
			//Console.WriteLine(rawdatapairs.mkstring());
			Pair<string, double>[] outputpairs = new Pair<string, double>[featureIdx.Length];
			foreach (Pair<string, double> x in rawdatapairs)
			{
				if (x == null)
					continue;
				featureIdx.SetValAtName(x.first, x.second);
			}
			for (int i = 0; i < featureIdx.Length; ++i)
			{
				double nval = featureIdx[i].GetVal();
				if (nval == double.NaN)
					continue;
				outputpairs[i] = new Pair<string, double>(attributes[i], nval);
			}
			//Console.WriteLine(outputpairs.mkstring());
			dataoutput(outputpairs);
		}
		
		public DataPairDelegate GetFilterInputDelegate()
		{
			return new DataPairDelegate(FilterData);
		}
		
		public void SetFilterOutputDelegate(DataPairDelegate d)
		{
			dataoutput = d;
		}
	}
}
