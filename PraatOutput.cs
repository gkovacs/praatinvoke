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
	public class PraatOutput
	{
		public void PrintPraatOutput(Pair<string, float>[] output)
		{
			try
			{
				if (output == null)
					return;
				foreach(Pair<string, float> x in output)
				{
					Console.WriteLine("PrintPraatOutput: "+x.mkstring());
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		
		public OutputPraatDelegate GetPraatOutputDelegate()
		{
			try
			{
				return new OutputPraatDelegate(PrintPraatOutput);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
	}
}
