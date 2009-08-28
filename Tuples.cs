// 
// Tuples.cs
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
	public class Pair<T, U>
	{
		public T first;
		public U second;
		public Pair()
		{
			
		}
		public Pair(T f, U s)
		{
			first = f;
			second = s;
		}
		public override string ToString()
		{
			try
			{
				return "("+first.ToString()+","+second.ToString()+")";
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
	}

	public class Triple<T, U, V>
	{
		public T first;
		public U second;
		public V third;
		public Triple()
		{
			
		}
		public Triple(T f, U s, V t)
		{
			first = f;
			second = s;
			third = t;
		}
		public override string ToString()
		{
			try
			{
				return "("+first.ToString()+","+second.ToString()+","+third.ToString()+")";
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
	}
}
