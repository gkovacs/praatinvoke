// main.cpp : main project file.

#include "stdafx.h"
#include <iostream>
#include <vector>
#include <string>
#include <utility>

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace praatinvoke;
using namespace std;

void printpraatSTL(std::vector<std::pair<std::string, float>> pvo)
{
	for (unsigned int i = 0; i < pvo.size(); ++i)
	{
		std::cout << "printpraatSTL: " << "(" << pvo[i].first << "," << pvo[i].second << ")" << std::endl;
	}
//	for (std::vector<std::pair<std::string, float>>::const_iterator i = pvo.begin; i != pvo.end; ++i)
//	{
//		std::cout << "printpraatSTL: " << "(" << i->first << "," << i->second << ")" << std::endl;
//	}
}

void MarshalString (System::String ^input, std::string &output)
{
	const char* chars = (const char*)(Marshal::StringToHGlobalAnsi(input)).ToPointer();
	output = chars;
	Marshal::FreeHGlobal(IntPtr((void*)chars));
}

void printpraatCLI(array<praatinvoke::Pair<System::String^, float>^> ^outputarray)
{
	std::vector<std::pair<std::string, float>> pvo;
	for (int i = 0; i < outputarray->Length; ++i)
	{
		std::pair<std::string, float> outpair;
		outpair.second = outputarray[i]->second;
		MarshalString(outputarray[i]->first, outpair.first);
		pvo.push_back(outpair);
	}
	printpraatSTL(pvo);
}

int main(array<System::String ^> ^args)
{
	if (args->Length < 2)
	{
		Console::WriteLine(L"not enough arguments");
		return 0;
	}
	PraatInvoke ^pri = gcnew PraatInvoke(args[0], args[1]);
	PraatOutput ^pao = gcnew PraatOutput();
	WaveWriter ^wwr = gcnew WaveWriter();
	PortAudioRecord ^rec = gcnew PortAudioRecord();
	rec->SetSamplesDelegate(wwr->GetSamplesDelegate());
	wwr->SetPraatDelegate(pri->GetPraatDelegate());
//	pri->SetOutputPraatDelegate(pao->GetPraatOutputDelegate());
	pri->SetOutputPraatDelegate(gcnew praatinvoke::OutputPraatDelegate(printpraatCLI));
	rec->Run(-1);
    return 0;
}
