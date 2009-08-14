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
using System.IO;
using System.Collections.Generic;
using Arbingersys.Audio.Aumplib;

namespace praatinvoke
{	
	public class WaveWriter
	{
		public CallPraatDelegate callpraat;
		public int NUM_CHANNELS = 1;
		public int SAMPLE_RATE = 44100;
		public double SCALEPOWER = 1.0;
		public double SILENCETHRESHOLD = 1.0;
		public int MAGNIFICATION = 5;
		public double BACKGROUND = 30.0;
		public uint FRAMESPERBUFFER = 1024;
		public int PAUSECOUNTDOWN = 12;
		public int PAUSECOUNTUP = 5;
		
		public int sndcapnum;
		public int pauseCountup;
		public int pauseCountdown;
		public IntPtr soundf;
		LibsndfileWrapper.SF_INFO soundfInfo;
		public List<float> upcomingSoundCache = new List<float>();
		
		public void ReceiveSamples(float[] inpSamples)
		{
			try
			{
				double inpvecsum = 0.0;
				foreach (float sample in inpSamples)
				{
					inpvecsum += MAGNIFICATION * Math.Abs(sample);
				}
				inpvecsum /= SILENCETHRESHOLD;
				inpvecsum -= BACKGROUND;
				Console.WriteLine(inpvecsum.ToString("f10"));
				if (pauseCountup == 0) // is recording
				{
					Console.WriteLine("recording");
					if (inpvecsum > 0) // have sound, recording as usual
					{
						Console.WriteLine("have sound");
						pauseCountdown = PAUSECOUNTDOWN;
						writeSampleBuffer(inpSamples);
					}
					else
					{
						if (pauseCountdown == 0) // hasn't had sound input for countdown turns, stop recording
						{
							pauseCountdown = PAUSECOUNTDOWN;
							pauseCountup = PAUSECOUNTUP;
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
						pauseCountup = PAUSECOUNTUP;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		
		public void changeSoundFile()
		{
			try
			{
				Console.WriteLine("soundfile changed");
				string origsndcapfile = Environment.CurrentDirectory+Path.DirectorySeparatorChar+"sndcap"+Path.DirectorySeparatorChar+sndcapnum.ToString()+".wav";
				IntPtr soundfold = soundf;
				string sndcapfile = nextSoundFile();
				soundf = LibsndfileWrapper.sf_open(sndcapfile, (int)LibsndfileWrapper.fileMode.SFM_WRITE, ref soundfInfo);
				LibsndfileWrapper.sf_close(soundfold);
				callpraat(origsndcapfile);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		
		public void writeSampleBuffer(float[] samples)
		{
			try
			{
				LibsndfileWrapper.sf_write_float(soundf, samples, samples.Length);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		
		public string nextSoundFile()
		{
			try
			{
				while (File.Exists(Environment.CurrentDirectory+Path.DirectorySeparatorChar+"sndcap"+Path.DirectorySeparatorChar+sndcapnum.ToString()+".wav"))
					++sndcapnum;
				return Environment.CurrentDirectory+Path.DirectorySeparatorChar+"sndcap"+Path.DirectorySeparatorChar+sndcapnum.ToString()+".wav";
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
		
		public void SetPraatDelegate(CallPraatDelegate pri)
		{
			try
			{
				callpraat = pri;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
		
		public ReceiveSamplesDelegate GetSamplesDelegate()
		{
			try
			{
				return new ReceiveSamplesDelegate(ReceiveSamples);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return null;
			}
		}
		
		public WaveWriter()
		{
			try
			{
				if (!Directory.Exists(Environment.CurrentDirectory+Path.DirectorySeparatorChar+"sndcap"))
				{
					Directory.CreateDirectory(Environment.CurrentDirectory+Path.DirectorySeparatorChar+"sndcap");
				}
				soundfInfo.channels = NUM_CHANNELS;
				soundfInfo.samplerate = SAMPLE_RATE;
				soundfInfo.format = ((int)LibsndfileWrapper.soundFormat.SF_FORMAT_WAV | (int)LibsndfileWrapper.soundFormat.SF_FORMAT_FLOAT);
				string sndcapfile = nextSoundFile();
				soundf = LibsndfileWrapper.sf_open(sndcapfile, (int)LibsndfileWrapper.fileMode.SFM_WRITE, ref soundfInfo);
				pauseCountup = PAUSECOUNTUP;
				pauseCountdown = PAUSECOUNTDOWN;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
