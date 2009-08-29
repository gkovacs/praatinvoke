// 
// ValueDisplayBars.cs
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
using System.Windows.Forms;

namespace praatinvoke
{
	public static class ValueDisplayBarsExt
	{
		public static void SetValue(this ProgressBar pb, int nv)
		{
			if (pb.InvokeRequired)
			{
				pb.BeginInvoke(new MethodInvoker(
				delegate()
				{
					SetValue(pb, nv);
				}
			));
			}
			else
			{
				pb.Value = nv;
			}
		}
		
		public static void SetText(this Label lb, string nv)
		{
			if (lb.InvokeRequired)
			{
				lb.BeginInvoke(new MethodInvoker(
				delegate()
				{
					SetText(lb, nv);
				}
			));
			}
			else
			{
				lb.Text = nv;
			}
		}

		public static void SetFontStyle(this Label lb, System.Drawing.FontStyle b)
		{
			if (lb.InvokeRequired)
			{
				lb.BeginInvoke(new MethodInvoker(
				delegate()
				{
					SetFontStyle(lb, b);
				}
			));
			}
			else
			{
				lb.Font = new System.Drawing.Font(lb.Font, b);
			}
		}
	}
	
	public class ValueDisplayBars : Form
	{
		public ProgressBar[] pb;
		public Label[] lb;
		public Label[] rc;
		public string[] names;
		public const int elemheight = 30;
		
		public ValueDisplayBars(string[] n)
		{
			names = n;
			int borderWidth = (Width - ClientSize.Width) / 2;
			int titleBarHeight = Height - ClientSize.Height - borderWidth;
			Height = titleBarHeight + elemheight * names.Length;
			Width = 850;
			Text = "Value Display Bars";
			pb = new ProgressBar[names.Length];
			lb = new Label[names.Length];
			rc = new Label[names.Length];
			for (int i = 0; i <  names.Length; ++i)
			{
				pb[i] = new ProgressBar();
				pb[i].Minimum = 0;
				pb[i].Maximum = 100;
				pb[i].Style = ProgressBarStyle.Continuous;
				pb[i].Location = new System.Drawing.Point(300, elemheight * i);
				pb[i].Size = new System.Drawing.Size(500, elemheight);
				pb[i].Show();
				Controls.Add(pb[i]);
				
				lb[i] = new Label();
				lb[i].Text = names[i];
				lb[i].Location = new System.Drawing.Point(0, elemheight * i);
				lb[i].Size = new System.Drawing.Size(300, elemheight);
				lb[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
				lb[i].Show();
				Controls.Add(lb[i]);
				
				rc[i] = new Label();
				rc[i].Text = "0";
				rc[i].Location = new System.Drawing.Point(800, elemheight * i);
				rc[i].Size = new System.Drawing.Size(50, elemheight);
				rc[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
				rc[i].Show();
				Controls.Add(rc[i]);
			}
		}
		
		public void SetPercent(string attr, int percent)
		{
			for (int i = 0; i < names.Length; ++i)
			{
				if (attr == names[i])
				{
					SetPercent(i, percent);
					return;
				}
			}
		}
		
		public void SetPercent(string attr, double fraction)
		{
			for (int i = 0; i < names.Length; ++i)
			{
				if (attr == names[i])
				{
					SetPercent(i, fraction);
					return;
				}
			}
		}
		
		public void SetPercent(string attr, float fraction)
		{
			for (int i = 0; i < names.Length; ++i)
			{
				if (attr == names[i])
				{
					SetPercent(i, fraction);
					return;
				}
			}
		}

		public void SetBoldIdx(int idx)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new MethodInvoker(
				delegate()
				{
					SetBoldIdx(idx);
				}
			));
			}
			else
			{
				for (int i = 0; i < names.Length; ++i)
				{
					if (i == idx)
						continue;
					lb[i].SetFontStyle(System.Drawing.FontStyle.Regular);
					rc[i].SetFontStyle(System.Drawing.FontStyle.Regular);
				}
				lb[idx].SetFontStyle(System.Drawing.FontStyle.Bold);
				rc[idx].SetFontStyle(System.Drawing.FontStyle.Bold);
			}
		}

		public void SetPercent(int i, int percent)
		{
			if (InvokeRequired)
			{
				BeginInvoke(new MethodInvoker(
				delegate()
				{
					SetPercent(i, percent);
				}
			));
			}
			else
			{
				pb[i].SetValue(percent);
				rc[i].SetText(percent.ToString());
			}
		}
		
		public void SetPercent(int i, double fraction)
		{
			SetPercent(i, (int)(fraction * 100));
		}
		
		public void SetPercent(int i, float fraction)
		{
			SetPercent(i, (int)(fraction * 100));
		}

		public OutputWekaDelegate GetWekaOutputDelegate()
		{
			return new OutputWekaDelegate(WekaDisplayOutput);
		}

		public void WekaDisplayOutput(double[] results)
		{
			for (int i = 0; i < results.Length; ++i)
			{
				SetPercent(i, results[i]);
				SetBoldIdx(results.greatest());
			}
		}
	}
}
