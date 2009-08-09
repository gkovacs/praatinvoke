// 
// PortAudioRecord.cs
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
