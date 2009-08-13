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
	public class FaceAPI
	{
		public FaceDataCallback facedatacbf;
		public HeadPoseCallback headposecbf;
		public LandmarkDelegate outputlandmark;
		
		[DllImport ("faceapi-wrapper.dll", CallingConvention=CallingConvention.Cdecl)]
		static extern void testme();
		
		[DllImport ("faceapi-wrapper.dll", CallingConvention=CallingConvention.StdCall)]
		static extern void run(HeadPoseCallback hpcf, FaceDataCallback fdcf);
		
		public static void LandmarkCBF(smFaceLandmark[] landmarks)
		{
			for (int i = 0; i < landmarks.Length; ++i)
			{
				Console.WriteLine(landmarks[i].ftc.u);
			}
		}
		
		unsafe public static void FaceDataCBF(void *user_data, smEngineFaceData face_data, smCameraVideoFrame video_frame)
		{
			Console.WriteLine("got face data");
			smFaceLandmark[] landmarks = new smFaceLandmark[face_data.num_landmarks];
			for (int i = 0; i < face_data.num_landmarks; ++i)
			{
				landmarks[i] = face_data.landmarks[i];
			}
			LandmarkCBF(landmarks);
		}
		
		unsafe public static void HeadPoseCBF(void *user_data, smEngineHeadPoseData head_pose, smCameraVideoFrame video_frame)
		{
			Console.WriteLine("got head pose");
			Console.WriteLine(head_pose.confidence);
		}
		
		unsafe public HeadPoseCallback GetHeadPoseDelegate()
		{
			return new HeadPoseCallback(HeadPoseCBF);
		}
		
		unsafe public FaceDataCallback GetFaceDataDelegate()
		{
			return new FaceDataCallback(FaceDataCBF);
		}
		
		unsafe public void Run()
		{
			run(HeadPoseCBF, FaceDataCBF);
		}
	}
}
