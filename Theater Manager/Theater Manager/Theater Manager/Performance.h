#include<string>
#include <ctime>
#include<vector>

using namespace std;

#pragma once
class Performance
{
public:
	Performance(string, string, string, float, tm);
	~Performance();

	//Getters
	string getPerformance_name() const;
	string getPerformance_type() const;
	string getAuthor_or_composer() const;
	vector<string> getPatrons() const;
	float getPerformance_cost () const;
	
	//Setters
	void setPerformance_name();
	void setPerformance_type();
	void setAuthor_or_composer();
	void setPerformance_cost();
	void addPatron(string patron_name);

	
private:
	string performance_name,performance_type,author_or_composer;
	float performance_cost;
	tm perfomance_date_and_time;
	vector<string> patrons;
};

