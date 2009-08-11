//  
//  Copyright (C) 2009 Geza Kovacs
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 

using System;

namespace praatinvoke
{
	public static class Extensions
	{
		public static string GetFirst(this string str, int num)
		{
			try
			{
				return str.GetMid(0, num);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
		
		public static string GetLast(this string str, int num)
		{
			try
			{
				return str.GetMid(str.Length-num, num);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
		
		public static string GetMid(this string str, int startpos, int num)
		{
			try
			{
				string outmsg = "";
				for (int i = startpos; i < startpos+num; ++i)
				{
					outmsg += str[i];
				}
				return outmsg;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
		
		public static int Count(this string str, string s)
		{
			try
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
			catch (Exception e)
			{
				Console.WriteLine(e);
				return 0;
			}
		}
		
		public static int Count(this string str, char s)
		{
			try
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
			catch (Exception e)
			{
				Console.WriteLine(e);
				return 0;
			}
		}
		
		public static float ToFloat(this string str, out bool success)
		{
			try
			{
				success = true;
				if (str == null || str == string.Empty)
				{
					success = false;
					return 0.0f;
				}
				float outval;
				if (!float.TryParse(str, out outval))
				{
					success = false;
					return 0.0f;
				}
				return outval;
			}
			catch (Exception e)
			{
				success = false;
				Console.WriteLine(e);
				return 0.0f;
			}
		}
		
		public static int ToInt(this string str, out bool success)
		{
			try
			{
				success = true;
				if (str == null || str == string.Empty)
				{
					success = false;
					return 0;
				}
				int outval;
				if (!int.TryParse(str, out outval))
				{
					success = false;
					return 0;
				}
				return outval;
			}
			catch (Exception e)
			{
				success = false;
				Console.WriteLine(e);
				return 0;
			}
		}
		
		public static string mkstring<T, U> (this Pair<T, U> str)
		{
			try
			{
				return "("+str.first.ToString()+","+str.second.ToString()+")";
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
		
		public static string mkstring<T, U, V>(this Triple<T, U, V> str)
		{
			try
			{
				return "("+str.first.ToString()+","+str.second.ToString()+","+str.third.ToString()+")";
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
		
		public static string mkstring<T, U, V, W, X, Y>(this Pair< Triple<T, U, V>, Triple<W, X, Y> > str)
		{
			try
			{
				return "{"+str.first.mkstring()+","+str.second.mkstring()+"}";
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
		
		public static string mkstring<T, U, W, X>(this Pair< Pair<T, U>, Pair<W, X> > str)
		{
			try
			{
				return "{"+str.first.mkstring()+","+str.second.mkstring()+"}";
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
	}
}
