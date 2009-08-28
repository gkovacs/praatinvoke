// 
// WekaInvoke.cs
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
using System.Collections.Generic;
using java.util;
using weka.classifiers.bayes;
using weka.core;
using weka.core.converters;

namespace praatinvoke
{
	public static class WekaExt
	{
		public static string attributeName(this Instance i)
        {
            return i.toString(i.classAttribute());
        }
	}
	
	public class WekaInvoke
	{
		public Instances trainset;
		public Instances structure;
		public NaiveBayes nb;
		public string[] attributes = null;
		public string[] classifications = null;
		
		public WekaInvoke(string trainfile)
		{
			nb = new NaiveBayes();
			ConverterUtils.DataSource ds = new ConverterUtils.DataSource(trainfile);
			trainset = ds.getDataSet();
			structure = ds.getStructure();
			if (trainset.classIndex() == -1)
				trainset.setClassIndex(trainset.numAttributes() - 1);
			for (int i = 0; i < trainset.numInstances(); ++i)
			{
				//Console.WriteLine(trainset.instance(i).attributeName());
			}
			nb.buildClassifier(trainset);
		}
		
		public string[] ListAttributes()
		{
		 	List<string> attrs = new List<string>();
			for (int i = 0; i < structure.numAttributes(); ++i)
			{
				if (!structure.attribute(i).isNumeric())
					continue;
				attrs.Add(structure.attribute(i).name());
			}
			return attrs.ToArray();
		}
		
		public string[] ListClassifications()
		{
			List<string> attrs = new List<string>();
			for (int i = 0; i < structure.numAttributes(); ++i)
			{
				if (!structure.attribute(i).isNominal())
					continue;
				Enumeration en = structure.attribute(i).enumerateValues();
				while (en.hasMoreElements())
				{
					attrs.Add(en.nextElement().ToString());
				}
			}
			return attrs.ToArray();
		}
		
		public double[] ClassifyInstance(Instance inst)
		{
			inst.setClassValue(nb.classifyInstance(inst));
			return nb.distributionForInstance(inst);
		}
	}
}
