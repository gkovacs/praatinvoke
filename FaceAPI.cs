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
using FaceAPIWrapper;

namespace praatinvoke
{
	public class FaceAPI
	{
		public FaceDataCallback facedatacbf;
		public HeadPoseCallback headposecbf;
		public LandmarkDelegate outputlandmark;
		
//		[DllImport ("TestAppConsole.dll", CallingConvention=CallingConvention.Cdecl)]
//		static extern void testme();
		
		[DllImport ("TestAppConsole.dll", CallingConvention=CallingConvention.Cdecl)]
		static extern void run(HeadPoseCallback hpcf, FaceDataCallback fdcf);
		
		public static void LandmarkCBF(smFaceLandmark[] landmarks)
		{
			for (int i = 0; i < landmarks.Length; ++i)
			{
				Console.WriteLine(landmarks[i].ftc.u);
			}
		}
		
		public unsafe static void FaceDataCBF(IntPtr user_data, smEngineFaceData face_data, smCameraVideoFrame video_frame)
		{
			
			Console.WriteLine("got face data ");
			Console.WriteLine(face_data.num_landmarks.ToString());
			Console.WriteLine("test1");
			smFaceLandmark landmark = *(face_data.landmarks);
			//smFaceLandmark landmark = (smFaceLandmark)//Marshal.PtrToStructure(face_data.landmarks, typeof(smFaceLandmark));
			Console.WriteLine("test2");
//			Console.WriteLine(landmark.id);
			Console.WriteLine("test3");
			//Console.WriteLine(face_data.landmarks->id.ToString());
			//smFaceLandmark[] landmarks = new smFaceLandmark[face_data.num_landmarks];
			//for (int i = 0; i < face_data.num_landmarks; ++i)
			//{
				//Console.WriteLine(face_data.landmarks[i].id);
				//landmarks[i] = face_data.landmarks[i];
			//}
			//LandmarkCBF(landmarks);
			
		}
		
		public static void HeadPoseCBF(IntPtr user_data, smEngineHeadPoseData head_pose, smCameraVideoFrame video_frame)
		{
			Console.WriteLine("got head pose");
			Console.WriteLine(head_pose.confidence);
			Console.WriteLine(head_pose.head_rot.x_rads);
				Console.WriteLine(head_pose.head_rot.y_rads);
				Console.WriteLine(head_pose.head_rot.z_rads);
		}
		
		public HeadPoseCallback GetHeadPoseDelegate()
		{
			return new HeadPoseCallback(HeadPoseCBF);
		}
		
		public FaceDataCallback GetFaceDataDelegate()
		{
			return new FaceDataCallback(FaceDataCBF);
		}
		
		public void Run()
		{
			run(HeadPoseCBF, FaceDataCBF);
		}
	}
}
