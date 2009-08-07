// 
// WaveWriter.cs
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
using System.IO;
using System.Collections.Generic;
using Arbingersys.Audio.Aumplib;

namespace praatinvoke
{	
	class WaveWriter
	{
		public CallPraatDelegate callpraat;
		
		public int sndcapnum;
		public int pauseCountup;
		public int pauseCountdown;
		public IntPtr soundf;
		public LibsndfileWrapper.SF_INFO soundfInfo = new LibsndfileWrapper.SF_INFO();
		public List<float> upcomingSoundCache = new List<float>();
		
		public void ReceiveSamples(float[] inpSamples)
		{
			double inpvecsum = 0.0;
			foreach (float sample in inpSamples)
			{
				inpvecsum += Constants.MAGNIFICATION * Math.Abs(sample);
			}
			inpvecsum /= Constants.SILENCETHRESHOLD;
			inpvecsum -= Constants.BACKGROUND;
			Console.WriteLine(inpvecsum.ToString("f10"));
			if (pauseCountup == 0) // is recording
			{
				Console.WriteLine("recording");
				if (inpvecsum > 0) // have sound, recording as usual
				{
					Console.WriteLine("have sound");
					pauseCountdown = Constants.PAUSECOUNTDOWN;
					writeSampleBuffer(inpSamples);
				}
				else
				{
					if (pauseCountdown == 0) // hasn't had sound input for countdown turns, stop recording
					{
						pauseCountdown = Constants.PAUSECOUNTDOWN;
						pauseCountup = Constants.PAUSECOUNTUP;
						changeSoundFile();
					}
					else // no sound but still recording
					{
						--pauseCountdown;
						writeSampleBuffer(inpSamples);
					}
				}
			}
			else
			{
				if (inpvecsum > 0) // not recording but have sound
				{
					upcomingSoundCache.AddRange(inpSamples);
					--pauseCountup;
					if (pauseCountup == 0) // have hit critical point, will now start recording, clear buffer and write to file
					{
						Console.WriteLine("have hit critical point");
						writeSampleBuffer(upcomingSoundCache.ToArray());
						upcomingSoundCache.Clear();
					}
				}
				else // no sound, reset pauseCountup and clear cache
				{
					Console.WriteLine("no sound");
					upcomingSoundCache.Clear();
					pauseCountup = Constants.PAUSECOUNTUP;
				}
			}
		}
		
		public void changeSoundFile()
		{
			Console.WriteLine("soundfile changed");
			string origsndcapfile = Environment.CurrentDirectory+Path.DirectorySeparatorChar+"sndcap"+Path.DirectorySeparatorChar+sndcapnum.ToString()+".wav";
			IntPtr soundfold = soundf;
			string sndcapfile = nextSoundFile();
			soundf = LibsndfileWrapper.sf_open(sndcapfile, (int)LibsndfileWrapper.fileMode.SFM_WRITE, ref soundfInfo);
			LibsndfileWrapper.sf_close(soundfold);
			callpraat(origsndcapfile);
		}
		
		public void writeSampleBuffer(float[] samples)
		{
			LibsndfileWrapper.sf_write_float(soundf, samples, samples.Length);
		}
		
		public string nextSoundFile()
		{
			while (File.Exists(Environment.CurrentDirectory+Path.DirectorySeparatorChar+"sndcap"+Path.DirectorySeparatorChar+sndcapnum.ToString()+".wav"))
				++sndcapnum;
			return Environment.CurrentDirectory+Path.DirectorySeparatorChar+"sndcap"+Path.DirectorySeparatorChar+sndcapnum.ToString()+".wav";
		}
		
		public void SetPraatDelegate(CallPraatDelegate pri)
		{
			callpraat = pri;
		}
		
		public ReceiveSamplesDelegate GetSamplesDelegate()
		{
			return new ReceiveSamplesDelegate(ReceiveSamples);
		}
		
		public WaveWriter()
		{
			if (!Directory.Exists(Environment.CurrentDirectory+Path.DirectorySeparatorChar+"sndcap"))
			{
				Directory.CreateDirectory(Environment.CurrentDirectory+Path.DirectorySeparatorChar+"sndcap");
			}
			soundfInfo.channels = Constants.NUM_CHANNELS;
			soundfInfo.samplerate = Constants.SAMPLE_RATE;
			soundfInfo.format = ((int)LibsndfileWrapper.soundFormat.SF_FORMAT_WAV | (int)LibsndfileWrapper.soundFormat.SF_FORMAT_FLOAT);
			string sndcapfile = nextSoundFile();
			soundf = LibsndfileWrapper.sf_open(sndcapfile, (int)LibsndfileWrapper.fileMode.SFM_WRITE, ref soundfInfo);
			pauseCountup = Constants.PAUSECOUNTUP;
			pauseCountdown = Constants.PAUSECOUNTDOWN;
		}
	}
}
