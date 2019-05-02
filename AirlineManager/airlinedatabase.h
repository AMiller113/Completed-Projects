#ifndef AIRLINEDATABASE_H
#define AIRLINEDATABASE_H
#include"flight.h"
#include"ticket.h"
#include"plane.h"
#include<map>
#include<vector>
#include<QString>
#include<QtCore>

class AirlineDatabase
{
private:
    static const std::string filePaths[3];
    std::map<int,Plane> all_planes;
    std::map<int,Flight> all_flights;
    std::map<std::string,Ticket> all_tickets;
    AirlineDatabase();
public:
    AirlineDatabase* getDatabase();
    bool AddPlane(Plane newPlane);
    bool AddFlight(Flight newFlight);
    bool AddTicket(Ticket newTicket);
};

#endif // AIRLINEDATABASE_H
