#ifndef TICKET_H
#define TICKET_H
#include "flight.h"
#include <string>

class Ticket
{
private:
    enum SeatType: char{
        Coach,Second_Class,First_Class
    };

    enum BaggageCost{
        None = 0, One = 25, Two = 35, Max = 75
    };
    enum PlaneSizeCost{
        Small = 230,Medium = 370, Large = 610
    };
    const Flight flight;
    float ticket_cost;
    constexpr static float seat_type_modifier[3] = {1.0, 1.25,2.0};
    std::string ticket_holder;
    SeatType seat_area;
public:     
    void setTicketCost(Ticket& ticket,float& newCost);
    void setSeatType(Ticket& ticket, SeatType& new_seat);
    float getTicketCost(Ticket& ticket) const;
    Flight getFlight(Ticket& ticket) const;
    SeatType getSeatType(Ticket& ticket) const;
    std::string getTicketHolder(Ticket& ticket) const;
    int CalculateTicketCost(BaggageCost& numberOfBags,Ticket::SeatType& seatType, Flight& customersFlight);
    Ticket(std::string& name,Flight& flight,float& baseCost, SeatType& seatArea);
};

#endif // TICKET_H
