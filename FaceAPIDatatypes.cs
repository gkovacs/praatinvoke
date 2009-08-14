// 
//  Copyright (C) 2009 Geza Kovacs
// 
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//  
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// 

using System;
using System.Runtime.InteropServices;

namespace praatinvoke
{
	using smFaceCoord = smCoord3f;
	using smPixel = smCoord2f;
	using smPos3f = smCoord3f;
	
	[StructLayout(LayoutKind.Sequential)]
	public struct smCoord2f
	{
		public float x;
		public float y;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct smCoord3f
	{
		public float x;
		public float y;
		public float z;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct smFaceTexCoord
	{
		public float u;
		public float v;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	public struct smFaceLandmark
	{
		public smFaceCoord fc;
		public smFaceTexCoord ftc;
		public int id;
		public smPixel pc;
		public smCoord3f wc;
	}
	
	/*
	public struct smEngineFaceData
	{
		public smFaceLandmark *landmarks;
		//public IntPtr landmarks;
		public int num_landmarks;
		public smPixel origin_pc;
		public smCoord3f origin_wc;
		public IntPtr texture;
		//public smFaceTexture *texture;
	}
	*/
	
	public struct smFaceTexture
	{
		// TODO
	}
	
	public struct smEngineHeadPoseData
	{
		public float confidence;
		public smCoord3f head_pos;
		public smCoord3f head_rot;
		public smCoord3f left_eye_pos;
		public smCoord3f right_eye_pos;
	}
	
	public struct smCameraVideoFrame
	{
		// TODO
	}
}
