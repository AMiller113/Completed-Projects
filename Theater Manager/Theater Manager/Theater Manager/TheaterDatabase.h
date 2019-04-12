#include "Performance.h"
#include <iostream>
#include <map>

using namespace std;

#pragma once
class TheaterDatabase
{
public:
	TheaterDatabase Get_Database();
	bool Add_To_Database();
	bool Delete_From_Database();
	~TheaterDatabase();
private:
	TheaterDatabase();
	multimap<int, Performance> Performances;
	const string file_path = "C:\\ProgramData\\Theater_Data";
};

