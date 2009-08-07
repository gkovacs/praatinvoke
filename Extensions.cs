// 
// Extensions.cs
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
	public static class Extensions
	{
		public static string GetFirst(this string str, int num)
		{
			return str.GetMid(0, num);
		}
		
		public static string GetLast(this string str, int num)
		{
			return str.GetMid(str.Length-num, num);
		}
		
		public static string GetMid(this string str, int startpos, int num)
		{
			string outmsg = "";
			for (int i = startpos; i < startpos+num; ++i)
			{
				outmsg += str[i];
			}
			return outmsg;
		}
		
		public static int Count(this string str, string s)
		{
			int num = 0;
			for (int i = 0; i < str.Length+1-s.Length; ++i)
			{
				if (str.GetMid(i, s.Length) == s)
				{
					++num;
					i += s.Length-1;
				}
			}
			return num;
		}
		
		public static int Count(this string str, char s)
		{
			int num = 0;
			for (int i = 0; i < str.Length; ++i)
			{
				if (str[i] == s)
				{
					++num;
				}
			}
			return num;
		}
		
		public static float ToFloat(this string str)
		{
			return System.Convert.ToSingle(str);
		}
		
		public static int ToInt(this string str)
		{
			return System.Convert.ToInt32(str);
		}
		
		public static string mkstring<T, U> (this Pair<T, U> str)
		{
			return "("+str.first.ToString()+","+str.second.ToString()+")";
		}
		
		public static string mkstring<T, U, V>(this Triple<T, U, V> str)
		{
			return "("+str.first.ToString()+","+str.second.ToString()+","+str.third.ToString()+")";
		}
		
		public static string mkstring<T, U, V, W, X, Y>(this Pair< Triple<T, U, V>, Triple<W, X, Y> > str)
		{
			return "{"+str.first.mkstring()+","+str.second.mkstring()+"}";
		}
		
		public static string mkstring<T, U, W, X>(this Pair< Pair<T, U>, Pair<W, X> > str)
		{
			return "{"+str.first.mkstring()+","+str.second.mkstring()+"}";
		}
	}
}
