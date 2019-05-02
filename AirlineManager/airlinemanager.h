#ifndef AIRLINEMANAGER_H
#define AIRLINEMANAGER_H
#include "airlinedatabase.h"


class AirlineManager
{
private:
    AirlineDatabase* airline_database;
public:
    AirlineManager();
    bool AddPlane(std::string& planeName,int id,std::string planeType);
    bool AddFlight(Plane& plane, tm& flightTime, std::string& flightName);
    bool ChangeFlightTime(Flight& flight, tm& newFlightTime);
    std::string DisplayFlight(int flightID);
    std::string DisplayFlightsToDestination(std::string& destination);
    std::string DisplayFlightPassangers(Flight &flight); //Displays all the passangers on a particular flight
    std::string DisplayPassangersFlight(Ticket& passangersTicket); //Displays a passangers flight
    std::string DisplayPlane(int planeID);
    std::string DisplayAllPlanes();
    std::string DisplayAllFlights();
    bool CreateTicket(std::string& name,Flight& flight,float& baseCost, std::string seatArea);
};

#endif // AIRLINEMANAGER_H
