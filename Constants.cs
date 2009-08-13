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
	public delegate void ReceiveSamplesDelegate(float[] samples);
	public delegate void CallPraatDelegate(string wavfile);
	public delegate void OutputPraatDelegate(Pair<string, float>[] output);
	public delegate void LandmarkDelegate(smFaceLandmark[] landmarks);
	unsafe public delegate void FaceDataCallback(void *user_data, smEngineFaceData face_data, smCameraVideoFrame video_frame);
	unsafe public delegate void HeadPoseCallback(void *user_data, smEngineHeadPoseData head_pose, smCameraVideoFrame video_frame);
}
