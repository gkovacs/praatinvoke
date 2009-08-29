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
using weka.core;

namespace praatinvoke
{
	public static class WekaExt
	{
		public static string AttributeName(this Instance i)
		{
			return i.toString(i.classAttribute());
		}
	}
	
	public class WekaInvoke
	{
		public DataPairDelegate wekainput;
		public OutputWekaDelegate wekaoutput;
		public Instances trainset;
		public Instances structure;
		//public weka.classifiers.lazy.IBk nb;
		public weka.classifiers.bayes.NaiveBayes nb;
		public string[] attributes = null;
		public string[] classifications = null;
		
		public WekaInvoke(string trainfile)
		{
			//nb = new weka.classifiers.lazy.IBk();
			nb = new weka.classifiers.bayes.NaiveBayes();
			weka.core.converters.ConverterUtils.DataSource ds = new weka.core.converters.ConverterUtils.DataSource(trainfile);
			trainset = ds.getDataSet();
			structure = ds.getStructure();
			if (trainset.classIndex() == -1)
				trainset.setClassIndex(trainset.numAttributes() - 1);
			for (int i = 0; i < trainset.numInstances(); ++i)
			{
				//Console.WriteLine(trainset.instance(i).AttributeName());
			}
			nb.buildClassifier(trainset);
			attributes = ListAttributes();
			classifications = ListClassifications();
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
				java.util.Enumeration en = structure.attribute(i).enumerateValues();
				while (en.hasMoreElements())
				{
					attrs.Add(en.nextElement().ToString());
				}
			}
			return attrs.ToArray();
		}
		
		public double[] ClassifyInstance(Instance inst)
		{
			return nb.distributionForInstance(inst);
		}
		
		public weka.core.Attribute FindAttribute(string attrname)
		{
			for (int i = 0; i < structure.numAttributes(); ++i)
			{
				if (structure.attribute(i).name() == attrname)
					return structure.attribute(i);
			}
			return null;
		}
		
		public DataPairDelegate GetWekaInputDelegate()
		{
			return new DataPairDelegate(WekaFeedInput);
		}
		
		public void SetWekaOutputDelegate(OutputWekaDelegate o)
		{
			wekaoutput = o;
		}
		
		public void WekaFeedInput(Pair<string, double>[] encinstance)
		{
			//Console.WriteLine(encinstance.mkstring());
			weka.core.Instance inst = new weka.core.Instance(attributes.Length+1);
			inst.setDataset(trainset);
			foreach (Pair<string, double> x in encinstance)
			{
				if (x == null)
					continue;
				//Console.WriteLine(x.mkstring());
				inst.setValue(FindAttribute(x.first), x.second);
			}
			wekaoutput(ClassifyInstance(inst));
		}
	}
}
