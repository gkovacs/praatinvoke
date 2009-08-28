// 
// OpGroup.cs
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

namespace praatinvoke
{
	public static class OpGroupExt
	{
		public static bool Contains<T>(this IEnumerable<T> strl, T item)
		{
			foreach (T x in strl)
			{
				if (x.Equals(item))
					return true;
			}
			return false;
		}
		
		public static bool ContainsInFirst<T, U>(this IEnumerable< Pair<T, U> > strl, T item)
		{
			foreach (Pair<T, U> x in strl)
			{
				if (x.first.Equals(item))
					return true;
			}
			return false;
		}
		
		public static bool Contains(this IEnumerable<OpGroup> strl, string item)
		{
			foreach (OpGroup x in strl)
			{
				if (x.Contains(item))
					return true;
			}
			return false;
		}
		
		public static string Join(this IEnumerable<string> strl, char sep)
		{
			string total = "";
			int i = 0;
			foreach (string x in strl)
			{
				if (i != 0)
					total += sep;
				total += x;
				++i;
			}
			return total;
		}
		
		public static double Sum(this IEnumerable<double> strl)
		{
			double total = 0.0;
			foreach (double x in strl)
			{
				total += x;
			}
			return total;
		}
		
		public static float Sum(this IEnumerable<float> strl)
		{
			float total = 0.0f;
			foreach (float x in strl)
			{
				total += x;
			}
			return total;
		}
		
		public static float SumSquares(this IEnumerable<float> strl)
		{
			float total = 0.0f;
			foreach (float x in strl)
			{
				total += (x*x);
			}
			return total;
		}
		
		public static double SumSquares(this IEnumerable<double> strl)
		{
			double total = 0.0;
			foreach (double x in strl)
			{
				total += x.Squared();
			}
			return total;
		}
		
		public static double L2Norm(this IEnumerable<double> strl)
		{
			return strl.SumSquares().SquareRoot();
		}
		
		public static float L2Norm(this IEnumerable<float> strl)
		{
			return strl.SumSquares().SquareRoot();
		}
		
		public static double Distance(this IEnumerable<double> strl)
		{
			IEnumerator<double> n = strl.GetEnumerator();
			double total = 0.0;
			double diff = 0.0;
			while (n.MoveNext())
			{
				diff = n.Current;
				n.MoveNext();
				total += Math.Abs(diff - n.Current).Squared();
			}
			return total.SquareRoot();
		}
		
		public static float Distance(this IEnumerable<float> strl)
		{
			IEnumerator<float> n = strl.GetEnumerator();
			float total = 0.0f;
			float diff = 0.0f;
			while (n.MoveNext())
			{
				diff = n.Current;
				n.MoveNext();
				total += Math.Abs(diff - n.Current).Squared();
			}
			return total.SquareRoot();
		}
		
		public static double Distance2D(this IEnumerable<double> strl)
		{
			IEnumerator<double> n = strl.GetEnumerator();
			double total = 0.0;
			double diff = 0.0;
			n.MoveNext();
			diff = n.Current;
			n.MoveNext();
			total += Math.Abs(diff - n.Current).Squared();
			n.MoveNext();
			diff = n.Current;
			n.MoveNext();
			total += Math.Abs(diff - n.Current).Squared();
			return total.SquareRoot();
		}
		
		public static float Distance2D(this IEnumerable<float> strl)
		{
			IEnumerator<float> n = strl.GetEnumerator();
			float total = 0.0f;
			float diff = 0.0f;
			n.MoveNext();
			diff = n.Current;
			n.MoveNext();
			total += Math.Abs(diff - n.Current).Squared();
			n.MoveNext();
			diff = n.Current;
			n.MoveNext();
			total += Math.Abs(diff - n.Current).Squared();
			return total.SquareRoot();
		}
		
		public static double Distance3D(this IEnumerable<double> strl)
		{
			IEnumerator<double> n = strl.GetEnumerator();
			double total = 0.0;
			double diff = 0.0;
			n.MoveNext();
			diff = n.Current;
			n.MoveNext();
			total += Math.Abs(diff - n.Current).Squared();
			n.MoveNext();
			diff = n.Current;
			n.MoveNext();
			total += Math.Abs(diff - n.Current).Squared();
			n.MoveNext();
			diff = n.Current;
			n.MoveNext();
			total += Math.Abs(diff - n.Current).Squared();
			return total.SquareRoot();
		}
		
		public static float Distance3D(this IEnumerable<float> strl)
		{
			IEnumerator<float> n = strl.GetEnumerator();
			float total = 0.0f;
			float diff = 0.0f;
			n.MoveNext();
			diff = n.Current;
			n.MoveNext();
			total += Math.Abs(diff - n.Current).Squared();
			n.MoveNext();
			diff = n.Current;
			n.MoveNext();
			total += Math.Abs(diff - n.Current).Squared();
			n.MoveNext();
			diff = n.Current;
			n.MoveNext();
			total += Math.Abs(diff - n.Current).Squared();
			return total.SquareRoot();
		}
		
		public static double Average(this double[] strl)
		{
			return strl.Sum() / strl.Length;
		}
		
		public static float Average(this float[] strl)
		{
			return strl.Sum() / strl.Length;
		}
		
		public static double AbsSubtract(this double[] strl)
		{
			return Math.Abs(strl[0] - strl[1]);
		}
		
		public static float AbsSubtract(this float[] strl)
		{
			return Math.Abs(strl[0] - strl[1]);
		}
		
		public static double Multiply(this IEnumerable<double> strl)
		{
			double total = 1.0;
			foreach (double x in strl)
			{
				total *= x;
			}
			return total;
		}
		
		public static float Multiply(this IEnumerable<float> strl)
		{
			float total = 1.0f;
			foreach (float x in strl)
			{
				total *= x;
			}
			return total;
		}
		
		public static double Squared(this double s)
		{
			return s*s;
		}
		
		public static float Squared(this float s)
		{
			return s*s;
		}
		
		public static double SquareRoot(this double s)
		{
			return Math.Sqrt(s);
		}
		
		public static float SquareRoot(this float s)
		{
			return (float)Math.Sqrt(s);
		}
		
		public static void SetValAtName(this IEnumerable<OpGroup> strl, string name, double val)
		{
			foreach (OpGroup x in strl)
			{
				if (x.Contains(name))
				{
					x.SetValAtName(name, val);
				}
			}
		}
		
		public static void SetValAtName(this IEnumerable<OpGroup> strl, string name, float val)
		{
			foreach (OpGroup x in strl)
			{
				if (x.Contains(name))
				{
					x.SetValAtName(name, val);
				}
			}
		}
	}
	
	public class OpGroup
	{
		public char op;
		// I = identity (value)
		// S = subtract (absolute value)
		// A = average (mean)
		// T = total (sum)
		// M = multiply
		// D = divide
		// L = L2 norm
		// W = tWo-dimensional distance
		// H = tHree-dimensional distance
		// G = General N-dimensional distance
		public string[] names;
		public double[] values;
		
		public OpGroup(string input)
		{
			string ninput;
			if (input[0] == 'S')
			{
				op = 'S';
				ninput = input.Remove(0, 1);
			}
			else if (input[0] == 'A')
			{
				op = 'A';
				ninput = input.Remove(0, 1);
			}
			else if (input[0] == 'L')
			{
				op = 'L';
				ninput = input.Remove(0, 1);
			}
			else if (input[0] == 'W')
			{
				op = 'W';
				ninput = input.Remove(0, 1);
			}
			else if (input[0] == 'H')
			{
				op = 'H';
				ninput = input.Remove(0, 1);
			}
			else if (input[0] == 'G')
			{
				op = 'G';
				ninput = input.Remove(0, 1);
			}
			else if (input[0] == 'I')
			{
				op = 'I';
				ninput = input.Remove(0, 1);
			}
			else
			{
				op = 'I';
				ninput = input;
			}
			if (op == 'I')
			{
				names = new string[1];
				names[0] = ninput;
			}
			else
			{
				names = ninput.Split('N');
			}
			values = new double[names.Length];
		}
		
		public bool Equals(string item)
		{
			return (this.ToString() == item);
		}
		
		public bool Contains(string item)
		{
			return names.Contains(item);
		}
		
		public override string ToString()
		{
			if (op == 'I')
				return names[0];
			else
				return op+names.Join('N');
		}
		
		public double GetVal()
		{
			if (op == 'I')
				return values[0];
			else if (op == 'S')
				return values.AbsSubtract();
			else if (op == 'A')
				return values.Average();
			else if (op == 'L')
				return values.L2Norm();
			else if (op == 'W')
				return values.Distance2D();
			else if (op == 'H')
				return values.Distance3D();
			else if (op == 'G')
				return values.Distance();
			else
				return double.NaN;
		}
		
		public void SetValAtName(string name, double val)
		{
			int i;
			for (i = 0; i < names.Length; ++i)
			{
				if (names[i] == name)
					break;
			}
			values[i] = val;
		}
		
		public void SetValAtName(string name, float val)
		{
			int i;
			for (i = 0; i < names.Length; ++i)
			{
				if (names[i] == name)
					break;
			}
			values[i] = val;
		}
	}
}
