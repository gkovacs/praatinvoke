
using System;

namespace FaceAPIDatatypes
{
	public delegate void LandmarksDelegate(smFaceLandmark_cli[] landmarks);
	public delegate void HeadPoseDelegate(smEngineHeadPoseData_cli headpose);

    public struct smCoord2f_cli
	{
		public float x;
		public float y;
		public string mkstring()
		{
			return "("+x.ToString()+","+y.ToString()+")";
		}
	}

    public struct smCoord3f_cli
	{
		public float x;
		public float y;
		public float z;
		public string mkstring()
		{
			return "("+x.ToString()+","+y.ToString()+","+z.ToString()+")";
		}
	}

    public struct smFaceLandmark_cli
	{
		public smCoord3f_cli fc;
		public smCoord2f_cli ftc;
		public int id;
		public smCoord2f_cli pc;
		public smCoord3f_cli wc;
		public string mkstring()
		{
			return id.ToString()+"\n"+fc.mkstring()+"\n"+ftc.mkstring()+"\n"+pc.mkstring()+"\n"+wc.mkstring();
		}
	}

    public struct smEngineHeadPoseData_cli
	{
		public float confidence;
		public smCoord3f_cli head_pos;
		public smCoord3f_cli head_rot;
		public smCoord3f_cli left_eye_pos;
		public smCoord3f_cli right_eye_pos;
		public string mkstring()
		{
			return confidence.ToString()+"\n"+head_pos.mkstring()+"\n"+head_rot.mkstring()+"\n"+left_eye_pos.mkstring()+"\n"+right_eye_pos.mkstring();
		}
	}
}
