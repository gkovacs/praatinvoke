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
				PraatInvoke pri = new PraatInvoke(args[0], args[1]);
				PraatOutput pao = new PraatOutput();
				WaveWriter wwr = new WaveWriter();
				PortAudioRecord rec = new PortAudioRecord();
				rec.SetSamplesDelegate(wwr.GetSamplesDelegate());
				wwr.SetPraatDelegate(pri.GetPraatDelegate());
				pri.SetOutputPraatDelegate(pao.GetPraatOutputDelegate());
				rec.audio.Start();
//				while (true)
//				{
//					
//				}
				rec.Sleep(-1);
				rec.Stop();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}