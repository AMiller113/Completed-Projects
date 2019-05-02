#ifndef PLANE_H
#define PLANE_H
#include<string>

class Plane
{
private:
    const std::string plane_name;
    enum Plane_Type{
        Small,Medium,Large
    };
    Plane_Type plane_size;
    const int number_of_seats;
    const int plane_id;
public:   
    Plane(std::string& planeName,int& id,Plane_Type& type);
    std::string getPlaneName() const;
    int getPlaneID() const;
    Plane_Type getPlaneType () const;
};
#endif // PLANE_H
