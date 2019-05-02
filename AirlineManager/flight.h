#ifndef FLIGHT_H
#define FLIGHT_H
#include"plane.h"
#include <string>
#include <vector>

class Flight
{
private:
    const Plane plane;
    static int current_id;
    const int flight_id;
    tm flight_time;
    const std::string flight_destination;
    std::string* passanger_list;
public:   
    Flight(Plane& plane, tm& flightTime, std::string& flightName);
    void addPassanger(std::string& newPassanger);
    void setFlightTime(tm& newFlightTime);
    std::string getPlaneName() const;
    std::string getFlightDestination () const;
    int getFlightID() const;
    tm getFlightTime() const;
    std::vector<std::string> getPassangerList() const;
    int generateInitialID;
    int GenerateFlightID();
    void ShowPassangerList() const;
};

#endif // FLIGHT_H
