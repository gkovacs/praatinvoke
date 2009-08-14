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

#pragma once

#ifndef _WIN32_WINNT		// Allow use of features specific to Windows XP or later.
#define _WIN32_WINNT 0x0501	// Change this to the appropriate value to target other versions of Windows.
#endif

#include <stdio.h>
#include <tchar.h>
#include <iostream>
#include <fstream>
#include <sstream>

#if 0
#include <stdint.h>

typedef uint64_t u_int64_t;
typedef uint32_t u_int32_t;
typedef uint16_t u_int16_t;
typedef uint8_t u_int8_t;
#endif

//#include <QtCore>
//#include <QtGui>
//#include <sm_api_qt.h>
#include <sm_api_cxx.h>

using namespace sm::faceapi;
//using namespace sm::faceapi::qt;

using namespace System;
using namespace FaceAPIDatatypes;


namespace FaceAPIWrapper
{


	

	

	

	

	static smCoord3f_cli ToCLIType_smCoord3f(smCoord3f o)
	{
		smCoord3f_cli n;
		n.x = o.x;
		n.y = o.y;
		n.z = o.z;
		return n;
	}

	static smCoord3f_cli ToCLIType_smCoord3f(smRotEuler o)
	{
		smCoord3f_cli n;
		n.x = o.x_rads;
		n.y = o.y_rads;
		n.z = o.z_rads;
		return n;
	}

	static smCoord2f_cli ToCLIType_smCoord2f(smPixel o)
	{
		smCoord2f_cli n;
		n.x = o.x;
		n.y = o.y;
		return n;
	}

	static smCoord2f_cli ToCLIType_smCoord2f(smFaceTexCoord o)
	{
		smCoord2f_cli n;
		n.x = o.u;
		n.y = o.v;
		return n;
	}

	static smEngineHeadPoseData_cli ToCLIType_smEngineHeadPoseData(smEngineHeadPoseData o)
	{
		smEngineHeadPoseData_cli n;
		n.confidence = o.confidence;
		n.head_pos = ToCLIType_smCoord3f(o.head_pos);
		n.head_rot = ToCLIType_smCoord3f(o.head_rot);
		n.left_eye_pos = ToCLIType_smCoord3f(o.left_eye_pos);
		n.right_eye_pos = ToCLIType_smCoord3f(o.right_eye_pos);
		return n;
	}

	static smFaceLandmark_cli ToCLIType_smFaceLandmark(smFaceLandmark o)
	{
		smFaceLandmark_cli n;
		n.fc = ToCLIType_smCoord3f(o.fc);
		n.ftc = ToCLIType_smCoord2f(o.ftc);
		n.id = o.id;
		n.pc = ToCLIType_smCoord2f(o.pc);
		n.wc = ToCLIType_smCoord3f(o.wc);
		return n;
	}



    value struct LandmarksDelegateWrapper
	{
		LandmarksDelegate ^d;
	};

	value struct HeadPoseDelegateWrapper
	{
		HeadPoseDelegate ^d;
	};


//	typedef smCoord3f_cli smFaceCoord_cli;
//	typedef smCoord2f_cli smPixel_cli;
extern "C" __declspec(dllexport) void __stdcall faceDataCallback(void *user_data, smEngineFaceData face_data, smCameraVideoFrame video_frame)
{
	//LandmarksDelegate ^outputlandmarks = *((LandmarksDelegate ^*)user_data);
	LandmarksDelegate ^outputlandmarks = ((LandmarksDelegateWrapper*)user_data)->d;
	array<smFaceLandmark_cli> ^landmarks = gcnew array<smFaceLandmark_cli>(face_data.num_landmarks);
	for (int i = 0; i < landmarks->Length; ++i)
	{
		landmarks[i] = ToCLIType_smFaceLandmark(face_data.landmarks[i]);
	}
	outputlandmarks(landmarks);
	/*
//	qApp->processEvents();
//	qDebug() << "time:" << video_frame.time.time_s;
	FrameInfo fi = *((FrameInfo*)user_data);
	QString outfn = getImgNumPath(fi.outputpath, video_frame.frame_num, fi.total_images);
	std::ofstream fdt;
	fdt.open(QString(outfn+".fdt").toAscii().constData(), std::ios_base::out);
	fdt << face_data;
	fdt.close();
//	qDebug() << outfn+".png";
//	face_data.landmarks
	*/
	//std::cout << face_data << std::endl;
	/*
	QString outfn = QString::number(fi.image_num+1, 10);
	while (outfn.size() < 5)
		outfn.prepend('0');
	outfn.append(".png");
	outfn.prepend(fi.outputpath);
	qDebug() << outfn;
	*/
//	Image sfi(video_frame.image_handle, SM_API_IMAGE_MEMORYCOPYMODE_AUTO);
//	Image sfi(face_data.texture->image_info, SM_API_IMAGE_MEMORYCOPYMODE_AUTO);
//	if (sfi.saveToPNG(outfn.toAscii().constData()) != SM_API_OK)
//	if (saveToPNGFile(outfn, face_data.texture->image_info) != SM_API_OK)
	/*
	if (saveToPNGFile(outfn+".png", video_frame.image_handle) != SM_API_OK)
	{
		qDebug() << "Error saving face-texture to " << outfn+".png";
	}
	*/
}

extern "C" __declspec(dllexport) void __stdcall headPoseCallback(void *user_data, smEngineHeadPoseData head_pose, smCameraVideoFrame video_frame)
{
	//HeadPoseDelegate ^outputheadpose = *((HeadPoseDelegate ^*)user_data);
	HeadPoseDelegate ^outputheadpose = ((HeadPoseDelegateWrapper*)user_data)->d;
	smEngineHeadPoseData_cli head_pose_cli = ToCLIType_smEngineHeadPoseData(head_pose);
	outputheadpose(head_pose_cli);
	/*
//	qApp->processEvents();
//	qDebug() << "time:" << video_frame.time.time_s;
	FrameInfo fi = *((FrameInfo*)user_data);
	QString outfn = getImgNumPath(fi.outputpath, video_frame.frame_num, fi.total_images);
	std::ofstream hpt;
	hpt.open(QString(outfn+".hpt").toAscii().constData(), std::ios_base::out);
	hpt << head_pose;
	hpt.close();
//	qDebug() << fi.frame_num;
	*/
	//std::cout << head_pose << std::endl;
}

extern "C" __declspec(dllexport) void __cdecl testme()
{
	printf("hello world\n");
}

void run(void (__stdcall *hpcf)(void*, smEngineHeadPoseData, smCameraVideoFrame), void (__stdcall *fdcf)(void*, smEngineFaceData, smCameraVideoFrame), LandmarksDelegate ^outputlandmarks, HeadPoseDelegate ^outputheadpose)
{
//	QSplashScreen splash;
//	splash.show();

	// Initialize the faceAPI Qt library
//	sm::faceapi::qt::initialize();

	// Create logging window first to catch API startup messages
//	LoggingWidget logging_window;

	// Initialize the API
//	updateSplash(splash,"Initializing faceAPI");
	APIScope faceapi_scope;

	// Ensure engine and camera are destroyed before the API instance.
	{
		CameraBase *camera;
		HeadTrackerV2 *engine;
//		QSharedPointer<CameraBase> camera;
//		QSharedPointer<EngineBase> engine;

		// Check license
		if (APIScope::isNonCommercialLicense())
		{
		/*
			QMessageBox::information(0,"Information","Non-Commercial License.\n\n"
													 "This demo cannot use the PS3 Eyetoy camera low-level driver,"
													 "as it requires the image-push camera interface.\n"
													 "The demo will run instead with the first detected DirectShow camera");
			// Create head-tracking engine v2 using first detected webcam
		*/
			CameraInfo::registerType(SM_API_CAMERA_TYPE_WDM);
			engine = new HeadTrackerV2();
//			engine = QSharedPointer<HeadTrackerV2>(new HeadTrackerV2());
		}
		else
		{
//			updateSplash(splash,"Creating Camera");


			CameraInfo::registerType(SM_API_CAMERA_TYPE_WDM);
			CameraInfo::registerType(SM_API_CAMERA_TYPE_PTGREY);
//			camera = QSharedPointer<Camera>(new Camera);
			camera = new Camera;

//			updateSplash(splash,"Creating Engine");

			// Create a head-tracking engine that uses the camera
			engine = new HeadTrackerV2(*camera);
//			engine = QSharedPointer<HeadTrackerV2>(new HeadTrackerV2(*camera));
		}

//		engine->registerFaceDataCallback(0, faceDataCallback);
//		engine->registerHeadPoseCallback(0, headPoseCallback);

		//engine->registerFaceDataCallback(0, fdcf);
		//engine->registerHeadPoseCallback(0, hpcf);

		LandmarksDelegateWrapper ldw;
		ldw.d = outputlandmarks;
		HeadPoseDelegateWrapper hdw;
		hdw.d = outputheadpose;

		engine->registerFaceDataCallback(&ldw, fdcf);
		engine->registerHeadPoseCallback(&hdw, hpcf);

		VideoDisplay video_display(*engine);
		video_display.setFlags(SM_API_VIDEO_DISPLAY_REFERENCE_FRAME | SM_API_VIDEO_DISPLAY_HEAD_MESH | /*SM_API_VIDEO_DISPLAY_PERFORMANCE |*/ SM_API_VIDEO_DISPLAY_LANDMARKS | SM_API_VIDEO_DISPLAY_COLOR);
		video_display.show();

		engine->start();

//		updateSplash(splash,"Building Widgets");

		// QMainWindow derived class. See mainwindow.h
//		MainWindow main_window(camera,engine,&logging_window);

		// Move to top-centre of screen so user looks towards the camera.
//		QDesktopWidget desktop;
//		main_window.move(desktop.screenGeometry().width()/2-main_window.width()/2,0);

//		main_window.show();
//		splash.finish(&main_window);

//		qApp->exec();

//		if (_kbhit())
//			{
//				quit = true;
//				_getch();
//			}

		while (1)
		{
			faceapi_scope.processEvents();
			Sleep(500);
//			Sleep(10000);
		}

	}
}

#if 0
int main(int argc, char **argv)
{

//		QApplication app(argc, argv);
//		app.setQuitOnLastWindowClosed(true);
//		QObject::connect(&app, SIGNAL(lastWindowClosed()), &app, SLOT(quit()));
//	QTimer::singleShot(3000, &app, SLOT(closeAllWindows()));
//	qInstallMsgHandler(msgoutput);
	/*
		try
		{
			run(headPoseCallback, faceDataCallback);
//				run();
		}
		catch (sm::faceapi::Error &e)
		{
			std::cout << "faceapi error" << e.what() << std::endl;
//				qDebug() << "faceapi error" << e.what();
		}
	*/
//	app.quit();

	run(headPoseCallback, faceDataCallback);

	return 0;
//	return app.exec();
} // imagePushCameraSampleCPP()
#endif








	public ref class FaceInvoke
	{
	public:
		LandmarksDelegate ^outputlandmarks;
		HeadPoseDelegate ^outputheadpose;

		

		

		

		

		static void PrintHeadpose(smEngineHeadPoseData_cli headpose)
		{
			Console::WriteLine(headpose.mkstring());
		}

		static void PrintLandmarks(array<smFaceLandmark_cli> ^landmarks)
		{
			for (int i = 0; i < landmarks->Length; ++i)
			{
				Console::WriteLine(landmarks[i].mkstring());
			}
		}

		HeadPoseDelegate^ GetHeadPoseDelegate()
		{
			return gcnew HeadPoseDelegate(PrintHeadpose);
		}

		LandmarksDelegate^ GetLandmarksDelegate()
		{
			return gcnew LandmarksDelegate(PrintLandmarks);
		}

		void SetHeadPoseDelegate(HeadPoseDelegate ^d)
		{
			outputheadpose = d;
		}

		void SetLandmarksDelegate(LandmarksDelegate ^d)
		{
			outputlandmarks = d;
		}

		void Run()
		{
			//outputlandmarks = gcnew LandmarksDelegate(PrintLandmarks);
			//outputheadpose = gcnew HeadPoseDelegate(PrintHeadpose);
			run(headPoseCallback, faceDataCallback, outputlandmarks, outputheadpose);
			//Console::WriteLine("mekngak");
		}
	};
}
