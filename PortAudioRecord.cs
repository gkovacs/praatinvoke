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
using System.Threading;
using System.Runtime.InteropServices;
using PortAudioSharp;

namespace praatinvoke
{
	public class PortAudioRecord
	{
		public ReceiveSamplesDelegate samplesDelegate;
		public Audio audio = null;
		public int NUM_CHANNELS = 1;
		public int SAMPLE_RATE = 44100;
		public uint FRAMESPERBUFFER = 1024;
		
		public PortAudioRecord()
		{
			try
			{
				Audio.LoggingEnabled = true;
				audio = new Audio(NUM_CHANNELS, 2, SAMPLE_RATE, FRAMESPERBUFFER,
					new PortAudio.PaStreamCallbackDelegate(recordCallback));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		
		public PortAudio.PaStreamCallbackResult recordCallback(
	 		IntPtr input,
	 		IntPtr output,
	 		uint frameCount, 
	 		ref PortAudio.PaStreamCallbackTimeInfo timeInfo,
	 		PortAudio.PaStreamCallbackFlags statusFlags,
	 		IntPtr userData)
		{
			try
			{
				float[] callbackBuffer = new float[frameCount];
				Marshal.Copy(input, callbackBuffer, 0, (int)frameCount);
				samplesDelegate(callbackBuffer);
			}
			catch (Exception e)
			{ 
	 			Console.WriteLine(e.ToString());
	 		}
	 		return PortAudio.PaStreamCallbackResult.paContinue;
		}
		
		public void SetSamplesDelegate(ReceiveSamplesDelegate wwr)
		{
			try
			{
				samplesDelegate = wwr;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		
		public void SetSamplesDelegate(WaveWriter wwr)
		{
			try
			{
				samplesDelegate = new ReceiveSamplesDelegate(wwr.ReceiveSamples);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		
		public void Stop()
		{
			try
			{
				if (audio == null)
					return;
				audio.Stop();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
			finally
			{
				if (audio != null) audio.Dispose();
			}
		}
		
		public void Sleep(int timeoutms)
		{
			try
			{
				if (timeoutms == -1)
					Thread.Sleep(Timeout.Infinite);
				else
					Thread.Sleep(timeoutms);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
